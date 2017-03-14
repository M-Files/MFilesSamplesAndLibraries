using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{
		/// <summary>
		/// Executes an extension method on the server-side.
		/// </summary>
		/// <typeparam name="TA">The expected response type.</typeparam>
		/// <typeparam name="TB">The type of the item to send.</typeparam>
		/// <param name="extensionMethodName">The name of the extension method.</param>
		/// <param name="input">The input (cannot be null) parameter.</param>
		/// <returns>The response of the extension method, deserialised to an instance of <see cref="TA"/>.</returns>
		public TA ExecuteExtensionMethod<TA, TB>(string extensionMethodName, TB input = null)
			where TA : new()
			where TB : class
		{
			// Create the request.
			var request = new RestRequest($"/REST/vault/extensionmethod/{extensionMethodName}");

			// If we have a parameter then serialise it.
			if (null != input)
			{
				request.AddJsonBody(input);
			}

			// Make the request and get the response.
			var response = this.Post<TA>(request);

			// Return the data.
			return response.Data;
		}
		/// <summary>
		/// Executes an extension method on the server-side.
		/// </summary>
		/// <typeparam name="TB">The type of the item to send.</typeparam>
		/// <param name="extensionMethodName">The name of the extension method.</param>
		/// <param name="input">The input (cannot be null) parameter.</param>
		/// <returns>The response of the extension method, deserialised to an instance of <see cref="TA"/>.</returns>
		public string ExecuteExtensionMethod<TB>(string extensionMethodName, TB input = null)
			where TB : class
		{
			// Create the request.
			var request = new RestRequest($"/REST/vault/extensionmethod/{extensionMethodName}");

			// If we have a parameter then serialise it.
			if (null != input)
			{
				request.AddJsonBody(input);
			}

			// Make the request and get the response.
			var response = this.Post(request);

			// Return the data.
			return response.Content;
		}

		/// <summary>
		/// Executes an extension method on the server-side.
		/// </summary>
		/// <param name="extensionMethodName">The name of the extension method.</param>
		/// <param name="input">The input parameter.</param>
		/// <returns>The response of the extension method as a string.</returns>
		public string ExecuteExtensionMethod(string extensionMethodName, string input = null)
		{

			// Create the request.
			var request = new RestRequest($"/REST/vault/extensionmethod/{extensionMethodName}");

			// If we have a parameter then send it.
			if (null != input)
			{
				// We need to copy the default parameters if we are adding new ones (??).
				request.Parameters.AddRange(this.DefaultParameters);

				// Add the message body.
				request.Parameters.Add(new Parameter()
				{
					Type = ParameterType.RequestBody,
					Value = input
				});
			}

			// Make the request and get the response.
			var response = this.Post(request);

			// Return the data.
			return response.Content;
		}

	}
	
}
