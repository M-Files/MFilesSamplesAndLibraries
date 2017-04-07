using System;
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
		/// <returns></returns>
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

	}
}
