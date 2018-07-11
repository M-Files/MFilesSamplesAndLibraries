using System;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.ConfigurationEditors
{
	/// <summary>
	/// Showcases options for "color"-style editors.
	/// </summary>
	[DataContract]
	public class ColorConfigurationEditors
	{
		/// <summary>
		/// A colour value that will be saved as a string.
		/// </summary>
		/// <remarks>Will be saved in the #RRGGBB format.</remarks>
		[DataMember]
		[JsonConfEditor(TypeEditor = "color")]
		public string ColorValueAsString { get; set; }

		/// <summary>
		/// A colour value that will be saved as a string.
		/// </summary>
		[DataMember]
		[JsonConfEditor(TypeEditor = "color", DefaultValue = "#FF0000")]
		public string ColorValueAsStringWithDefault { get; set; } = "#FF0000";
	}
}
