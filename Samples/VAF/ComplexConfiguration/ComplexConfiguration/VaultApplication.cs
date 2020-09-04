using System;
using System.Collections.Generic;
using MFiles.VAF;
using MFiles.VAF.AdminConfigurations;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Configuration.AdminConfigurations;
using MFiles.VAF.Core;
using MFilesAPI;

namespace ComplexConfiguration
{
	/// <summary>
	/// The entry point for this Vault Application Framework application.
	/// </summary>
	/// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
	public partial class VaultApplication
		: ConfigurableVaultApplicationBase<MyConfiguration>
	{
		protected override IEnumerable<ValidationFinding> CustomValidation(Vault vault, MyConfiguration config)
		{
			return this.CustomValidator(config);
		}

		protected override void OnConfigurationUpdated(IConfigurationRequestContext context, ClientOperations clientOps, MyConfiguration oldConfiguration)
		{
			SysUtils.ReportInfoToEventLog($"Updated (complex) configuration is:\r\n{this.Configuration}");
		}

		/// <inheritdoc />
		protected override void StartApplication()
		{
			// Allow the application to start up as normal.
			base.StartApplication();

			// Output the (startup) configuration information to the Windows Event Log.
			SysUtils.ReportInfoToEventLog($"Startup (complex) configuration is:\r\n{this.Configuration}");
		}
	}
}