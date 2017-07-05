using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Methods to create and modify objects.
	/// </summary>
	public class MFWSVaultClassOperations
		: MFWSVaultOperationsBase
	{
		/// <summary>
		/// Creates a new <see cref="MFWSVaultClassOperations"/> object.
		/// </summary>
		/// <param name="client">The client to interact with the server.</param>
		internal MFWSVaultClassOperations(MFWSClientBase client)
			: base(client)
		{
		}

		/// <summary>
		/// Gets a list of all classes in the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All classes in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public async Task<List<ExtendedObjectClass>> GetAllObjectClassesAsync(CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/structure/classes");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<ExtendedObjectClass>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets a list of all classes in the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All classes in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public List<ExtendedObjectClass> GetAllObjectClasses(CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetAllObjectClassesAsync(token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

	}
	
}
