using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.ConfigurationEditors
{
	/// <summary>
	/// Showcases options for editors that allow users to create templates with placeholder text that can be replaced at runtime.
	/// </summary>
	/// <remarks>
	/// Use ObjVerEx.ExpandPlaceholerText (https://developer.m-files.com/Frameworks/Vault-Application-Framework/Helpers/ObjVerEx/#expandplaceholdertext) at runtime to use the template.
	/// </remarks>
	[DataContract]
	public class PlaceholderConfigurationEditors
	{

		/// <summary>
		/// A value allowing the user to create/customise a template (e.g. for notifications).
		/// </summary>
		[DataMember]
		[JsonConfEditor(TypeEditor = "placeholderText")]
		public string TextPlaceholderTemplateValue { get; set; }
	}
}
