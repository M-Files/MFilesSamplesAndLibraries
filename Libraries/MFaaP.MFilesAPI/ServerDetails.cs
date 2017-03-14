namespace MFaaP.MFilesAPI
{
	/// <summary>
	/// The details of the server to connect to.
	/// </summary>
	public sealed class ServerDetails
	{
		/// <summary>
		/// The connection type to use to connect to the server.
		/// </summary>
		public ConnectionType ConnectionType { get; private set; }

		/// <summary>
		/// The network address (typically IP address or DNS name) to connect to.
		/// </summary>
		public string NetworkAddress { get; private set; }

		/// <summary>
		/// The end point (typically port number) to connect to.
		/// </summary>
		public string EndPoint { get; private set; }

		/// <summary>
		/// Specifies whether to encrypt data between the M-Files client service and the M-Files Server service,
		/// if the protocol allows.
		/// </summary>
		public bool EncryptedConnection { get; set; }

		/// <summary>
		/// Creates a <see cref="ServerDetails"/> object representing
		/// the given connection type.
		/// </summary>
		/// <param name="connectionType">The connection type to use.</param>
		public ServerDetails(ConnectionType connectionType)
		{
			this.ConnectionType = connectionType;
		}

		/// <summary>
		/// Creates an <see cref="ServerDetails"/> object representing
		/// connecting to a server via TCP/IP.
		/// </summary>
		/// <param name="serverName">The server name (or IP address) to connect to.</param>
		/// <param name="port">The port the server is listening on.</param>
		/// <returns>The server details object.</returns>
		public static ServerDetails CreateForTcpIp(string serverName = "localhost", int port = 2266)
		{
			return new ServerDetails(ConnectionType.TcpIp)
			{
				NetworkAddress = serverName,
				EndPoint = port.ToString()
			};
		}

		/// <summary>
		/// Creates an <see cref="ServerDetails"/> object representing
		/// connecting to a server via HTTPS.
		/// </summary>
		/// <param name="webAddress">The server name (or IP address) to connect to.</param>
		/// <param name="port">The port the server is listening on.</param>
		/// <returns>The server details object.</returns>
		public static ServerDetails CreateForHttps(string webAddress, int port = 4466)
		{
			return new ServerDetails(ConnectionType.Https)
			{
				NetworkAddress = webAddress,
				EndPoint = port.ToString()
			};
		}

		/// <summary>
		/// Creates a <see cref="ServerDetails"/> object to connect using a
		/// local interprocess call (LPC).
		/// </summary>
		/// <returns>The server details.</returns>
		/// <remarks>This overload will connect to the same version of the M-Files Server as the M-Files API version being used.</remarks>
		public static ServerDetails CreateForLpc()
		{
			return new ServerDetails(ConnectionType.Lpc);
		}
		
		/// <summary>
		/// Creates a <see cref="ServerDetails"/> object to connect using a
		/// local interprocess call (LPC).
		/// </summary>
		/// <returns>The server details.</returns>
		/// <remarks>This overload will connect the local M-Files Server instance regardless of its version.  If multiple versions are running
		/// then the first instance started will be used.</remarks>
		public static ServerDetails CreateForLpcInvariantVersion()
		{
			return new ServerDetails(ConnectionType.Lpc)
			{
				EndPoint = "MFServerCommon_F5EE352D-6A03-4866-9988-C69CEA2C39BF"
			};
		}
	}
}