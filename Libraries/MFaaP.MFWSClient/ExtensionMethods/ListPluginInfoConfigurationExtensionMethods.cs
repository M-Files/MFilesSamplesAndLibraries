using System;
using System.Collections.Generic;
using System.Linq;
using MFaaP.MFWSClient;

namespace MFaaP.MFWSClient.ExtensionMethods
{
	/// <summary>
	/// Helper methods for working with authentication plugin configuration data.
	/// </summary>
	public static class ListPluginInfoConfigurationExtensionMethods
	{
		/// <summary>
		/// The <see cref="PluginInfoConfiguration.Protocol"/> for OAuth 2.0 configurations.
		/// </summary>
		private const string OAuth2PluginConfigurationProtocol = "OAuth 2.0";

		/// <summary>
		/// Returns true if the plugin configuration data contains an OAuth 2.0 authentication configuration.
		/// </summary>
		/// <param name="pluginInfoConfiguration">The list of defined authentication plugins defined.</param>
		/// <returns>True if OAuth is found, false otherwise.</returns>
		/// <remarks>Use <see cref="TryGetOAuth2Configuration(List{PluginInfoConfiguration}, out PluginInfoConfiguration)"/> to obtain the configuration efficiently.</remarks>
		public static bool SupportsOAuth2(this List<PluginInfoConfiguration> pluginInfoConfiguration)
		{
			// Sanity.
			if (null == pluginInfoConfiguration)
				throw new ArgumentNullException(nameof(pluginInfoConfiguration));

			// Is OAuth 2.0 specified?
			return pluginInfoConfiguration
				.Any(pic => pic.Protocol == ListPluginInfoConfigurationExtensionMethods.OAuth2PluginConfigurationProtocol);
		}

		/// <summary>
		/// Obtains the plugin configuration for the OAuth 2.0 authentication process.
		/// </summary>
		/// <param name="pluginInfoConfiguration">The list of defined authentication plugins defined.</param>
		/// <param name="oAuth2Configuration">The configuration, if found, or null otherwise.</param>
		/// <returns>True if OAuth is found, false otherwise.</returns>
		public static bool TryGetOAuth2Configuration(this List<PluginInfoConfiguration> pluginInfoConfiguration, out PluginInfoConfiguration oAuth2Configuration)
		{
			// Sanity.
			if (null == pluginInfoConfiguration)
				throw new ArgumentNullException(nameof(pluginInfoConfiguration));

			// Is OAuth 2.0 specified?
			oAuth2Configuration = pluginInfoConfiguration
				.FirstOrDefault(pic => pic.Protocol == ListPluginInfoConfigurationExtensionMethods.OAuth2PluginConfigurationProtocol);

			// Did we get a value?
			return oAuth2Configuration != null;

		}
	}
}
