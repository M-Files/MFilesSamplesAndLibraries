using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.ConfigurationEditors
{
	/// <summary>
	/// Showcases options for "numeric"-style editors.
	/// </summary>
	[DataContract]
	public class NumericConfigurationEditors
	{
		/// <summary>
		/// A simple integer value that the user can add a valid integer into.
		/// </summary>
		/// <remarks>Note that for simple data types the <see cref="JsonConfIntegerEditorAttribute"/> and <see cref="JsonConfFloatEditorAttribute"/> attributes are optional.</remarks>
		[DataMember]
		public int SimpleIntegerValue { get; set; }

		/// <summary>
		/// A simple float value that the user can add a valid value into.
		/// </summary>
		/// <remarks>Note that for simple data types the <see cref="JsonConfIntegerEditorAttribute"/> and <see cref="JsonConfFloatEditorAttribute"/> attributes are optional.</remarks>
		[DataMember]
		public float SimpleFloatValue { get; set; }

		#region Default values

		/// <summary>
		/// A default value for <see cref="IntegerValueWithNumber"/>.
		/// </summary>
		private const int defaultIntegerValue = 4466;

		/// <summary>
		/// A simple integer value that has a default.
		/// </summary>
		[DataMember]
		[JsonConfIntegerEditor(DefaultValue = NumericConfigurationEditors.defaultIntegerValue)]
		public int IntegerValueWithNumber { get; set; }
			= NumericConfigurationEditors.defaultIntegerValue;

		/// <summary>
		/// A default value for <see cref="FloatValueWithNumber"/>.
		/// </summary>
		private const float defaultFloatValue = 1.2f;

		/// <summary>
		/// A simple integer value that has a default.
		/// </summary>
		[DataMember]
		[JsonConfFloatEditor(DefaultValue = NumericConfigurationEditors.defaultFloatValue)]
		public float FloatValueWithNumber { get; set; }
			= NumericConfigurationEditors.defaultFloatValue;

		#endregion

		#region Custom label

		/// <summary>
		/// A simple value that has a custom label.
		/// </summary>
		[DataMember]
		[JsonConfIntegerEditor(Label = "This has a custom label")]
		public int IntegerValueWithCustomLabel { get; set; }

		#endregion

		#region Help text

		/// <summary>
		/// A simple value that has some help text.
		/// </summary>
		[DataMember]
		[JsonConfIntegerEditor(HelpText = "This is shown when the user clicks the small 'i' next to the configuration option.")]
		public int IntegerValueWithHelpText { get; set; }

		#endregion

		#region Maximum and minimum values

		/// <summary>
		/// A value that must reside within a range.
		/// </summary>
		/// <remarks>Maximum value defaults to 2147483647 and minimum value to -2147483648 (on integers).</remarks>
		[DataMember]
		[JsonConfIntegerEditor(Max = 10000, Min = 0, DefaultValue = NumericConfigurationEditors.defaultIntegerValue)]
		public int IntegerValueWithMaximumAndMinimum { get; set; }
			= NumericConfigurationEditors.defaultIntegerValue;

		/// <summary>
		/// A value that must reside within a range.
		/// </summary>
		/// <remarks>
		/// Maximum value defaults to <see cref="double.PositiveInfinity"/> and minimum value to <see cref="double.NegativeInfinity"/> (on floats).
		/// Maximum precision defaults to 7 and minimum precision to 2.
		/// </remarks>
		[DataMember]
		[JsonConfFloatEditor(Max = 10, Min = -10, 
			MinPrecision = 1, MaxPrecision = 3,
			DefaultValue = NumericConfigurationEditors.defaultFloatValue)]
		public float FloatValueWithMaximumAndMinimum { get; set; }
			= NumericConfigurationEditors.defaultFloatValue;

		#endregion

	}
}