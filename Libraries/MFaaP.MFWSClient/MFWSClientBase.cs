using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// A base class for the M-Files Web Service Client.
	/// Used primarily to force the IRestClient from being directly used within the actual client.
	/// </summary>
	public abstract partial class MFWSClientBase
	{
		/// <summary>
		/// The HTTP header name for the "Accept Language" header.
		/// </summary>
		public const string AcceptLanguageHttpHeaderName = "Accept-Language";

		/// <summary>
		/// The HTTP header for the extensions.
		/// </summary>
		public const string ExtensionsHttpHeaderName = "X-Extensions";

		/// <summary>
		/// Defines the extensions that are enabled via the <see cref="ExtensionsHttpHeaderName"/> HTTP header.
		/// </summary>
		/// <remarks>Some extensions may be required for server functionality to work.
		/// For example: <see cref="MFWSExtensions.IML"/> is required for methods on <see cref="MFWSVaultAutomaticMetadataOperations"/>.</remarks>
		public MFWSExtensions EnabledMFWSExtensions { get; set; }
			= MFWSExtensions.None;

		/// <summary>
		/// This is the RestClient which will do the actual requests.
		/// </summary>
		private readonly IRestClient restClient;

		/// <summary>
		/// Returns the default parameters for requests.
		/// </summary>
		public IList<Parameter> DefaultParameters => this.restClient.DefaultParameters;

		/// <summary>
		/// If true, exceptions returned (e.g. Forbidden) by the web service will be converted
		/// to .NET exceptions and thrown.
		/// </summary>
		public bool ThrowWebServiceResponseExceptions { get; set; } = true;

		/// <summary>
		/// The cookie container used for requests.
		/// </summary>
		public CookieContainer CookieContainer
		{
			get { return this.restClient.CookieContainer; }
			set { this.restClient.CookieContainer = value; }
		}

        /// <summary>
        /// The base rest service url.
        /// </summary>
	    public Uri BaseUrl => this.restClient.BaseUrl;

		/// <summary>
		/// Gets the object search operations interface.
		/// </summary>
		public MFWSVaultObjectSearchOperations ObjectSearchOperations { get; }

		/// <summary>
		/// Gets the object operations interface.
		/// </summary>
		public MFWSVaultObjectOperations ObjectOperations { get; }

		/// <summary>
		/// Gets the object property interface.
		/// </summary>
		public MFWSVaultObjectPropertyOperations ObjectPropertyOperations { get; }

		/// <summary>
		/// Gets the extension method operations interface.
		/// </summary>
		public MFWSVaultExtensionMethodOperations ExtensionMethodOperations { get; }

		/// <summary>
		/// Gets the view operations interface.
		/// </summary>
		public MFWSVaultViewOperations ViewOperations { get; }

		/// <summary>
		/// Gets the view operations interface.
		/// </summary>
		public MFWSVaultValueListItemOperations ValueListItemOperations { get; }

		/// <summary>
		/// Gets the object type operations interface.
		/// </summary>
		public MFWSVaultObjectTypeOperations ObjectTypeOperations { get; }

		/// <summary>
		/// Gets the value list operations interface.
		/// </summary>
		public MFWSVaultValueListOperations ValueListOperations { get; }

		/// <summary>
		/// Gets the object file operations interface.
		/// </summary>
		public MFWSVaultObjectFileOperations ObjectFileOperations { get; }

		/// <summary>
		/// Gets the class operations interface.
		/// </summary>
		public MFWSVaultClassOperations ClassOperations { get; }

		/// <summary>
		/// Gets the property definitions operations interface.
		/// </summary>
		public MFWSVaultPropertyDefOperations PropertyDefOperations { get; }

		/// <summary>
		/// Gets the automatic metadata operations interface.
		/// </summary>
		public MFWSVaultAutomaticMetadataOperations AutomaticMetadataOperations { get; }

		/// <summary>
		/// Gets the workflow operations interface.
		/// </summary>
		public MFWSVaultWorkflowOperations WorkflowOperations { get; }

		/// <summary>
		/// Gets the external object operations interface.
		/// </summary>
		public MFWSVaultExternalObjectOperations ExternalObjectOperations { get; }

		/// <summary>
		/// Gets the extension authentications operations interface.
		/// </summary>
		public MFWSVaultExtensionAuthenticationOperations ExtensionAuthenticationOperations { get; }

		/// <summary>
		/// Creates an MFWSClient pointing at the MFWA site.
		/// </summary>
		/// <param name="restClient">The <see cref="IRestClient"/> to use for HTTP requests.</param>
		protected MFWSClientBase(IRestClient restClient)
		{
			// Sanity.
			if (null == restClient)
				throw new ArgumentNullException(nameof(restClient));

			// Set up the RestClient.
			this.restClient = restClient;

			// Set up our sub-objects.
			this.ObjectSearchOperations = new MFWSVaultObjectSearchOperations(this);
			this.ObjectOperations = new MFWSVaultObjectOperations(this);
			this.ObjectPropertyOperations = new MFWSVaultObjectPropertyOperations(this);
			this.ExtensionMethodOperations = new MFWSVaultExtensionMethodOperations(this);
			this.ViewOperations = new MFWSVaultViewOperations(this);
			this.ValueListItemOperations = new MFWSVaultValueListItemOperations(this);
			this.ObjectTypeOperations = new MFWSVaultObjectTypeOperations(this);
			this.ValueListOperations = new MFWSVaultValueListOperations(this);
			this.ObjectFileOperations = new MFWSVaultObjectFileOperations(this);
			this.ClassOperations = new MFWSVaultClassOperations(this);
			this.PropertyDefOperations = new MFWSVaultPropertyDefOperations(this);
			this.AutomaticMetadataOperations = new MFWSVaultAutomaticMetadataOperations(this);
			this.WorkflowOperations = new MFWSVaultWorkflowOperations(this);
			this.ExternalObjectOperations = new MFWSVaultExternalObjectOperations(this);
			this.ExtensionAuthenticationOperations = new MFWSVaultExtensionAuthenticationOperations(this);
		}

		/// <summary>
		/// Creates an MFWSClient pointing at the MFWA site.
		/// </summary>
		/// <param name="baseUrl">The base url of the MFWA (web access) site; note that this should be of the form
		/// "http://localhost", not of the form "http://localhost/REST".</param>
		protected MFWSClientBase(string baseUrl)
			: this(new RestClient(baseUrl)
			{
				FollowRedirects = true,
				PreAuthenticate = true
			})
		{
		}

		/// <summary>
		/// Adds a default header to requests.
		/// </summary>
		/// <param name="name">The name of the HTTP header.</param>
		/// <param name="value">The value of the HTTP header.</param>
		public void AddDefaultHeader(string name, string value)
		{
			this.restClient.AddDefaultHeader(name, value);
		}

		/// <summary>
		/// Sets the default value for the "Accept-Language"
		/// HTTP header.
		/// </summary>
		/// <param name="acceptLanguages">
		/// The language string to use, in a valid format: http://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.4.
		/// <example>"da, en-gb;q=0.8, en;q=0.7" would mean "I prefer Danish, but will accept British English and other types of English in that order of preference".</example>
		/// </param>
		/// <remarks>Removes any existing "Accept-Language" headers.</remarks>
		public void SetAcceptLanguage(string acceptLanguages)
		{
			// Remove any existing accept-langauge header.
			var existingHeaders = this.DefaultParameters
				.Where(p => p.Type == ParameterType.HttpHeader)
				.Where(p => p.Name == MFWSClientBase.AcceptLanguageHttpHeaderName)
				.ToArray();
			foreach (var existingHeader in existingHeaders)
			{
				this.DefaultParameters.Remove(existingHeader);
			}

			// Sanity.
			if (null == acceptLanguages)
				return;

			// Set the 
			this.AddDefaultHeader(MFWSClientBase.AcceptLanguageHttpHeaderName, acceptLanguages);
		}

		/// <summary>
		/// Sets the default value for the "Accept-Language"
		/// HTTP header.
		/// </summary>
		/// <param name="acceptLanguages">
		/// A collection of language strings to use.  Each string should contain the culture name (any acceptable format), optionally followed by a semi-colon and the weighting.
		/// ref: http://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.4.
		/// <example>"da", "en-gb;q=0.8", "en;q=0.7" would mean "I prefer Danish, but will accept British English and other types of English in that order of preference".</example>
		/// </param>
		/// <remarks>Removes any existing "Accept-Language" headers.</remarks>
		public void SetAcceptLanguage(params string[] acceptLanguages)
		{
			this.SetAcceptLanguage(string.Join(",", acceptLanguages ?? new string[0]));
		}

		/// <summary>
		/// Resolves multiple vault structural aliases to ids at once.
		/// </summary>
		/// <param name="aliasRequest">The collection of aliases to resolve.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Only available in M-Files 12.0.6768.0 upwards.</remarks>
		public async Task<VaultStructureAliasResponse> GetMetadataStructureIDsByAliasesAsync(VaultStructureAliasRequest aliasRequest,
			CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == aliasRequest)
				throw new ArgumentNullException(nameof(aliasRequest));

			// Create the request.
			var request = new RestRequest($"/REST/structure/metadatastructure/itemidbyalias.aspx");

			// Assign the body.
			request.AddJsonBody(aliasRequest);

			// Make the request and get the response.
			var response = await this.Post<VaultStructureAliasResponse>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Resolves multiple vault structural aliases to ids at once.
		/// </summary>
		/// <param name="aliasRequest">The collection of aliases to resolve.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		public VaultStructureAliasResponse GetMetadataStructureIDsByAliases(VaultStructureAliasRequest aliasRequest,
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetMetadataStructureIDsByAliasesAsync(aliasRequest, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

	}
}