using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using MFiles.VAF.Common;
using MFilesAPI;

namespace XmlImporter
{
	public partial class VaultApplication
	{
		/// <summary>
		/// Executes an <see cref="ImportInstruction"/>, importing files to the vault as required.
		/// </summary>
		/// <param name="importInstruction">The import instruction to execute.</param>
		public void ImportXmlFile(ImportInstruction importInstruction)
		{
			// Sanity.
			if (null == importInstruction)
				throw new ArgumentNullException(nameof(importInstruction));
			if (null == importInstruction.PathsToSearch)
				return;
			if (string.IsNullOrWhiteSpace(importInstruction.SearchPattern))
				return;

			// Iterate over the configured paths to find appropriate files.
			foreach (var path in importInstruction.PathsToSearch)
			{
				// Attempt to find the path.
				var directoryInfo = new DirectoryInfo(path);
				if (false == directoryInfo.Exists)
					continue;

				// Identify the files by the search pattern.
				foreach (var file in directoryInfo.GetFiles(importInstruction.SearchPattern))
				{
					// Execute the vault extension method (below).
					try
					{
						this.PermanentVault
							.ExtensionMethodOperations
							.ExecuteVaultExtensionMethod("ImportXmlFile", file.FullName);
					}
					catch
					{
						// Allow other files to import.
					}
				}
			}
		}

		/// <summary>
		/// Registers a Vault Extension Method with name "ImportXmlFile".
		/// Users must have at least MFVaultAccess.MFVaultAccessChangeFullControlRole access to execute the method.
		/// </summary>
		/// <param name="env">The vault/object environment.</param>
		/// <returns>The any output from the vault extension method execution.</returns>
		/// <remarks>The input to the vault extension method is available in <see cref="EventHandlerEnvironment.Input"/>.</remarks>
		[VaultExtensionMethod("ImportXmlFile",
			RequiredVaultAccess = MFVaultAccess.MFVaultAccessChangeFullControlRole)]
		private string ImportXmlFile(EventHandlerEnvironment env)
		{
			// Sanity.
			if (null == env)
				throw new ArgumentNullException(nameof(env));
			if (string.IsNullOrWhiteSpace(env.Input))
				throw new ArgumentException("The event handler input was empty and could not be processed.", nameof(env));

			// Check that the input path is valid.
			foreach (var importInstruction in this.Configuration
				.ImportInstructions
				.Where(i => i.Enabled))
			{
				foreach (var path in importInstruction.PathsToSearch)
				{
					// Attempt to find the path.
					var directoryInfo = new DirectoryInfo(path);
					if (false == directoryInfo.Exists)
						continue;

					// Identify the files by the search pattern.
					foreach (var file in directoryInfo.GetFiles(importInstruction.SearchPattern))
					{
						// If the file path matches the one we were told to import then process it, otherwise die.
						if (false == String.Equals(file.FullName, env.Input, StringComparison.CurrentCultureIgnoreCase))
							continue;

						// Import the file.
						try
						{
							// Import the files.
							var attachedFilesToDelete = this.ImportXmlFile(env.Vault, importInstruction, file);

							// Delete the file and any attached files.
							file.Delete();
							foreach (var attachedFile in attachedFilesToDelete.Select(fi => fi.FullName.ToLower()).Distinct())
							{
								System.IO.File.Delete(attachedFile);
							}
						}
						catch (Exception importException)
						{
							// Process the import exception.
							this.HandleImportException(importInstruction, file, importException);

							// Re-throw (force the transaction to roll back).
							throw;
						}
					}
				}
			}

			// It worked.
			return "Success";
		}

		/// <summary>
		/// Imports a specific XML file according to the import instructions.
		/// </summary>
		/// <param name="vault">The vault reference to use for processing the import.</param>
		/// <param name="importInstruction">The import instructions to execute.</param>
		/// <param name="fileToImport">The file to import.</param>
		/// <returns>A list of files which were attached to the object, for deletion.</returns>
		public List<FileInfo> ImportXmlFile(Vault vault, ImportInstruction importInstruction, FileInfo fileToImport)
		{
			// Sanity.
			if (null == vault)
				throw new ArgumentNullException(nameof(vault));
			if (null == importInstruction)
				throw new ArgumentNullException(nameof(importInstruction));
			if (null == fileToImport)
				throw new ArgumentNullException(nameof(fileToImport));

			// Create a list of attached files (which can then be deleted later).
			var attachedFilesToDelete = new List<FileInfo>();

			// Sanity.
			if (null == importInstruction)
				throw new ArgumentNullException(nameof(importInstruction));
			if (null == fileToImport)
				throw new ArgumentNullException(nameof(fileToImport));
			if (false == fileToImport.Exists)
				throw new ArgumentException("The file does not exist on disk.", nameof(fileToImport));
			if (null == importInstruction.ObjectSelectors)
				return attachedFilesToDelete;

			// Load the file contents into an XDocument.
			var xDocument = XDocument.Load(fileToImport.FullName);

			// Iterate over our selectors and find objects to import.
			foreach (var objectSelector in importInstruction.ObjectSelectors)
			{
				// Import the file.
				attachedFilesToDelete.AddRange(this.ImportXmlFile(vault, xDocument.Root, objectSelector, fileToImport));
			}

			// Return the files to delete.
			return attachedFilesToDelete;

		}

		/// <summary>
		/// Executes the <see cref="ObjectSelector"/> rule against the <see cref="XNode"/>,
		/// importing matching objects.
		/// </summary>
		/// <param name="vault">The vault reference to use for processing the import.</param>
		/// <param name="node">The node to import data from.</param>
		/// <param name="objectSelector">The selector to execute.</param>
		/// <param name="xmlFile">The information about the XML file being imported.</param>
		/// <param name="parent">A parent object to create a relationship to, if appropriate.</param>
		/// <param name="xmlNamespaceManager">A namespace manager for using XML prefixes in XPath statements.</param>
		/// <returns>A list of files which were attached to the object, for deletion.</returns>
		public List<FileInfo> ImportXmlFile(
			Vault vault,
			XNode node,
			ObjectSelector objectSelector,
			FileInfo xmlFile = null,
			ObjVer parent = null,
			XmlNamespaceManager xmlNamespaceManager = null)
		{
			// Sanity.
			if (vault == null)
				throw new ArgumentNullException(nameof(vault));
			if (null == node)
				throw new ArgumentNullException(nameof(node));
			if (null == objectSelector)
				throw new ArgumentNullException(nameof(objectSelector));
			if (string.IsNullOrWhiteSpace(objectSelector.XPathQuery))
				throw new ArgumentException("The XPath query for the object was empty", nameof(objectSelector));
			if (null == objectSelector.PropertySelectors)
				throw new ArgumentException("The object selector contained no property selectors.", nameof(objectSelector));
			if (false == objectSelector.ObjectType.IsResolved)
				throw new InvalidOperationException("The object selector object type is not resolved");
			if (false == objectSelector.Class.IsResolved)
				throw new InvalidOperationException("The object selector class is not resolved");

			// Create a list of attached files (which can then be deleted later).
			var attachedFilesToDelete = new List<FileInfo>();

			// Create the namespace manager.
			if (null != xmlNamespaceManager)
			{
				// Copy data from the other manager (so we don't accidentally affect other queries).
				var xmlNamespaceManager2 = new XmlNamespaceManager(new NameTable());
				foreach (string prefix in xmlNamespaceManager)
				{
					// Don't add default.
					if (string.IsNullOrWhiteSpace(prefix))
						continue;
					if (prefix == "xsi")
						continue;
					if (prefix == "xmlns")
						continue;

					// Add.
					xmlNamespaceManager2.AddNamespace(prefix, xmlNamespaceManager.LookupNamespace(prefix));
				}
				xmlNamespaceManager = xmlNamespaceManager2;
			}
			else
			{
				xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			}

			// Populate the namespace manager.
			if (null != objectSelector.XmlNamespaces)
			{
				foreach (var ns in objectSelector.XmlNamespaces)
				{
					// If the namespace manager already contains a prefix then remove it.
					string existingPrefix = xmlNamespaceManager.LookupPrefix(ns.Uri);
					if (false == string.IsNullOrEmpty(existingPrefix))
					{
						xmlNamespaceManager.RemoveNamespace(existingPrefix, ns.Uri);
					}

					xmlNamespaceManager.AddNamespace(ns.Prefix, ns.Uri);
				}
			}

			// Find matching nodes.
			foreach (var matchingElement in node.XPathSelectElements(objectSelector.XPathQuery, xmlNamespaceManager))
			{
				// Hold all the properties being read.
				var propertyValuesBuilder = new MFPropertyValuesBuilder(vault);

				// Add the class property value.
				propertyValuesBuilder.SetClass(objectSelector.Class.ID);

				// Retrieve the properties.
				foreach (var propertySelector in objectSelector.PropertySelectors)
				{
					// Sanity.
					if (string.IsNullOrWhiteSpace(propertySelector.XPathQuery))
						throw new ArgumentException("The object selector contained no property selectors.", nameof(objectSelector));
					if (false == propertySelector.PropertyDef.IsResolved)
						throw new InvalidOperationException("The property value selector property definition is not resolved");

					// Retrieve the element for the property value.
					var matchingPropertyElement = matchingElement
						.XPathSelectElement(propertySelector.XPathQuery, xmlNamespaceManager);
					if (null == matchingPropertyElement)
						continue;

					// Find the property definition type.
					var propertyDefType = vault
						.PropertyDefOperations
						.GetPropertyDef(propertySelector.PropertyDef.ID)
						.DataType;

					// Add the property to the builder.
					propertyValuesBuilder.Add(
						propertySelector.PropertyDef.ID,
						propertyDefType,
						matchingPropertyElement.Value);

				}

				// Set the static values
				foreach (var staticPropertyValue in objectSelector.StaticPropertyValues ?? new List<StaticPropertyValue>())
				{
					// Sanity.
					if (false == staticPropertyValue.PropertyDef.IsResolved)
						throw new InvalidOperationException("The property value selector property definition is not resolved");

					// Find the property definition type.
					var propertyDefType = vault
						.PropertyDefOperations
						.GetPropertyDef(staticPropertyValue.PropertyDef.ID)
						.DataType;

					// Add the property to the builder.
					propertyValuesBuilder.Add(
						staticPropertyValue.PropertyDef.ID,
						propertyDefType,
						staticPropertyValue.Value);

				}


				// Create a reference to the parent?
				if (null != parent)
				{
					// If the property definition to use was configured then use that.
					if (true == objectSelector.ParentRelationshipPropertyDef?.IsResolved)
					{
						// Check that this property is a list and is for the correct object type.
						var parentRelationshipPropertyDef = vault
							.PropertyDefOperations
							.GetPropertyDef(objectSelector.ParentRelationshipPropertyDef.ID);
						if (false == parentRelationshipPropertyDef.BasedOnValueList
							|| parentRelationshipPropertyDef.ValueList != parent.Type)
						{
							throw new InvalidOperationException(
								$"The property def {parentRelationshipPropertyDef.Name} ({parentRelationshipPropertyDef.ID}) is not based on value list {parent.Type}.");
						}

						// Use the configured property definition.
						propertyValuesBuilder.Add(
							parentRelationshipPropertyDef.ID,
							parentRelationshipPropertyDef.DataType,
							parent.ID);

					}
					else
					{

						// Retrieve data about the parent object type.
						var parentObjectType = vault
							.ObjectTypeOperations
							.GetObjectType(parent.Type);

						// Retrieve data about the child object type.
						var childObjectType = vault
							.ObjectTypeOperations
							.GetObjectType(objectSelector.ObjectType.ID);

						// Is there an owner for this child type?
						if (childObjectType.HasOwnerType)
						{
							// Use the "owner" property definition.
							propertyValuesBuilder.Add(
								parentObjectType.OwnerPropertyDef,
								MFDataType.MFDatatypeLookup,
								parent.ID);
						}
						else
						{
							// Use the default property definition.
							propertyValuesBuilder.Add(
								parentObjectType.DefaultPropertyDef,
								MFDataType.MFDatatypeMultiSelectLookup,
								parent.ID);
						}
					}


				}

				// Create a container for any attached files.
				var sourceObjectFiles = new SourceObjectFiles();

				// Should we attach the file to this object?
				if (objectSelector.AttachFileToThisObject)
				{
					// Locate the files to retrieve.
					sourceObjectFiles = this.FindFilesToAttach(objectSelector, xmlFile, matchingElement, xmlNamespaceManager);

					// If we were supposed to attach a file but no files were found then throw an exception.
					if (objectSelector.AttachedFileConfiguration?.FileNotFoundHandlingStrategy == FileNotFoundHandlingStrategy.Fail
						&& 0 == sourceObjectFiles.Count)
					{
						throw new InvalidOperationException("Attached file expected but not found.");
					}

					if (objectSelector.AttachedFileConfiguration?.AttachedFileHandlingStrategy
						== AttachedFileHandlingStrategy.AttachToCurrentObject)
					{
						// Retrieve information about the object type from the vault.
						var objectType = vault
							.ObjectTypeOperations
							.GetObjectType(objectSelector.ObjectType.ID);

						// If the object type cannot have files but we are meant to attach a file, then fail.
						if (false == objectType.CanHaveFiles)
						{
							throw new InvalidOperationException(
								$"The object type {objectType.NameSingular} cannot have files, but the configuration states to attach a file.");
						}
					}
				}

				// Which source object files should we use for the new object?
				var sourceObjectFilesForNewObject = objectSelector.AttachedFileConfiguration?.AttachedFileHandlingStrategy
													== AttachedFileHandlingStrategy.AttachToCurrentObject
					? sourceObjectFiles
					: new SourceObjectFiles();

				// Add the object to the vault.
				var createdObject = vault
					.ObjectOperations
					.CreateNewObjectEx(
						objectSelector.ObjectType.ID,
						propertyValuesBuilder.Values,
						sourceObjectFilesForNewObject,
						SFD: objectSelector.ObjectType.ID == (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument
							&& sourceObjectFilesForNewObject.Count == 1
						);

				// The files which need to be deleted.
				attachedFilesToDelete.AddRange(
					sourceObjectFiles
						.Cast<SourceObjectFile>()
						.Select(sof => new FileInfo(sof.SourceFilePath))
					);

				// Are there any related objects (e.g. children) to create?
				foreach (var childObjectSelector in objectSelector.ChildObjectSelectors)
				{
					attachedFilesToDelete.AddRange(this.ImportXmlFile(vault,
						matchingElement,
						childObjectSelector,
						xmlFile: xmlFile,
						parent: createdObject.ObjVer,
						xmlNamespaceManager: xmlNamespaceManager));
				}

				// Clean up the collections we were using.
				propertyValuesBuilder = new MFPropertyValuesBuilder(vault);

				// Handle creating a new object for the file.
				if (
					objectSelector.AttachFileToThisObject && 
					objectSelector.AttachedFileConfiguration?.AttachedFileHandlingStrategy
					== AttachedFileHandlingStrategy.CreateNewObject)
				{
					// Set the static values
					foreach (var staticPropertyValue in objectSelector.AttachedFileConfiguration?.StaticPropertyValues ?? new List<StaticPropertyValue>())
					{
						// Sanity.
						if (false == staticPropertyValue.PropertyDef.IsResolved)
							throw new InvalidOperationException("The property value selector property definition is not resolved");

						// Find the property definition type.
						var propertyDefType = vault
							.PropertyDefOperations
							.GetPropertyDef(staticPropertyValue.PropertyDef.ID)
							.DataType;

						// Add the property to the builder.
						propertyValuesBuilder.Add(
							staticPropertyValue.PropertyDef.ID,
							propertyDefType,
							staticPropertyValue.Value);

					}

					// Add the class property value.
					propertyValuesBuilder.SetClass(objectSelector.AttachedFileConfiguration.Class.ID);

					// Add a reference from this new object to the one we created earlier.
					{
						// Retrieve data about the parent object type.
						var parentObjectType = vault
							.ObjectTypeOperations
							.GetObjectType(createdObject.ObjVer.Type);

						// Set the relationship.
						propertyValuesBuilder.Add(
							parentObjectType.DefaultPropertyDef,
							MFDataType.MFDatatypeMultiSelectLookup,
							createdObject.ObjVer.ID);
					}

					// Add the object to the vault.
					var createdDocumentObject = vault
						.ObjectOperations
						.CreateNewObjectEx(
							objectSelector.AttachedFileConfiguration.ObjectType.ID,
							propertyValuesBuilder.Values,
							sourceObjectFiles,
							SFD: objectSelector.AttachedFileConfiguration.ObjectType.ID == (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument
								&& sourceObjectFiles.Count == 1
						);
				}

			}

			// Return the files to remove.
			return attachedFilesToDelete;

		}

		/// <summary>
		/// Locates files to attach using the appropriate file location strategy.
		/// </summary>
		/// <param name="objectSelector">The selector being executed.</param>
		/// <param name="xmlFile">The XML file being imported.</param>
		/// <param name="matchingElement">The current node, the context of which will be used to locate files if appropriate.</param>
		/// <param name="xmlNamespaceManager">Manager used to hold any XML namespace prefixes that are in use.</param>
		/// <returns>The files that should be attached to the object (or attached to the new object).</returns>
		public SourceObjectFiles FindFilesToAttach(
			ObjectSelector objectSelector,
			FileInfo xmlFile,
			XNode matchingElement,
			XmlNamespaceManager xmlNamespaceManager
			)
		{
			// Create a collection for the attached files.
			var sourceObjectFiles = new SourceObjectFiles();

			// Identify the file(s) to attach using the FileLocationStrategy.
			switch (objectSelector.AttachedFileConfiguration.FileLocationStrategy)
			{
				// "Invoice1.xml" should use "Invoice1.pdf".
				case FileLocationStrategy.LookForFileWithSameName:
					{
						// Attempt to find the file.
						var attachedFile = new FileInfo(Path.Combine(
							xmlFile.DirectoryName,
							xmlFile.Name.Substring(0, xmlFile.Name.Length - xmlFile.Extension.Length)
							+ objectSelector.AttachedFileConfiguration?.ExpectedFileExtension ?? ".pdf"
						));

						// Attach it if it exists.
						if (attachedFile.Exists)
						{
							sourceObjectFiles.Add(-1, new SourceObjectFile()
							{
								Extension = attachedFile.Extension.Substring(1),
								SourceFilePath = attachedFile.FullName,
								Title = attachedFile.Name.Substring(0, attachedFile.Name.IndexOf(attachedFile.Extension))
							});
						}
					}
					break;

				// Use XPath to find an element or attribute that contains the file name.
				case FileLocationStrategy.UseXPathQueryToLocateFileName:
					{
						// Evaluate the XPath query
						List<string> fileNames = new List<string>();
						var results = matchingElement
							.XPathEvaluate(objectSelector.AttachedFileConfiguration.XPathQueryToLocateFile, xmlNamespaceManager);
						if (results == null)
							break;
						if (results is IEnumerable)
						{
							// Iterate over the items returned.
							foreach (var item in ((IEnumerable)results).Cast<XObject>())
							{
								// If it's an attribute then add the value.
								if (item is XAttribute)
									fileNames.Add(((XAttribute)item).Value);
								else if (item is XElement)
									fileNames.Add(((XElement)item).Value);
								else
									throw new InvalidOperationException(
										$"The XPath expression {objectSelector.AttachedFileConfiguration.XPathQueryToLocateFile} returned something other than an element or attribute.");
							}
						}
						else
						{
							// It is a simple value; retrieve it as a string.
							fileNames.Add(results.ToString());
						}

						// Iterate over the file names that match the XPath query.
						foreach (var fileName in fileNames)
						{
							FileInfo attachedFile = null;
							// Find the file on disk.
							if (System.IO.Path.IsPathRooted(fileName))
							{
								// It is a full path.
								attachedFile = new FileInfo(fileName);
							}
							else
							{
								// It is relative.
								attachedFile = new FileInfo(System.IO.Path.Combine(
									xmlFile.DirectoryName,
									fileName));
							}

							// Attach it if it exists.
							if (attachedFile.Exists)
							{
								sourceObjectFiles.Add(-1, new SourceObjectFile()
								{
									Extension = attachedFile.Extension.Substring(1),
									SourceFilePath = attachedFile.FullName,
									Title = attachedFile.Name.Substring(0, attachedFile.Name.IndexOf(attachedFile.Extension))
								});
							}
						}
					}
					break;
			}

			// Return the source files.
			return sourceObjectFiles;
		}

	}
	
}
