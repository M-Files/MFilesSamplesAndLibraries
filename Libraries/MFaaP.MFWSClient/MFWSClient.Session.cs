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
		/// <returns>An awaitable task.</returns>
		public async Task<Vault> GetCurrentSessionVaultAsync(CancellationToken token = default(CancellationToken))
		{
			// Build up the request.
			var request = new RestRequest("/REST/session/vault");

			// Execute the request.
			var response = await this.Get<Vault>(request, token);

			// Return the content.
			return response?.Data;
		}

		/// <summary>
		/// Retrieves information about the vault for the current session, if available.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The vault returned by the request.</returns>
		public Vault GetCurrentSessionVault(CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			var task = this.GetCurrentSessionVaultAsync(token);
			Task.WaitAll(new Task[] { task }, token);
			return task.Result;
		}

		/// <summary>
		/// Retrieves information about the current session, if available.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task.</returns>
		public async Task<SessionInfo> GetCurrentSessionInfoAsync(CancellationToken token = default(CancellationToken))
		{
			// Build up the request.
			var request = new RestRequest("/REST/session");

			// Execute the request.
			var response = await this.Get<SessionInfo>(request, token);

			// Return the content.
			return response?.Data;
		}

		/// <summary>
		/// Retrieves information about the current session, if available.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The vault returned by the request.</returns>
		public SessionInfo GetCurrentSessionInfo(CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			var task = this.GetCurrentSessionInfoAsync(token);
			Task.WaitAll(new Task[] { task }, token);
			return task.Result;
		}
	}
}
