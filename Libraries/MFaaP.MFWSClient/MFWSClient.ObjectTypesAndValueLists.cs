using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{
		/// <summary>
		/// Gets a list of all "real" object types in the vault.
		/// </summary>
		/// <returns>All object types in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public async Task<List<ObjType>> GetObjectTypes()
		{
			// Create the request.
			var request = new RestRequest($"/REST/structure/objecttypes");

			// Make the request and get the response.
			var response = await this.Get<List<ObjType>>(request);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets a list of all value lists in the vault.
		/// </summary>
		/// <returns>All value lists in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public async Task<List<ObjType>> GetValueLists()
		{
			// Create the request.
			var request = new RestRequest($"/REST/valuelists");

			// Make the request and get the response.
			var response = await this.Get<List<ObjType>>(request);

			// Return the data.
			return response.Data;
		}
	}
}
