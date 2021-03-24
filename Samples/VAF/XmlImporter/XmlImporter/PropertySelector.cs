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

		/// <summary>
		/// Strategy how to search with Lookup, ValueList or Object value 
		/// </summary>
		[DataMember(Order = 2)]
		[JsonConfEditor(Label = "Search type",
			HelpText = "SearchNoWhere=don\'t search\nSearchLookup=use a lookup Name or ID\nSearchValueList=use a valuelist\nSearchObjectValue=use a object value",
			DefaultValue = LookupOrValuelistStrategy.SearchNoWhere)]
		public LookupOrValuelistStrategy LookupOrValuelistStrategy { get; set; }
			= LookupOrValuelistStrategy.SearchNoWhere;

		/// <summary>
		/// Object to search for.
		/// </summary>
		[DataMember(Order = 3)]
		[MFObjType(AllowEmpty = true)]
		[JsonConfEditor(Label = "Object to search",
			IsRequired = false,
			Hidden = true,
			HelpText = "Select Object to search",
			ShowWhen = ".parent._children{.key == 'LookupOrValuelistStrategy' && (.value == 'SearchLookup' || .value == 'SearchObjectValue')  }",
			DefaultValue = null)]
		public MFIdentifier LookupObjectDef { get; set; }

		/// <summary>
		/// ValueList to search for.
		/// </summary>
		[DataMember(Order = 3)]
		[MFValueList(AllowEmpty = true)]
		[JsonConfEditor(Label = "Valuelist",
			IsRequired = false,
			Hidden = true,
			HelpText = "The Valuelist type to search for..",
			ShowWhen = ".parent._children{.key == 'LookupOrValuelistStrategy' && .value == 'SearchValueList' }",
			DefaultValue = null)]
		public MFIdentifier LookupValueListDef { get; set; }

		/// <summary>
		/// If Lookup or MultiSelectlookup use ID or Name search.
		/// </summary>
		[DataMember(Order = 4)]
		[JsonConfEditor(Label = "Use ID to search",
			Hidden = true,
			HelpText = "Yes = Search by ID, No = Search by Name.",
			ShowWhen = ".parent._children{.key == 'LookupOrValuelistStrategy' && .value == 'SearchLookup' }",
			DefaultValue = false)]
		public bool SearchByLookupID { get; set; }
			= false;

		/// <summary>
		/// Property where tp search inside an Object.
		/// </summary>
		[DataMember(Order = 4)]
		[MFPropertyDef(AllowEmpty = true)]
		[JsonConfEditor(Label = "Property to search",
			IsRequired = false,
			Hidden = true,
			HelpText = "Select Property of Object Type to search",
			ShowWhen = ".parent._children{.key == 'LookupOrValuelistStrategy' && .value == 'SearchObjectValue' }",
			DefaultValue = null
			)]
		public MFIdentifier objSearchProperty { get; set; }

	}
}