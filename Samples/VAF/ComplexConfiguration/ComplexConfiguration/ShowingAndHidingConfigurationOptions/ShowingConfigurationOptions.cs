using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.ShowingAndHidingConfigurationOptions
{
	[DataContract]
	public class ShowingConfigurationOptions
	{
		/// <summary>
		/// Used as a trigger to show different integration options.
		/// </summary>
		[DataMember]
		[JsonConfEditor(DefaultValue = SupportedIntegrationTypes.NotConfigured)]
		public SupportedIntegrationTypes ConfigurationType { get; set; }
			= SupportedIntegrationTypes.NotConfigured;

		/// <summary>
		/// Shown only if <see cref="ConfigurationType"/> is <see cref="SupportedIntegrationTypes.TypeOne"/>.
		/// </summary>
		[DataMember]
		[JsonConfEditor(
			Hidden = true,
			Label = "Integration Configuration",
			ShowWhen = ".parent._children{.key == 'ConfigurationType' && .value == 'TypeOne' }",
			HelpText = "This is hidden by default but shown if ConfigurationType is set to TypeOne.")]
		public IntegrationTypeOneConfiguration IntegrationTypeOneConfiguration { get; set; }

		/// <summary>
		/// Shown only if <see cref="ConfigurationType"/> is <see cref="SupportedIntegrationTypes.TypeOne"/>.
		/// </summary>
		[DataMember]
		[JsonConfEditor(
			Hidden = true,
			Label = "Integration Configuration",
			ShowWhen = ".parent._children{.key == 'ConfigurationType' && .value == 'TypeTwo' }",
			HelpText = "This is hidden by default but shown if ConfigurationType is set to TypeTwo.")]
		public IntegrationTypeTwoConfiguration IntegrationTypeTwoConfiguration { get; set; }
	}

	/// <summary>
	/// Dummy integration types supported by this module.
	/// </summary>
	public enum SupportedIntegrationTypes
	{
		/// <summary>
		/// The configuration is not configured.
		/// </summary>
		NotConfigured = 0,

		/// <summary>
		/// When selected, the configuration from <see cref="IntegrationTypeOneConfiguration"/> should be used.
		/// </summary>
		TypeOne = 1,

		/// <summary>
		/// When selected, the configuration from <see cref="IntegrationTypeTwoConfiguration"/> should be used.
		/// </summary>
		TypeTwo = 2
	}

	/// <summary>
	/// A dummy class that represents a type of external integration supported by this module.
	/// </summary>
	[DataContract]
	public class IntegrationTypeOneConfiguration
	{
		/// <summary>
		/// A sample value that should only be populated in integration one.
		/// </summary>
		[DataMember]
		public string IntegrationOneEndPoint { get; set; }
	}

	/// <summary>
	/// A dummy class that represents a type of external integration supported by this module.
	/// </summary>
	[DataContract]
	public class IntegrationTypeTwoConfiguration
	{
		/// <summary>
		/// A sample value that should only be populated in integration two.
		/// </summary>
		[DataMember]
		public string IntegrationTwoEndPoint { get; set; }
	}
}
