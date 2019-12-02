using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RestSharp;

namespace MFaaP.MFWSClient.ExtensionMethods
{
	/// <summary>
	/// REST request extension methods.
	/// </summary>
	public static class RestRequestExtensionMethods
	{
		/// <summary>
		/// Calls <see cref="RestRequest.AddParameter(string, object)"/> if the <see paramref="value"/>
		/// is not null or whitespace.
		/// </summary>
		/// <param name="request">The request to add the parameter to.</param>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <returns>The request, for chaining.</returns>
		public static RestRequest AddParameterIfNotNullOrWhitespace(this RestRequest request, string name, string value)
		{
			// Call the other method if appropriate.
			if (false == string.IsNullOrWhiteSpace(value))
				request.AddParameter(name, value);

			// Return the request for chaining.
			return request;
		}
	}
}
