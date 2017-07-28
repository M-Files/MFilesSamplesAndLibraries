using System;
using System.IO;
using MFiles.VAF;
using MFiles.VAF.Common;
using MFilesAPI;

namespace ServerComponent
{
	public class VaultApplication
		: VaultApplicationBase
	{
		/// <summary>
		/// Install the UIX application, as it will not be installed by default.
		/// </summary>
		/// <param name="vault">The vault to install the application into.</param>
		protected override void InitializeApplication(Vault vault)
		{
			try
			{
				string appPath = "UIX.mfappx";
				if (File.Exists(appPath))
				{
					vault.CustomApplicationManagementOperations.InstallCustomApplication(appPath);
				}
				else
				{
					SysUtils.ReportErrorToEventLog("File: " + appPath + " does not exist");
				}
			}
			catch (Exception ex)
			{
				SysUtils.ReportErrorToEventLog(ex.Message);
			}

			base.InitializeApplication(vault);
		}

		/// <summary>
		/// A vault extension method, that will be installed to the vault with the application.
		/// The vault extension method can be called through the API.
		/// </summary>
		/// <param name="env">The event handler environment for the method.</param>
		/// <returns>The output string to the caller.</returns>
		[VaultExtensionMethod("VaultExtensionMethod_VAF", RequiredVaultAccess = MFVaultAccess.MFVaultAccessNone)]
		private string TestVaultExtensionMethod(EventHandlerEnvironment env)
		{
			return "The VAF event handler said the time was: "
				+ DateTime.Now.ToLongTimeString()
				+ " (input was: " + env.Input
			    + ")";
		}
	}
}