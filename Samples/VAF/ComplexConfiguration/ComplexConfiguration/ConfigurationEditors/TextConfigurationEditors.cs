using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.ConfigurationEditors
{
	/// <summary>
	/// Showcases options for "text"-style editors.
	/// </summary>
	[DataContract]
	public class TextConfigurationEditors
	{
		/// <summary>
		/// A simple text value that the user can add anything into.
		/// </summary>
		/// <remarks>Note that for simple data types the <see cref="TextEditorAttribute"/> and <see cref="MultilineTextEditorAttribute"/> attributes are optional.</remarks>
		[DataMember]
		public string SimpleTextValue { get; set; } = string.Empty;

		#region Default values

		/// <summary>
		/// A default value for <see cref="TextValueWithDefault"/>.
		/// </summary>
		private const string defaultValue = "My Default Value";

		/// <summary>
		/// A simple text value that has a default.
		/// </summary>
		[DataMember]
		[TextEditor(DefaultValue = TextConfigurationEditors.defaultValue)]
		public string TextValueWithDefault { get; set; }
			= TextConfigurationEditors.defaultValue;

		#endregion

		#region Custom label

		/// <summary>
		/// A simple text value that has a custom label.
		/// </summary>
		[DataMember]
		[TextEditor(Label = "This has a custom label")]
		public string TextValueWithCustomLabel { get; set; }

		#endregion

		#region Help text

		/// <summary>
		/// A simple text value that has some help text.
		/// </summary>
		[DataMember]
		[TextEditor(HelpText = "This is shown when the user clicks the small 'i' next to the configuration option.")]
		public string TextValueWithHelpText { get; set; } = string.Empty;

		#endregion

		#region Regular expression masks

		/// <summary>
		/// A simple text value that has a regular expression mask.
		/// </summary>
		[DataMember]
		[TextEditor(RegExMask = @"^\d{3,}$", HelpText = "Only numeric characters are allowed; at least three are required to validate.")]
		public string AtLeastThreeNumericCharacters { get; set; } = string.Empty;

		#endregion

		#region Multi-line text values

		/// <summary>
		/// A multi-line text value.
		/// </summary>
		[DataMember]
		[MultilineTextEditor]
		public string SimpleMultiLineTextValue { get; set; } = string.Empty;

		/// <summary>
		/// A multi-line text value with a custom label.
		/// </summary>
		/// <remarks><see cref="MultilineTextEditorAttribute"/> can take the same parameters as <see cref="TextEditorAttribute"/>.</remarks>
		[DataMember]
		[MultilineTextEditor(Label = "Multi-line text value with a custom label")]
		public string SimpleMultiLineTextValueWithCustomLabel { get; set; } = string.Empty;

		#endregion

	}
}