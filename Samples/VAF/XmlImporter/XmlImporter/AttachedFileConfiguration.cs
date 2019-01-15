using System.Collections.Generic;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace XmlImporter
{
	/// <summary>
	/// Configuration for attaching files to this object.
	/// </summary>
	[DataContract]
	public class AttachedFileConfiguration
	{
		/// <summary>
		/// The strategy to use if the attached file could not be found.
		/// If set to Ignore then the system will skip adding the file.
		/// If set to Fail then the system will throw an exception whilst adding the file.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "File not found handling strategy", HelpText = "What to do if the expected attached file is not found.")]
		public FileNotFoundHandlingStrategy FileNotFoundHandlingStrategy { get; set; }
			= FileNotFoundHandlingStrategy.Ignore;

		/// <summary>
		/// The strategy to use when attaching the file; which object should it be attached to?
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Attached file handling strategy")]
		public AttachedFileHandlingStrategy AttachedFileHandlingStrategy { get; set; }
			= AttachedFileHandlingStrategy.AttachToCurrentObject;

		/// <summary>
		/// The object type of the created object.
		/// </summary>
		/// <remarks>Only used if <see cref="AttachedFileHandlingStrategy"/> is CreateNewObject.</remarks>
		[DataMember]
		[MFObjType]
		[JsonConfEditor(Hidden = true,
			ShowWhen = ".parent._children{.key == 'AttachedFileHandlingStrategy' && .value == 'CreateNewObject' }")]
		public MFIdentifier ObjectType { get; set; }

		/// <summary>
		/// The class type of the created object.
		/// </summary>
		/// <remarks>Only used if <see cref="AttachedFileHandlingStrategy"/> is CreateNewObject.</remarks>
		[DataMember]
		[MFClass]
		[JsonConfEditor(Hidden = true,
			ShowWhen = ".parent._children{.key == 'AttachedFileHandlingStrategy' && .value == 'CreateNewObject' }")]
		public MFIdentifier Class { get; set; }

		/// <summary>
		/// A list of static values to be applied to the new object.
		/// </summary>
		/// <remarks>Only used if <see cref="AttachedFileHandlingStrategy"/> is CreateNewObject.</remarks>
		[DataMember]
		[JsonConfEditor(Hidden = true,
			ShowWhen = ".parent._children{.key == 'AttachedFileHandlingStrategy' && .value == 'CreateNewObject' }")]
		public List<StaticPropertyValue> StaticPropertyValues { get; set; }
			= new List<StaticPropertyValue>();

		[DataMember]
		[JsonConfEditor(DefaultValue = XmlImporter.FileLocationStrategy.LookForFileWithSameName,
			Label = "File Location Strategy")]
		public FileLocationStrategy FileLocationStrategy { get; set; }
			= FileLocationStrategy.LookForFileWithSameName;

		/// <summary>
		/// The expected file extension of the attached file.  Defaults to ".pdf".
		/// File name is expected to be the same as the XML file.
		/// </summary>
		/// <remarks>Only used if <see cref="FileLocationStrategy"/> is LookForFileWithSameName.</remarks>
		[DataMember]
		[TextEditor(DefaultValue = ".pdf",
			Hidden = false,
			HideWhen = ".parent._children{.key == 'FileLocationStrategy' && .value != 'LookForFileWithSameName' }")]
		public string ExpectedFileExtension { get; set; }
			= ".pdf";

		/// <summary>
		/// The XPath query to use to identify the file name.
		/// </summary>
		/// <remarks>Only used if <see cref="FileLocationStrategy"/> is UseXPathQueryToLocateFileName.</remarks>
		[DataMember]
		[TextEditor(Label = "XPath query to locate the file name (from this object)",
			Hidden = true,
			ShowWhen = ".parent._children{.key == 'FileLocationStrategy' && .value == 'UseXPathQueryToLocateFileName' }")]
		public string XPathQueryToLocateFile { get; set; }

	}
}