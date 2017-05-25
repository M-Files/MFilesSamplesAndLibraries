using System.Net;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public class MFWSVaultValueListItemOperations
		: MFWSVaultOperationsBase
	{
		/// <inheritdoc />
		public MFWSVaultValueListItemOperations(MFWSClientBase client)
			: base(client)
		{
		}

		/// <summary>
		/// Gets the contents of the value list with Id <see cref="valueListId"/>.
		/// </summary>
		/// <param name="valueListId">The Id of the value list to return the items from.</param>
		/// <param name="nameFilter">If has a value, is used to filter the items by name.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The contents of the value list.</returns>
		/// <remarks>Note that this may be limited.</remarks>
		public async Task<Results<ValueListItem>> GetValueListItemsAsync(int valueListId, string nameFilter = null, CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/valuelists/{valueListId}/items");

			// Filter by name?
			if (false == string.IsNullOrWhiteSpace(nameFilter))
			{
				request.Resource += "?filter=" + WebUtility.UrlEncode(nameFilter);
			}

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<Results<ValueListItem>>(request, token);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets the contents of the value list with Id <see cref="valueListId"/>.
		/// </summary>
		/// <param name="valueListId">The Id of the value list to return the items from.</param>
		/// <param name="nameFilter">If has a value, is used to filter the items by name.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The contents of the value list.</returns>
		/// <remarks>Note that this may be limited.</remarks>
		public Results<ValueListItem> GetValueListItems(int valueListId, string nameFilter = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			var task = this.GetValueListItemsAsync(valueListId, nameFilter, token);
			Task.WaitAll(new Task[]
			{
				task
			}, token);
			return task.Result;
		}
	}
}
