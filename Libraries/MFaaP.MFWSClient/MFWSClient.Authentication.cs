using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MFaaP.MFWSClient.ExtensionMethods;
using MFaaP.MFWSClient.OAuth2;
using RestSharp;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// M-Files REST API operations.
	/// </summary>
	public partial class MFWSClient
	{
		private string authenticationToken;

		/// <summary>
		/// The HTTP header name for the X-Authentication header.
		/// </summary>
		protected const string XAuthenticationHttpHeaderName = "X-Authentication";

		/// <summary>
		/// The HTTP header name used for the vault guid (<see cref="AddVaultHeader(System.Guid)"/>).
		/// </summary>
		protected const string VaultHttpHeaderName = "X-Vault";

		/// <summary>
		/// This is the authentication token used in the request headers.
		/// </summary>
		/// <remarks>Not used when SSO or OAuth authentication is used.</remarks>
		public string AuthenticationToken
		{
			protected get { return this.authenticationToken; }
			set
			{
				// Set the token.
				this.authenticationToken = value;

				// Remove any existing default parameters.
				foreach (Parameter parameter in this.DefaultParameters.Where(p => p.Name == MFWSClient.XAuthenticationHttpHeaderName)
					.ToArray())
				{
					this.DefaultParameters.Remove(parameter);
				}

				// Add the new one.
				if(null != this.authenticationToken)
					this.AddDefaultHeader(MFWSClient.XAuthenticationHttpHeaderName, this.authenticationToken);
			}
		}

		/// <summary>
		/// Adds the "X-Vault" header to the default headers collection.
		/// </summary>
		/// <param name="vaultGuid">The GUID of the vault to use.</param>
		/// <remarks>Typically used with OAuth authentication tokens.</remarks>
		public void AddVaultHeader(Guid vaultGuid)
		{
			this.AddVaultHeader(vaultGuid.ToString("B"));
		}

		/// <summary>
		/// Adds the "X-Vault" header to the default headers collection.
		/// </summary>
		/// <param name="vaultGuid">The GUID of the vault to use.  All common .NET GUID formatting accepted.</param>
		/// <remarks>Typically used with OAuth authentication tokens.</remarks>
		public void AddVaultHeader(string vaultGuid)
		{
			this.AddDefaultHeader(MFWSClient.VaultHttpHeaderName, vaultGuid);
		}

		/// <summary>
		/// Removes the "X-Vault" header from the default headers collection.
		/// </summary>
		public void ClearVaultHeader()
		{
			// Remove the authorisation header.
			foreach (Parameter parameter in this.DefaultParameters.Where(p => p.Name == MFWSClient.VaultHttpHeaderName)
				.ToArray())
			{
				this.DefaultParameters.Remove(parameter);
			}
		}

		/// <summary>
		/// Clears the authentication tokens used by the client.
		/// </summary>
		/// <remarks>If <see cref="AddVaultHeader(System.Guid)"/> has been set then does not remove it.  Call <see cref="ClearVaultHeader"/> too.</remarks>
		protected virtual void ClearAuthenticationToken()
		{
			// Clear the authentication token.
			this.AuthenticationToken = null;

			// Clear any cookies that are held (e.g. SSO) too.
			this.CookieContainer = new CookieContainer();

			// Remove the authorisation header.
			foreach (Parameter parameter in this.DefaultParameters.Where(p => p.Name == MFWSClient.AuthorizationHttpHeaderName)
				.ToArray())
			{
				this.DefaultParameters.Remove(parameter);
			}
		}

		/// <summary>
		/// Attempts SSO (Single Sign On) authentication with the remote web server.
		/// </summary>
		/// <param name="vaultId">The id of the vault to authenticate to.</param>
		/// <param name="token">A cancellation token for the request.</param>
		public async Task AuthenticateUsingSingleSignOnAsync(Guid vaultId, CancellationToken token = default(CancellationToken))
		{
			// Clear any current tokens.
			this.ClearAuthenticationToken();

			// Make a request to the WebServiceSSO.aspx file (which will give our token).
			// Note: "popup=1" in the QueryString just indicates that we don't care about redirecting on success/failure.
			// Note: The vault Id in the QueryString indicates which vault to authenticate to.  This is optional if there is only one vault.
			var request = new RestRequest($"/WebServiceSSO.aspx?popup=1&vault={vaultId:D}");

			// Set the credentials of the request to be our current network credentials.
			request.Credentials = CredentialCache.DefaultNetworkCredentials;

			// Execute the request and store the response.
			IRestResponse response = await this.Get(request, token)
				.ConfigureAwait(false);

			// Save the response cookies in our persistent RestClient cookie container.
			// Note: We should have at least one returned which is the ASP.NET session Id.
			this.CookieContainer = new CookieContainer();
			if (null != response.Cookies)
			{
				foreach (var cookie in response.Cookies)
				{
					this.CookieContainer.Add(this.BaseUrl, new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
				}
			}
		}

		/// <summary>
		/// Attempts SSO (Single Sign On) authentication with the remote web server.
		/// </summary>
		/// <param name="vaultId">The id of the vault to authenticate to.</param>
		/// <param name="token">A cancellation token for the request.</param>
		public void AuthenticateUsingSingleSignOn(Guid vaultId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			this.AuthenticateUsingSingleSignOnAsync(vaultId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Authenticates to the server using the details provided.
		/// </summary>
		/// <param name="vaultId">The Id of the vault to connect to.</param>
		/// <param name="username">The username to use.</param>
		/// <param name="password">The password to use.</param>
		/// <param name="expiration">The date and time that the token should expire.</param>
		/// <param name="token">A cancellation token for the request.</param>
		public Task AuthenticateUsingCredentialsAsync(Guid? vaultId, string username, string password, DateTime? expiration = null, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.AuthenticateUsingCredentialsAsync(new Authentication()
			{
				Username = username,
				Password = password,
				VaultGuid = vaultId,
				Expiration = expiration
			}, token);
		}

		/// <summary>
		/// Authenticates to the server using the details provided.
		/// </summary>
		/// <param name="vaultId">The Id of the vault to connect to.</param>
		/// <param name="username">The username to use.</param>
		/// <param name="password">The password to use.</param>
		/// <param name="token">A cancellation token for the request.</param>
		public void AuthenticateUsingCredentials(Guid? vaultId, string username, string password, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			this.AuthenticateUsingCredentialsAsync(vaultId, username, password, null, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Authenticates to the server using the details provided.
		/// </summary>
		/// <param name="vaultId">The Id of the vault to connect to.</param>
		/// <param name="username">The username to use.</param>
		/// <param name="password">The password to use.</param>
		/// <param name="expiration">The date and time that the token should expire.</param>
		/// <param name="token">A cancellation token for the request.</param>
		public void AuthenticateUsingCredentials(Guid? vaultId, string username, string password, DateTime expiration, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			this.AuthenticateUsingCredentialsAsync(vaultId, username, password, expiration, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Authenticates to the server using the details provided.
		/// </summary>
		/// <param name="vaultId">The Id of the vault to connect to.</param>
		/// <param name="username">The username to use.</param>
		/// <param name="password">The password to use.</param>
		/// <param name="expiration">The duration of time (from now) in which the token should expire.</param>
		/// <param name="token">A cancellation token for the request.</param>
		public void AuthenticateUsingCredentials(Guid? vaultId, string username, string password, TimeSpan expiration, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			this.AuthenticateUsingCredentials(vaultId, username, password, DateTime.Now.Add(expiration), token);
		}

		/// <summary>
		/// Authenticates to the server using details passed in the authentication parameter.
		/// </summary>
		/// <param name="authentication">The authentication details to use.</param>
		/// <param name="token">A cancellation token for the request.</param>
		public async Task AuthenticateUsingCredentialsAsync(Authentication authentication, CancellationToken token = default(CancellationToken))
		{
			// Clear any current tokens.
			this.ClearAuthenticationToken();

			// Sanity.
			if (null == authentication)
				return;

			// Build the request to authenticate to the server.
			{
				var request = new RestRequest("/REST/server/authenticationtokens");
				request.AddJsonBody(authentication);

				// Execute the request and store the response.
				var response = await this.Post<PrimitiveType<string>>(request, token)
					.ConfigureAwait(false);

				// Save the authentication token.
				this.AuthenticationToken = response?.Data?.Value;
			}

		}

		/// <summary>
		/// Authenticates to the server using details passed in the authentication parameter.
		/// </summary>
		/// <param name="authentication">The authentication details to use.</param>
		/// <param name="token">A cancellation token for the request.</param>
		protected void AuthenticateUsingCredentials(Authentication authentication, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			this.AuthenticateUsingCredentialsAsync(authentication, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Gets the vaults from the server, using the current authentication token.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A list of online vaults.</returns>
		public async Task<List<Vault>> GetOnlineVaultsAsync(CancellationToken token = default(CancellationToken))
		{
			// Build the request to authenticate to the vault.
			var request = new RestRequest("/REST/server/vaults?online=true");

			// Execute the request and store the response.
			var response = await this.Get<List<Vault>>(request, token)
				.ConfigureAwait(false);

			// Store the authentication token.
			return response?.Data;
		}

		/// <summary>
		/// Gets the vaults from the server, using the current authentication token.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A list of online vaults.</returns>
		public List<Vault> GetOnlineVaults(CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetOnlineVaultsAsync(token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Gets the configured authentication plugins.
		/// </summary>
		/// <param name="vaultGuid">Vault GUID, Guid.Empty to get just server configuration.</param>
		/// <param name="token">A cancellation token for the task.</param>
		/// <returns>The authentication plugin configuration.</returns>
		public async Task<List<MFaaP.MFWSClient.PluginInfoConfiguration>> GetAuthenticationPluginsAsync(
			Guid vaultGuid = default(Guid),
			CancellationToken token = default(CancellationToken))
		{
			// Create the request, include vault if GUID was defined.
			string requestUrl = "/REST/server/authenticationprotocols";
			if( vaultGuid != default(Guid) )
				requestUrl += "?vault=" + vaultGuid.ToString( "B" );
			var request = new RestRequest( requestUrl, Method.GET);

			// Return the plugins specified.
			return (await base.Get<List<MFaaP.MFWSClient.PluginInfoConfiguration>>(request, token)).Data
					?? new List<PluginInfoConfiguration>();
		}

		/// <summary>
		/// Gets the configured authentication plugins.
		/// </summary>
		/// <param name="vaultGuid">Vault GUID.</param>
		/// <param name="token">A cancellation token for the task.</param>
		/// <returns>The authentication plugin configuration.</returns>
		public List<MFaaP.MFWSClient.PluginInfoConfiguration> GetAuthenticationPlugins( 
			Guid vaultGuid = default(Guid),
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetAuthenticationPluginsAsync( vaultGuid, token )
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

	}
	
}
