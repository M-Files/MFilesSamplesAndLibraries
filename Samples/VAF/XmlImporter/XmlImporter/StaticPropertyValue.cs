using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace XmlImporter
{
	/// <summary>
	/// A static property value to be set on new objects.
	/// </summary>
	[DataContract]
	[JsonConfEditor(NameMember = nameof(StaticPropertyValue.PropertyDef))]
	public class StaticPropertyValue
	{
		/// <summary>
		/// The property definition that this value is for.
		/// </summary>
		[DataMember]
		[MFPropertyDef]
		public MFIdentifier PropertyDef { get; set; }

		/// <summary>
		/// The value for the property value.
		/// </summary>
		[DataMember]
		[TextEditor]
		public object Value { get; set; }
	}
}