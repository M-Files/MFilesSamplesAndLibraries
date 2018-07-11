using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.Lists
{
	/// <summary>
	/// Examples of using lists in the configuration area.
	/// </summary>
	[DataContract]
	public class Lists
	{
		/// <summary>
		/// Example of using lists of simple data types.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Lists of simple data types")]
		public SimpleDataTypes SimpleDataTypes { get; set; }
			= new SimpleDataTypes();

		/// <summary>
		/// Example of using lists of vault structural references.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Lists of vault structural references")]
		// ReSharper disable once InconsistentNaming
		public MFIdentifiers MFIdentifiers { get; set; }
			= new MFIdentifiers();

		/// <summary>
		/// Example of using lists of vault structural references.
		/// </summary>
		[DataMember]
		[JsonConfEditor(Label = "Plain Old CLR Objects")]
		// ReSharper disable once InconsistentNaming
		public POCOs POCOs { get; set; }
			= new POCOs();
	}
}
