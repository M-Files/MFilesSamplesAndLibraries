namespace XmlImporter
{
	/// <summary>
	/// The strategy to use for exceptions during file import.
	/// </summary>
	public enum ImportExceptionHandlingStrategy
	{
		/// <summary>
		/// Items will be moved to a subfolder in the path in which they were found.
		/// </summary>
		MoveToSubFolder = 0,

		/// <summary>
		/// Items will be deleted.
		/// </summary>
		Delete = 1,

		/// <summary>
		/// Items will be moved to a specific folder.
		/// </summary>
		MoveToSpecificFolder = 2

		// TODO: CreateMFilesObject = 3
	}
}