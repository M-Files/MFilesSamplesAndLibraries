using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace XmlImporter
{
	/// <summary>
	/// An instruction for importing content.
	/// </summary>
	[DataContract]
	public class ImportInstruction
	{
		/// <summary>
		/// Whether this import instruction should be actioned by the code.
		/// </summary>
		[DataMember]
		[JsonConfEditor(DefaultValue = false)]
		public bool Enabled { get; set; }
			= false;

		/// <summary>
		/// A search string (e.g. "*.xml") that defines the files to import.
		/// </summary>
		[DataMember]
		[TextEditor(DefaultValue = "*.xml",
			Label = "Search Pattern")]
		public string SearchPattern { get; set; }
			= "*.xml";

		/// <summary>
		/// A list of paths to search for files.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Search Paths", ChildName = "Path")]
		public List<string> PathsToSearch { get; set; }
			= new List<string>();

		/// <summary>
		/// The selectors used to identify objects in the file.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Object Selectors", ChildName = "Object Selector")]
		public List<ObjectSelector> ObjectSelectors { get; set; }
			= new List<ObjectSelector>();

		/// <summary>
		/// The strategy to use if exceptions are encountered during a file import.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Import Exception Handling Strategy",
			HelpText = "What to do if an exception occurs during the file import.",
			DefaultValue = ImportExceptionHandlingStrategy.MoveToSubFolder)]
		public ImportExceptionHandlingStrategy ImportExceptionHandlingStrategy { get; set; }
			= ImportExceptionHandlingStrategy.MoveToSubFolder;

		/// <summary>
		/// The subfolder that the matching XML file will be moved to if an exception occurs.
		/// </summary>
		/// <remarks>Only used if <see cref="ImportExceptionHandlingStrategy"/> is 'MoveToSubFolder'.</remarks>
		[DataMember]
		[JsonConfEditor(Label = "Exception Subfolder Name",
			Hidden = true,
			HelpText = "The subfolder to move the XML file to if an exception occurs.  This value must be the name of a subfolder, which will be created in the path(s) above as required.",
			ShowWhen = ".parent._children{.key == 'ImportExceptionHandlingStrategy' && .value == 'MoveToSubFolder' }",
			DefaultValue = "Exceptions")]
		public string ExceptionSubFolderName { get; set; }
			= "Exceptions";

		/// <summary>
		/// The folder that the matching XML file will be moved to if an exception occurs.
		/// </summary>
		/// <remarks>Only used if <see cref="ImportExceptionHandlingStrategy"/> is 'MoveToSpecificFolder'.</remarks>
		[DataMember]
		[JsonConfEditor(Label = "Exception Folder Name",
			Hidden = true,
			HelpText = @"The folder to move the XML file to if an exception occurs.  This value must be a full, valid folder path (e.g. 'C:\temp\exceptions\') which XML files will be moved into as required.",
			ShowWhen = ".parent._children{.key == 'ImportExceptionHandlingStrategy' && .value == 'MoveToSpecificFolder' }",
			DefaultValue = @"C:\temp\exceptions\")]
		public string ExceptionFolderName { get; set; }
			= @"C:\temp\exceptions\";
	}
}