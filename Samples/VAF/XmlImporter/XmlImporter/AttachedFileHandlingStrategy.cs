using System.ComponentModel;
using System.Runtime.Serialization;

namespace XmlImporter
{
	/// <summary>
	/// The strategy to use when attaching files to object selectors.
	/// </summary>
	public enum AttachedFileHandlingStrategy
	{
		None = 0,

		/// <summary>
		/// The file is attached to the object created by the object selector.
		/// </summary>
		AttachToCurrentObject = 1,

		/// <summary>
		/// The file is attached to a new object, then related to the object created by the object selector.
		/// </summary>
		CreateNewObject = 2
	}
}