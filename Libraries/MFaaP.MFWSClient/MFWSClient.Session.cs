using System;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{
		/// <summary>
		/// Retrieves information about the vault for the current session, if available.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The vault returned by the request.</returns>
		public async Task<Vault> GetCurrentSessionVault(CancellationToken token = default(CancellationToken))
		{
			// Build up the request.
			var request = new RestRequest("/REST/session/vault");

			// Execute the request.
			var response = await this.Get<Vault>(request, token);

			// Return the content.
			return response?.Data;
		}

		/// <summary>
		/// Retrieves information about the current session, if available.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The vault returned by the request.</returns>
		public async Task<SessionInfo> GetCurrentSessionInfo(CancellationToken token = default(CancellationToken))
		{
			// Build up the request.
			var request = new RestRequest("/REST/session");

			// Execute the request.
			var response = await this.Get<SessionInfo>(request, token);

			// Return the content.
			return response?.Data;
		}
	}
}
