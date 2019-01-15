namespace XmlImporter
{
	/// <summary>
	/// The strategy to use when expecting to attach a file, but the file not being found.
	/// </summary>
	public enum FileNotFoundHandlingStrategy
	{
		/// <summary>
		/// An exception is thrown and the file is not imported.
		/// </summary>
		Fail = 0,

		/// <summary>
		/// The fact that the file is missing is ignored and the new object creation skipped.
		/// </summary>
		Ignore = 1
	}
}