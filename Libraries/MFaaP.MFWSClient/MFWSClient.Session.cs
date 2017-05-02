using System;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{
		/// <summary>
		/// Retrieves information about the vault for the current session, if available.
		/// </summary>
		/// <returns>The vault returned by the request.</returns>
		public async Task<Vault> GetCurrentSessionVault()
		{
			// Build up the request.
			var request = new RestRequest("/REST/session/vault");

			// Execute the request.
			var response = await this.Get<Vault>(request);

			// Return the content.
			return response?.Data;
		}

		/// <summary>
		/// Retrieves information about the current session, if available.
		/// </summary>
		/// <returns>The vault returned by the request.</returns>
		public async Task<SessionInfo> GetCurrentSessionInfo()
		{
			// Build up the request.
			var request = new RestRequest("/REST/session");

			// Execute the request.
			var response = await this.Get<SessionInfo>(request);

			// Return the content.
			return response?.Data;
		}
	}
}
