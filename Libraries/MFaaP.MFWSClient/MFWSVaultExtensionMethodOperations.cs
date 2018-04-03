using System;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public class MFWSVaultExtensionMethodOperations
		: MFWSVaultOperationsBase
	{

		/// <summary>
		/// Creates a new <see cref="MFWSVaultObjectOperations"/> object.
		/// </summary>
		/// <param name="client">The client to interact with the server.</param>
		internal MFWSVaultExtensionMethodOperations(MFWSClientBase client)
			: base(client)
		{
		}

		#region Execute extension methods

		/// <summary>
		/// Executes an extension method on the server-side.
		/// </summary>
		/// <typeparam name="TA">The expected response type.</typeparam>
		/// <typeparam name="TB">The type of the item to send.</typeparam>
		/// <param name="extensionMethodName">The name of the extension method.</param>
		/// <param name="input">The input (cannot be null) parameter.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The response of the extension method, deserialised to an instance of <see cref="TA"/>.</returns>
		public async Task<TA> ExecuteVaultExtensionMethodAsync<TA, TB>(string extensionMethodName, TB input = null, CancellationToken token = default(CancellationToken))
			where TA : new()
			where TB : class
		{
			// Create the request.
			var request = new RestRequest($"/REST/vault/extensionmethod/{extensionMethodName}.aspx");

			// If we have a parameter then serialise it.
			if (null != input)
			{
				request.AddJsonBody(input);
			}

			// Make the request and get the response.
			var response = await this.MFWSClient.Post<TA>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Executes an extension method on the server-side.
		/// </summary>
		/// <typeparam name="TA">The expected response type.</typeparam>
		/// <typeparam name="TB">The type of the item to send.</typeparam>
		/// <param name="extensionMethodName">The name of the extension method.</param>
		/// <param name="input">The input (cannot be null) parameter.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The response of the extension method, deserialised to an instance of <see cref="TA"/>.</returns>
		public TA ExecuteVaultExtensionMethod<TA, TB>(string extensionMethodName, TB input = null, CancellationToken token = default(CancellationToken))
			where TA : new()
			where TB : class
		{

			// Execute the async method.
			return this.ExecuteVaultExtensionMethodAsync<TA, TB>(extensionMethodName, input, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();

		}

		/// <summary>
		/// Executes an extension method on the server-side.
		/// </summary>
		/// <typeparam name="TB">The type of the item to send.</typeparam>
		/// <param name="extensionMethodName">The name of the extension method.</param>
		/// <param name="input">The input (cannot be null) parameter.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The response of the extension method.</returns>
		public async Task<string> ExecuteVaultExtensionMethodAsync<TB>(string extensionMethodName, TB input = null, CancellationToken token = default(CancellationToken))
			where TB : class
		{
			// Create the request.
			var request = new RestRequest($"/REST/vault/extensionmethod/{extensionMethodName}.aspx");

			// If we have a parameter then serialise it.
			if (null != input)
			{
				request.AddJsonBody(input);
			}

			// Make the request and get the response.
			var response = await this.MFWSClient.Post(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Content;
		}

		/// <summary>
		/// Executes an extension method on the server-side.
		/// </summary>
		/// <typeparam name="TB">The type of the item to send.</typeparam>
		/// <param name="extensionMethodName">The name of the extension method.</param>
		/// <param name="input">The input (cannot be null) parameter.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The response of the extension method.</returns>
		public string ExecuteVaultExtensionMethod<TB>(string extensionMethodName, TB input = null, CancellationToken token = default(CancellationToken))
			where TB : class
		{
			// Execute the async method.
			return this.ExecuteVaultExtensionMethodAsync<TB>(extensionMethodName, input, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Executes an extension method on the server-side.
		/// </summary>
		/// <param name="extensionMethodName">The name of the extension method.</param>
		/// <param name="input">The input parameter.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The response of the extension method as a string.</returns>
		public async Task<string> ExecuteVaultExtensionMethodAsync(string extensionMethodName, string input = null, CancellationToken token = default(CancellationToken))
		{

			// Create the request.
			var request = new RestRequest($"/REST/vault/extensionmethod/{extensionMethodName}.aspx");

			// If we have a parameter then send it.
			if (null != input)
			{
				// We need to copy the default parameters if we are adding new ones (??).
				request.Parameters.AddRange(this.MFWSClient.DefaultParameters);

				// Add the message body.
				request.Parameters.Add(new Parameter()
				{
					Type = ParameterType.RequestBody,
					Value = input
				});
			}

			// Make the request and get the response.
			var response = await this.MFWSClient.Post(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Content;
		}

		/// <summary>
		/// Executes an extension method on the server-side.
		/// </summary>
		/// <param name="extensionMethodName">The name of the extension method.</param>
		/// <param name="input">The input parameter.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The response of the extension method as a string.</returns>
		public string ExecuteVaultExtensionMethod(string extensionMethodName, string input = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.ExecuteVaultExtensionMethodAsync(extensionMethodName, input, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

	}
	
}
