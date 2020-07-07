using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using MFiles.VAF.Common;
using MFiles.VAF.Common.ApplicationTaskQueue;
using MFiles.VAF.Core;
//using MFiles.VAF.Extensions.MultiServerMode;
using MFiles.VAF.MultiserverMode;
using MFilesAPI;

namespace RecurringTask
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
		/// The task queue ID. This should be unique for your application
		/// so that you don't interfere with other task queues.
		/// </summary>
		public const string BackgroundOperationTaskQueueId = "MFiles.Samples.RecurringBackgroundOperation.Application.TaskQueueId";

		/// <summary>
		/// The concurrent task processor.
		/// </summary>
		public AppTaskBatchProcessor TaskProcessor { get; set; }

		/// <summary>
		/// The task type.
		/// This can be used to have different handlers for different types of task in your queue.
		/// </summary>
		public const string TaskTypeHourlyRecurringTask = "TaskType.HourlyRecurringTask";

		/// <summary>
		/// Override the start operations so we can register our task processor.
		/// </summary>
		/// <param name="vaultPersistent">Non-transactional Vault object.</param>
		public override void StartOperations(Vault vaultPersistent)
		{
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
							TaskType = TaskQueueManager.TaskType.Both,
							Id = VaultApplication.BackgroundOperationTaskQueueId,
							ProcessingBehavior = MFTaskQueueProcessingBehavior.MFProcessingBehaviorConcurrent,
							MaximumPollingIntervalInSeconds = this.Configuration.MaxPollingIntervalInSeconds,
							LastBroadcastId = ""
						},
						PermanentVault = this.PermanentVault,
						MaxConcurrentBatches = this.Configuration.MaxConcurrentBatches,
						MaxConcurrentJobs = this.Configuration.MaxConcurrentJobs,
						TaskHandlers = new Dictionary<string, TaskProcessorJobHandler>
						{
							{ VaultApplication.TaskTypeHourlyRecurringTask, ProcessHourlyTask }
						},
						TaskQueueManager = this.TaskQueueManager,
						EnableAutomaticTaskUpdates = true,
						DisableAutomaticProgressUpdates = false,
						PollTasksOnJobCompletion = true
					},
					this.TokenSource.Token
				);

				// Schedule the hourly task.
				ScheduleHourlyTask();

			}

			// Register the task queue.
			this.TaskProcessor.RegisterTaskQueues();

			// Ensure that the configuration broadcast queue is initialized.
			this.InitializeConfigurationBroadcastProcessor();
		}

		/// <summary>
		/// Cancels pre-existing hourly tasks in the waiting or in progress state
		/// and schedules a new task to process in one hour.
		/// </summary>
		private void ScheduleHourlyTask()
		{
			try
			{
				// Find any tasks of the appropriate type that are already scheduled.
				ApplicationTaskInfos tasksToCancel = TaskQueueAdministrator.FindTasks
				(
					this.PermanentVault,
					VaultApplication.BackgroundOperationTaskQueueId,
					t => t.Type == VaultApplication.TaskTypeHourlyRecurringTask,
					new[] { MFTaskState.MFTaskStateWaiting, MFTaskState.MFTaskStateInProgress }
				);

				// Cancel the pre-existing hourly tasks.
				foreach (ApplicationTaskInfo taskInfo in tasksToCancel)
					this.TaskProcessor.UpdateCancelledJobInTaskQueue
					(
						taskInfo.ToApplicationTask(),
						string.Empty,
						"Superseded."
					);
			}
			finally
			{
				// Schedule the task to execute in 1 hour.
				string nextHourlyTaskId = this.TaskProcessor.CreateApplicationTaskSafe
				(
					true,
					VaultApplication.BackgroundOperationTaskQueueId,
					VaultApplication.TaskTypeHourlyRecurringTask,
					null,
					DateTime.Now.AddHours(1).ToUniversalTime()
				);

				// Debug Logging.
				if (this.Configuration.LoggingEnabled)
					Debug.WriteLine($"Hourly task scheduled with task id => {nextHourlyTaskId}.");
			}
		}

		/// <summary>
		/// Processes a re-occuring hourly task.
		/// </summary>
		/// <param name="job">The hourly task job.</param>
		private void ProcessHourlyTask(TaskProcessorJob job)
		{
			// Debug Logging.
			if (this.Configuration.LoggingEnabled)
				Debug.WriteLine($"Hourly task processing with task id => {job.Data?.Value.Id}.");

			// Bind to the completed event ( called always ) of the job.
			// That way even if the job is canceled, fails, or finishes successfully
			// ...we always schedule the next run.
			job.ProcessingCompleted += (s, op) =>
				this.TaskProcessor.CreateApplicationTaskSafe(
				true,
				VaultApplication.BackgroundOperationTaskQueueId,
				VaultApplication.TaskTypeHourlyRecurringTask,
				null,
				DateTime.Now.AddHours(1).ToUniversalTime());

			// The hourly task has come due and is being processed.
			job.ThrowIfCancellationRequested();

			// Update has having been assigned.
			this.TaskProcessor.UpdateTaskAsAssignedToProcessor(job);

			// TODO: Do hourly work here...
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
			"MFiles.Samples.RecurringBackgroundOperation.Application.ConfigurationTaskQueueId";

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
}