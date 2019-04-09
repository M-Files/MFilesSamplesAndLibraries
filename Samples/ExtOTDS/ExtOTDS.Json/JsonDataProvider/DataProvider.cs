using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace JsonDataProvider
{
	/// <summary>
	/// A base implementation of a data provider for a generic type.
	/// </summary>
	/// <typeparam name="T">The type of items provided.</typeparam>
	public class DataProvider<T>
		: IDataProvider<T>
		where T : IEntity
	{
		/// <summary>
		/// The file location to persist the data to.
		/// </summary>
		public FileInfo FileLocation { get; private set; }

		/// <summary>
		/// Used for locking access to the files.
		/// </summary>
		private object _lock = new object();

		public DataProvider(FileInfo fileLocation)
		{
			// Sanity.
			if(null == fileLocation)
				throw new ArgumentNullException(nameof(fileLocation));
			if(false == fileLocation.Exists)
				throw new ArgumentException("The JSON file must exist.", nameof(fileLocation));
			this.FileLocation = fileLocation;
		}

		/// <inheritdoc />
		public IEnumerable<T> GetAll()
		{
			// Open the file for reading.
			using (var streamReader = this.FileLocation.OpenText())
			{
				// Read the content and deserialize.
				return JsonConvert.DeserializeObject<IEnumerable<T>>(streamReader.ReadToEnd());
			}
		}

		/// <inheritdoc />
		public int Insert(T item)
		{
			lock (this._lock)
			{
				// Read all the current data.
				var allItems = this.GetAll()
					.ToList();

				// Get the next Id for the item.
				var id = (allItems.OrderByDescending(i => i.Id)
					          .Select(i => (int?) i.Id)
					          .FirstOrDefault()
				          ?? 0) + 1;

				// Update the item.
				item.Id = id;

				// Add it to the list.
				allItems.Add(item);

				// Save it to the file.
				this.SaveAll(allItems);

				// Return the new id.
				return id;
			}
		}

		/// <inheritdoc />
		public void Update(int itemId, T item)
		{
			lock (this._lock)
			{
				// Read all the current data.
				var allItems = this.GetAll()
					.ToList();

				// Find the item with the id provided.
				var existingItem = allItems
					.FirstOrDefault(i => i.Id == itemId);

				// If we cannot find it then die.
				if (null == existingItem)
					return;

				// Update the collection.
				allItems.Remove(existingItem);
				item.Id = existingItem.Id;
				allItems.Add(item);

				// Save it to the file.
				this.SaveAll(allItems);
			}
		}

		/// <inheritdoc />
		public void Delete(int itemId)
		{
			lock (this._lock)
			{
				// Read all the current data.
				var allItems = this.GetAll()
					.ToList();

				// Remove any items with that id.
				allItems.RemoveAll(i => i.Id == itemId);

				// Save it to the file.
				this.SaveAll(allItems);
			}
		}

		/// <summary>
		/// Saves the items to the file location, overwriting any existing content.
		/// </summary>
		/// <param name="items">The items to save.</param>
		protected void SaveAll(IEnumerable<T> items)
		{
			lock (this._lock)
			{
				// Open the file for writing.
				using (var streamWriter = this.FileLocation.CreateText())
				{
					// Serialise the content.
					var text = null == items
						? string.Empty
						: JsonConvert.SerializeObject(items);

					// Write it to the stream.
					streamWriter.WriteLine(text);
					streamWriter.Close();
				}
			}
		}

	}
}