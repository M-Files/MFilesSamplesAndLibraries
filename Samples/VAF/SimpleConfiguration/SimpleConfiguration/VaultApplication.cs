using MFiles.VAF;
using MFiles.VAF.AdminConfigurations;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration.AdminConfigurations;
using MFiles.VAF.Core;

namespace SimpleConfiguration
{
	/// <summary>
	/// An example vault application showing a basic configuration.
	/// </summary>
	/// <remarks><see cref="MyConfiguration"/> defines the specific options that the user sees in the administration area.</remarks>
	public class VaultApplication
		: ConfigurableVaultApplicationBase<MyConfiguration>
	{

		protected override void OnConfigurationUpdated(IConfigurationRequestContext context, ClientOperations clientOps, MyConfiguration oldConfiguration)
		{
			SysUtils.ReportInfoToEventLog($"Updated (simple) configuration is:\r\n{this.Configuration}");
		}

		/// <inheritdoc />
		protected override void StartApplication()
		{
			// Allow the application to start up as normal.
			base.StartApplication();

			// Output the (startup) configuration information to the Windows Event Log.
			SysUtils.ReportInfoToEventLog($"Startup (simple) configuration is:\r\n{this.Configuration}");
		}
	}
}