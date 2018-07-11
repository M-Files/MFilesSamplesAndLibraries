using System;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.ConfigurationEditors
{
	/// <summary>
	/// Showcases options for "date"-style editors.
	/// </summary>
	/// <remarks>The user cannot select a "time" portion of the associated <see cref="DateTime"/> value.</remarks>
	[DataContract]
	public class DateConfigurationEditors
	{
		/// <summary>
		/// A simple nullable datetime value.
		/// </summary>
		[DataMember]
		[JsonConfEditor(TypeEditor = "date")]
		public DateTime? SimpleDateValue { get; set; }

		/// <summary>
		/// A datetime value that will be saved as a string.
		/// </summary>
		/// <remarks>Will be saved in a locale-neutral ISO format (YYYY-MM-DD).  May be displayed in the administration area in a locale-specific format.</remarks>
		[DataMember]
		[JsonConfEditor(TypeEditor = "date")]
		public string DateValueAsString { get; set; }

		/// <summary>
		/// A simple nullable datetime value.
		/// </summary>
		[DataMember]
		[JsonConfEditor(TypeEditor = "date", DefaultValue = "2018-01-01")]
		public DateTime? DateValueWithDefault { get; set; } = DateTime.Parse("2018-01-01");
	}
}
