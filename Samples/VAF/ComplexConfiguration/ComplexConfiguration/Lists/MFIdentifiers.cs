using System.Collections.Generic;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.Lists
{
	/// <summary>
	/// Examples of how to use lists of vault structural references.
	/// </summary>
	[DataContract]
	// ReSharper disable once InconsistentNaming
	public class MFIdentifiers
	{
		/// <summary>
		/// A set of classes that the user can add to, update, and remove from.
		/// </summary>
		[DataMember]
		[MFClass]
		public List<MFIdentifier> ListOfClasses { get; set; }

		/// <summary>
		/// A set of property definitions that the user can add to, update, and remove from.
		/// </summary>
		[DataMember]
		[MFPropertyDef]
		public List<MFIdentifier> ListOfPropertyDefinitions { get; set; }
	}
}
