using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
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
				case HttpStatusCode.MethodNotAllowed:
					{
						// Parse exception information, if we can.
						var error = MFWSClientBase.jsonDeserializer.Deserialize<WebServiceError>(response);

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
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The response.</returns>
		public async Task<IRestResponse<T>> Get<T>(IRestRequest request, CancellationToken token = default(CancellationToken))
			where T : new()
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			request.Method = Method.GET;

			// We only deal with Json.
			request.RequestFormat = DataFormat.Json;

			// Ensure the extensions headers are specified.
			this.EnsureEnabledExtensionsAreSpecified(request);

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = await this.restClient.ExecuteTaskAsync<T>(request, token)
				.ConfigureAwait(false);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}

		/// <summary>
		/// Executes the request using a "GET" HTTP method.
		/// </summary>
		/// <param name="request">The request to execute.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The response.</returns>
		public async Task<IRestResponse> Get(IRestRequest request, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			request.Method = Method.GET;

			// We only deal with Json.
			request.RequestFormat = DataFormat.Json;

			// Ensure the extensions headers are specified.
			this.EnsureEnabledExtensionsAreSpecified(request);

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = await this.restClient.ExecuteTaskAsync(request, token)
				.ConfigureAwait(false);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}

		/// <summary>
		/// Executes the request using a "POST" HTTP method.
		/// </summary>
		/// <param name="request">The request to execute.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The response.</returns>
		public async Task<IRestResponse> Post(IRestRequest request, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			request.Method = Method.POST;

			// We only deal with Json.
			request.RequestFormat = DataFormat.Json;

			// Ensure the extensions headers are specified.
			this.EnsureEnabledExtensionsAreSpecified(request);

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = await this.restClient.ExecuteTaskAsync(request, token)
				.ConfigureAwait(false);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}

		/// <summary>
		/// Executes the request using a "POST" HTTP method.
		/// </summary>
		/// <typeparam name="T">The expected return type.</typeparam>
		/// <param name="token">A cancellation token for the request.</param>
		/// <param name="request">The request to execute.</param>
		/// <returns>The response.</returns>
		public async Task<IRestResponse<T>> Post<T>(IRestRequest request, CancellationToken token = default(CancellationToken))
			where T : new()
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			request.Method = Method.POST;

			// We only deal with Json.
			request.RequestFormat = DataFormat.Json;

			// Ensure the extensions headers are specified.
			this.EnsureEnabledExtensionsAreSpecified(request);

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = await this.restClient.ExecuteTaskAsync<T>(request, token)
				.ConfigureAwait(false);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}

		/// <summary>
		/// Executes the request using a "DELETE" HTTP method.
		/// </summary>
		/// <param name="request">The request to execute.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The response.</returns>
		public async Task<IRestResponse> Delete(IRestRequest request, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			// Note: we don't set the method to DELETE as this is not supported in some IIS instances.
			request.Method = Method.POST;
			request.AddQueryParameter("_method", "DELETE");

			// We only deal with Json.
			request.RequestFormat = DataFormat.Json;

			// Ensure the extensions headers are specified.
			this.EnsureEnabledExtensionsAreSpecified(request);

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = await this.restClient.ExecuteTaskAsync(request, token)
				.ConfigureAwait(false);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}

		/// <summary>
		/// Executes the request using a "DELETE" HTTP method.
		/// </summary>
		/// <typeparam name="T">The expected return type.</typeparam>
		/// <param name="token">A cancellation token for the request.</param>
		/// <param name="request">The request to execute.</param>
		/// <returns>The response.</returns>
		public async Task<IRestResponse<T>> Delete<T>(IRestRequest request, CancellationToken token = default(CancellationToken))
			where T : new()
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			// Note: we don't set the method to DELETE as this is not supported in some IIS instances.
			request.Method = Method.POST;
			request.AddQueryParameter("_method", "DELETE");

			// We only deal with Json.
			request.RequestFormat = DataFormat.Json;

			// Ensure the extensions headers are specified.
			this.EnsureEnabledExtensionsAreSpecified(request);

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = await this.restClient.ExecuteTaskAsync<T>(request, token)
				.ConfigureAwait(false);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}

		/// <summary>
		/// Executes the request using a "PUT" HTTP method.
		/// </summary>
		/// <param name="request">The request to execute.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The response.</returns>
		public async Task<IRestResponse> Put(IRestRequest request, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			// Note: we don't set the method to PUT as this is not supported in some IIS instances.
			request.Method = Method.POST;
			request.AddQueryParameter("_method", "PUT");

			// We only deal with Json.
			request.RequestFormat = DataFormat.Json;

			// Ensure the extensions headers are specified.
			this.EnsureEnabledExtensionsAreSpecified(request);

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = await this.restClient.ExecuteTaskAsync(request, token)
				.ConfigureAwait(false);

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
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The response.</returns>
		public async Task<IRestResponse<T>> Put<T>(IRestRequest request, CancellationToken token = default(CancellationToken))
			where T : new()
		{
			// Sanity.
			if (null == request)
				throw new ArgumentNullException(nameof(request));

			// Ensure method is correct.
			// Note: we don't set the method to PUT as this is not supported in some IIS instances.
			request.Method = Method.POST;
			request.AddQueryParameter("_method", "PUT");

			// We only deal with Json.
			request.RequestFormat = DataFormat.Json;

			// Ensure the extensions headers are specified.
			this.EnsureEnabledExtensionsAreSpecified(request);

			// Notify before we execute a request.
			this.OnBeforeExecuteRequest(request);

			// Execute the request.
			var response = await this.restClient.ExecuteTaskAsync<T>(request, token)
				.ConfigureAwait(false);

			// Notify after the request.
			this.OnAfterExecuteRequest(response);

			// Return.
			return response;
		}

		/// <summary>
		/// Ensures that extensions specified in <see cref="EnabledMFWSExtensions"/> are
		/// contained within the <see cref="ExtensionsHttpHeaderName"/> HTTP header on the request.
		/// </summary>
		/// <param name="request">The request to alter.</param>
		public virtual void EnsureEnabledExtensionsAreSpecified(IRestRequest request)
		{
			// Sanity.
			if(null == request)
				throw new ArgumentNullException(nameof(request));

			// Shortcut if we can.
			if (this.EnabledMFWSExtensions == MFWSExtensions.None)
				return;

			// Retrieve the current X-Extensions values (comma-separated) as an array.
			// Need to handle various null/empty scenarios here.
			var existingExtensions = ((request.Parameters ?? new List<Parameter>())
										.FirstOrDefault(p =>
											p.Type == ParameterType.HttpHeader
											&& p.Name == MFWSClientBase.ExtensionsHttpHeaderName)?
										.Value as string)?
									.Split(",".ToCharArray())
									.Select(v => v.Trim())?
									.ToList()
									?? new List<string>();

			// Ensure that the ones we want are added.
			foreach (var possibleExtension in Enum.GetValues(typeof(MFWSExtensions)).Cast<MFWSExtensions>())
			{
				// Ignore "none".
				if (possibleExtension == MFWSExtensions.None)
					continue;

				// Have we enabled this extension?
				if (false == this.EnabledMFWSExtensions.HasFlag(possibleExtension))
					continue;

				// Do we need to add it?
				if (false == existingExtensions.Contains(possibleExtension.ToString()))
					existingExtensions.Add(possibleExtension.ToString());
			}

			// Remove the existing header, if it exists.
			request.Parameters?
				.RemoveAll(p => p.Type == ParameterType.HttpHeader && p.Name == MFWSClientBase.ExtensionsHttpHeaderName);

			// Add the header.
			request.Parameters?.Add(new Parameter()
			{
				Type = ParameterType.HttpHeader,
				Name = MFWSClientBase.ExtensionsHttpHeaderName,
				Value = string.Join(",", existingExtensions)
			});
		}
	}
}
