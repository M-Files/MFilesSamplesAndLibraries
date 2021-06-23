using MFilesAPI;
using System;
using System.Text;

namespace COMAPI.ExtensionMethods
{
	public static class PluginInfoExtensionMethods
	{
		private static string GetValueOrNull(this NamedValues namedValues, string name)
		{
			try
			{
				var value = namedValues[name]?.ToString();
				if (null == value || value.Length == 0)
					return null;
				return value;
			}
			catch { return null; }
		}

		/// <summary>
		/// Retrieves the token endpoint.
		/// </summary>
		/// <param name="plugin">The OAuth authentication plugin details.</param>
		/// <returns>The redirect URI.</returns>
		public static string GetTokenEndpoint
		(
			this PluginInfo plugin
		) => plugin?.Configuration.GetValueOrNull("TokenEndpoint");

		/// <summary>
		/// Retrieves the site realm.
		/// </summary>
		/// <param name="plugin">The OAuth authentication plugin details.</param>
		/// <returns>The redirect URI.</returns>
		public static string GetSiteRealm
		(
			this PluginInfo plugin
		) => plugin?.Configuration.GetValueOrNull("SiteRealm");

		/// <summary>
		/// Retrieves the client ID.
		/// </summary>
		/// <param name="plugin">The OAuth authentication plugin details.</param>
		/// <returns>The redirect URI.</returns>
		public static string GetClientID
		(
			this PluginInfo plugin
		) => plugin?.Configuration.GetValueOrNull("ClientID");

		/// <summary>
		/// Retrieves the resource.
		/// </summary>
		/// <param name="plugin">The OAuth authentication plugin details.</param>
		/// <returns>The redirect URI.</returns>
		public static string GetResource
		(
			this PluginInfo plugin
		) => plugin?.Configuration.GetValueOrNull("Resource");

		/// <summary>
		/// Retrieves the scope.
		/// </summary>
		/// <param name="plugin">The OAuth authentication plugin details.</param>
		/// <returns>The redirect URI.</returns>
		public static string GetScope
		(
			this PluginInfo plugin
		) => plugin?.Configuration.GetValueOrNull("Scope");

		/// <summary>
		/// Retrieves the client secret.
		/// </summary>
		/// <param name="plugin">The OAuth authentication plugin details.</param>
		/// <returns>The redirect URI.</returns>
		public static string GetClientSecret
		(
			this PluginInfo plugin
		) => plugin?.Configuration.GetValueOrNull("ClientSecret");

		/// <summary>
		/// Retrieves the redirect URI that should be used for authentication.
		/// </summary>
		/// <param name="plugin">The OAuth authentication plugin details.</param>
		/// <returns>The redirect URI.</returns>
		public static string GetAppropriateRedirectUri
		(
			this PluginInfo plugin
		)
		{
			// Sanity.
			if (null == plugin)
				throw new ArgumentNullException(nameof(plugin));

			return plugin.Configuration.GetValueOrNull("RedirectURIForNative")
				?? plugin.Configuration.GetValueOrNull("RedirectURIForMobile")
				?? plugin.Configuration.GetValueOrNull("RedirectURIForWeb")
				?? plugin.Configuration.GetValueOrNull("RedirectURIForWOPI")
				?? "http://localhost";
		}

		/// <summary>
		/// Returns whether the plugin is an OAuth plugin.
		/// </summary>
		/// <param name="plugin">The plugin details.</param>
		/// <returns>true if the plugin represents an OAuth configuration.</returns>
		public static bool IsOAuthPlugin(this PluginInfo plugin)
		{
			return plugin?.AssemblyName == "MFiles.AuthenticationProviders.OAuth";
		}

		/// <summary>
		/// Generates a valid authorization URI for use when doing OAuth authentication.
		/// </summary>
		/// <param name="plugin">The OAuth authentication plugin details.</param>
		/// <param name="state">The state - must not be empty/null - used to passed to the authorization endpoint.</param>
		/// <param name="forceLogin">If true then the user will be forced to log in, even if they have already authenticated recently.</param>
		/// <returns>The URI that can be shown in a browser to undertake the OAuth flow.</returns>
		public static Uri GenerateAuthorizationUri
		(
			this PluginInfo plugin,
			string state,
			bool forceLogin = false
		)
		{
			// Sanity.
			if (null == plugin)
				throw new ArgumentNullException(nameof(plugin));
			if (string.IsNullOrWhiteSpace(state))
				throw new ArgumentNullException(nameof(state));
			if (false == plugin.IsOAuthPlugin())
				throw new ArgumentException("The authentication plugin does not refer to an OAuth authentication type", nameof(plugin));
			var promptType = forceLogin ? "login" : null;
			var redirectUri = plugin.GetAppropriateRedirectUri();

			// Build up the URI with mandatory data.
			var uriBuilder = new UriBuilder(plugin.Configuration["AuthorizationEndpoint"]?.ToString());
			uriBuilder.SetQueryParam("client_id", plugin.Configuration["ClientID"]?.ToString());
			uriBuilder.SetQueryParam("redirect_uri", redirectUri);
			uriBuilder.SetQueryParam("response_type", "code");

			// Add the optional items, if set.
			uriBuilder.SetQueryParamIfNotNullOrWhitespace("scope", plugin.Configuration["Scope"]?.ToString());
			uriBuilder.SetQueryParamIfNotNullOrWhitespace("state", state);
			uriBuilder.SetQueryParamIfNotNullOrWhitespace("prompt", promptType);
			uriBuilder.SetQueryParamIfNotNullOrWhitespace("resource", plugin.Configuration["Resource"]?.ToString());

			// Return the generated URI.
			return uriBuilder.Uri;
		}
	}
}
