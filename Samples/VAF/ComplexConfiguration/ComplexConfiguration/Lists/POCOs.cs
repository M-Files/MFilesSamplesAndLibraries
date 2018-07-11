using System.Collections.Generic;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;
// ReSharper disable InconsistentNaming

namespace ComplexConfiguration.Lists
{
	/// <summary>
	/// Examples of how to use POCOs (Plain Old CLR Objects)
	/// in lists.
	/// </summary>
	[DataContract]
	public class POCOs
	{
		/// <summary>
		/// A set of POCO objects that the user can add to, update, and remove from.
		/// </summary>
		[DataMember]
		public List<MyObject> SimplePOCOs { get; set; }

		/// <summary>
		/// A set of classes that the user can add to, update, and remove from.
		/// </summary>
		[DataMember]
		[JsonConfEditor(HelpText = "As this POCO object contains a Name property, it will be used in the configuration screen, instead of 'MyObjectWithName[1]'")]
		public List<MyObjectWithName> SimplePOCOsWithNameProperty { get; set; }

		/// <summary>
		/// A set of classes that the user can add to, update, and remove from.
		/// </summary>
		[DataMember]
		[JsonConfEditor(HelpText = "As this POCO object contains a Value1 property which has been marked as the name property. It will be used in the configuration screen, instead of 'MyObjectWithCustomName[1]'")]
		public List<MyObjectWithCustomName> SimplePOCOsWithCustomNameProperty { get; set; }
	}

	[DataContract]
	public class MyObject
	{
		[DataMember]
		public string Value1 { get; set; }
	}

	[DataContract]
	public class MyObjectWithName
	{
		[DataMember]
		public string Name { get; set; }
	}

	[DataContract]
	[JsonConfEditor(NameMember = "Value1")]
	public class MyObjectWithCustomName
	{
		[DataMember]
		public string Value1 { get; set; }
	}
}
