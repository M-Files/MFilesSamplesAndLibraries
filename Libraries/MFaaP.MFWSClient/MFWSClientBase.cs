using System;
using System.Collections.Generic;
using System.Net;
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
		/// Expected signature for the <see cref="MFWSClientBase.BeforeExecuteRequest"/> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The arguments.</param>
		public delegate void BeforeExecuteRequestHandler(object sender, RestRequestEventArgs e);

		/// <summary>
		/// Occurs before a request is executed.
		/// </summary>
		public event BeforeExecuteRequestHandler BeforeExecuteRequest;
		/// <summary>
		/// Expected signature for the <see cref="MFWSClientBase.AfterExecuteRequest"/> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The arguments.</param>
		public delegate void AfterExecuteRequestHandler(object sender, RestResponseEventArgs e);

		/// <summary>
		/// Occurs after a request is executed.
		/// </summary>
		public event AfterExecuteRequestHandler AfterExecuteRequest;

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
		/// Notifies any subscribers of <see cref="BeforeExecuteRequest"/>.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnBeforeExecuteRequest(IRestRequest e)
		{
#if DEBUG
			// Output the basic request data.
			System.Diagnostics.Debug.WriteLine($"Executing {e.Method} request to {e.Resource}");

			// If we have any parameters then output them.
			if ((e.Parameters?.Count ?? 0) != 0)
			{
				foreach (var parameter in e.Parameters)
				{
					System.Diagnostics.Debug.WriteLine($"\t({parameter.Type}) {parameter.Name} = {parameter.Value} (type: {parameter.ContentType ?? "Unspecified"})");
				}
			}
#endif

			// Notify subscribers.
			BeforeExecuteRequest?.Invoke(this, new RestRequestEventArgs(e));
		}

		/// <summary>
		/// Notifies any subscribers of <see cref="AfterExecuteRequest"/>
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnAfterExecuteRequest(IRestResponse e)
		{
#if DEBUG
			if (null != e)
			{
				System.Diagnostics.Debug.WriteLine($"{e.StatusCode} received from {e.ResponseUri}: {e.Content}");
			}
#endif

			// Notify subscribers.
			AfterExecuteRequest?.Invoke(this, new RestResponseEventArgs(e));

			// If we had an invalid response, throw it.
			this.EnsureValidResponse(e);
		}
	}
}