using System;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.ConfigurationEditors
{
	/// <summary>
	/// Showcases options for "timestamp" (date and time)-style editors.
	/// </summary>
	[DataContract]
	public class TimestampConfigurationEditors
	{
		/// <summary>
		/// A simple nullable timestamp value.
		/// </summary>
		[DataMember]
		[JsonConfEditor(TypeEditor = "timestamp")]
		public DateTime? SimpleTimestampValue { get; set; }

		/// <summary>
		/// A timestamp value that will be saved as a string.
		/// </summary>
		/// <remarks>Will be saved in the YYYY-MM-DD HH:mm:SS format.</remarks>
		[DataMember]
		[JsonConfEditor(TypeEditor = "timestamp")]
		public string TimestampValueAsString { get; set; }

		/// <summary>
		/// A simple nullable datetime value.
		/// </summary>
		[DataMember]
		[JsonConfEditor(TypeEditor = "timestamp", DefaultValue = "2018-01-01 02:00:00")]
		public DateTime? TimestampValueWithDefault { get; set; } = DateTime.Parse("2018-01-01 02:00:00");
	}
}
