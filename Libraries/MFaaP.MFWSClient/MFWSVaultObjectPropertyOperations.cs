using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Methods to modify object properties.
	/// </summary>
	public class MFWSVaultObjectPropertyOperations
		: MFWSVaultOperationsBase
	{
		/// <summary>
		/// Creates a new <see cref="MFWSVaultObjectPropertyOperations"/> object.
		/// </summary>
		/// <param name="client">The client to interact with the server.</param>
		internal MFWSVaultObjectPropertyOperations(MFWSClientBase client)
			: base(client)
		{
		}

		#region Get properties of objects

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objVers">The objects to retrieve the properties of.</param>
		/// <returns>A collection of property values, one for each object version provided in <see cref="objVers"/>.</returns>
		public Task<PropertyValue[][]> GetPropertiesOfMultipleObjects(params ObjVer[] objVers)
		{
			return this.GetPropertiesOfMultipleObjects(CancellationToken.None, objVers);
		}

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objVers">The objects to retrieve the properties of.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values, one for each object version provided in <see cref="objVers"/>.</returns>
		public async Task<PropertyValue[][]> GetPropertiesOfMultipleObjects(CancellationToken token, params ObjVer[] objVers)
		{
			// Sanity.
			if (null == objVers)
				return new PropertyValue[0][];

			// Create the request.
			var request = new RestRequest("/REST/objects/properties");

			// Add the content to the request.
			foreach (var objVer in objVers)
			{
				request.Resource += $";{objVer.Type}/{objVer.ID}/{objVer.Version}";
			}

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<List<PropertyValue>>>(request, token);

			// Return the data.
			return response.Data?.Select(a => a.ToArray()).ToArray();

		}

		/// <summary>
		/// Retrieves the properties of a single object.
		/// </summary>
		/// <param name="objVer">The object to retrieve the properties of.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values for the supplied object.</returns>
		public async Task<PropertyValue[]> GetProperties(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Use the other overload to retrieve the content.
			var response = await this.GetPropertiesOfMultipleObjects(token, new[] { objVer });

			// Sanity.
			return null != response && response.Length > 0
				? new PropertyValue[0] 
				: response[0];
		}

		#endregion

	}
	
}
