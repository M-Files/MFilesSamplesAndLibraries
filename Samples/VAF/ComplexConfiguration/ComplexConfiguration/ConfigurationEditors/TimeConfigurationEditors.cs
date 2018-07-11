using System;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.ConfigurationEditors
{
	/// <summary>
	/// Showcases options for "time"-style editors.
	/// </summary>
	/// <remarks>The values will be saved as a <see cref="TimeSpan"/> so cannot include date information.</remarks>
	[DataContract]
	public class TimeConfigurationEditors
	{
		/// <summary>
		/// A simple nullable time value.
		/// </summary>
		[DataMember]
		[JsonConfEditor(TypeEditor = "time")]
		public TimeSpan? SimpleTimeValue { get; set; }

		/// <summary>
		/// A time value that will be saved as a string.
		/// </summary>
		/// <remarks>Will be saved in the HH:MM:SS format.</remarks>
		[DataMember]
		[JsonConfEditor(TypeEditor = "time")]
		public string TimeValueAsString { get; set; }

		/// <summary>
		/// A simple nullable time value.
		/// </summary>
		[DataMember]
		[JsonConfEditor(TypeEditor = "time", DefaultValue = "02:00:00")]
		public TimeSpan? TimeValueWithDefault { get; set; } = TimeSpan.Parse("02:00:00");
	}
}
