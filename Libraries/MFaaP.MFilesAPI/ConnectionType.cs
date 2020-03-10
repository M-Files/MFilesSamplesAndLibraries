namespace MFaaP.MFilesAPI
{
	/// <summary>
	/// The type of connection to make to the server.
	/// </summary>
	public enum ConnectionType
	{
		/// <summary>
		/// An unknown connection type; will fail if used.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// A connection using TCP/IP.
		/// Equivalent to the protocol sequence "ncacn_ip_tcp" in the API.
		/// </summary>
		TcpIp = 1,

		/// <summary>
		/// A connection using HTTPS.
		/// Equivalent to the protocol squence "ncacn_http" in the API.
		/// </summary>
		Https = 2,

		/// <summary>
		/// A connection using a local interprocess call (LPC).
		/// Equivalent to the protocol squence "ncalrpc" in the API.
		/// </summary>
		Lpc = 3
	}
}