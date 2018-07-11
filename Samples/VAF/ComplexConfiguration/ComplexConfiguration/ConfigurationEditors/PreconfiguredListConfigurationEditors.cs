using System.Runtime.Serialization;
using MFiles.VAF.Configuration;
using MFilesAPI;

namespace ComplexConfiguration.ConfigurationEditors
{
	/// <summary>
	/// Showcases options for editors that work with predefined lists.
	/// </summary>
	[DataContract]
	public class PreconfiguredListConfigurationEditors
	{

		#region Enumerations

		/// <summary>
		/// A value allowing the user to choose one value from a set defined in a .NET enumeration.
		/// </summary>
		[DataMember]
		[JsonConfEditor(HelpText = "Displays items that are defined in the MFBuiltInPropertyDef enumeration.")]
		public MFBuiltInPropertyDef EnumerationValue { get; set; }

		#endregion

		#region Prefined lists

		/// <summary>
		/// A value allowing the user to select one of a predefined list of options.
		/// </summary>
		/// <remarks>
		/// The "Options" parameter must be a JSON-formatted string containing a property that defines an array.
		/// If the array simply contains strings, those values will be used as options that the user can choose.
		/// </remarks>
		[DataMember]
		[JsonConfEditor(TypeEditor = "options", 
			Options = "{selectOptions:[\"My first option\",\"My second option\",\"My third option\"]}")]
		public string SimpleListOfValues { get; set; }

		/// <summary>
		/// A value allowing the user to select one of a predefined list of options.
		/// </summary>
		/// <remarks>
		/// The "Options" parameter must be a JSON-formatted string containing a property that defines an array.
		/// If the array simply contains strings, those values will be used as options that the user can choose.
		/// </remarks>
		[DataMember]
		[JsonConfEditor(TypeEditor = "options", 
			Options = "{selectOptions:[{label:\"My first option\", value:\"1\"},{label:\"My second option\", value: \"2\"}]}")]
		public string ListOfValuesWithDifferingLabelsAndValues { get; set; }

		#endregion

	}
}
