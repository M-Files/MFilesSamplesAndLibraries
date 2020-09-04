using MFiles.VAF.Configuration.Domain.Dashboards;

namespace ComplexConfiguration
{
	public partial class VaultApplication
	{
		/// <summary>
		/// Generates the dashboard when the user selects the "Dashboard" tab in the configuration area.
		/// </summary>
		/// <returns>The dashboard HTML.</returns>
		private string GenerateDashboard()
		{
			// Note: only some HTML tags and attributes are allowed.
			// ref: https://developer.m-files.com/Frameworks/Vault-Application-Framework/Configuration/Custom-Dashboards/

			// Retrieve something from the configuration.
			var propertyDefId = this
									.Configuration?
									.ConfigurationEditors?
									.IdentifierConfigurationEditors?
									.PropertyDefValue?
									.ID ?? -1;

			// Build up a dashboard.
			var dashboard = new StatusDashboard()
			{
				Contents = new DashboardContentCollection()
				{
					new DashboardPanel()
					{
						Title = "My first dashboard",
						InnerContent = new DashboardContentCollection()
						{
							// Output what one of the MFIdentifiers is currently resolved to.
							new DashboardCustomContent(
								$"<p>The property definition ID for <code>ConfigurationEditors.IdentifierConfigurationEditors.PropertyDefValue</code> was <code>{propertyDefId}</code>.</p>"),
						}
					}
				}
			};
			return dashboard.ToString();
		}

	}
}