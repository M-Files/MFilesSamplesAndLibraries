using System;
using System.Linq;
using MFiles.VAF;
using MFiles.VAF.Common;
using MFilesAPI;

namespace CopyingObjects
{
	public class VaultApplication 
		: VaultApplicationBase
	{
		
		/// <summary>
		/// The properties to remove from the cloned property values collection.
		/// </summary>
		public readonly int[] PropertiesToRemove = 
		{
			(int)MFBuiltInPropertyDef.MFBuiltInPropertyDefWorkflow,
			(int)MFBuiltInPropertyDef.MFBuiltInPropertyDefState
		};

		/// <summary>
		/// Executed when an object is moved into a workflow state
		/// with alias "CopyObject".
		/// </summary>
		/// <param name="env">The vault/object environment.</param>
		[StateAction("CopyObject")]
		public void CopyObjectHandler(StateEnvironment env)
		{
			// Sanity.
			if (null == env?.PropertyValues || null == env?.ObjVerEx)
				return;

			// Let's get a copy of the property values.
			var newObjectPropertyValues = this.GetNewObjectPropertyValues(env.PropertyValues);

			// Let's get a copy of the files.
			var newObjectSourceFiles = this.GetNewObjectSourceFiles(env.Vault, env.ObjVer);

			// Is it a single-file-document (has exactly one file, and type of document)?
			var isSingleFileDocument = (newObjectSourceFiles.Count == 1
										&& env.ObjVer.Type == (int) MFBuiltInObjectType.MFBuiltInObjectTypeDocument);

			// Use a default ACL.
			var accessControlList = new AccessControlList();

			// Create the object.
			env.Vault.ObjectOperations.CreateNewObjectExQuick(
				env.ObjVer.Type,
				newObjectPropertyValues,
				newObjectSourceFiles,
				isSingleFileDocument,
				CheckIn: true,
				AccessControlList: accessControlList);

			// Clean up.
			this.ClearTemporaryFiles(newObjectSourceFiles);

		}

		/// <summary>
		/// Downloads the files associated with the supplied <see cref="objVer"/>
		/// and creates a <see cref="SourceObjectFiles"/> to be used in new object creation.
		/// </summary>
		/// <param name="vault">The vault connection used to download the files.</param>
		/// <param name="objVer">The version of the object to download the files from.</param>
		/// <returns>A copy of the current files, as a <see cref="SourceObjectFiles"/>.</returns>
		private SourceObjectFiles GetNewObjectSourceFiles(Vault vault, ObjVer objVer)
		{
			// Sanity.
			if (null == vault)
				throw new ArgumentNullException(nameof(vault));
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Get the files for the current ObjVer.
			var objectFiles = vault.ObjectFileOperations.GetFiles(objVer)
				.Cast<ObjectFile>()
				.ToArray();

			// Create the collection to return.
			var sourceObjectFiles = new SourceObjectFiles();

			// Iterate over the files and download each in turn.
			foreach (var objectFile in objectFiles)
			{

				// Where can we download it?
				var temporaryFilePath = System.IO.Path.Combine(
					System.IO.Path.GetTempPath(), // The temporary file folder.
					System.IO.Path.GetTempFileName() + "." + objectFile.Extension); // The name including extension.

				// Download the file to a temporary location.
				vault.ObjectFileOperations.DownloadFile(objectFile.ID, objectFile.Version, temporaryFilePath);

				// Create an object source file for this temporary file
				// and add it to the collection.
				sourceObjectFiles.Add(-1, new SourceObjectFile()
				{
					Extension = objectFile.Extension,
					SourceFilePath = temporaryFilePath,
					Title = objectFile.Title
				});

			}

			// Return the collection.
			return sourceObjectFiles;
		}

		/// <summary>
		/// Clears up any temporary files used with the creation of an object.
		/// </summary>
		/// <param name="sourceObjectFiles">The files to clear up.</param>
		private void ClearTemporaryFiles(SourceObjectFiles sourceObjectFiles)
		{
			// Sanity.
			if (null == sourceObjectFiles)
				return; // No point throwing; nothing to clear up.

			// Iterate over the files and clear them up.
			foreach (var sourceObjectFile in sourceObjectFiles.Cast<SourceObjectFile>())
			{
				try
				{
					System.IO.File.Delete(sourceObjectFile.SourceFilePath);
				}
				catch(Exception e)
				{
					SysUtils.ReportErrorToEventLog(SysUtils.DefaultEventSourceIdentifier,
						$"Exception removing temporary file from {sourceObjectFile.SourceFilePath}.",
						e);
				}
			}
		}

		/// <summary>
		/// Copies property values from <see cref="cloneFrom"/>, removing items that exist in
		/// <see cref="PropertiesToRemove"/>
		/// </summary>
		/// <param name="cloneFrom">The collection of properties to clone.</param>
		/// <returns>The cloned set of properties, with the requested properties removed.</returns>
		private PropertyValues GetNewObjectPropertyValues(PropertyValues cloneFrom)
		{
			// Sanity.
			if(null == cloneFrom)
				throw new ArgumentNullException(nameof(cloneFrom));

			// Get a basic copy.
			var propertyValues = cloneFrom.Clone();

			// Remove the properties we don't want.
			foreach (var propertyId in this.PropertiesToRemove)
			{
				// If the property is not in the collection then skip.
				int index = propertyValues.IndexOf(propertyId);
				if (-1 == index)
					continue;

				// Remove it.
				propertyValues.Remove(index);
			}

			// Return.
			return propertyValues;
		}

	}
}