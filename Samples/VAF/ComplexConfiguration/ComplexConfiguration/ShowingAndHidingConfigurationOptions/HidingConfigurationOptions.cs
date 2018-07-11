using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.ShowingAndHidingConfigurationOptions
{
	[DataContract]
	public class HidingConfigurationOptions
	{
		/// <summary>
		/// Used as a trigger to show <see cref="AdvancedConfiguration"/>.
		/// </summary>
		/// <remarks>If true, <see cref="AdvancedConfiguration"/> will be shown in the administration area.  If false then it will not.</remarks>
		[DataMember]
		[JsonConfEditor(
			DefaultValue = true,
			HelpText = "If true, the AdvancedConfiguration settings will be shown.  If false (or not set) then it will not.")]
		public bool UsesAdvancedConfiguration { get; set; }
			= true;

		/// <summary>
		/// Shown only if <see cref="UsesAdvancedConfiguration"/> is true.
		/// </summary>
		[DataMember]
		[JsonConfEditor(
			Hidden = false,
			HideWhen = ".parent._children{.key == 'UsesAdvancedConfiguration' && .value != true }",
			HelpText = "This is hidden by default but shown if UsesAdvancedConfiguration is set to true.")]
		public AdvancedConfiguration AdvancedConfiguration { get; set; }
	}

	/// <summary>
	/// A dummy "advanced configuration" class.
	/// </summary>
	[DataContract]
	public class AdvancedConfiguration
	{
		/// <summary>
		/// A sample value that should only be populated in advanced configurations.
		/// </summary>
		[DataMember]
		public string Value { get; set; }
	}
}
