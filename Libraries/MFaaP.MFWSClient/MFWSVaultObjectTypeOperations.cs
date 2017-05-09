using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public class MFWSVaultObjectTypeOperations
		: MFWSVaultOperationsBase
	{

		/// <inheritdoc />
		public MFWSVaultObjectTypeOperations(MFWSClientBase client)
			: base(client)
		{
		}

		/// <summary>
		/// Gets a list of all "real" object types in the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All object types in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public async Task<List<ObjType>> GetObjectTypes(CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/structure/objecttypes");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<ObjType>>(request, token);

			// Return the data.
			return response.Data;
		}
	}
}
