using MFiles.VAF.Common;
using MFilesAPI;

namespace EventTracing
{
	/// <summary>
	/// A sample vault application which aids in tracing events and VAF lifecycle.
	/// </summary>
	public partial class VaultApplication
	{
		/// <inheritdoc />
		protected override void InitializeApplication(Vault vault)
		{
			SysUtils.ReportInfoToEventLog($"Begin InitializeApplication");
			base.InitializeApplication(vault);
			SysUtils.ReportInfoToEventLog($"End InitializeApplication");
		}

		/// <inheritdoc />
		protected override void InstallApplication(Vault vault)
		{
			SysUtils.ReportInfoToEventLog($"Begin InstallApplication");
			base.InstallApplication(vault);
			SysUtils.ReportInfoToEventLog($"End InstallApplication");
		}

		/// <inheritdoc />
		protected override void StartApplication()
		{
			SysUtils.ReportInfoToEventLog($"Begin StartApplication");
			base.StartApplication();
			SysUtils.ReportInfoToEventLog($"End StartApplication");
		}

		/// <inheritdoc />
		protected override void UninitializeApplication(Vault vault)
		{
			SysUtils.ReportInfoToEventLog($"Begin UninitializeApplication");
			base.UninitializeApplication(vault);
			SysUtils.ReportInfoToEventLog($"End UninitializeApplication");
		}

		/// <inheritdoc />
		protected override void UninstallApplication(Vault vault)
		{
			SysUtils.ReportInfoToEventLog($"Begin UninstallApplication");
			base.UninstallApplication(vault);
			SysUtils.ReportInfoToEventLog($"End UninstallApplication");
		}
	}
}