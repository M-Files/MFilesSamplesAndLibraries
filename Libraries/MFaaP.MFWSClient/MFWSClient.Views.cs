using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFaaP.MFWSClient.ExtensionMethods;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{
		/// <summary>
		/// Gets the contents of the root ("home") view.
		/// </summary>
		/// <returns>The contents of the view.</returns>
		/// <remarks>Identical to calling <see cref="GetViewContents"/> with no parameters.</remarks>
		public Task<FolderContentItems> GetRootViewContents()
		{
			// Get the root view contents.
			return this.GetViewContents();
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see cref="items"/>.
		/// </summary>
		/// <param name="items">A collection representing the view depth.
		/// Should contain zero or more <see cref="FolderContentItem"/>s representing the view being shown,
		/// then zero or more <see cref="FolderContentItem"/>s representing the appropriate property groups being shown.</param>
		/// <returns>The contents of the view.</returns>
		public async Task<FolderContentItems> GetViewContents(params FolderContentItem[] items)
		{
			// Create the request.
			var request = new RestRequest($"/REST/views/{items.GetPath()}items");

			// Make the request and get the response.
			var response = await this.Get<FolderContentItems>(request);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets the favourited items.
		/// </summary>
		/// <returns>The favorited items.</returns>
		public async Task<List<ObjectVersion>> GetFavorites()
		{
			// Create the request.
			var request = new RestRequest($"/REST/favorites");

			// Make the request and get the response.
			var response = await this.Get<List<ObjectVersion>>(request);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Adds the supplied item to the favorites.
		/// </summary>
		/// <returns>The item that was added.</returns>
		public async Task<ExtendedObjectVersion> AddToFavorites(ObjID objId)
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Create the request.
			var request = new RestRequest($"/REST/favorites");
			request.AddJsonBody(objId);

			// Make the request and get the response.
			var response = await this.Post<ExtendedObjectVersion>(request);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Adds the supplied item to the favorites.
		/// </summary>
		/// <returns>The item that was added.</returns>
		public Task<ExtendedObjectVersion> AddToFavorites(int objectTypeId, int objectId)
		{
			return this.AddToFavorites(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId
			});
		}

		/// <summary>
		/// Removes the supplied item to the favorites.
		/// </summary>
		/// <returns>The item that was removed.</returns>
		public async Task<ExtendedObjectVersion> RemoveFromFavorites(ObjID objId)
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Create the request.
			var request = new RestRequest($"/REST/favorites/{objId.Type}/{objId.ID}");

			// Make the request and get the response.
			var response = await this.Delete<ExtendedObjectVersion>(request);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Removes the supplied item to the favorites.
		/// </summary>
		/// <returns>The item that was removed.</returns>
		public Task<ExtendedObjectVersion> RemoveFromFavorites(int objectTypeId, int objectId)
		{
			return this.RemoveFromFavorites(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId
			});
		}

	}
}
