using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{
		private string authenticationToken;

		/// <summary>
		/// This is the authentication token used in the request headers.
		/// </summary>
		/// <remarks>Not used when SSO authentication is used.</remarks>
		protected string AuthenticationToken
		{
			get { return this.authenticationToken; }
			private set
			{
				this.authenticationToken = value;

				// Update the authentication header.
				string authenticationHttpHeaderName = "X-Authentication";

				// Remove any existing default parameters.
				foreach (var parameter in Enumerable.Where<Parameter>(this.DefaultParameters, p => p.Name == authenticationHttpHeaderName)
					.ToArray())
				{
					this.DefaultParameters.Remove(parameter);
				}

				// Add the new one.
				this.AddDefaultHeader(authenticationHttpHeaderName, this.authenticationToken);
			}
		}

		/// <summary>
		/// Clears the authentication tokens used by the client.
		/// </summary>
		protected void ClearAuthenticationToken()
		{
			// Clear the authentication token.
			this.AuthenticationToken = null;

			// Clear any cookies that are held (e.g. SSO) too.
			this.CookieContainer = new CookieContainer();
		}

		/// <summary>
		/// Attempts SSO (Single Sign On) authentication with the remote web server.
		/// </summary>
		/// <param name="vaultId">The id of the vault to authenticate to.</param>
		public async Task AuthenticateUsingSingleSignOn(Guid vaultId)
		{
			// Clear any current tokens.
			this.ClearAuthenticationToken();
			
			// Make a request to the WebServiceSSO.aspx file (which will give our token).
			// Note: "popup=1" in the QueryString just indicates that we don't care about redirecting on success/failure.
			// Note: The vault Id in the QueryString indicates which vault to authenticate to.  This is optional if there is only one vault.
			var request = new RestRequest($"/WebServiceSSO.aspx?popup=1&vault={vaultId.ToString("D")}");

			// Set the credentials of the request to be our current network credentials.
			request.Credentials = CredentialCache.DefaultNetworkCredentials;

			// Execute the request and store the response.
			IRestResponse response = await this.Get(request);

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
		/// Authenticates to the server using the details provided.
		/// </summary>
		/// <param name="vaultId">The Id of the vault to connect to.</param>
		/// <param name="username">The username to use.</param>
		/// <param name="password">The password to use.</param>
		public Task AuthenticateUsingCredentials(Guid? vaultId, string username, string password)
		{
			// Use the other overload.
			return this.AuthenticateUsingCredentials(new Authentication()
			{
				Username = username,
				Password = password,
				VaultGuid = vaultId
			});
		}

		/// <summary>
		/// Authenticates to the server using details passed in the authentication parameter.
		/// </summary>
		/// <param name="authentication">The authentication details to use.</param>
		protected async Task AuthenticateUsingCredentials(Authentication authentication)
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
				var response = await this.Post<PrimitiveType<string>>(request);

				// Save the authentication token.
				this.AuthenticationToken = response?.Data?.Value;
			}

		}

		/// <summary>
		/// Gets the vaults from the server, using the current authentication token.
		/// </summary>
		/// <returns></returns>
		public async Task<List<Vault>> GetOnlineVaults()
		{
			// Build the request to authenticate to the vault.
			var request = new RestRequest("/REST/server/vaults?online=true");

			// Execute the request and store the response.
			var response = await this.Get<List<Vault>>(request);

			// Store the authentication token.
			return response?.Data;
		}

	}
	
}
