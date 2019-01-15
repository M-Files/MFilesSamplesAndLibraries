using System.Collections.Generic;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace XmlImporter
{
	/// <summary>
	/// Identifies data to be imported to objects within the vault.
	/// </summary>
	/// <remarks>Extends <see cref="XPathSelector"/>.</remarks>
	[DataContract]
	[JsonConfEditor(
		NameMember = nameof(ObjectSelector.ObjectType))]
	public class ObjectSelector
		: XPathSelector
	{
		/// <summary>
		/// The XML namespace prefix/Uri combinations used to resolve XML element namespaces
		/// in XPath queries.
		/// </summary>
		[DataMember(Order = 1)]
		[JsonConfEditor(Label = "XML Namespaces",
			ChildName = "XML Namespace",
			HelpText = "Populate these if your incoming XML file uses namespaces.  These namespace prefixes can then be used when writing XPath queries.  Note that these namespaces will be available both in the query for this object selector, but also its properties and (unless overridden) for its children.")]
		public List<XmlNamespace> XmlNamespaces { get; set; }
			= new List<XmlNamespace>();

		/// <summary>
		/// The type of object to create.
		/// </summary>
		[DataMember(Order = 2)]
		[MFObjType]
		[JsonConfEditor(Label = "Object Type",
			HelpText = "The object type to create when elements matching the XPath Query are found.")]
		public MFIdentifier ObjectType { get; set; }

		/// <summary>
		/// The class of the object to create.
		/// </summary>
		[DataMember(Order = 3)]
		[MFClass]
		[JsonConfEditor(
			HelpText = "The class of object to use when creating objects.")]
		public MFIdentifier Class { get; set; }

		/// <summary>
		/// A list of static values to be applied to the new object.
		/// </summary>
		[DataMember(Order = 4)]
		[JsonConfEditor(Label = "Static Property Values",
			ChildName = "Static Property Value",
			HelpText = "These property values are hard-coded and not retrieved from the XML file.")]
		public List<StaticPropertyValue> StaticPropertyValues { get; set; }
			= new List<StaticPropertyValue>();

		/// <summary>
		/// A list of selectors for property values of the current object.
		/// </summary>
		[DataMember(Order = 5)]
		[JsonConfEditor(Label = "Property Value Selectors",
			ChildName = "Property Value Selector",
			HelpText = "These property values are retrieved from the XML file itself.")]
		public List<PropertySelector> PropertySelectors { get; set; }
			= new List<PropertySelector>();

		/// <summary>
		/// A list of selectors for related (child/sub/etc.) objects under this one.
		/// </summary>
		[DataMember(Order = 6)]
		[JsonConfEditor(Label = "Child Objects",
			ChildName = "Object Selector")]
		public List<ObjectSelector> ChildObjectSelectors { get; set; }
			= new List<ObjectSelector>();

		/// <summary>
		/// The property to use for relating this object to the parent.
		/// </summary>
		/// <remarks>Only used when creating child/sub objects.</remarks>
		[DataMember(Order = 7)]
		[MFPropertyDef(AllowEmpty = true)]
		[JsonConfEditor(IsRequired = false,
			DefaultValue = null,
			Label = "Parent Relationship Property Definition",
			HelpText =
				"If set, this property will be used for the relationship from this object to its parent.  If it is left empty then the system will use either the 'Owner' or default property definitions, as appropriate.")]
		public MFIdentifier ParentRelationshipPropertyDef { get; set; }

		/// <summary>
		/// Whether to attach the associated file to this object.
		/// </summary>
		[DataMember(Order = 8)]
		[JsonConfEditor(Label = "Attach file to this object?")]
		public bool AttachFileToThisObject { get; set; }

		/// <summary>
		/// The configuration for attaching files to this object.
		/// </summary>
		[DataMember(Order = 8)]
		[JsonConfEditor(Label = "Configuration for attaching files",
			Hidden = true,
			ShowWhen = ".parent._children{.key == 'AttachFileToThisObject' && .value == true }"
		)]
		public AttachedFileConfiguration AttachedFileConfiguration { get; set; }
			= new AttachedFileConfiguration();
	}
}