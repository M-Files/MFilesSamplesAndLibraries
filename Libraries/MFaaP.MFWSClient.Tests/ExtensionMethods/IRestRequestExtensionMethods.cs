using System;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;

namespace MFaaP.MFWSClient.Tests.ExtensionMethods
{
	// ReSharper disable once InconsistentNaming
	internal static class IRestRequestExtensionMethods
	{
		/// <summary>
		/// Extracts the body from the request and attempts to deserialize it to the provided type.
		/// </summary>
		/// <typeparam name="T">The body type.</typeparam>
		/// <param name="request">The request.</param>
		/// <returns>The value, or null.</returns>
		public static T DeserializeBody<T>(this IRestRequest request)
		{
			// Sanity.
			if(null == request)
				throw new ArgumentNullException(nameof(request));

			// Get the body or return null if none found.
			return JsonConvert
				.DeserializeObject<T>(request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value?.ToString() ?? null);
		}

	}
}
