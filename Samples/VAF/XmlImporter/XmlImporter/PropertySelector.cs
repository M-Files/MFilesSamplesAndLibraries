using System.Runtime.Serialization;
using MFiles.VAF.Configuration;
using MFilesAPI;

namespace XmlImporter
{
	/// <summary>
	/// Identifies a specific property value to import for an object.
	/// </summary>
	/// <remarks>Extends <see cref="XPathSelector"/>.</remarks>
	[DataContract]
	[JsonConfEditor(NameMember = nameof(PropertySelector.PropertyDef))]
	public class PropertySelector
		: XPathSelector
	{
		/// <summary>
		/// The property definition that this value is for.
		/// </summary>
		[DataMember(Order = 1)]
		[MFPropertyDef]
		public MFIdentifier PropertyDef { get; set; }
	}
}