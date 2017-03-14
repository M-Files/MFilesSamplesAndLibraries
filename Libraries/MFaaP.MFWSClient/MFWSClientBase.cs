using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public abstract partial class MFWSClientBase
	{
		/// <summary>
		/// Occurs before a request is executed.
		/// </summary>
		public event EventHandler<IRestRequest> BeforeExecuteRequest;

		/// <summary>
		/// Occurs after a request is executed.
		/// </summary>
		public event EventHandler<IRestResponse> AfterExecuteRequest;

		/// <summary>
		/// This is the RestClient which will do the actual requests.
		/// </summary>
		private readonly IRestClient restClient;

		/// <summary>
		/// Returns the default parameters for requests.
		/// </summary>
		public IList<Parameter> DefaultParameters => this.restClient.DefaultParameters;

		/// <summary>
		/// The cookie container used for requests.
		/// </summary>
		public CookieContainer CookieContainer
		{
			get { return this.restClient.CookieContainer; }
			set { this.restClient.CookieContainer = value; }
		}

		/// <summary>
		/// Creates an MFWSClient pointing at the MFWA site.
		/// </summary>
		/// <param name="baseUrl">The base url of the MFWA (web access) site; note that this should be of the form
		/// "http://localhost", not of the form "http://localhost/REST".</param>
		protected MFWSClientBase(string baseUrl)
		{
			// Set up the RestClient.
			this.restClient = new RestClient(baseUrl)
			{
				FollowRedirects = true,
				PreAuthenticate = true
			};
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
			System.Diagnostics.Debug.WriteLine($"Executing {e.Method} request to {e.Resource} with body {e.Parameters}.");
			BeforeExecuteRequest?.Invoke(this, e);
		}

		/// <summary>
		/// Notifies any subscribers of <see cref="AfterExecuteRequest"/>
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnAfterExecuteRequest(IRestResponse e)
		{
			System.Diagnostics.Debug.WriteLine($"{e.StatusCode} received from {e.ResponseUri}: {e.Content}");
			AfterExecuteRequest?.Invoke(this, e);
		}
	}
}