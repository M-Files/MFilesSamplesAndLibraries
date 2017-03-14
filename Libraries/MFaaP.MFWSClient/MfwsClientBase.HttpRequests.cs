using System;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public abstract partial class MFWSClientBase
	{

		/// <summary>
		/// Executes the request using a "GET" HTTP method.
		/// </summary>
		/// <typeparam name="T">The expected return type.</typeparam>
		/// <param name="request">The request to execute.</param>
		/// <returns>The response.</returns>
		public IRestResponse<T> Get<T>(IRestRequest request)
			where T : new()
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			request.Method = Method.GET;

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = this.restClient.Get<T>(request);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}

		/// <summary>
		/// Executes the request using a "GET" HTTP method.
		/// </summary>
		/// <param name="request">The request to execute.</param>
		/// <returns>The response.</returns>
		public IRestResponse Get(IRestRequest request)
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			request.Method = Method.GET;

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = this.restClient.Get(request);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}

		/// <summary>
		/// Executes the request using a "POST" HTTP method.
		/// </summary>
		/// <typeparam name="T">The expected return type.</typeparam>
		/// <param name="request">The request to execute.</param>
		/// <returns>The response.</returns>
		public IRestResponse<T> Post<T>(IRestRequest request)
			where T : new()
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			request.Method = Method.POST;

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = this.restClient.Post<T>(request);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}

		/// <summary>
		/// Executes the request using a "POST" HTTP method.
		/// </summary>
		/// <param name="request">The request to execute.</param>
		/// <returns>The response.</returns>
		public IRestResponse Post(IRestRequest request)
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			request.Method = Method.POST;

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = this.restClient.Post(request);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}
	}
}
