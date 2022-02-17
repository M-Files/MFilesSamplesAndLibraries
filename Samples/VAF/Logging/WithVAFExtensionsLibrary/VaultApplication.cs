using MFiles.VAF;
using MFiles.VAF.AppTasks;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Core;
using MFiles.VAF.Extensions.Dashboards;
using MFiles.VaultApplications.Logging;
using MFilesAPI;
using System;
using System.Diagnostics;

namespace WithVAFExtensionsLibrary
{
    /// <summary>
    /// This shows how to log in the VAF (https://developer.m-files.com/Frameworks/Logging/Vault-Application-Framework/)
    /// using the VAF Extensions library.
    /// </summary>
    public class VaultApplication
        : MFiles.VAF.Extensions.ConfigurableVaultApplicationBase<Configuration>
    {

        /// <summary>
        /// Registers a task queue with ID "LoggingWithVAFExtensionsLibrary".
        /// </summary>
        // Note: this queue ID must be unique to this application!
        [TaskQueue]
        public const string TaskQueueID = "LoggingWithVAFExtensionsLibrary";
        public const string TaskType = "WriteLogEntries";

        /// <summary>
        /// Processes items of type <see cref="TaskType" /> on queue <see cref="TaskQueueID" />
        /// </summary>
        [TaskProcessor(TaskQueueID, TaskType, TransactionMode = TransactionMode.Unsafe)]
        [ShowOnDashboard("Write Log Entries", ShowRunCommand = true)]
        public void WriteLogEntriesTaskProcessor(ITaskProcessingJob<TaskDirective> job)
        {
            // You can log at different levels: https://developer.m-files.com/Frameworks/Logging/#log-levels
            this.Logger?.Trace("Starting job");
        }

    }
}