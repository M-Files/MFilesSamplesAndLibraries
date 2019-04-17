using System;
using System.Collections.Generic;
using System.Linq;
using MFaaP.MFWSClient;
using MFaaP.MFWSClient.OAuth2;

namespace MFaaP.MFWSClient.ExtensionMethods
{
	/// <summary>
	/// Helper methods for working with authentication plugin configuration data.
	/// </summary>
	// ReSharper disable once InconsistentNaming
	public static class IEnumerablePluginInfoConfigurationExtensionMethods
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
		/// <remarks>Use <see cref="TryGetOAuth2Configuration(IEnumerable{PluginInfoConfiguration}, out OAuth2Configuration)"/> to obtain the configuration efficiently.</remarks>
		public static bool SupportsOAuth2(this IEnumerable<PluginInfoConfiguration> pluginInfoConfiguration)
		{
			// Sanity.
			if (null == pluginInfoConfiguration)
				throw new ArgumentNullException(nameof(pluginInfoConfiguration));

			// Use the other overload.
			OAuth2Configuration configuration;
			return pluginInfoConfiguration.TryGetOAuth2Configuration(out configuration);
		}

		/// <summary>
		/// Obtains the plugin configuration for the OAuth 2.0 authentication process.
		/// </summary>
		/// <param name="pluginInfoConfiguration">The list of defined authentication plugins defined.</param>
		/// <param name="oAuth2Configuration">The configuration, if found, or null otherwise.</param>
		/// <returns>True if OAuth is found, false otherwise.</returns>
		public static bool TryGetOAuth2Configuration(this IEnumerable<PluginInfoConfiguration> pluginInfoConfiguration, out OAuth2Configuration oAuth2Configuration)
		{
			// Sanity.
			if (null == pluginInfoConfiguration)
				throw new ArgumentNullException(nameof(pluginInfoConfiguration));

			// Is OAuth 2.0 specified?
			oAuth2Configuration = pluginInfoConfiguration
				.Where(pic => pic.Protocol == IEnumerablePluginInfoConfigurationExtensionMethods.OAuth2PluginConfigurationProtocol)
				.Select(pic => OAuth2Configuration.ParseFrom(pic.Configuration))
				.FirstOrDefault();

			// Did we get a value?
			return oAuth2Configuration != null;

		}
	}
}
