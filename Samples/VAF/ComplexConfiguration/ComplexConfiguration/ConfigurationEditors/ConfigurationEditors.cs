using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.ConfigurationEditors
{
	/// <summary>
	/// Examples of the various configuration editors available.
	/// </summary>
	[DataContract]
	public class ConfigurationEditors
	{

		/// <summary>
		/// Examples of textual configuration editors.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Text Configuration Editors")]
		public TextConfigurationEditors TextConfigurationEditors { get; set; }
			= new TextConfigurationEditors();

		/// <summary>
		/// Examples of numeric configuration editors.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Numeric Configuration Editors")]
		public NumericConfigurationEditors NumericConfigurationEditors { get; set; }
			= new NumericConfigurationEditors();

		/// <summary>
		/// Examples of configuration editors that reference vault structural elements.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Vault Structural Element Configuration Editors")]
		public MFIdentifierConfigurationEditors IdentifierConfigurationEditors { get; set; }
			= new MFIdentifierConfigurationEditors();

		/// <summary>
		/// Examples of configuration editors that have preconfigured lists of items to select.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Preconfigured List Configuration Editors")]
		public PreconfiguredListConfigurationEditors PreconfiguredListConfigurationEditors { get; set; }
			= new PreconfiguredListConfigurationEditors();

		/// <summary>
		/// Examples of configuration editors that allow users to configure their own customisable set of text.
		/// </summary>
		/// <remarks>Useful for situations such as custom notification text.</remarks>
		[DataMember]
		[JsonConfEditor(
			Label = "Placeholder Configuration Editors",
			HelpText = "Useful for situations such as custom notification text. Use ObjVerEx.ExpandPlaceholerText (https://developer.m-files.com/Frameworks/Vault-Application-Framework/Helpers/ObjVerEx/#expandplaceholdertext) at runtime to use the template.")]
		public PlaceholderConfigurationEditors PlaceholderConfigurationEditors { get; set; }
			= new PlaceholderConfigurationEditors();

		/// <summary>
		/// Examples of configuration editors that allow users to configure their own search conditions.
		/// </summary>
		[DataMember]
		[JsonConfEditor(
			Label = "Search Condition Configuration Editors",
			HelpText = "See https://developer.m-files.com/Frameworks/Vault-Application-Framework/Configuration/Editors/#search-conditions-editor for information on usage.")]
		public SearchConditionsConfigurationEditors SearchConditionsConfigurationEditors { get; set; }
			= new SearchConditionsConfigurationEditors();

		/// <summary>
		/// Examples of date-based configuration editors.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Date Configuration Editors")]
		public DateConfigurationEditors DateConfigurationEditors { get; set; }
			= new DateConfigurationEditors();

		/// <summary>
		/// Examples of time-based configuration editors.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Time Configuration Editors")]
		public TimeConfigurationEditors TimeConfigurationEditors { get; set; }
			= new TimeConfigurationEditors();

		/// <summary>
		/// Examples of timepicker-based configuration editors.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Timestamp (date and time) Configuration Editors")]
		public TimestampConfigurationEditors TimestampConfigurationEditors { get; set; }
			= new TimestampConfigurationEditors();

		/// <summary>
		/// Examples of color-based configuration editors.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Color Configuration Editors")]
		public ColorConfigurationEditors ColorConfigurationEditors { get; set; }
			= new ColorConfigurationEditors();
	}
}
