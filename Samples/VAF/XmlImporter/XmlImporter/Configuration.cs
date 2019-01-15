using System.Collections.Generic;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace XmlImporter
{
	/// <summary>
	/// The configuration of the Xml importer.
	/// </summary>
	[DataContract]
	public class Configuration
	{

		/// <summary>
		/// Instructions for importing.
		/// Each import instruction is a definition of what to import and can
		/// import from multiple locations.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Import Instructions", ChildName = "Import Instruction")]
		public List<ImportInstruction> ImportInstructions { get; set; }
			= new List<ImportInstruction>();

	}
}