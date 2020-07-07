using System;
using System.Collections.Generic;
using System.Threading;
using MFiles.VAF.Common;
using MFiles.VAF.Common.ApplicationTaskQueue;
using MFiles.VAF.MultiserverMode;
using MFilesAPI;

namespace ConcurrentTaskQueue
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
		/// The concurrent task processor.
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
		public const string ConcurrentTaskQueueId = "MFiles.Samples.ConcurrentTaskQueue.Application.TaskQueueId";

		/// <summary>
		/// The task type.
		/// This can be used to have different handlers for different types of task in your queue.
		/// </summary>
		public const string TaskTypeConcurrentTask = "TaskType.ConcurrentTask";

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
						DisableAutomaticProgressUpdates = false,
						PollTasksOnJobCompletion = true,
						MaxConcurrentBatches = this.Configuration.MaxConcurrentBatches,
						MaxConcurrentJobs = this.Configuration.MaxConcurrentJobs,
						PermanentVault = this.PermanentVault,
						EnableAutomaticTaskUpdates = true,
						QueueDef = new TaskQueueDef
						{
							TaskType = TaskQueueManager.TaskType.ApplicationTasks,
							Id = VaultApplication.ConcurrentTaskQueueId,
							ProcessingBehavior = MFTaskQueueProcessingBehavior.MFProcessingBehaviorConcurrent,
							MaximumPollingIntervalInSeconds = this.Configuration.MaxPollingIntervalInSeconds,
							LastBroadcastId = ""
						},
						TaskHandlers = new Dictionary<string, TaskProcessorJobHandler>
						{
							/* One task type is handled here, by the ProcessConcurrentTask handler. */
							{ VaultApplication.TaskTypeConcurrentTask, ProcessConcurrentTask }
						},
						TaskQueueManager = this.TaskQueueManager
					},
					this.TokenSource.Token
				);


			}

			// Register the task queue.
			this.TaskProcessor.RegisterTaskQueues();

			// Ensure that the configuration broadcast queue is initialized.
			this.InitializeConfigurationBroadcastProcessor();
		}

		private void ProcessConcurrentTask(TaskProcessorJob job)
		{
			// Debug Logging.
			if (this.Configuration.LoggingEnabled)
				SysUtils.ReportInfoToEventLog($"Concurrent task processing with task id => {job.Data?.Value.Id}.");

			// Ensure cancellation has not been requested.
			job.ThrowIfCancellationRequested();

			// Update the progress of the task in the task queue.
			this.TaskProcessor.UpdateTaskAsAssignedToProcessor(job);

			// Sanity.
			if (null == job.Data?.Value)
			{
				return;
			}

			// Deserialize the directive.
			var dir = TaskQueueDirective.Parse<ObjVerExTaskQueueDirective>(job.Data?.Value);

			// Sanity.
			if (string.IsNullOrWhiteSpace(dir?.ObjVerEx))
				return;

			// Update the object.
			try
			{
				// Mark that we're updating the object.
				this.TaskProcessor.UpdateTaskInfo
				(
					job.Data?.Value,
					MFTaskState.MFTaskStateInProgress,
					$"Updating object {dir.ObjVerEx}",
					false
				);

				// Load the object, check it out, update, check it in.
				var objVerEx = ObjVerEx.Parse(job.Vault, dir.ObjVerEx);
				objVerEx.CheckOut();
				objVerEx.SetProperty
				(
					MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle,
					MFDataType.MFDatatypeText,
					DateTime.Now.ToLongTimeString()
				);
				objVerEx.SaveProperties();
				objVerEx.CheckIn();

				// Updated.
				this.TaskProcessor.UpdateTaskInfo
				(
					job.Data?.Value,
					MFTaskState.MFTaskStateCompleted,
					$"Object {dir.ObjVerEx} updated",
					false
				);
			}
			catch (Exception e)
			{
				// Exception.
				this.TaskProcessor.UpdateTaskInfo
				(
					job.Data?.Value,
					MFTaskState.MFTaskStateFailed,
					e.Message,
					false
				);
			}
		}

		/// <summary>
		/// Registers a Vault Extension Method with name "AddItemToConcurrentQueue".
		/// Users must have at least MFVaultAccess.MFVaultAccessNone access to execute the method.
		/// </summary>
		/// <param name="env">The vault/object environment.</param>
		/// <returns>The any output from the vault extension method execution.</returns>
		/// <remarks>The input to the vault extension method is available in <see cref="EventHandlerEnvironment.Input"/>.</remarks>
		[VaultExtensionMethod("AddItemToConcurrentQueue",
			RequiredVaultAccess = MFVaultAccess.MFVaultAccessNone)]
		private string AddItemToConcurrentQueue(EventHandlerEnvironment env)
		{
			// Sanity.
			if (string.IsNullOrWhiteSpace(env?.Input))
				return "No item provided.";
			if (false == ObjVerEx.TryParse(env.Vault, env.Input, out ObjVerEx item))
				return $"Input '{env.Input}' not in expected format.";

			SysUtils.ReportInfoToEventLog($"Adding {item.ToString()} to the queue.");

			// Create a task in the task queue.
			var itemId = this.TaskProcessor.CreateApplicationTaskSafe
			(
				true,
				VaultApplication.ConcurrentTaskQueueId,
				VaultApplication.TaskTypeConcurrentTask,
				new ObjVerExTaskQueueDirective { ObjVerEx = item.ToString() }.ToBytes()
			);

			// Return the ID.
			return itemId;
		}

		#region Enable configuration changes to be broadcast to other servers

		/// <summary>
		/// Vault application task broadcast processor cancellation token source.
		/// </summary>
		private CancellationTokenSource ConfigurationBroadcastTokenSource { get; set; }

		/// <summary>
		/// Broadcast task processor.
		/// </summary>
		private AppTaskBatchProcessor ConfigurationBroadcastProcessor { get; set; }

		/// <summary>
		/// Broadcast task queue id.
		/// </summary>
		public const string ConfigurationBroadcastTaskQueueId =
			"MFiles.Samples.ConcurrentTaskQueue.Application.ConfigurationTaskQueueId";

		/// <summary>
		/// Provides the ID of the task queue to use for rebroadcasting changes to all servers.
		/// </summary>
		/// <returns><see cref="BroadcastTaskQueueId" /></returns>
		public override string GetRebroadcastQueueId()
		{
			// We wish to re-use the above broadcast task processor / queue.
			return ConfigurationBroadcastTaskQueueId;
		}

		/// <summary>
		/// Initializes the configuration broadcast task processor.
		/// - This processor also handles the rebroadcast messages from the SaveCommand from the MFAdmin.
		/// </summary>
		private void InitializeConfigurationBroadcastProcessor()
		{
			// Verify the broadcast task processor token source has been created.
			if (this.ConfigurationBroadcastTokenSource == null)
				this.ConfigurationBroadcastTokenSource = new CancellationTokenSource();

			// Initialize the batch task processor.
			if (this.ConfigurationBroadcastProcessor == null)
				this.ConfigurationBroadcastProcessor = new AppTaskBatchProcessor(
					new AppTaskBatchProcessorSettings
					{
						QueueDef = new TaskQueueDef
						{
							TaskType = TaskQueueManager.TaskType.BroadcastMessages,
							Id = ConfigurationBroadcastTaskQueueId,
							ProcessingBehavior = MFTaskQueueProcessingBehavior.MFProcessingBehaviorConcurrent,
							MaximumPollingIntervalInSeconds = this.Configuration.MaxPollingIntervalInSeconds,
							LastBroadcastId = ""
						},
						PermanentVault = this.PermanentVault,
						MaxConcurrentBatches = this.Configuration.MaxConcurrentBatches,
						MaxConcurrentJobs = this.Configuration.MaxConcurrentJobs,
						// This does not require any task handlers, but if other broadcast tasks are used then they could be added here.
						TaskHandlers = new Dictionary<string, TaskProcessorJobHandler>()
						{
							// Note that we have to provide at least one task handler or the underlying call excepts.
							{ Guid.NewGuid().ToString(), (j) => { } }
						},
						TaskQueueManager = this.TaskQueueManager,
						EnableAutomaticTaskUpdates = true,
						DisableAutomaticProgressUpdates = false,
						PollTasksOnJobCompletion = true,
						VaultExtensionMethodProxyId = this.GetVaultExtensionMethodEventHandlerProxyName()
					},
					this.ConfigurationBroadcastTokenSource.Token);

			// Register the task queues.
			this.ConfigurationBroadcastProcessor.RegisterTaskQueues();
		}


		#endregion

	}

	public class ObjVerExTaskQueueDirective
		: TaskQueueDirective
	{
		/// <summary>
		/// Parse-able ObjVerEx string.
		/// </summary>
		public string ObjVerEx { get; set; }
	}
}