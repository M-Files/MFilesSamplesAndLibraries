using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ComplexConfiguration.Lists
{
	/// <summary>
	/// Examples of how to use lists of basic data types.
	/// </summary>
	[DataContract]
	public class SimpleDataTypes
	{
		/// <summary>
		/// A set of strings that the user can add to, update, and remove from.
		/// </summary>
		[DataMember]
		public List<string> ListOfStrings { get; set; }

			= new List<string>();
		/// <summary>
		/// A set of integers that the user can add to, update, and remove from.
		/// </summary>
		[DataMember]
		public List<int> ListOfIntegers { get; set; }
			= new List<int>();
	}
}
