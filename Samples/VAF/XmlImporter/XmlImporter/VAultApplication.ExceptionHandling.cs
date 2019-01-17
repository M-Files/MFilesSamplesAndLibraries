using System;
using System.IO;
using MFiles.VAF.Common;

namespace XmlImporter
{
	public partial class VaultApplication
	{
		/// <summary>
		/// Handles an exception according to the <see cref="ImportInstruction.ImportExceptionHandlingStrategy"/>.
		/// </summary>
		/// <param name="importInstruction">The import instruction being processed.</param>
		/// <param name="fileBeingProcessed">The file which was being processed when the exception occurred.</param>
		/// <param name="importException">The exception raised.</param>
		public void HandleImportException(
			ImportInstruction importInstruction,
			FileInfo fileBeingProcessed,
			Exception importException)
		{
			// Sanity.
			if (null == importInstruction)
				throw new ArgumentNullException(nameof(importInstruction));
			if (null == fileBeingProcessed)
				throw new ArgumentNullException(nameof(fileBeingProcessed));
			if (null == importException)
				throw new ArgumentNullException(nameof(importException));
#if DEBUG
			System.Diagnostics.Debugger.Launch();
#endif
			// Report the error.
			SysUtils.ReportErrorMessageToEventLog($"Exception importing {fileBeingProcessed.FullName}", importException);

			// What is the exception handling strategy?
			try
			{
				switch (importInstruction.ImportExceptionHandlingStrategy)
				{
					case ImportExceptionHandlingStrategy.Delete:
						{
							// Delete the file.
							fileBeingProcessed.Delete();
							break;
						}
					case ImportExceptionHandlingStrategy.MoveToSpecificFolder:
						{
							// Get a reference to the folder to move the file to.
							var targetDirectoryInfo = new DirectoryInfo(importInstruction.ExceptionFolderName);

							// Create it if it does not exist.
							if (false == targetDirectoryInfo.Exists)
								targetDirectoryInfo.Create();

							// Move the file there.
							fileBeingProcessed.MoveTo(
								System.IO.Path.Combine(targetDirectoryInfo.FullName, fileBeingProcessed.Name)
							);
							break;
						}
					case ImportExceptionHandlingStrategy.MoveToSubFolder:
						{
							// Get a reference to the folder to move the file to.
							var targetDirectoryInfo = new DirectoryInfo(System.IO.Path.Combine(
								fileBeingProcessed.DirectoryName,
								importInstruction.ExceptionSubFolderName));

							// Create it if it does not exist.
							if (false == targetDirectoryInfo.Exists)
								targetDirectoryInfo.Create();

							// Move the file there.
							fileBeingProcessed.MoveTo(
								System.IO.Path.Combine(targetDirectoryInfo.FullName, fileBeingProcessed.Name)
							);
							break;
						}
					default:
						throw new InvalidOperationException(
							$"Unhandled import exception handling strategy: {importInstruction.ImportExceptionHandlingStrategy}");
				}
			}
			catch (Exception e)
			{
				// Report the exception.
				SysUtils.ReportErrorMessageToEventLog(
					"An exception was thrown whilst trying to process the exception handling strategy.",
					e);
			}
		}
	}
}
