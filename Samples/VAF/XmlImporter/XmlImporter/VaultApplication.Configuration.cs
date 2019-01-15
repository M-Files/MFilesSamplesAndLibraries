using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFiles.VAF.AdminConfigurations;
using MFiles.VAF.Configuration.Domain.Dashboards;

namespace XmlImporter
{
	public partial class VaultApplication
		: IUsesAdminConfigurations
	{
		/// <summary>
		/// The current configuration.
		/// </summary>
		public ConfigurationNode<Configuration> Configuration { get; set; }

		#region Implementation of IUsesAdminConfigurations

		/// <inheritdoc />
		public void InitializeAdminConfigurations(IAdminConfigurations adminConfigurations)
		{
			this.Configuration = adminConfigurations
				.AddSimpleConfigurationNode<Configuration>("Xml Importer", this.DashboardGenerator);
		}

		#endregion

	}
}
