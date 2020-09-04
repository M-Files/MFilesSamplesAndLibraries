using System;
using System.Diagnostics;
using MFiles.VAF.Common;
using MFilesAPI;

namespace XmlImporter
{
	public partial class VaultApplication
	{
		/// <summary>
		/// The name of the background operation.
		/// </summary>
		public const string BackgroundOperationName = "XmlImporter";

		#region Overrides of VaultApplicationBase

		private void StartRecurringBackgroundOperation(string name = VaultApplication.BackgroundOperationName)
		{
			this.BackgroundOperations.StartRecurringBackgroundOperation(
				"XmlImporter",
#if DEBUG
				TimeSpan.FromSeconds(10),
#else
				TimeSpan.FromMinutes(1),
#endif
				() =>
				{
					// Sanity.
					if (null == this.Configuration?.ImportInstructions)
						return;

					// Iterate over the import instructions and import each one in turn.
					foreach (var importInstruction in this.Configuration.ImportInstructions)
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
				})
				.ContinueWith((t) =>
				{
					// Did the task complete successfully?
					if(t.IsCanceled || t.IsFaulted)
					{
						SysUtils.ReportErrorMessageToEventLog
							(
							"Could not start Xml importer background operation",
							t.Exception
							);
						return;
					}

					// If successful, store the background operation reference.
					this.ImportBackgroundOperation = t.Result;
				});
		}

		#endregion

		/// <summary>
		/// The background operation for importing files.
		/// </summary>
		private BackgroundOperation ImportBackgroundOperation { get; set; }

		/// <summary>
		/// Stops the current import operation, if possible.
		/// </summary>
		public void StopImportBackgroundOperation()
		{
			// Sanity.
			if (null == this.ImportBackgroundOperation)
				return;

			// Stop the background operation from running, if we can.
			try
			{
				this.ImportBackgroundOperation.StopRunningAtIntervals();
				this.ImportBackgroundOperation.Cancel(true);
			}
			catch (Exception e)
			{
				SysUtils.ReportErrorMessageToEventLog(
					base.EventSourceIdentifier,
					"Exception cancelling background operation.",
					e,
					EventLogEntryType.Warning);
			}
		}

		/// <summary>
		/// Starts the background operation, if possible.
		/// </summary>
		public void StartImportBackgroundOperation()
		{
			// First stop the operation if we can.
			this.StopImportBackgroundOperation();

			// Run the background at the appropriate interval.
			try
			{
				this.StartRecurringBackgroundOperation();
			}
			catch (Exception e)
			{
				SysUtils.ReportErrorMessageToEventLog("Exception starting background operation", e);
			}
		}

	}
}
