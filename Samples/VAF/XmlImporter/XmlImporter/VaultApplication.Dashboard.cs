using System;
using MFiles.VAF.Configuration.Domain.Dashboards;

namespace XmlImporter
{
	public partial class VaultApplication
	{
		/// <summary>
		/// Generates the HTML for the dashboard.
		/// </summary>
		/// <returns></returns>
		public string DashboardGenerator()
		{

			// Create the content for the dashboard.
			var content = new DashboardContentCollection
			{
				// Display the licence information.
				new DashboardCustomContent($"<p>The current licence status is: <strong>{this.License.LicenseStatus}</strong>.</p>"),

				// Display some configuration information.
				new DashboardCustomContent($"<p>There are <strong>{this.Configuration.ImportInstructions.Count}</strong> import instructions configured.</p>")
			};

			// The status dashboard is our root object, and will handle generating the required HTML.
			var statusDashboard = new StatusDashboard();

			// Create a panel for this content to sit in and set the content and title.
			var panel = new DashboardPanel()
			{
				Title = "Xml Importer",
				InnerContent = content
			};

			// Add the panel to the dashboard.
			statusDashboard.Contents.Add(panel);

			//Return the dashboard HTML.
			return statusDashboard.ToString();
		}
	}
}
