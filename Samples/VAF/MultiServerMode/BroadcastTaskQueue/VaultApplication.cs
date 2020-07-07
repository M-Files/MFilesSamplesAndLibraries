using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using MFiles.VAF.Common;
using MFiles.VAF.Common.ApplicationTaskQueue;
using MFiles.VAF.MultiserverMode;
using MFilesAPI;

namespace BroadcastTaskQueue
{
	/// <summary>
	/// The entry point for this Vault Application Framework application.
	/// </summary>
	/// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
	public class VaultApplication
		: MFiles.VAF.Core.ConfigurableVaultApplicationBase<Configuration>, IUsesTaskQueue
	{
		/// <summary>
		/// The token source for task processing cancellation.
		/// </summary>
		private CancellationTokenSource TokenSource { get; set; }
		
		/// <summary>
		/// The broadcast task processor.
		/// </summary>
		public AppTaskBatchProcessor TaskProcessor { get; set; }

		/// <summary>
		/// The server that this application is running on.
		/// </summary>
		internal static VaultServerAttachment CurrentServer { get; private set; }

		/// <summary>
		/// The task queue ID. This should be unique for your application
		/// so that you don't interfere with other task queues.
		/// </summary>
		public const string BroadcastTaskQueueId = "MFiles.Samples.BroadcastTaskQueue.Application.BroadcastQueueId";
		
		/// <summary>
		/// The task type.
		/// This can be used to have different handlers for different types of task in your queue.
		/// </summary>
		public const string TaskTypeBroadcastTask = "TaskType.BroadcastTask";

		/// <summary>
		/// Override the start operations so we can register our task processor.
		/// </summary>
		/// <param name="vaultPersistent">Non-transactional Vault object.</param>
		public override void StartOperations(Vault vaultPersistent)
		{
			// Set a reference to the current server reference.
			VaultApplication.CurrentServer = vaultPersistent
				.GetVaultServerAttachments()
				.GetCurrent();

			// Allow the base to process the start operations.
			base.StartOperations(vaultPersistent);

			// Enable polling/processing of the queue.
			this.TaskQueueManager.EnableTaskPolling(true);
		}

		/// <summary>
		/// Registers the task queue used by this application or module.
		/// </summary>
		public void RegisterTaskQueues()
		{
			// Create the cancellation token source.
			if (this.TokenSource == null)
				this.TokenSource = new CancellationTokenSource();

			// Create the task processor.
			if (this.TaskProcessor == null)
			{
				// Initialize the task processor.
				this.TaskProcessor = new AppTaskBatchProcessor
				(
					new AppTaskBatchProcessorSettings
					{
						QueueDef = new TaskQueueDef
						{
							TaskType = TaskQueueManager.TaskType.BroadcastMessages,
							Id = BroadcastTaskQueueId,
							ProcessingBehavior = MFTaskQueueProcessingBehavior.MFProcessingBehaviorConcurrent,
							MaximumPollingIntervalInSeconds = this.Configuration.MaxPollingIntervalInSeconds,
							LastBroadcastId = ""
						},
						PermanentVault = this.PermanentVault,
						MaxConcurrentBatches = this.Configuration.MaxConcurrentBatches,
						MaxConcurrentJobs = this.Configuration.MaxConcurrentJobs,
						TaskHandlers = new Dictionary<string, TaskProcessorJobHandler>
						{
							{ TaskTypeBroadcastTask, ProcessBroadcastTask }
						},
						TaskQueueManager = this.TaskQueueManager,
						EnableAutomaticTaskUpdates = true,
						DisableAutomaticProgressUpdates = false,
						PollTasksOnJobCompletion = true,
						VaultExtensionMethodProxyId = this.GetVaultExtensionMethodEventHandlerProxyName()
					},
					this.TokenSource.Token
				);
			}

			// Register the task queue.
			this.TaskProcessor.RegisterTaskQueues();
		}

		private void ProcessBroadcastTask(TaskProcessorJob job)
		{
			// Ensure cancellation has not been requested.
			job.ThrowIfCancellationRequested();

			// Update the progress of the task in the task queue.
			this.TaskProcessor.UpdateTaskAsAssignedToProcessor( job );

			// Sanity.
			if (null == job.Data?.Value)
			{
				return;
			}

			// Deserialize the directive.
			EmailByUserPatchDirective dir = TaskQueueDirective.Parse<EmailByUserPatchDirective>( job.Data.Value );
			if( dir.GeneratedFromGuid != CurrentServer.ServerID )
			{
				// Debug Logging.
				if( this.Configuration.LoggingEnabled )
					SysUtils.ReportInfoToEventLog( $"Broadcast processing with task id => {job.Data.Value.Id} on server {CurrentServer.ServerID}." );

				// The task was created on another server, so we should process it on this server.
				AddOrUpdateEmailByUserId( dir.UserAccount, dir.Email );
			}

		}

		/// <summary>
		/// Example Very Large in-memory cache.
		/// </summary>
		private ConcurrentDictionary<int, string> EmailByUserID { get; set; }
			= new ConcurrentDictionary<int, string>();
		
		/// <summary>
		/// patches the EmailByUserID cache with the passed values.
		/// </summary>
		/// <param name="userId">User account id.</param>
		/// <param name="emailAddress">Corresponding email address.</param>
		private void AddOrUpdateEmailByUserId(
			int userId,
			string emailAddress
		) => this.EmailByUserID.AddOrUpdate(
			userId,
			usrId => emailAddress,
			( usrId, address ) => emailAddress );


		/// <summary>
		/// Registers a Vault Extension Method with name "AddItemToBroadcastQueue".
		/// Users must have at least MFVaultAccess.MFVaultAccessNone access to execute the method.
		/// </summary>
		/// <param name="env">The vault/object environment.</param>
		/// <returns>The any output from the vault extension method execution.</returns>
		/// <remarks>The input to the vault extension method is available in <see cref="EventHandlerEnvironment.Input"/>.</remarks>
		[VaultExtensionMethod("AddItemToBroadcastQueue",
			RequiredVaultAccess = MFVaultAccess.MFVaultAccessNone)]
		private string AddItemToBroadcastQueue(EventHandlerEnvironment env)
		{
			// Sanity.
			if(string.IsNullOrWhiteSpace(env?.Input))
				return "No item provided.";
			SysUtils.ReportInfoToEventLog($"Adding {env?.Input} to the queue.");

			// Create a task in the task queue.
			var itemId = this.TaskProcessor.CreateApplicationTaskSafe
			(
				true,
				VaultApplication.BroadcastTaskQueueId,
				VaultApplication.TaskTypeBroadcastTask,
				new EmailByUserPatchDirective
					{
						UserAccount = Int32.Parse(env?.Input.Split("****".ToCharArray())[0]),
						Email = env?.Input.Split("****".ToCharArray())[1]
					}.ToBytes()
			);

			// Return the ID.
			return itemId;
		}

		/// <summary>
		/// Provides the ID of the task queue to use for rebroadcasting changes to all servers.
		/// </summary>
		/// <returns><see cref="BroadcastTaskQueueId" /></returns>
		public override string GetRebroadcastQueueId()
		{
			// We wish to re-use the primary broadcast queue.
			return BroadcastTaskQueueId;
		}

	}

	/// <summary>
	/// Directive used to serialize immutable broadcast data.
	/// </summary>
	public class EmailByUserPatchDirective : BroadcastDirective
	{
		/// <summary>
		/// User account corresponding to the email.
		/// </summary>
		public int UserAccount { get; set; }

		/// <summary>
		/// Email corresponding to the User account.
		/// </summary>
		public string Email { get; set; }
	}
}