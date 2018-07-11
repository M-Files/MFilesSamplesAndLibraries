using System.Runtime.Serialization;
using MFiles.VAF.Configuration;
using Newtonsoft.Json;

namespace SimpleConfiguration
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
		/// A default value for <see cref="TextValueWithDefault"/>.
		/// </summary>
		private const string defaultValue = "My Default Value";

		/// <summary>
		/// A simple text value that has a default.
		/// </summary>
		[DataMember]
		[TextEditor(DefaultValue = MyConfiguration.defaultValue)]
		public string TextValueWithDefault { get; set; }
			= MyConfiguration.defaultValue;

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
