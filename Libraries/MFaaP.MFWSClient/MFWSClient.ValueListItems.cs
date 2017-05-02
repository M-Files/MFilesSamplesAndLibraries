using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{
		/// <summary>
		/// Gets the contents of the value list with Id <see cref="valueListId"/>.
		/// </summary>
		/// <param name="valueListId">The Id of the value list to return the items from.</param>
		/// <param name="nameFilter">If has a value, is used to filter the items by name.</param>
		/// <returns>The contents of the value list.</returns>
		/// <remarks>Note that this may be limited.</remarks>
		public async Task<Results<ValueListItem>> GetValueListItems(int valueListId, string nameFilter = null)
		{
			// Create the request.
			var request = new RestRequest($"/REST/valuelists/{valueListId}/items");

			// Filter by name?
			if (false == string.IsNullOrWhiteSpace(nameFilter))
			{
				request.Resource += "?filter=" + WebUtility.UrlEncode(nameFilter);
			}

			// Make the request and get the response.
			var response = await this.Get<Results<ValueListItem>>(request);

			// Return the data.
			return response.Data;
		}
	}
}
