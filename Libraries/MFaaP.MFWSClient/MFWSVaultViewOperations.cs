using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MFaaP.MFWSClient.ExtensionMethods;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public class MFWSVaultViewOperations
		: MFWSVaultOperationsBase
	{

		/// <inheritdoc />
		public MFWSVaultViewOperations(MFWSClientBase client)
			: base(client)
		{
		}

		/// <summary>
		/// Gets the contents of the root ("home") view.
		/// </summary>
		/// <returns>The contents of the view.</returns>
		/// <param name="token">A cancellation token for the request.</param>
		/// <remarks>Identical to calling <see cref="GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/> with no parameters.</remarks>
		public Task<FolderContentItems> GetRootFolderContents(CancellationToken token = default(CancellationToken))
		{
			// Get the root view contents.
			return this.GetFolderContents(token);
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see cref="items"/>.
		/// </summary>
		/// <param name="items">A collection representing the view depth.
		/// Should contain zero or more <see cref="FolderContentItem"/>s representing the view being shown,
		/// then zero or more <see cref="FolderContentItem"/>s representing the appropriate property groups being shown.</param>
		/// <returns>The contents of the view.</returns>
		public Task<FolderContentItems> GetFolderContents(params FolderContentItem[] items)
		{
			return this.GetFolderContents(CancellationToken.None, items);
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see cref="items"/>.
		/// </summary>
		/// <param name="items">A collection representing the view depth.
		/// Should contain zero or more <see cref="FolderContentItem"/>s representing the view being shown,
		/// then zero or more <see cref="FolderContentItem"/>s representing the appropriate property groups being shown.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The contents of the view.</returns>
		public async Task<FolderContentItems> GetFolderContents(CancellationToken token, params FolderContentItem[] items)
		{
			// Create the request.
			var request = new RestRequest($"/REST/views/{items.GetPath()}items");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<FolderContentItems>(request, token);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets the favourited items.
		/// </summary>
		/// <param name="builtInView">An enumeration of the built-in view to retrieve.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The favorited items.</returns>
		public async Task<List<FolderContentItems>> GetFolderContents(MFBuiltInView builtInView, CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/views/v{(int)builtInView}/items");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<FolderContentItems>>(request, token);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Based on the M-Files API.
		/// </summary>
		public enum MFBuiltInView
		{
			/// <summary>
			/// Checked-out-to-me view.
			/// </summary>
			MFBuiltInViewCheckedOutToCurrentUser = 5,

			/// <summary>
			/// Recently-modified-by-me view.
			/// </summary>
			MFBuiltInViewRecentlyModifiedByMe = 7,

			/// <summary>
			/// Templates view.
			/// </summary>
			MFBuiltInViewTemplates = 8,

			/// <summary>
			/// Assigned-to-me view.
			/// </summary>
			MFBuiltInViewAssignedToMe = 9,

			/// <summary>
			/// Latest-searches view container.
			/// </summary>
			MFBuiltInViewLatestSearches = 11,

			/// <summary>
			/// Recently Accessed by Me view.
			/// </summary>
			MFBuiltInViewRecentlyAccessedByMe = 14,

			/// <summary>
			/// Favorites view.
			/// </summary>
			MFBuiltInViewFavorites = 15
		}
	}
}
