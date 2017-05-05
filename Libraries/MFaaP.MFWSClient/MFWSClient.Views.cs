using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
		/// <param name="token">A cancellation token for the request.</param>
		/// <remarks>Identical to calling <see cref="GetViewContents"/> with no parameters.</remarks>
		public Task<FolderContentItems> GetRootViewContents(CancellationToken token = default(CancellationToken))
		{
			// Get the root view contents.
			return this.GetViewContents(token);
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see cref="items"/>.
		/// </summary>
		/// <param name="items">A collection representing the view depth.
		/// Should contain zero or more <see cref="FolderContentItem"/>s representing the view being shown,
		/// then zero or more <see cref="FolderContentItem"/>s representing the appropriate property groups being shown.</param>
		/// <returns>The contents of the view.</returns>
		public Task<FolderContentItems> GetViewContents(params FolderContentItem[] items)
		{
			return this.GetViewContents(CancellationToken.None, items);
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see cref="items"/>.
		/// </summary>
		/// <param name="items">A collection representing the view depth.
		/// Should contain zero or more <see cref="FolderContentItem"/>s representing the view being shown,
		/// then zero or more <see cref="FolderContentItem"/>s representing the appropriate property groups being shown.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The contents of the view.</returns>
		public async Task<FolderContentItems> GetViewContents(CancellationToken token, params FolderContentItem[] items)
		{
			// Create the request.
			var request = new RestRequest($"/REST/views/{items.GetPath()}items");

			// Make the request and get the response.
			var response = await this.Get<FolderContentItems>(request, token);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets the favourited items.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The favorited items.</returns>
		public async Task<List<ObjectVersion>> GetFavorites(CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest("/REST/favorites");

			// Make the request and get the response.
			var response = await this.Get<List<ObjectVersion>>(request, token);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Adds the supplied item to the favorites.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The item that was added.</returns>
		public async Task<ExtendedObjectVersion> AddToFavorites(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Create the request.
			var request = new RestRequest("/REST/favorites");
			request.AddJsonBody(objId);

			// Make the request and get the response.
			var response = await this.Post<ExtendedObjectVersion>(request, token);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Adds the supplied item to the favorites.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The item that was added.</returns>
		public Task<ExtendedObjectVersion> AddToFavorites(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			return this.AddToFavorites(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId
			}, token);
		}

		/// <summary>
		/// Removes the supplied item to the favorites.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The item that was removed.</returns>
		public async Task<ExtendedObjectVersion> RemoveFromFavorites(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Create the request.
			var request = new RestRequest($"/REST/favorites/{objId.Type}/{objId.ID}");

			// Make the request and get the response.
			var response = await this.Delete<ExtendedObjectVersion>(request, token);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Removes the supplied item to the favorites.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The item that was removed.</returns>
		public Task<ExtendedObjectVersion> RemoveFromFavorites(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			return this.RemoveFromFavorites(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId
			}, token);
		}

	}
}
