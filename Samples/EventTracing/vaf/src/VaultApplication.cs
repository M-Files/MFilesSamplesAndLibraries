
using MFiles.VAF;
using MFiles.VAF.Common;

namespace EventTracing
{
	/// <summary>
	/// A sample vault application which aids in tracing events and VAF lifecycle.
	/// </summary>
	public partial class VaultApplication
		: VaultApplicationBase
	{
		/// <inheritdoc />
		public VaultApplication()
		{
			SysUtils.ReportInfoToEventLog($"Constructor called.");
		}
	}
}