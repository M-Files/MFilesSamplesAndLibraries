using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{

		/// <summary>
		/// Searches the vault for a simple text string.
		/// </summary>
		/// <param name="searchTerm">The string to search for.</param>
		/// <returns>An array of items that match the search term.</returns>
		public ObjectVersion[] ExecuteSimpleSearch(string searchTerm)
		{

			// Create the request.
			var request = new RestRequest("/REST/objects?q=" + System.Net.WebUtility.UrlEncode(searchTerm));

			// Make the request and get the response.
			var response = this.Get<Results<ObjectVersion>>(request);

			// Return the data.
			return response.Data?.Items?.ToArray();
		}

	}
	
}
