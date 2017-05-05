using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{

		#region Checking in and out

		/// <summary>
		/// Sets an object checkout status.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="status">The checkout status.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<ObjectVersion> SetCheckoutStatus(int objectTypeId, int objectId, MFCheckOutStatus status, int? version = null,
			CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");
			if (objectId <= 0)
				throw new ArgumentException("The object id cannot be less than or equal to zero");

			// Use the other overload.
			return this.SetCheckoutStatus(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId
			}, status, version, token);
		}

		/// <summary>
		/// Sets an object checkout status.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="status">The checkout status.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public async Task<ObjectVersion> SetCheckoutStatus(ObjID objId, MFCheckOutStatus status, int? version = null,
			CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objId.Type}/{objId.ID}/{version?.ToString() ?? "latest"}/checkedout");
			request.AddJsonBody(status);

			// Make the request and get the response.
			var response = await this.Put<ObjectVersion>(request, token);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Sets an object checkout status.
		/// </summary>
		/// <param name="objVer">The Id and version of the object.</param>
		/// <param name="status">The checkout status.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public async Task<ObjectVersion> SetCheckoutStatus(ObjVer objVer, MFCheckOutStatus status, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objVer.Type}/{objVer.ID}/{objVer.Version}/checkedout");
			request.AddJsonBody(status);

			// Make the request and get the response.
			var response = await this.Put<ObjectVersion>(request, token);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Retrieves an object' checkout status.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<MFCheckOutStatus?> GetCheckoutStatus(int objectTypeId, int objectId, int? version = null, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");
			if (objectId <= 0)
				throw new ArgumentException("The object id cannot be less than or equal to zero");

			// Use the other overload.
			if (null == version)
			{
				return this.GetCheckoutStatus(new ObjID()
				{
					ID = objectId,
					Type = objectTypeId
				}, token: token);
			}
			else
			{
				return this.GetCheckoutStatus(new ObjVer()
				{
					ID = objectId,
					Type = objectTypeId,
					Version = version.Value
				}, token);
			}
		}

		/// <summary>
		/// Retrieves an object' checkout status.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public async Task<MFCheckOutStatus?> GetCheckoutStatus(ObjID objId, int? version = null, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objId.Type}/{objId.ID}/{version?.ToString() ?? "latest"}/checkedout");

			// Make the request and get the response.
			var response = await this.Get<PrimitiveType<MFCheckOutStatus>>(request, token);

			// Return the data.
			return response.Data?.Value;
		}

		/// <summary>
		/// Retrieves an object' checkout status.
		/// </summary>
		/// <param name="objVer">The Id and version of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public async Task<MFCheckOutStatus?> GetCheckoutStatus(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objVer.Type}/{objVer.ID}/{objVer.Version}/checkedout");

			// Make the request and get the response.
			var response = await this.Get<PrimitiveType<MFCheckOutStatus>>(request, token);

			// Return the data.
			return response.Data?.Value;
		}

		/// <summary>
		/// Checks out an object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<ObjectVersion> CheckOut(int objectTypeId, int objectId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			return this.SetCheckoutStatus(objectTypeId, objectId, MFCheckOutStatus.CheckedOutToMe, version, token);
		}

		/// <summary>
		/// Checks in an object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<ObjectVersion> CheckIn(int objectTypeId, int objectId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			return this.SetCheckoutStatus(objectTypeId, objectId, MFCheckOutStatus.CheckedIn, version, token);
		}

		#endregion

		#region Deleted status

		/// <summary>
		/// Retrieves an object's deleted status.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<bool?> GetDeletedStatus(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");
			if (objectId <= 0)
				throw new ArgumentException("The object id cannot be less than or equal to zero");

			// Use the other overload.
			return this.GetDeletedStatus(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId
			}, token);
		}

		/// <summary>
		/// Retrieves an object's deleted status.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public async Task<bool?> GetDeletedStatus(ObjID objId, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objId.Type}/{objId.ID}/deleted");

			// Make the request and get the response.
			var response = await this.Get<PrimitiveType<bool>>(request, token);

			// Return the data.
			return response.Data?.Value;
		}

		#endregion

		#region Get properties of objects

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objVers">The objects to retrieve the properties of.</param>
		/// <returns>A collection of property values, one for each object version provided in <see cref="objVers"/>.</returns>
		public Task<PropertyValue[][]> GetObjectPropertyValues(params ObjVer[] objVers)
		{
			return this.GetObjectPropertyValues(CancellationToken.None, objVers);
		}

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objVers">The objects to retrieve the properties of.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values, one for each object version provided in <see cref="objVers"/>.</returns>
		public async Task<PropertyValue[][]> GetObjectPropertyValues(CancellationToken token, params ObjVer[] objVers)
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
			var response = await this.Get<List<List<PropertyValue>>>(request, token);

			// Return the data.
			return response.Data?.Select(a => a.ToArray()).ToArray();

		}

		/// <summary>
		/// Retrieves the properties of a single object.
		/// </summary>
		/// <param name="objVer">The object to retrieve the properties of.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of property values for the supplied object.</returns>
		public async Task<PropertyValue[]> GetObjectPropertyValues(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Use the other overload to retrieve the content.
			var response = await this.GetObjectPropertyValues(token, new[] { objVer });

			// Sanity.
			return null != response && response.Length > 0
				? new PropertyValue[0] 
				: response[0];
		}

		#endregion

		#region History

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of <see cref="ObjectVersion"/>s representing the object history.</returns>
		/// <remarks>Note that not all versions may be shown: http://www.m-files.com/mfws/resources/objects/type/objectid/history.html</remarks>
		public Task<List<ObjectVersion>> GetHistory(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			return this.GetHistory(new ObjID()
			{
				Type = objectTypeId,
				ID = objectId
			}, token);

		}

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objID">The object to retrieve the history from.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of <see cref="ObjectVersion"/>s representing the object history.</returns>
		/// <remarks>Note that not all versions may be shown: http://www.m-files.com/mfws/resources/objects/type/objectid/history.html</remarks>
		public async Task<List<ObjectVersion>> GetHistory(ObjID objID, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objID)
				throw new ArgumentNullException(nameof(ObjID));

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objID.Type}/{objID.ID}/history");

			// Make the request and get the response.
			var response = await this.Get<List<ObjectVersion>>(request, token);

			// Return the data.
			return response.Data;

		}

		#endregion

	}
	
}
