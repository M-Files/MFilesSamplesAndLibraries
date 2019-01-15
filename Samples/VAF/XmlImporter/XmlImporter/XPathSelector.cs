using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace XmlImporter
{
	/// <summary>
	/// An abstract class for selecting data via an XPath query.
	/// </summary>
	[DataContract]
	public abstract class XPathSelector
	{
		/// <summary>
		/// The XPath query that selects the data.
		/// </summary>
		[DataMember(Order = 0)]
		[TextEditor(Label = "XPath Query")]
		public string XPathQuery { get; set; }

	}
}