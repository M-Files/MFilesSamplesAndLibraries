using System.Runtime.Serialization;
using MFiles.VAF.Configuration;
using MFiles.VAF.Configuration.JsonAdaptor;

namespace ComplexConfiguration.ConfigurationEditors
{
	/// <summary>
	/// Showcases options for editors that allow users to define their own search conditions.
	/// </summary>
	/// <remarks>
	/// See https://developer.m-files.com/Frameworks/Vault-Application-Framework/Configuration/Editors/#search-conditions-editor for information on usage.
	/// </remarks>
	[DataContract]
	public class SearchConditionsConfigurationEditors
	{

		/// <summary>
		/// A value allowing the user to customise their own set of search conditions.
		/// </summary>
		[DataMember]
		public SearchConditionsJA SearchConditionsValue { get; set; }
	}
}
