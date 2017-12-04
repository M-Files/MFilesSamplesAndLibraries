using System;
using System.Net;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	/// <summary>
	/// Describes the expected behaviour of a REST API endpoint.
	/// </summary>
	public class RestApiTestAttribute
		: Attribute
	{
		/// <summary>
		/// The HTTP method expected.
		/// </summary>
		public RestSharp.Method ExpectedMethod { get; set; }

		/// <summary>
		/// The resource that should be requested.
		/// </summary>
		public string ExpectedResourceAddress { get; set; }

		/// <summary>
		/// The type of the response body.
		/// </summary>
		public Type ResponseBodyType { get; set; }

		/// <summary>
		/// The HTTP response code to provide.
		/// </summary>
		public HttpStatusCode ResponseStatusCode { get; set; }
			= HttpStatusCode.OK;

		/// <summary>
		/// The expected request format (used for formatting the body, if required).
		/// </summary>
		public DataFormat? ExpectedRequestFormat { get; set; }

		/// <summary>
		/// The request body which is expected to be passed.
		/// </summary>
		public Parameter ExpectedRequestBody { get; set; }

		public RestApiTestAttribute(RestSharp.Method expectedMethod,
			string expectedResourceAddress,
			Type responseBodyType)
		{
			this.ExpectedMethod = expectedMethod;
			this.ExpectedResourceAddress = expectedResourceAddress;
			this.ResponseBodyType = responseBodyType;
		}
	}
}