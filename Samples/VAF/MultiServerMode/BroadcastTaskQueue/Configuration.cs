using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;
using MFiles.VAF.Configuration.JsonAdaptor;

namespace BroadcastTaskQueue
{
	[DataContract]
	public class Configuration
	{
		/// <summary>
		/// If true, running data is logged to the Windows Event Log.
		/// </summary>
		[DataMember( Order = 0 )]
		[JsonConfEditor(
			Label = "Enable Logging",
			DefaultValue = false
		)]
		public bool LoggingEnabled { get; set; } = false;

		/// <summary>
		/// The max number of seconds that can pass between polling intervals.
		/// </summary>
		[DataMember( Order = 1 )]
		[JsonConfIntegerEditor(
			DefaultValue = 15,
			Label = "Maximum Polling Interval",
			HelpText = "The max number of seconds that can pass between polling intervals.",
			Min = 5
		)]
		public int MaxPollingIntervalInSeconds { get; set; } = 10;

		/// <summary>
		/// The max number of batches that can be running in the task processor at once.
		/// </summary>
		[DataMember( Order = 2 )]
		[JsonConfIntegerEditor(
			HelpText = "The max number of batches that can be running in the task processor at once.",
			Label = "Max Concurrent Batches",
			Min = 1,
			Max = 100,
			DefaultValue = 5
		)]
		public int MaxConcurrentBatches { get; set; } = 5;

		/// <summary>
		/// The max number of jobs that can be running within a batch at once.
		/// </summary>
		[DataMember( Order = 3 )]
		[JsonConfIntegerEditor(
			HelpText = "The max number of jobs that can be running within a batch at once.",
			Label = "Maximum Concurrent Jobs",
			Min = 5,
			Max = 100,
			DefaultValue = 5
		)]
		public int MaxConcurrentJobs { get; set; } = 5;
	}
}