using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

		#region Get properties of multiple objects

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objVers">The objects to retrieve the properties of.</param>
		/// <returns>A collection of property values, one for each object version provided in <see paramref="objVers"/>.</returns>
		public Task<PropertyValue[][]> GetPropertiesOfMultipleObjectsAsync(params ObjVer[] objVers)
		{
			return this.GetPropertiesOfMultipleObjectsAsync(CancellationToken.None, objVers);
		}

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objVers">The objects to retrieve the properties of.</param>
		/// <returns>A collection of property values, one for each object version provided in <see paramref="objVers"/>.</returns>
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
		/// <returns>A collection of property values, one for each object version provided in <see paramref="objVers"/>.</returns>
		public async Task<PropertyValue[][]> GetPropertiesOfMultipleObjectsAsync(CancellationToken token, params ObjVer[] objVers)
		{
			// Sanity.
			if (null == objVers || objVers.Length == 0)
				return new PropertyValue[0][];
			foreach (var objVer in objVers)
			{
				if (objVer.Version < 1)
					throw new ArgumentException("The object version must be greater than zero.");
			}

			// Create the request.
			var request = new RestRequest("/REST/objects/properties.aspx");

			// Add the body.
			request.AddJsonBody(objVers);

			// Make the request and get the response.
			var response = await this.MFWSClient.Post<List<List<PropertyValue>>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data?.Select(a => a.ToArray()).ToArray();

		}

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objVers">The objects to retrieve the properties of.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values, one for each object version provided in <see paramref="objVers"/>.</returns>
		public PropertyValue[][] GetPropertiesOfMultipleObjects(CancellationToken token, params ObjVer[] objVers)
		{
			// Execute the async method.
			return this.GetPropertiesOfMultipleObjectsAsync(token, objVers)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();

		}

		#endregion

		#region Get properties of single objects

		/// <summary>
		/// Retrieves the properties of a single object.  Returns the properties on the latest version of the object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values for the supplied object.</returns>
		public PropertyValue[] GetProperties(int objectTypeId, int objectId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetPropertiesAsync(objectTypeId, objectId, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves the properties of a single object.  Returns the properties on the latest version of the object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values for the supplied object.</returns>
		public Task<PropertyValue[]> GetPropertiesAsync(int objectTypeId, int objectId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Use the other method.
			return this.GetPropertiesAsync(new ObjVer()
			{
				Type = objectTypeId,
				ID = objectId,
				Version = version ?? -1
			}, token);
		}

		/// <summary>
		/// Retrieves the properties of a single object.  Returns the properties on the latest version of the object.
		/// </summary>
		/// <param name="objID">The object to retrieve the properties of.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values for the supplied object.</returns>
		public PropertyValue[] GetProperties(ObjID objID, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetPropertiesAsync(objID, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves the properties of a single object.  Returns the properties on the latest version of the object.
		/// </summary>
		/// <param name="objID">The object to retrieve the properties of.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values for the supplied object.</returns>
		public Task<PropertyValue[]> GetPropertiesAsync(ObjID objID, CancellationToken token = default(CancellationToken))
		{
			// Use the other method.
			return this.GetPropertiesAsync(objID.Type, objID.ID, version: null, token: token);
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

			// Extract the URI elements.
			int objectTypeId;
			string objectId, objectVersionId;
			objVer.GetUriParameters(out objectTypeId, out objectId, out objectVersionId);

			// Create the request.
			string resource = $"/REST/objects/{objectTypeId}/{objectId}/{objectVersionId}/properties.aspx";

			var request = new RestRequest(resource);

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<PropertyValue>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data?.ToArray();
		}

		#endregion

		#region Set (single) property

		/// <summary>
		/// Sets a single property on a single object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="newPropertyValue">The new (or updated) property value.</param>
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
		/// <param name="newPropertyValue">The new (or updated) property value.</param>
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
		/// <param name="newPropertyValue">The new (or updated) property value.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>The object must be checked out to perform this action.</remarks>
		public Task<ExtendedObjectVersion> SetPropertyAsync(ObjID objId, PropertyValue newPropertyValue, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.SetPropertyAsync(objId.Type, objId.ID, newPropertyValue, null, token);
		}

		/// <summary>
		/// Sets a single property on a single object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="newPropertyValue">The new (or updated) property value.</param>
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
		/// Sets a single property on a single object.
		/// </summary>
		/// <param name="objVer">The object to retrieve the properties of.</param>
		/// <param name="newPropertyValue">The new (or updated) property value.</param>
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
		/// Sets a single property on a single object.
		/// </summary>
		/// <param name="objId">The object to set the property on.</param>
		/// <param name="newPropertyValue">The new (or updated) property value.</param>
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

		#region Set (multiple) properties

		/// <summary>
		/// Set multiple properties on a single object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="propertyValues">The property values for the object.</param>
		/// <param name="replaceAllProperties">If true, <see paramref="propertyValues"/> contains all properties for the object (and others will be removed).</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>If <see paramref="replaceAllProperties"/> is true then <see paramref="propertyValues"/> must contain values for property 0 (name or title), property 100 (class), property 22 (is single file), and any other mandatory properties for the given class.</remarks>
		public async Task<ExtendedObjectVersion> SetPropertiesAsync(int objectTypeId, int objectId, PropertyValue[] propertyValues, bool replaceAllProperties, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");
			if (objectId <= 0)
				throw new ArgumentException("The object id cannot be less than or equal to zero");
			if (null == propertyValues)
				throw new ArgumentNullException(nameof(propertyValues));

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/{version?.ToString() ?? "latest"}/properties");
			request.Method = replaceAllProperties
				? Method.PUT // If we wish to replace all properties then we need to use a PUT.
				: Method.POST; // A POST will add or replace just the properties provided.

			// Set the request body.
			request.AddJsonBody(propertyValues);

			// Make the request and get the response.
			var response = replaceAllProperties
				? await this.MFWSClient.Put<ExtendedObjectVersion>(request, token).ConfigureAwait(false)
				: await this.MFWSClient.Post<ExtendedObjectVersion>(request, token).ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Set multiple properties on a single object.
		/// </summary>
		/// <param name="objVer">The object to set the property on.</param>
		/// <param name="propertyValues">The property values for the object.</param>
		/// <param name="replaceAllProperties">If true, <see paramref="propertyValues"/> contains all properties for the object (and others will be removed).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>If <see paramref="replaceAllProperties"/> is true then <see paramref="propertyValues"/> must contain values for property 0 (name or title), property 100 (class), property 22 (is single file), and any other mandatory properties for the given class.</remarks>
		public Task<ExtendedObjectVersion> SetPropertiesAsync(ObjVer objVer, PropertyValue[] propertyValues, bool replaceAllProperties, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.SetPropertiesAsync(objVer.Type, objVer.ID, propertyValues, replaceAllProperties, objVer.Version, token);
		}

		/// <summary>
		/// Set multiple properties on a single object.
		/// </summary>
		/// <param name="objId">The object to set the property on.</param>
		/// <param name="propertyValues">The property values for the object.</param>
		/// <param name="replaceAllProperties">If true, <see paramref="propertyValues"/> contains all properties for the object (and others will be removed).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>If <see paramref="replaceAllProperties"/> is true then <see paramref="propertyValues"/> must contain values for property 0 (name or title), property 100 (class), property 22 (is single file), and any other mandatory properties for the given class.</remarks>
		public Task<ExtendedObjectVersion> SetPropertiesAsync(ObjID objId, PropertyValue[] propertyValues, bool replaceAllProperties, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.SetPropertiesAsync(objId.Type, objId.ID, propertyValues, replaceAllProperties, null, token);
		}

		/// <summary>
		/// Set multiple properties on a single object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="propertyValues">The property values for the object.</param>
		/// <param name="replaceAllProperties">If true, <see paramref="propertyValues"/> contains all properties for the object (and others will be removed).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>If <see paramref="replaceAllProperties"/> is true then <see paramref="propertyValues"/> must contain values for property 0 (name or title), property 100 (class), property 22 (is single file), and any other mandatory properties for the given class.</remarks>
		public ExtendedObjectVersion SetProperties(int objectTypeId, int objectId, PropertyValue[] propertyValues, bool replaceAllProperties, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.SetPropertiesAsync(objectTypeId, objectId, propertyValues, replaceAllProperties, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Set multiple properties on a single object.
		/// </summary>
		/// <param name="objVer">The object to retrieve the properties of.</param>
		/// <param name="propertyValues">The property values for the object.</param>
		/// <param name="replaceAllProperties">If true, <see paramref="propertyValues"/> contains all properties for the object (and others will be removed).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>If <see paramref="replaceAllProperties"/> is true then <see paramref="propertyValues"/> must contain values for property 0 (name or title), property 100 (class), property 22 (is single file), and any other mandatory properties for the given class.</remarks>
		public ExtendedObjectVersion SetProperties(ObjVer objVer, PropertyValue[] propertyValues, bool replaceAllProperties, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.SetPropertiesAsync(objVer, propertyValues, replaceAllProperties, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Set multiple properties on a single object.
		/// </summary>
		/// <param name="objId">The object to set the property on.</param>
		/// <param name="propertyValues">The property values for the object.</param>
		/// <param name="replaceAllProperties">If true, <see paramref="propertyValues"/> contains all properties for the object (and others will be removed).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new object version.</returns>
		/// <remarks>If <see paramref="replaceAllProperties"/> is true then <see paramref="propertyValues"/> must contain values for property 0 (name or title), property 100 (class), property 22 (is single file), and any other mandatory properties for the given class.</remarks>
		public ExtendedObjectVersion SetProperties(ObjID objId, PropertyValue[] propertyValues, bool replaceAllProperties, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.SetPropertiesAsync(objId, propertyValues, replaceAllProperties, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

		#region Set properties of multiple objects at once

		/// <summary>
		/// Sets the properties of multiple objects in one HTTP request.
		/// </summary>
		/// <param name="objectVersionUpdateInformation">Information on the objects to promote.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <remarks>Will also promote objects if they are external and a class property value is provided (see <see cref="MFWSVaultExternalObjectOperations.PromoteObjectsAsync"/>).</remarks>
		public async Task<List<ExtendedObjectVersion>> SetPropertiesOfMultipleObjectsAsync(CancellationToken token = default(CancellationToken), params ObjectVersionUpdateInformation[] objectVersionUpdateInformation)
		{
			// Sanity.
			if (null == objectVersionUpdateInformation)
				throw new ArgumentNullException(nameof(objectVersionUpdateInformation));
			if (objectVersionUpdateInformation.Length == 0)
				return new List<ExtendedObjectVersion>();

			// Create the request.
			var request = new RestRequest($"/REST/objects/setmultipleobjproperties");

			// Create the request body.
			var body = new ObjectsUpdateInfo();
			body.MultipleObjectInfo.AddRange(objectVersionUpdateInformation);

			// Set the request body.
			request.AddJsonBody(body);

			// Make the request and get the response.
			var response = await this.MFWSClient.Put<List<ExtendedObjectVersion>>(request, token)
				.ConfigureAwait(false);

			// Return the object data.
			return response.Data;
		}

		/// <summary>
		/// Promotes unmanaged objects to managed object.
		/// </summary>
		/// <param name="objectVersionUpdateInformation">Information on the objects to promote.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <remarks>The property values must be valid for the class, as they would if an object were being created.</remarks>
		public List<ExtendedObjectVersion> SetPropertiesOfMultipleObjects(CancellationToken token = default(CancellationToken), params ObjectVersionUpdateInformation[] objectVersionUpdateInformation)
		{
			// Execute the async method.
			return this.SetPropertiesOfMultipleObjectsAsync(token, objectVersionUpdateInformation)
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
