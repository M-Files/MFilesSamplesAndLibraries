using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.ShowingAndHidingConfigurationOptions
{
	/// <summary>
	/// Examples of using ShowWhen and HideWhen to control when configuration options are shown.
	/// </summary>
	[DataContract]
	public class ShowingAndHidingConfigurationOptions
	{
		/// <summary>
		/// Example of using values to show other configuration values.
		/// </summary>
		[DataMember]
		[JsonConfEditor(
			Label = "Showing values based on other configuration settings",
			HelpText = "ShowWhen and HideWhen configuration options use JSPath syntax to define the triggers to use.  More information is available at https://developer.m-files.com/Frameworks/Vault-Application-Framework/Configuration/Showing-And-Hiding/.")]
		public ShowingConfigurationOptions ShowingOptions { get; set; }
			= new ShowingConfigurationOptions();

		/// <summary>
		/// Example of using values to show other configuration values.
		/// </summary>
		[DataMember]
		[JsonConfEditor(
			Label = "Hiding values based on other configuration settings",
			HelpText = "ShowWhen and HideWhen configuration options use JSPath syntax to define the triggers to use.  More information is available at https://developer.m-files.com/Frameworks/Vault-Application-Framework/Configuration/Showing-And-Hiding/.")]
		public HidingConfigurationOptions HidingOptions { get; set; }
			= new HidingConfigurationOptions();

	}
}
