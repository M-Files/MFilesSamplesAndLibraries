using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace XmlImporter
{
	/// <summary>
	/// A prefix/URI combination,
	/// used in combination with an <see cref="System.Xml.XmlNamespaceManager"/>
	/// when querying data in XML files containing XML namespaces.
	/// </summary>
	[DataContract]
	public class XmlNamespace
	{
		/// <summary>
		/// The prefix to be used in the XPath query.
		/// </summary>
		/// <remarks>Does not need to be the same prefix as the document, but recommended for readability.</remarks>
		[DataMember]
		public string Prefix { get; set; }

		/// <summary>
		/// The Uri that this prefix represents.
		/// </summary>
		[DataMember]
		public string Uri { get; set; }
	}
}