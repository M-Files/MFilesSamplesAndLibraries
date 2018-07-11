using System;
using System.Collections.Generic;
using MFiles.VAF;
using MFiles.VAF.AdminConfigurations;
using MFiles.VAF.Common;

namespace ComplexConfiguration
{
	/// <summary>
	/// The entry point for this Vault Application Framework application.
	/// </summary>
	/// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
	public partial class VaultApplication
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
			// The configuration node will be named "Complex Configuration Integration Example".
			// The configuration options shown will be defined by the structure of the MyConfiguration class.
			// The GenerateDashboard method will be used to create the dashboard when that is selected in the configuration area.
			// Note: GenerateDashboard method is in VaultApplication.Dashboard.cs.
			this.ConfigurationNode =
				adminConfigurations
					.AddSimpleConfigurationNode<MyConfiguration>("Complex Configuration Integration Example", this.GenerateDashboard);

			// React when the configuration is changed in the M-Files Admin, output the configuration information.
			this.ConfigurationNode.Changed += (oldConfig, newConfig) =>
			{
				SysUtils.ReportInfoToEventLog($"Updated (complex) configuration is:\r\n{newConfig}");
			};

			// Provide some custom (server-side) validation.
			this.ConfigurationNode.Validator = this.CustomValidator;
		}

		#endregion

		/// <inheritdoc />
		protected override void StartApplication()
		{
			// Allow the application to start up as normal.
			base.StartApplication();

			// Output the (startup) configuration information to the Windows Event Log.
			SysUtils.ReportInfoToEventLog($"Startup (complex) configuration is:\r\n{this.ConfigurationNode.CurrentConfiguration}");
		}
	}
}