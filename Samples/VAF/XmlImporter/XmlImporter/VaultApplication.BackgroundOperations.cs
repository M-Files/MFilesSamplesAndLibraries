using System;
using MFiles.VAF.Common;

namespace XmlImporter
{
	public partial class VaultApplication
	{

		#region Overrides of VaultApplicationBase

		/// <inheritdoc />
		protected override void StartApplication()
		{
			base.StartApplication();

			// Every one minute execute our import instructions.
			this.BackgroundOperations.StartRecurringBackgroundOperation
			(
				"XmlImporter",
#if DEBUG
				TimeSpan.FromSeconds(10),
#else
				TimeSpan.FromMinutes(1),
#endif
				() =>
				{
					// Sanity.
					if (null == this.Configuration?.CurrentConfiguration?.ImportInstructions)
						return;

					// Iterate over the import instructions and import each one in turn.
					foreach (var importInstruction in this.Configuration.CurrentConfiguration.ImportInstructions)
					{
						// Is it enabled?
						if (false == importInstruction.Enabled)
							continue;

						// Import it.
						try
						{
							this.ImportXmlFile(importInstruction);
						}
						catch (Exception e)
						{
							SysUtils.ReportErrorMessageToEventLog("Exception importing file", e);
						}
					}
				}
			);

		}

#endregion

	}
}
