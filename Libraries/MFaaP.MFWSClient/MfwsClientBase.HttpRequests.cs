using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace MFaaP.MFWSClient
{
	public abstract partial class MFWSClientBase
	{
		/// <summary>
		/// The deserialiser used for deserialising exception data.
		/// </summary>
		private static readonly JsonDeserializer jsonDeserializer = new JsonDeserializer();

		/// <summary>
		/// Handles any exceptions returned by the web service, throwing exceptions as needed.
		/// </summary>
		/// <param name="response">The response returned by the web service.</param>
		protected virtual void EnsureValidResponse(IRestResponse response)
		{
			// Sanity.
			if (null == response)
				throw new ArgumentNullException(nameof(response));

			// Ensure we're supposed to throw exceptions.
			if (false == this.ThrowWebServiceResponseExceptions)
			{
				return;
			}

			// If there was a protocol exception, throw it.
			if (null != response.ErrorException)
				throw response.ErrorException;
			
			// If it's okay then die.
			if(response.StatusCode == HttpStatusCode.OK)
			{
				return;
			}

			// Look at the HTTP response code.
			switch (response.StatusCode)
			{

				// Handle "expected" exceptions.
				case HttpStatusCode.InternalServerError:
				case HttpStatusCode.BadRequest:
				case HttpStatusCode.Forbidden:
					{
						// Parse exception information, if we can.
						var error = jsonDeserializer.Deserialize<WebServiceError>(response);

						// TODO: Stack doesn't seem to be deserialised properly.

						// If we can, throw detailed data.
						throw (null == error)
							? new Exception(response.Content)
							: (Exception)error;
					}

				// Anything else is unhandled at the moment.
				default:
				{
					break;
				}
			}
		}

		/// <summary>
		/// Executes the request using a "GET" HTTP method.
		/// </summary>
		/// <typeparam name="T">The expected return type.</typeparam>
		/// <param name="request">The request to execute.</param>
		/// <returns>The response.</returns>
		public async Task<IRestResponse<T>> Get<T>(IRestRequest request)
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
			var response = await this.restClient.ExecuteTaskAsync<T>(request);

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
		public async Task<IRestResponse> Get(IRestRequest request)
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			request.Method = Method.GET;

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = await this.restClient.ExecuteTaskAsync(request);

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
		public async Task<IRestResponse<T>> Post<T>(IRestRequest request)
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
			var response = await this.restClient.ExecuteTaskAsync<T>(request);

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
		public async Task<IRestResponse> Post(IRestRequest request)
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			request.Method = Method.POST;

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = await this.restClient.ExecuteTaskAsync(request);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}

		/// <summary>
		/// Executes the request using a "PUT" HTTP method.
		/// </summary>
		/// <typeparam name="T">The expected return type.</typeparam>
		/// <param name="request">The request to execute.</param>
		/// <returns>The response.</returns>
		public async Task<IRestResponse<T>> Put<T>(IRestRequest request)
			where T : new()
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			request.Method = Method.PUT;

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = await this.restClient.ExecuteTaskAsync<T>(request);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}

		/// <summary>
		/// Executes the request using a "PUT" HTTP method.
		/// </summary>
		/// <param name="request">The request to execute.</param>
		/// <returns>The response.</returns>
		public async Task<IRestResponse> Put(IRestRequest request)
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			request.Method = Method.PUT;

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = await this.restClient.ExecuteTaskAsync(request);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}
	}
}
