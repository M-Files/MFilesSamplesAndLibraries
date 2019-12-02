using RestSharp.Deserializers;

namespace MFaaP.MFWSClient.OAuth2
{
	/// <summary>
	/// Represents a set of tokens returned from an OAuth 2.0 token endpoint.
	/// The <see cref="TokenType"/> should be "Bearer", and the <see cref="AccessToken"/> can be used
	/// to access resources.  Should this expire, a subsequent request can be made using the <see cref="RefreshToken"/>
	/// to retrieve an updated token.
	/// </summary>
	public class OAuth2TokenResponse
	{
		/// <summary>
		/// The token type.  Should be "Bearer".
		/// </summary>
		[DeserializeAs(Name = "token_type")]
		public string TokenType { get; set; }

		/// <summary>
		/// Any scopes defined for the tokens.
		/// </summary>
		[DeserializeAs(Name = "scope")]
		public string Scope { get; set; }

		/// <summary>
		/// How long - since the original request - the access token is valid for.
		/// </summary>
		[DeserializeAs(Name = "expires_in")]
		public long ExpiresIn { get; set; }

		//[DeserializeAs(Name = "ext_expires_in")]
		//public long ExtExpiresIn { get; set; }

		/// <summary>
		/// When the access token expires.
		/// </summary>
		[DeserializeAs(Name = "expires_on")]
		public long ExpiresOn { get; set; }

		/// <summary>
		/// When token becomes valid.
		/// </summary>

		[DeserializeAs(Name = "not_before")]
		public long NotBefore { get; set; }

		/// <summary>
		/// The resource this token is for.
		/// </summary>
		[DeserializeAs(Name = "resource")]
		public string Resource { get; set; }

		/// <summary>
		/// The access token.
		/// </summary>
		[DeserializeAs(Name = "access_token")]
		public string AccessToken { get; set; }

		/// <summary>
		/// The refresh token.
		/// </summary>
		[DeserializeAs(Name = "refresh_token")]
		public string RefreshToken { get; set; }

		/// <summary>
		/// The ID token.
		/// </summary>
		[DeserializeAs(Name = "id_token")]
		public string IdToken { get; set; }

	}
}
