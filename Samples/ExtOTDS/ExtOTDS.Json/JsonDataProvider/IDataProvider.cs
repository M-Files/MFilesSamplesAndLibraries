using System.Collections.Generic;

namespace JsonDataProvider
{
	/// <summary>
	/// Sample data provider interface for a generic type.
	/// </summary>
	/// <typeparam name="T">The type of items provided.</typeparam>
	public interface IDataProvider<T>
		where T : IEntity
	{
		/// <summary>
		/// Returns all items in the data provider.
		/// </summary>
		/// <returns></returns>
		IEnumerable<T> GetAll();

		/// <summary>
		/// Inserts a new item.
		/// </summary>
		/// <param name="item">The item to insert.</param>
		/// <returns>The Id of the item.</returns>
		int Insert(T item);

		/// <summary>
		/// Deletes the item with the provided Id.
		/// </summary>
		/// <param name="itemId">The Id of the item to delete.</param>
		/// <remarks>Does not throw an exception if the itemId cannot be found.</remarks>
		void Delete(int itemId);

		/// <summary>
		/// Updates an existing item in the system.
		/// </summary>
		/// <param name="itemId">The Id of the item to update.</param>
		/// <param name="item">The new item details (note that the item id will be overwritten).</param>
		/// <remarks>Does not throw an exception if the itemId cannot be found.</remarks>
		void Update(int itemId, T item);
	}
}
