using System;
using System.Diagnostics;
using MFiles.VAF;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;

namespace XmlImporter
{
	public partial class VaultApplication
	{
		public VaultApplication()
		{
			try
			{
				// Set up the license decoder.
				var licenseDecoder =
					new LicenseDecoder(LicenseDecoder.EncMode.TwoKey);

				// This is from the key file (MainKey.PublicXml).
				licenseDecoder.MainKey = "<RSAKeyValue><Modulus>rOXHN+8mGTmAtJj+OOIJYmDkHnr6Zu+s+Z9PWVzbZlQf3u9EKlP5dN2rRMcX+PPFLBWRciJZlJez5Z19kTU0OTBZrEA1BRYTZyfXqHWuV7xDeVJ0kEm/7J0IoZx2V6n1IUZrTNgkllRfCmn6s+4Q8ttSlD0MOBcDnLXs/xm8q6U=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

				// This is from the key file (SecondKey.SecretXml)
				licenseDecoder.AltKey = "<RSAKeyValue><Modulus>nWr/Mk/B1l5FpnJrv5mkT6paDmrCsB9b5x++TtnUS3H8XRXXkNMnEMgP1ESAG5TpUw1CscXsCpBpExeK/VhVLk/7IRxJJeg4FLsaKNx8D29m5GCGSXxcxHbFEsK630Gpavs8g9S7/sPLUU/aD2NidWRXu27RyG5DlpsiFFM7Qk0=</Modulus><Exponent>AQAB</Exponent><P>z13X38M+fX4wLg2FX36yO6A4B3kaDTudVGhCO2hONjAS0qgNQIGLSUGK37khtTP41bmeWvmI3wOjobroVul5Gw==</P><Q>wlZFOyr7JJumkBjHb5p9tpmxObkxZzVt0l2yvvxQt+IIjqmUvojnWCbyEah5dgQryP8VlcxFOqNIK8caIIwQtw==</Q><DP>N/EJG8SARzqTpNjg8jIrwwomE14IOSmr9+lodL5e2x989HeBm7VW7hxQaqp2/XtX1dCNd915nzBMJIVXyJqToQ==</DP><DQ>gT1eTTsIShA5dRsFhvL3J7iOZBUFBd5CauRlOx4RkDiB+F5OzWe+cqFz2spv/ExJ0iHR+Q0f/R8ZoAOJHJwJgQ==</DQ><InverseQ>WjUv/GaQL7rXvU84R+GhPgOiZh0MtJ1GLRHqGPhkHcNFTzn55gRc+NXITYRHa7e7x1Za6aPchJKY4Tbqw5bAAA==</InverseQ><D>LXWuyOq7gZqr5ot4jlZiWxdI9oziOFZ2BeLm5IKHiloalQ8vt4Ui7Pe5ioVVsaFpWDCmKAyOQ+a8UWUlIFCYGFnCzGdPyJffN+uKyOucuAnAKa08ZkoHSvbNJ96o+d6p/bTnXdyGb3+mpsm5Y2as4bKQYGuILfytWa4UIdQj30k=</D></RSAKeyValue>";
				this.License =
					new LicenseManagerBase<LicenseContentBase>(licenseDecoder);

			}
			catch (Exception ex)
			{
				SysUtils.ReportErrorToEventLog(this.EventSourceIdentifier,
					ex.Message);
			}
		}
		/// <inheritdoc />
		public override void StartOperations(Vault vaultPersistent)
		{
			// Evaluate the license validity.
			this.License.Evaluate(vaultPersistent, false);

			// Output the license status.
			switch (this.License.LicenseStatus)
			{
				case MFApplicationLicenseStatus.MFApplicationLicenseStatusTrial:
				{
					SysUtils.ReportToEventLog(
						"Application is running in a trial mode.",
						EventLogEntryType.Warning);
					break;
				}
				case MFApplicationLicenseStatus.MFApplicationLicenseStatusValid:
				{
					SysUtils.ReportInfoToEventLog("Application is licensed.");
					break;
				}
				default:
				{
					SysUtils.ReportToEventLog(
						$"Application is in an unexpected state: {this.License.LicenseStatus}.",
						EventLogEntryType.Error);
					break;
				}
			}
			base.StartOperations(vaultPersistent);
		}

	}
}
