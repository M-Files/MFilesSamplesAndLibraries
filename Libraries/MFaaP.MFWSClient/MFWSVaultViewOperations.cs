using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MFaaP.MFWSClient.ExtensionMethods;
using RestSharp;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// View operations.
	/// </summary>
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
		/// <remarks>Identical to calling <see cref="GetFolderContentsAsync(MFaaP.MFWSClient.FolderContentItem[])"/> with no parameters.</remarks>
		public Task<FolderContentItems> GetRootFolderContentsAsync(CancellationToken token = default(CancellationToken))
		{
			// Get the root view contents.
			return this.GetFolderContentsAsync(token);
		}

		/// <summary>
		/// Gets the contents of the root ("home") view.
		/// </summary>
		/// <returns>The contents of the view.</returns>
		/// <param name="token">A cancellation token for the request.</param>
		/// <remarks>Identical to calling <see cref="GetFolderContents(MFaaP.MFWSClient.FolderContentItem[])"/> with no parameters.</remarks>
		public FolderContentItems GetRootFolderContents(CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetRootFolderContentsAsync(token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see paramref="items"/>.
		/// </summary>
		/// <param name="items">A collection representing the view depth.
		/// Should contain zero or more <see cref="FolderContentItem"/>s representing the view being shown,
		/// then zero or more <see cref="FolderContentItem"/>s representing the appropriate property groups being shown.</param>
		/// <returns>The contents of the view.</returns>
		public Task<FolderContentItems> GetFolderContentsAsync(params FolderContentItem[] items)
		{
			return this.GetFolderContentsAsync(CancellationToken.None, items);
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see paramref="items"/>.
		/// </summary>
		/// <param name="items">A collection representing the view depth.
		/// Should contain zero or more <see cref="FolderContentItem"/>s representing the view being shown,
		/// then zero or more <see cref="FolderContentItem"/>s representing the appropriate property groups being shown.</param>
		/// <returns>The contents of the view.</returns>
		public FolderContentItems GetFolderContents(params FolderContentItem[] items)
		{
			// Execute the async method.
			return this.GetFolderContentsAsync(items)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see paramref="path"/>.
		/// </summary>
		/// <param name="path">A view path, formatted as per http://www.m-files.com/mfws/syntax.html#sect:viewpath. </param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The contents of the view.</returns>
		public async Task<FolderContentItems> GetFolderContentsAsync(CancellationToken token, string path)
		{
			// Sanity.
			if (false == string.IsNullOrWhiteSpace(path))
			{
				// If the path is not blank then it must end with a slash.
				if (false == path.EndsWith("/"))
					path += "/";

				// It cannot start with a slash.
				if (path.StartsWith("/"))
					path = path.Substring(1);
			}

			// Create the request.
			var request = new RestRequest($"/REST/views/{path}items");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<FolderContentItems>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see paramref="path"/>.
		/// </summary>
		/// <param name="path">A view path, formatted as per http://www.m-files.com/mfws/syntax.html#sect:viewpath. </param>
		/// <returns>The contents of the view.</returns>
		public Task<FolderContentItems> GetFolderContentsAsync(string path)
		{
			// Execute the async method.
			return this.GetFolderContentsAsync(CancellationToken.None, path);
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see paramref="path"/>.
		/// </summary>
		/// <param name="path">A view path, formatted as per http://www.m-files.com/mfws/syntax.html#sect:viewpath. </param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The contents of the view.</returns>
		public FolderContentItems GetFolderContents(CancellationToken token, string path)
		{
			// Execute the async method.
			return this.GetFolderContentsAsync(token, path)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see paramref="path"/>.
		/// </summary>
		/// <param name="path">A view path, formatted as per http://www.m-files.com/mfws/syntax.html#sect:viewpath. </param>
		/// <returns>The contents of the view.</returns>
		public FolderContentItems GetFolderContents(string path)
		{
			// Execute the async method.
			return this.GetFolderContents(CancellationToken.None, path);
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see paramref="items"/>.
		/// </summary>
		/// <param name="items">A collection representing the view depth.
		/// Should contain zero or more <see cref="FolderContentItem"/>s representing the view being shown,
		/// then zero or more <see cref="FolderContentItem"/>s representing the appropriate property groups being shown.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The contents of the view.</returns>
		public Task<FolderContentItems> GetFolderContentsAsync(CancellationToken token, params FolderContentItem[] items)
		{
			return this.GetFolderContentsAsync(token, items.GetPath());
		}

		/// <summary>
		/// Gets the contents of the view specified by the <see paramref="items"/>.
		/// </summary>
		/// <param name="items">A collection representing the view depth.
		/// Should contain zero or more <see cref="FolderContentItem"/>s representing the view being shown,
		/// then zero or more <see cref="FolderContentItem"/>s representing the appropriate property groups being shown.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The contents of the view.</returns>
		public FolderContentItems GetFolderContents(CancellationToken token, params FolderContentItem[] items)
		{
			// Execute the async method.
			return this.GetFolderContentsAsync(token, items)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Gets the contents of a built-in view.
		/// </summary>
		/// <param name="builtInView">An enumeration of the built-in view to retrieve.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The favorited items.</returns>
		public async Task<List<FolderContentItems>> GetFolderContentsAsync(MFBuiltInView builtInView, CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/views/v{(int)builtInView}/items");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<FolderContentItems>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets the contents of a built-in view.
		/// </summary>
		/// <param name="builtInView">An enumeration of the built-in view to retrieve.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The favorited items.</returns>
		public List<FolderContentItems> GetFolderContents(MFBuiltInView builtInView, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetFolderContentsAsync(builtInView, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}
	}
}
