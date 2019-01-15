namespace XmlImporter
{
	/// <summary>
	/// Strategy to use to locate the PDF (or other) file to attach.
	/// </summary>
	public enum FileLocationStrategy
	{
		/// <summary>
		/// Look for a file with the same name as the current XML file (e.g. "Invoice0001.xml" would look for "Invoice0001.pdf").
		/// </summary>
		LookForFileWithSameName = 0,

		/// <summary>
		/// Use an XPath query to identify the file to attach.
		/// </summary>
		UseXPathQueryToLocateFileName = 1
	}
}