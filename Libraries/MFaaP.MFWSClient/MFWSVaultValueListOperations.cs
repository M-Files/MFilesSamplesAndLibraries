using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public class MFWSVaultValueListOperations
		: MFWSVaultOperationsBase
	{
		/// <inheritdoc />
		internal MFWSVaultValueListOperations(MFWSClientBase client)
			: base(client)
		{
		}

		/// <summary>
		/// Gets a list of all value lists in the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All value lists in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public async Task<List<ObjType>> GetValueListsAsync(CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/valuelists");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<ObjType>>(request, token);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets a list of all value lists in the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All value lists in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public List<ObjType> GetValueLists(CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			var task = this.GetValueListsAsync(token);
			Task.WaitAll(new Task[]
			{
				task
			}, token);
			return task.Result;
		}
	}
}
