using System.Runtime.Serialization;
using ComplexConfiguration.ConfigurationEditors;
using ComplexConfiguration.Lists;
using MFiles.VAF.Configuration;
using Newtonsoft.Json;

namespace ComplexConfiguration
{
	/// <summary>
	/// Defines the configuration which the application needs.
	/// Will be editable using the M-Files Admin interface on M-Files 2018 and higher.
	/// </summary>
	/// <remarks>More information and examples are available on the M-Files Developer Portal: https://developer.m-files.com/Frameworks/Vault-Application-Framework/Configuration/. </remarks>
	[DataContract]
	public class MyConfiguration
	{

		/// <summary>
		/// Examples of configuration editors.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Configuration Editors")]
		public ConfigurationEditors.ConfigurationEditors ConfigurationEditors { get; set; }
			= new ConfigurationEditors.ConfigurationEditors();

		/// <summary>
		/// Examples of using lists in configuration editors.
		/// </summary>
		[DataMember]
		public Lists.Lists Lists { get; set; }
			= new Lists.Lists();

		/// <summary>
		/// Examples of showing and hiding configuration options depending on other configuration settings.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Controlling Configuration Visibility")]
		public ShowingAndHidingConfigurationOptions.ShowingAndHidingConfigurationOptions ShowingAndHiding { get; set; }
			= new ShowingAndHidingConfigurationOptions.ShowingAndHidingConfigurationOptions();

		#region Serialization (just used to log to the Windows Event Log - not needed in real-world environments)

		/// <summary>
		/// The default settings used for serialization.
		/// </summary>
		public static JsonSerializerSettings DefaultSerializationSettings { get; set; }
			= new JsonSerializerSettings()
			{
				DateFormatHandling = DateFormatHandling.IsoDateFormat,
				Formatting = Formatting.Indented
			};

		/// <summary>
		/// Returns a JSON-serialized representation of the current object.
		/// </summary>
		/// <param name="serializationSettings">The serialization settings to use.  If null will use <see cref="DefaultSerializationSettings"/>.</param>
		/// <returns>The JSON-serialized representation of the current object.</returns>
		public string ToString(JsonSerializerSettings serializationSettings)
		{
			// Ensure we have some serialization settings to use.
			var settings = serializationSettings
							?? MyConfiguration.DefaultSerializationSettings
							?? new JsonSerializerSettings();

			// Serialize the object and return it.
			return JsonConvert.SerializeObject(this, settings);
		}

		#region Overrides of Object

		/// <inheritdoc />
		/// <remarks>Returns a JSON-serialized representation of the current object.  See <see cref="ToString(Newtonsoft.Json.JsonSerializerSettings)"/>.</remarks>
		public override string ToString()
		{
			// Use the default serialization settings.
			return this.ToString(MyConfiguration.DefaultSerializationSettings);
		}

		#endregion

		#endregion

	}
}
