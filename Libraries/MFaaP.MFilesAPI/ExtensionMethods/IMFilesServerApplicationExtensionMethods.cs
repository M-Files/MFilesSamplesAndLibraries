using System;
using System.Globalization;
using MFilesAPI;

namespace MFaaP.MFilesAPI.ExtensionMethods
{
	// ReSharper disable once InconsistentNaming
	/// <summary>
	/// Extension methods for objects implementing <see cref="IMFilesServerApplication"/>.
	/// </summary>
	public static class IMFilesServerApplicationExtensionMethods
	{
		/// <summary>
		/// Connects to the server using the details in the supplied parameters.
		/// </summary>
		/// <param name="serverApplication">The server application to connect with.</param>
		/// <param name="connectionDetails">The connection details to use</param>
		/// <returns>The state of the server connection and authentication.</returns>
		public static MFServerConnection Connect(this IMFilesServerApplication serverApplication,
			ConnectionDetails connectionDetails)
		{
			// Sanity.
			if(null == serverApplication)
				throw new ArgumentNullException(nameof(serverApplication));
			if(null == connectionDetails)
				throw new ArgumentNullException(nameof(connectionDetails));
			if (connectionDetails.ServerDetails.ConnectionType == ConnectionType.Unknown)
				throw new ArgumentException("The connection type was unknown and is unsupported", nameof(connectionDetails));

			// Connect!
			return serverApplication.ConnectEx4(TimeZone: connectionDetails.TimeZoneInformation,
				AuthType: connectionDetails.AuthenticationDetails.AuthenticationType,
				UserName: connectionDetails.AuthenticationDetails.Username,
				Password: connectionDetails.AuthenticationDetails.Password,
				Domain: connectionDetails.AuthenticationDetails.Domain,
				SPN: connectionDetails.AuthenticationDetails.ServicePrincipalName,
				ProtocolSequence: connectionDetails.ServerDetails.ConnectionType.ToProtocolSequence(),
				NetworkAddress: connectionDetails.ServerDetails.NetworkAddress,
				Endpoint: connectionDetails.ServerDetails.EndPoint,
				EncryptedConnection: connectionDetails.ServerDetails.EncryptedConnection,
				LocalComputerName: connectionDetails.LocalComputerName,
				AllowAnonymousConnection: connectionDetails.AllowAnonymousConnection,
				AllowUsingAuthenticationPlugins: connectionDetails.AuthenticationDetails.AllowUsingAuthenticationPlugins,
				LogicalTargetServer: connectionDetails.AuthenticationDetails.LogicalTargetServer,
				ClientCulture: (connectionDetails.ClientCulture ?? CultureInfo.CurrentUICulture).Name);
		}

		/// <summary>
		/// Disconnects from the server and vault.
		/// </summary>
		/// <param name="serverApplication">The server to disconnect from.</param>
		/// <param name="vault">The vault to log out from.</param>
		/// <returns>true if logout and disconnect executed with no exceptions, false otherwise.</returns>
		public static bool Disconnect(this IMFilesServerApplication serverApplication, IVault vault = null)
		{
			try
			{
				vault?.LogOutSilent();
				serverApplication?.Disconnect();
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Converts the connection type enum to a magic string used for the connection
		/// method.
		/// </summary>
		/// <param name="connectionType">The connection type to convert.</param>
		/// <returns>The string.</returns>
		/// <exception cref="ArgumentException">Thrown if the connection type is not supported.</exception>
		private static string ToProtocolSequence(this ConnectionType connectionType)
		{
			switch (connectionType)
			{
				case ConnectionType.TcpIp:
					return "ncacn_ip_tcp";
				case ConnectionType.Https:
					return "ncacn_http";
				case ConnectionType.Lpc:
					return "ncalrpc";
			}
			throw new ArgumentException("The connection type was not supported", nameof(connectionType));
		}

	}
}
