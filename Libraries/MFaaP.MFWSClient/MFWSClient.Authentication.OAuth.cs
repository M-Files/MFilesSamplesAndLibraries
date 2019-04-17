using System;
using System.Threading;
using System.Threading.Tasks;
using MFaaP.MFWSClient.ExtensionMethods;
using MFaaP.MFWSClient.OAuth2;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{
		/// <summary>
		/// The HTTP header name used for the authorization token (<see cref="AddAuthorizationHeader"/>).
		/// </summary>
		protected const string AuthorizationHttpHeaderName = "Authorization";

		/// <summary>
		/// Adds the default Authorization HTTP header as appropriate for the OAuth configuration.
		/// </summary>
		/// <param name="pluginConfiguration">The configuration for the OAuth 2.0 plugin.</param>
		/// <param name="oAuthTokens">The access tokens retrieved via the OAuth flow.</param>
		/// <remarks><see cref="OAuth2Configuration.UseIdTokenAsAccessToken"/> defines whether <see cref="OAuth2TokenResponse.IdToken"/> (if true) or
		/// <see cref="OAuth2TokenResponse.AccessToken"/> (if false) should be used for the bearer value.</remarks>
		public void AddAuthorizationHeader(OAuth2Configuration pluginConfiguration, OAuth2TokenResponse oAuthTokens)
		{
			// Sanity.
			if (null == pluginConfiguration)
				throw new ArgumentNullException(nameof(pluginConfiguration));

			// Clear the authorisation token.
			this.ClearAuthenticationToken();

			// Add the authorisation token to the headers.
			if (pluginConfiguration.UseIdTokenAsAccessToken)
			{
				this.AddDefaultHeader(MFWSClient.AuthorizationHttpHeaderName, "Bearer " + oAuthTokens.IdToken);
			}
			else
			{
				this.AddDefaultHeader(MFWSClient.AuthorizationHttpHeaderName, "Bearer " + oAuthTokens.AccessToken);
			}
		}

		/// <summary>
		/// Using the <see cref="code"/> from the OAuth authorisation endpoint, 
		/// requests tokens from the token endpoint and sets up the client to use them.
		/// The token data is returned in case it is needed in the future (e.g. <see cref="Refresh2OAuthTokenAsync"/>).
		/// </summary>
		/// <param name="pluginConfiguration">The configuration of the OAuth plugin.</param>
		/// <param name="code">The code returned from the OAuth authorisation endpoint.</param>
		/// <returns>The data returned from the token endpoint.</returns>
		public OAuth2TokenResponse ConvertOAuth2AuthorizationCodeToTokens(OAuth2Configuration pluginConfiguration, string code)
		{
			// Sanity.
			if (null == pluginConfiguration)
				throw new ArgumentNullException(nameof(pluginConfiguration));
			if (null == code)
				throw new ArgumentNullException(nameof(code));

			// Create the request, adding the mandatory items.
			var tokenEndpoint = new Uri(pluginConfiguration.TokenEndpoint, uriKind: UriKind.Absolute);
			var request = new RestSharp.RestRequest(tokenEndpoint.PathAndQuery, RestSharp.Method.POST);
			request.AddParameter("code", code);
			request.AddParameter("grant_type", pluginConfiguration.GrantType);
			request.AddParameter("redirect_uri", pluginConfiguration.GetAppropriateRedirectUri());

			// Add the client id.  If there's a realm then use that here too.
			request.AddParameter(
				"client_id",
				string.IsNullOrWhiteSpace(pluginConfiguration.SiteRealm)
					? pluginConfiguration.ClientID // If no site realm is supplied, just pass the client ID.
					: $"{pluginConfiguration.ClientID}@{pluginConfiguration.SiteRealm}" // Otherwise pass client ID @ site realm.
			);

			// Add the optional bits.
			request.AddParameterIfNotNullOrWhitespace("resource", pluginConfiguration.Resource);
			request.AddParameterIfNotNullOrWhitespace("scope", pluginConfiguration.Scope);;
			request.AddParameterIfNotNullOrWhitespace("client_secret", pluginConfiguration.ClientSecret);

			// Make a post to the token endpoint.
			// NOTE: We must use a new RestClient here otherwise it'll try and add the token endpoint to the MFWA base url.
			var restClient = new RestSharp.RestClient(tokenEndpoint.GetLeftPart(UriPartial.Authority));
			var response = restClient.ExecuteAsPost<OAuth2TokenResponse>(request, "POST");

			// Validate response.
			if (null == response.Data
				|| response.Data.TokenType != "Bearer")
			{
				throw new InvalidOperationException("OAuth token not received from endpoint, or token type was not bearer.");
			}

			// Return the access token data.
			return response.Data;
		}
		
		/// <summary>
		/// Refreshes the OAuth 2.0 access token using the refresh token provided.
		/// </summary>
		/// <param name="pluginConfiguration">The configuration for the OAuth 2.0 identity provider.</param>
		/// <param name="oAuthTokens">The OAuth 2.0 tokens.</param>
		/// <param name="token">A cancellation token for the task.</param>
		/// <returns>The updated access token data.</returns>
		public OAuth2TokenResponse RefreshOAuth2Token(
			OAuth2Configuration pluginConfiguration,
			OAuth2TokenResponse oAuthTokens,
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.RefreshOAuth2TokenAsync(pluginConfiguration, oAuthTokens, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}
		
		/// <summary>
		/// Refreshes the OAuth 2.0 access token using the refresh token provided.
		/// </summary>
		/// <param name="pluginConfiguration">The configuration for the OAuth 2.0 identity provider.</param>
		/// <param name="refreshToken">The refresh token.</param>
		/// <param name="token">A cancellation token for the task.</param>
		/// <returns>The updated access token data.</returns>
		public OAuth2TokenResponse RefreshOAuth2Token(
			OAuth2Configuration pluginConfiguration,
			string refreshToken,
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.RefreshOAuth2TokenAsync(pluginConfiguration, refreshToken, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}
		
		/// <summary>
		/// Refreshes the OAuth 2.0 access token using the refresh token provided.
		/// </summary>
		/// <param name="pluginConfiguration">The configuration for the OAuth 2.0 identity provider.</param>
		/// <param name="oAuthTokens">The OAuth 2.0 tokens.</param>
		/// <param name="token">A cancellation token for the task.</param>
		/// <returns>The updated access token data.</returns>
		public Task<OAuth2TokenResponse> RefreshOAuth2TokenAsync(
			OAuth2Configuration pluginConfiguration,
			OAuth2TokenResponse oAuthTokens,
			CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == pluginConfiguration)
				throw new ArgumentNullException(nameof(pluginConfiguration));
			if (null == oAuthTokens)
				throw new ArgumentNullException(nameof(oAuthTokens));

			// Execute the other overload.
			return this.RefreshOAuth2TokenAsync(pluginConfiguration, oAuthTokens.RefreshToken, token);
		}

		/// <summary>
		/// Refreshes the OAuth 2.0 access token using the refresh token provided.
		/// </summary>
		/// <param name="pluginConfiguration">The configuration for the OAuth 2.0 identity provider.</param>
		/// <param name="refreshToken">The refresh token.</param>
		/// <param name="token">A cancellation token for the task.</param>
		/// <returns>The updated access token data.</returns>
		public async Task<OAuth2TokenResponse> RefreshOAuth2TokenAsync(
			OAuth2Configuration pluginConfiguration,
			string refreshToken,
			CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == pluginConfiguration)
				throw new ArgumentNullException(nameof(pluginConfiguration));
			if (string.IsNullOrWhiteSpace(refreshToken))
				throw new ArgumentException("The OAuth 2.0 refresh token cannot be empty.", nameof(refreshToken));

			// Create the request, adding the mandatory items.
			var tokenEndpoint = new Uri(pluginConfiguration.TokenEndpoint, uriKind: UriKind.Absolute);
			var request = new RestSharp.RestRequest(tokenEndpoint.PathAndQuery, RestSharp.Method.POST);
			request.AddParameter("grant_type", "refresh_token");
			request.AddParameter("refresh_token", refreshToken);
			request.AddParameter("redirect_uri", pluginConfiguration.GetAppropriateRedirectUri());

			// Add the client id.  If there's a realm then use that here too.
			request.AddParameter(
				"client_id",
				string.IsNullOrWhiteSpace(pluginConfiguration.SiteRealm)
					? pluginConfiguration.ClientID // If no site realm is supplied, just pass the client ID.
					: $"{pluginConfiguration.ClientID}@{pluginConfiguration.SiteRealm}" // Otherwise pass client ID @ site realm.
			);

			// Add the optional bits.
			request.AddParameterIfNotNullOrWhitespace("resource", pluginConfiguration.Resource);
			request.AddParameterIfNotNullOrWhitespace("scope", pluginConfiguration.Scope);
			request.AddParameterIfNotNullOrWhitespace("client_secret", pluginConfiguration.ClientSecret);

			// Make a post to the token endpoint.
			// NOTE: We must use a new RestClient here otherwise it'll try and add the token endpoint to the MFWA base url.
			var restClient = new RestSharp.RestClient(tokenEndpoint.GetLeftPart(UriPartial.Authority));
			var response = await restClient.ExecutePostTaskAsync<OAuth2TokenResponse>(request, token);

			// Validate response.
			if (null == response.Data
				|| response.Data.TokenType != "Bearer")
			{
				throw new InvalidOperationException("OAuth token not received from endpoint, or token type was not bearer.");
			}

			// Return the access token data.
			return response.Data;
		}
	}
}
