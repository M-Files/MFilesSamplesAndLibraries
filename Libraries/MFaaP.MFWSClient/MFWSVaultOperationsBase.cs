using System;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Vault operations base class.
	/// </summary>
	public abstract class MFWSVaultOperationsBase
	{
		/// <summary>
		/// The <see cref="MFWSClientBase"/> that this object uses to interact with the server.
		/// </summary>
		protected MFWSClientBase MFWSClient { get; private set; }

		/// <summary>
		/// Creates a new <see cref="MFWSVaultOperationsBase"/> object.
		/// </summary>
		/// <param name="client">The client to interact with the server.</param>
		internal MFWSVaultOperationsBase(MFWSClientBase client)
		{
			// Sanity.
			if (null == client)
				throw new ArgumentNullException(nameof(client));
			this.MFWSClient = client;
		}
	}
}
