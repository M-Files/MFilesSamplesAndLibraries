using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace SequentialTaskQueue
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
	}
}