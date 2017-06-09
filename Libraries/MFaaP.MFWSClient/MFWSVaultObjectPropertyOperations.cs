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
		public Task<PropertyValue[][]> GetPropertiesOfMultipleObjectsAsync(params ObjVer[] objVers)
		{
			return this.GetPropertiesOfMultipleObjectsAsync(CancellationToken.None, objVers);
		}

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objVers">The objects to retrieve the properties of.</param>
		/// <returns>A collection of property values, one for each object version provided in <see cref="objVers"/>.</returns>
		public PropertyValue[][] GetPropertiesOfMultipleObjects(params ObjVer[] objVers)
		{
			// Execute the async method.
			return this.GetPropertiesOfMultipleObjectsAsync(objVers)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objVers">The objects to retrieve the properties of.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values, one for each object version provided in <see cref="objVers"/>.</returns>
		public async Task<PropertyValue[][]> GetPropertiesOfMultipleObjectsAsync(CancellationToken token, params ObjVer[] objVers)
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
			var response = await this.MFWSClient.Get<List<List<PropertyValue>>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data?.Select(a => a.ToArray()).ToArray();

		}

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objVers">The objects to retrieve the properties of.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values, one for each object version provided in <see cref="objVers"/>.</returns>
		public PropertyValue[][] GetPropertiesOfMultipleObjects(CancellationToken token, params ObjVer[] objVers)
		{
			// Execute the async method.
			return this.GetPropertiesOfMultipleObjectsAsync(token, objVers)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();

		}

		/// <summary>
		/// Retrieves the properties of a single object.
		/// </summary>
		/// <param name="objVer">The object to retrieve the properties of.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values for the supplied object.</returns>
		public async Task<PropertyValue[]> GetPropertiesAsync(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Use the other overload to retrieve the content.
			var response = await this.GetPropertiesOfMultipleObjectsAsync(token, new[] { objVer })
				.ConfigureAwait(false);

			// Sanity.
			return null != response && response.Length > 0
				? new PropertyValue[0]
				: response[0];
		}

		/// <summary>
		/// Retrieves the properties of a single object.
		/// </summary>
		/// <param name="objVer">The object to retrieve the properties of.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values for the supplied object.</returns>
		public PropertyValue[] GetProperties(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetPropertiesAsync(objVer, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

		#region Set property

		/// <summary>
		/// Sets a single property on a single object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="newPropertyValue">The new (or updated) property vlaue.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public async Task<ExtendedObjectVersion> SetPropertyAsync(int objectTypeId, int objectId, PropertyValue newPropertyValue, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");
			if (objectId <= 0)
				throw new ArgumentException("The object id cannot be less than or equal to zero");
			if (null == newPropertyValue)
				throw new ArgumentNullException(nameof(newPropertyValue));
			if (newPropertyValue.PropertyDef < 0)
				throw new ArgumentException("The property definition is invalid", nameof(newPropertyValue));

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/{version?.ToString() ?? "latest"}/properties/{newPropertyValue.PropertyDef}");
			request.Method = Method.PUT;

			// Set the request body.
			request.AddJsonBody(newPropertyValue);

			// Make the request and get the response.
			var response = await this.MFWSClient.Put<ExtendedObjectVersion>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Sets a single property on a single object.
		/// </summary>
		/// <param name="objVer">The object to set the property on.</param>
		/// <param name="newPropertyValue">The new (or updated) property vlaue.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public Task<ExtendedObjectVersion> SetPropertyAsync(ObjVer objVer, PropertyValue newPropertyValue, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.SetPropertyAsync(objVer.Type, objVer.ID, newPropertyValue, objVer.Version, token);
		}

		/// <summary>
		/// Sets a single property on a single object.
		/// </summary>
		/// <param name="objId">The object to set the property on.</param>
		/// <param name="newPropertyValue">The new (or updated) property vlaue.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public Task<ExtendedObjectVersion> SetPropertyAsync(ObjID objId, PropertyValue newPropertyValue, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.SetPropertyAsync(objId.Type, objId.ID, newPropertyValue, null, token);
		}

		/// <summary>
		/// Retrieves the properties of a single object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="newPropertyValue">The new (or updated) property vlaue.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public ExtendedObjectVersion SetProperty(int objectTypeId, int objectId, PropertyValue newPropertyValue, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.SetPropertyAsync(objectTypeId, objectId, newPropertyValue, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves the properties of a single object.
		/// </summary>
		/// <param name="objVer">The object to retrieve the properties of.</param>
		/// <param name="newPropertyValue">The new (or updated) property vlaue.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public ExtendedObjectVersion SetProperty(ObjVer objVer, PropertyValue newPropertyValue, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.SetPropertyAsync(objVer, newPropertyValue, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves the properties of a single object.
		/// </summary>
		/// <param name="objId">The object to set the property on.</param>
		/// <param name="newPropertyValue">The new (or updated) property vlaue.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public ExtendedObjectVersion SetProperty(ObjID objId, PropertyValue newPropertyValue, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.SetPropertyAsync(objId, newPropertyValue, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

		#region Remove property

		/// <summary>
		/// Removes a single property from a single object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="propertyDef">The property to remove.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public async Task<ExtendedObjectVersion> RemovePropertyAsync(int objectTypeId, int objectId, int propertyDef, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");
			if (objectId <= 0)
				throw new ArgumentException("The object id cannot be less than or equal to zero");
			if (propertyDef < 0)
				throw new ArgumentException("The property definition is invalid", nameof(propertyDef));

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/{version?.ToString() ?? "latest"}/properties/{propertyDef}");
			request.Method = Method.DELETE;

			// Make the request and get the response.
			var response = await this.MFWSClient.Delete<ExtendedObjectVersion>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Removes a single property from a single object.
		/// </summary>
		/// <param name="objVer">The object to set the property on.</param>
		/// <param name="propertyDef">The property to remove.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public Task<ExtendedObjectVersion> RemovePropertyAsync(ObjVer objVer, int propertyDef, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.RemovePropertyAsync(objVer.Type, objVer.ID, propertyDef, objVer.Version, token);
		}

		/// <summary>
		/// Removes a single property from a single object.
		/// </summary>
		/// <param name="objId">The object to set the property on.</param>
		/// <param name="propertyDef">The property to remove.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public Task<ExtendedObjectVersion> RemovePropertyAsync(ObjID objId, int propertyDef, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.RemovePropertyAsync(objId.Type, objId.ID, propertyDef, null, token);
		}

		/// <summary>
		/// Removes a single property from a single object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="propertyDef">The property to remove.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public ExtendedObjectVersion RemoveProperty(int objectTypeId, int objectId, int propertyDef, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.RemovePropertyAsync(objectTypeId, objectId, propertyDef, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Removes a single property from a single object.
		/// </summary>
		/// <param name="objVer">The object to retrieve the properties of.</param>
		/// <param name="propertyDef">The property to remove.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public ExtendedObjectVersion RemoveProperty(ObjVer objVer, int propertyDef, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.RemovePropertyAsync(objVer, propertyDef, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Removes a single property from a single object.
		/// </summary>
		/// <param name="objId">The object to set the property on.</param>
		/// <param name="propertyDef">The property to remove.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public ExtendedObjectVersion RemoveProperty(ObjID objId, int propertyDef, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.RemovePropertyAsync(objId, propertyDef, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

	}

}
