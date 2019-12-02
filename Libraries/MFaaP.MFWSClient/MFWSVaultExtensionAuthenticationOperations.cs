using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using RestSharp;
//using RestSharp.Extensions.MonoHttp;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// The 'VaultExtensionAuthenticationOperations' class represents the available extension authentication operations.
	/// </summary>
	/// <remarks>ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultExtensionAuthenticationOperations.html </remarks>
	public class MFWSVaultExtensionAuthenticationOperations
		: MFWSVaultOperationsBase
	{
		/// <summary>
		/// Creates a new <see cref="MFWSVaultExtensionAuthenticationOperations"/> object.
		/// </summary>
		/// <param name="client">The client to interact with the server.</param>
		internal MFWSVaultExtensionAuthenticationOperations(MFWSClientBase client)
			: base(client)
		{
		}

		#region Retrieval of external repository information

		/// <summary>
		/// Returns the repositories configured within the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task.</returns>
		public async Task<List<RepositoryAuthenticationTarget>> GetExtensionAuthenticationTargetsAsync(CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/repositories.aspx");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<RepositoryAuthenticationTarget>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Returns the repositories configured within the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The authentication targets.</returns>
		public List<RepositoryAuthenticationTarget> GetExtensionAuthenticationTargets(CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetExtensionAuthenticationTargetsAsync(token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

		#region Logging into external repository connection

		/// <summary>
		/// Attempts to log into an external repository connection.
		/// </summary>
		/// <param name="targetID">The connection to authenticate against.</param>
		/// <param name="authentication">The authentication details to use.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task.</returns>
		public async Task<RepositoryAuthenticationStatus> LogInWithExtensionAuthenticationAsync(
			string targetID,
			RepositoryAuthentication authentication,
			CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == targetID)
				throw new ArgumentNullException(nameof(targetID));
			if (string.IsNullOrWhiteSpace(targetID))
				throw new ArgumentException("The target cannot be null or empty.", nameof(targetID));
			if (null == authentication)
				throw new ArgumentNullException(nameof(authentication));
			if (string.IsNullOrWhiteSpace(authentication.ConfigurationName))
				throw new ArgumentException("The authentication plugin configuration name must be provided.", nameof(authentication));

			// Create the request.
			var request = new RestRequest($"/REST/repositories/{HttpUtility.UrlEncode(targetID)}/session.aspx");

			// If authentication token is a blank string, replace it with null.
			// This is because the remote end tests against null.
			if (string.IsNullOrWhiteSpace(authentication.AuthenticationToken))
				authentication.AuthenticationToken = null;

			// Set the request body.
			request.AddJsonBody(authentication);

			// Make the request and get the response.
			var response = await this.MFWSClient.Post<RepositoryAuthenticationStatus>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Attempts to log into an external repository connection.
		/// </summary>
		/// <param name="targetID">The connection to authenticate against.</param>
		/// <param name="authentication">The authentication details to use.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The repository state after login attempt.</returns>
		public RepositoryAuthenticationStatus LogInWithExtensionAuthentication(
			string targetID,
			RepositoryAuthentication authentication,
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.LogInWithExtensionAuthenticationAsync(targetID, authentication, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

		#region Logging out of external repository connection

		/// <summary>
		/// Logs out with the existing extension authentication data.
		/// </summary>
		/// <param name="targetID">The connection to authenticate against.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task.</returns>
		public async Task LogOutWithExtensionAuthenticationAsync(
			string targetID,
			CancellationToken token = default(CancellationToken))
		{
			if (null == targetID)
				throw new ArgumentNullException(nameof(targetID));
			if (string.IsNullOrWhiteSpace(targetID))
				throw new ArgumentException("The target cannot be null or empty.", nameof(targetID));

			// Create the request.
			var request = new RestRequest($"/REST/repositories/{HttpUtility.UrlEncode(targetID)}/session.aspx");

			// Make the request and get the response.
			await this.MFWSClient.Delete(request, token)
				.ConfigureAwait(false);
		}

		/// <summary>
		/// Logs out with the existing extension authentication data.
		/// </summary>
		/// <param name="targetID">The connection to authenticate against.</param>
		/// <param name="token">A cancellation token for the request.</param>
		public void LogOutWithExtensionAuthentication(
			string targetID,
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			this.LogOutWithExtensionAuthenticationAsync(targetID, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

	}
}
