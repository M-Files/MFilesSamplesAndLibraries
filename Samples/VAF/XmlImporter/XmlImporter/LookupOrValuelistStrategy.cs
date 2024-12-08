namespace XmlImporter
{
	/// <summary>
	/// The type to use a lookup filed.
	/// </summary>
	public enum LookupOrValuelistStrategy
	{
		/// <summary>
		/// Don't search for something
		/// </summary>
		SearchNoWhere = 0,

		/// <summary>
		/// Search for an object item.
		/// </summary>
		SearchLookup = 1,

		/// <summary>
		/// Search for an valuelist item.
		/// </summary>
		SearchValueList = 2,

		/// <summary>
		/// Search for an value inside an object.
		/// </summary>
		SearchObjectValue = 3
	}
}
