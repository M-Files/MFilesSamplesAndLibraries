using MFilesAPI;

namespace MFaaP.MFilesAPI
{
	/// <summary>
	/// Details used to authenticate with the server.
	/// </summary>
	public sealed class AuthenticationDetails
	{
		/// <summary>
		/// The authentication type to use.
		/// </summary>
		public MFAuthType AuthenticationType { get; private set; }

		/// <summary>
		/// The username, if required, to use for authentication.
		/// </summary>
		public string Username { get; private set; }

		/// <summary>
		/// The password, if required, to use for authentication.
		/// </summary>
		public string Password { get; private set; }

		/// <summary>
		/// The domain, if required, to use for authentication.
		/// </summary>
		public string Domain { get; private set; }

		/// <summary>
		/// The service principal name (SPN), if required, to use for authentication.
		/// </summary>
		public string ServicePrincipalName { get; private set; }

		/// <summary>
		/// Set to true to allow authentication plugins to be used for authentication.
		/// </summary>
		public bool AllowUsingAuthenticationPlugins { get; set; }

		/// <summary>
		/// The name of the computer that the user attempts to connect to.
		/// </summary>
		public string LogicalTargetServer { get; set; }

		/// <summary>
		/// Creates an <see cref="AuthenticationDetails"/> object representing the
		/// given authentication type.
		/// </summary>
		/// <param name="authenticationType">The authentication type to use.</param>
		private AuthenticationDetails(MFAuthType authenticationType)
		{
			this.AuthenticationType = authenticationType;
		}

		/// <summary>
		/// Creates an <see cref="AuthenticationDetails"/> object representing
		/// logging in with the current Windows user.
		/// </summary>
		/// <returns>The authentication object.</returns>
		public static AuthenticationDetails CreateForLoggedOnWindowsUser()
		{
			return new AuthenticationDetails(MFAuthType.MFAuthTypeLoggedOnWindowsUser);
		}

		/// <summary>
		/// Creates an <see cref="AuthenticationDetails"/> object representing
		/// logging in with a specific M-Files user.
		/// </summary>
		/// <returns>The authentication object.</returns>
		public static AuthenticationDetails CreateForSpecificMFilesUser(
			string username,
			string password)
		{
			return new AuthenticationDetails(MFAuthType.MFAuthTypeSpecificMFilesUser)
			{
				Username = username,
				Password = password
			};
		}
		
		/// <summary>
		/// Creates an <see cref="AuthenticationDetails"/> object representing
		/// logging in with a specific Windows user.
		/// </summary>
		/// <returns>The authentication object.</returns>
		public static AuthenticationDetails CreateForSpecificWindowsUser(
			string username, 
			string password,
			string domain = null,
			string servicePrincipalName = null)
		{
			return new AuthenticationDetails(MFAuthType.MFAuthTypeSpecificWindowsUser)
			{
				Username = username,
				Password = password,
				Domain = domain,
				ServicePrincipalName = servicePrincipalName
			};
		}

	}
}