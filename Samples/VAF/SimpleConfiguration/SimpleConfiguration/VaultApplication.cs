using MFiles.VAF;
using MFiles.VAF.AdminConfigurations;
using MFiles.VAF.Common;

namespace SimpleConfiguration
{
	/// <summary>
	/// An example vault application showing a basic configuration.
	/// </summary>
	/// <remarks><see cref="MyConfiguration"/> defines the specific options that the user sees in the administration area.</remarks>
	public class VaultApplication
		: VaultApplicationBase, IUsesAdminConfigurations
	{
		/// <summary>
		/// A reference to the configuration note added in <see cref="InitializeAdminConfigurations"/>.
		/// </summary>
		public ConfigurationNode<MyConfiguration> ConfigurationNode { get; private set; }

		#region Implementation of IUsesAdminConfigurations

		/// <inheritdoc />
		/// <remarks>Sets up <see cref="ConfigurationNode"/> and adds a configuration node to the M-Files Admin interface to edit the configuration.</remarks>
		public void InitializeAdminConfigurations(IAdminConfigurations adminConfigurations)
		{
			// Define a configuration node that will be shown in the admin area, and configuration will be made available in this vault application.
			// The configuration node will be named "Simple Configuration Integration Example".
			// The configuration options shown will be defined by the structure of the MyConfiguration class.
			this.ConfigurationNode =
				adminConfigurations.AddSimpleConfigurationNode<MyConfiguration>("Simple Configuration Integration Example");

			// React when the configuration is changed in the M-Files Admin, output the configuration information.
			this.ConfigurationNode.Changed += (oldConfig, newConfig) =>
			{
				SysUtils.ReportInfoToEventLog($"Updated (simple) configuration is:\r\n{newConfig}");
			};
		}

		#endregion

		/// <inheritdoc />
		protected override void StartApplication()
		{
			// Allow the application to start up as normal.
			base.StartApplication();

			// Output the (startup) configuration information to the Windows Event Log.
			SysUtils.ReportInfoToEventLog($"Startup (simple) configuration is:\r\n{this.ConfigurationNode.CurrentConfiguration}");
		}
	}
}