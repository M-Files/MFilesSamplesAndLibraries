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
	/// Methods to create and modify objects.
	/// </summary>
	public class MFWSVaultObjectOperations
		: MFWSVaultOperationsBase
	{
		/// <summary>
		/// Creates a new <see cref="MFWSVaultObjectOperations"/> object.
		/// </summary>
		/// <param name="client">The client to interact with the server.</param>
		internal MFWSVaultObjectOperations(MFWSClientBase client)
			: base(client)
		{
		}

		#region Renaming objects

		/// <summary>
		/// Renames the object by altering its title.
		/// </summary>
		/// <param name="objVer">The object to rename.</param>
		/// <param name="newObjectName">The new name of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the renamed object.</returns>
		public async Task<ObjectVersion> RenameObjectAsync(ObjVer objVer,
			string newObjectName,
			CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Extract the URI elements.
			int objectTypeId;
			string objectId, objectVersionId;
			objVer.GetUriParameters(out objectTypeId, out objectId, out objectVersionId);

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/{objectVersionId}/title");
			request.AddJsonBody(new PrimitiveType<string>() { Value = newObjectName });

			// Make the request and get the response.
			var response = await this.MFWSClient.Put<ObjectVersion>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Renames the object by altering its title.
		/// </summary>
		/// <param name="objId">The object to rename.</param>
		/// <param name="newObjectName">The new name of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the renamed object.</returns>
		public Task<ObjectVersion> RenameObjectAsync(
			ObjID objId,
			string newObjectName,
			CancellationToken token = default(CancellationToken))
		{
			return this.RenameObjectAsync(new ObjVer()
			{
				Version = -1,
				ID = objId.ID,
				Type = objId.Type,
				ExternalRepositoryObjectID = objId.ExternalRepositoryObjectID,
				ExternalRepositoryName = objId.ExternalRepositoryName,
				ExternalRepositoryObjectVersionID = null,
				VersionType = MFObjVerVersionType.Latest
			}, newObjectName, token);
		}

		/// <summary>
		/// Renames the object by altering its title.
		/// </summary>
		/// <param name="objVer">The object to rename.</param>
		/// <param name="newObjectName">The new name of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the renamed object.</returns>
		/// <remarks>ref: http://developer.m-files.com/APIs/REST-API/Reference/resources/objects/type/objectid/version/title/ </remarks>
		public ObjectVersion RenameObject(ObjVer objVer,
			string newObjectName,
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.RenameObjectAsync(objVer, newObjectName, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Renames the object by altering its title.
		/// </summary>
		/// <param name="objId">The object to rename.</param>
		/// <param name="newObjectName">The new name of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the renamed object.</returns>
		public ObjectVersion RenameObject(
			ObjID objId,
			string newObjectName,
			CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.RenameObject(new ObjVer()
			{
				Version = -1,
				ID = objId.ID,
				Type = objId.Type,
				ExternalRepositoryObjectID = objId.ExternalRepositoryObjectID,
				ExternalRepositoryName = objId.ExternalRepositoryName,
				ExternalRepositoryObjectVersionID = null,
				VersionType = MFObjVerVersionType.Latest
			}, newObjectName, token);
		}

		#endregion

		#region Getting the latest object version and properties

		/// <summary>
		/// Retrieves the latest version of the specified object version.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The ObjectVersion, if it is visible to the user.</returns>
		public async Task<ExtendedObjectVersion> GetLatestObjectVersionAndPropertiesAsync(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Extract the URI elements.
			int objectTypeId;
			string objectId;
			objId.GetUriParameters(out objectTypeId, out objectId);

			// Create the request.
			string resource = $"/REST/objects/{objectTypeId}/{objectId}/latest.aspx?include=properties";
			var request = new RestRequest(resource);

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<ExtendedObjectVersion>(request, token)
				.ConfigureAwait(false);

			// Return the object data.
			return response.Data;
		}

		/// <summary>
		/// Retrieves the latest version of the specified object version.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The ObjectVersion, if it is visible to the user.</returns>
		public Task<ExtendedObjectVersion> GetLatestObjectVersionAndPropertiesAsync(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			// Execute the other overload.
			return this.GetLatestObjectVersionAndPropertiesAsync(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId,
			}, token);
		}

		/// <summary>
		/// Retrieves the latest version of the specified object version.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The ObjectVersion, if it is visible to the user.</returns>
		public ExtendedObjectVersion GetLatestObjectVersionAndProperties(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetLatestObjectVersionAndPropertiesAsync(objId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves the latest version of the specified object version.
		/// </summary>
		/// <param name="objectTypeId">Object type ID.</param>
		/// <param name="objectId">The ID of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The ObjectVersion, if it is visible to the user.</returns>
		public ExtendedObjectVersion GetLatestObjectVersionAndProperties(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			// Execute the other overload.
			return this.GetLatestObjectVersionAndProperties(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId,
			}, token);
		}

		#endregion

		#region Creating new objects

		/// <summary>
		/// Creates an object.
		/// </summary>
		/// <param name="objectTypeId">The type of the object.</param>
		/// <param name="creationInfo">The creation information for the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the created object.</returns>
		public async Task<ObjectVersion> CreateNewObjectAsync(int objectTypeId, ObjectCreationInfo creationInfo, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == creationInfo)
				throw new ArgumentNullException();
			if (objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");
			creationInfo.Files = creationInfo.Files ?? new UploadInfo[0];

			// Remove the extension from the item title if it exists.
			foreach (var item in creationInfo.Files)
			{
				// Sanity.
				if (string.IsNullOrWhiteSpace(item.Title) || string.IsNullOrWhiteSpace(item.Extension))
					continue;

				// If the title ends with the extension then remove it.
				if (true == item.Title?.EndsWith("." + item.Extension))
				{
					// Note the +1 is because we want to remove the dot as well.
					item.Title = item.Title.Substring(0, item.Title.Length - (item.Extension.Length + 1));
				}
			}

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}");
			request.AddJsonBody(creationInfo);

			// Make the request and get the response.
			var response = await this.MFWSClient.Post<ObjectVersion>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;

		}

		/// <summary>
		/// Creates an object.
		/// </summary>
		/// <param name="objectTypeId">The type of the object.</param>
		/// <param name="creationInfo">The creation information for the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the created object.</returns>
		public ObjectVersion CreateNewObject(int objectTypeId, ObjectCreationInfo creationInfo, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.CreateNewObjectAsync(objectTypeId, creationInfo, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

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
		public Task<ObjectVersion> SetCheckoutStatusAsync(int objectTypeId, int objectId, MFCheckOutStatus status, int? version = null,
			CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");
			if (objectId <= 0)
				throw new ArgumentException("The object id cannot be less than or equal to zero");

			// Use the other overload.
			return this.SetCheckoutStatusAsync(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId
			}, status, version, token);
		}

		/// <summary>
		/// Sets an object checkout status.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="status">The checkout status.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public ObjectVersion SetCheckoutStatus(int objectTypeId, int objectId, MFCheckOutStatus status, int? version = null,
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.SetCheckoutStatusAsync(objectTypeId, objectId, status, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Sets an object checkout status.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="status">The checkout status.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<ObjectVersion> SetCheckoutStatusAsync(ObjID objId, MFCheckOutStatus status, int? version = null,
			CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Use the other overload.
			return this.SetCheckoutStatusAsync(new ObjVer()
				{
					Version = version ?? -1,
					ID = objId.ID,
					Type = objId.Type,
					VersionType =  version >= 0 ? MFObjVerVersionType.Specific : MFObjVerVersionType.Latest,
					ExternalRepositoryObjectID = objId.ExternalRepositoryObjectID,
					ExternalRepositoryName = objId.ExternalRepositoryName,
					ExternalRepositoryObjectVersionID = null
				},
				status,
				token);
		}

		/// <summary>
		/// Sets an object checkout status.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="status">The checkout status.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public ObjectVersion SetCheckoutStatus(ObjID objId, MFCheckOutStatus status, int? version = null,
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.SetCheckoutStatusAsync(objId, status, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Sets an object checkout status.
		/// </summary>
		/// <param name="objVer">The Id and version of the object.</param>
		/// <param name="status">The checkout status.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public async Task<ObjectVersion> SetCheckoutStatusAsync(ObjVer objVer, MFCheckOutStatus status, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Extract the URI elements.
			int objectTypeId;
			string objectId, objectVersionId;
			objVer.GetUriParameters(out objectTypeId, out objectId, out objectVersionId);

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/{objectVersionId}/checkedout");
			request.AddJsonBody(new PrimitiveType<MFCheckOutStatus>() { Value = status });

			// Make the request and get the response.
			var response = await this.MFWSClient.Put<ObjectVersion>(request, token)
				.ConfigureAwait(false);

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
		public ObjectVersion SetCheckoutStatus(ObjVer objVer, MFCheckOutStatus status, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.SetCheckoutStatusAsync(objVer, status, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves an object' checkout status.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<MFCheckOutStatus?> GetCheckoutStatusAsync(int objectTypeId, int objectId, int? version = null, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");
			if (objectId <= 0)
				throw new ArgumentException("The object id cannot be less than or equal to zero");

			// Use the other overload.
			if (null == version)
			{
				return this.GetCheckoutStatusAsync(new ObjID()
				{
					ID = objectId,
					Type = objectTypeId
				}, token: token);
			}
			else
			{
				return this.GetCheckoutStatusAsync(new ObjVer()
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
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public MFCheckOutStatus? GetCheckoutStatus(int objectTypeId, int objectId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetCheckoutStatusAsync(objectTypeId, objectId, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves an object' checkout status.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<MFCheckOutStatus?> GetCheckoutStatusAsync(ObjID objId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Use the other overload.
			return this.GetCheckoutStatusAsync(new ObjVer()
			{
				ID = objId.ID,
				Type = objId.Type,
				Version = -1,
				VersionType = MFObjVerVersionType.Latest,
				ExternalRepositoryObjectID = objId.ExternalRepositoryObjectID,
				ExternalRepositoryName = objId.ExternalRepositoryName,
				ExternalRepositoryObjectVersionID = null
			}, token);
		}

		/// <summary>
		/// Retrieves an object' checkout status.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public MFCheckOutStatus? GetCheckoutStatus(ObjID objId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetCheckoutStatusAsync(objId, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves an object' checkout status.
		/// </summary>
		/// <param name="objVer">The Id and version of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public async Task<MFCheckOutStatus?> GetCheckoutStatusAsync(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Create the request.
			string id = objVer.ID == 0
						&& false == string.IsNullOrWhiteSpace(objVer.ExternalRepositoryName)
						&& false == string.IsNullOrWhiteSpace(objVer.ExternalRepositoryObjectID)
				? $"u{WebUtility.UrlEncode(objVer.ExternalRepositoryName)}:{WebUtility.UrlEncode(objVer.ExternalRepositoryObjectID)}" // External object.
				: objVer.ID.ToString(); // Internal object.
			string version = objVer.ID > 0
				? (objVer.Version > 0 ? objVer.Version.ToString() : "latest") // Internal object.
				: string.IsNullOrWhiteSpace(objVer.ExternalRepositoryObjectVersionID) ? "latest" : WebUtility.UrlEncode(objVer.ExternalRepositoryObjectVersionID); // External object.
			var request = new RestRequest($"/REST/objects/{objVer.Type}/{id}/{version}/checkedout");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<PrimitiveType<MFCheckOutStatus>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data?.Value;
		}

		/// <summary>
		/// Retrieves an object' checkout status.
		/// </summary>
		/// <param name="objVer">The Id and version of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public MFCheckOutStatus? GetCheckoutStatus(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetCheckoutStatusAsync(objVer, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Checks out an object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<ObjectVersion> CheckOutAsync(int objectTypeId, int objectId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			return this.SetCheckoutStatusAsync(objectTypeId, objectId, MFCheckOutStatus.CheckedOutToMe, version, token);
		}

		/// <summary>
		/// Checks out an object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public ObjectVersion CheckOut(int objectTypeId, int objectId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.CheckOutAsync(objectTypeId, objectId, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Checks out an object.
		/// </summary>
		/// <param name="objId">The Id of the object to check out.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public ObjectVersion CheckOut(ObjID objId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Execute the async method.
			return this.CheckOutAsync(objId.Type, objId.ID, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Checks out an object.
		/// </summary>
		/// <param name="objVer">The Id and version of the object to check out.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<ObjectVersion> CheckOutAsync(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{
			return this.SetCheckoutStatusAsync(objVer, MFCheckOutStatus.CheckedOutToMe, token);
		}

		/// <summary>
		/// Checks out an object.
		/// </summary>
		/// <param name="objVer">The Id and version of the object to check out.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public ObjectVersion CheckOut(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Execute the async method.
			return this.CheckOutAsync(objVer, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Checks out an object.
		/// </summary>
		/// <param name="objId">The Id of the object to check out.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<ObjectVersion> CheckOutAsync(ObjID objId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			return this.SetCheckoutStatusAsync(objId, MFCheckOutStatus.CheckedOutToMe, version, token);
		}

		/// <summary>
		/// Checks in an object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<ObjectVersion> CheckInAsync(int objectTypeId, int objectId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			return this.SetCheckoutStatusAsync(objectTypeId, objectId, MFCheckOutStatus.CheckedIn, version, token);
		}

		/// <summary>
		/// Checks in an object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public ObjectVersion CheckIn(int objectTypeId, int objectId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.CheckInAsync(objectTypeId, objectId, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Checks in an object.
		/// </summary>
		/// <param name="objVer">The Id and version of the object to check out.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<ObjectVersion> CheckInAsync(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			return this.SetCheckoutStatusAsync(objVer, MFCheckOutStatus.CheckedIn, token);
		}

		/// <summary>
		/// Checks in an object.
		/// </summary>
		/// <param name="objVer">The Id and version of the object to check out.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public ObjectVersion CheckIn(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Execute the async method.
			return this.CheckInAsync(objVer, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Checks in an object.
		/// </summary>
		/// <param name="objId">The Id of the object to check in.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<ObjectVersion> CheckInAsync(ObjID objId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			return this.SetCheckoutStatusAsync(objId.Type, objId.ID, MFCheckOutStatus.CheckedIn, version, token);
		}

		/// <summary>
		/// Checks in an object.
		/// </summary>
		/// <param name="objId">The Id of the object to check in.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public ObjectVersion CheckIn(ObjID objId, int? version = null, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Execute the async method.
			return this.CheckInAsync(objId.Type, objId.ID, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Performs "undo checkout" for the specified object version.
		/// </summary>
		/// <param name="objVer">The Id, type and version of the object.</param>
		/// <param name="force">If true, will undo checkout even if this object isn't checked out to this user on this machine (subject to user rights).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new ObjectVersion information if it is still visible to the user.</returns>
		protected async Task<ObjectVersion> UndoCheckoutAsync(ObjVer objVer, bool force, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objVer.Type}/{objVer.ID}/{objVer.Version}?force={force.ToString().ToLower()}");
			request.Method = Method.DELETE;

			// Make the request and get the response.
			var response = await this.MFWSClient.Delete<ObjectVersion>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Performs "undo checkout" for the specified object version.
		/// </summary>
		/// <param name="objVer">The Id, type and version of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new ObjectVersion information if it is still visible to the user.</returns>
		public Task<ObjectVersion> UndoCheckoutAsync(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.UndoCheckoutAsync(objVer, force: false, token: token);
		}

		/// <summary>
		/// Performs "undo checkout" for the specified object version.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new ObjectVersion information if it is still visible to the user.</returns>
		public Task<ObjectVersion> UndoCheckoutAsync(int objectTypeId, int objectId, int version, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.UndoCheckoutAsync(new ObjVer()
			{
				ID = objectId,
				Type = objectTypeId,
				Version = version
			}, token: token);
		}

		/// <summary>
		/// Performs "undo checkout" for the specified object version.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new ObjectVersion information if it is still visible to the user.</returns>
		public ObjectVersion UndoCheckout(
			int objectTypeId,
			int objectId,
			int version,
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.UndoCheckoutAsync(new ObjVer()
				{
					ID = objectId,
					Type = objectTypeId,
					Version = version
				}, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Performs "undo checkout" for the specified object version.
		/// </summary>
		/// <param name="objVer">The Id, type and version of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new ObjectVersion information if it is still visible to the user.</returns>
		public ObjectVersion UndoCheckout(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));

			// Execute the async method.
			return this.UndoCheckoutAsync(objVer.Type, objVer.ID, objVer.Version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Performs "undo checkout" for the specified object version.
		/// Will undo checkout even if this object isn't checked out to this user on this machine (subject to user rights).
		/// </summary>
		/// <param name="objVer">The Id, type and version of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new ObjectVersion information if it is still visible to the user.</returns>
		public Task<ObjectVersion> ForceUndoCheckoutAsync(ObjVer objVer, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.UndoCheckoutAsync(objVer, force: true, token: token);
		}

		/// <summary>
		/// Performs "undo checkout" for the specified object version.
		/// Will undo checkout even if this object isn't checked out to this user on this machine (subject to user rights).
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new ObjectVersion information if it is still visible to the user.</returns>
		public Task<ObjectVersion> ForceUndoCheckoutAsync(int objectTypeId, int objectId, int version, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.ForceUndoCheckoutAsync(new ObjVer()
			{
				ID = objectId,
				Type = objectTypeId,
				Version = version
			}, token: token);
		}

		/// <summary>
		/// Performs "undo checkout" for the specified object version.
		/// Will undo checkout even if this object isn't checked out to this user on this machine (subject to user rights).
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The new ObjectVersion information if it is still visible to the user.</returns>
		public ObjectVersion ForceUndoCheckout(int objectTypeId, int objectId, int version, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.ForceUndoCheckoutAsync(objectTypeId, objectId, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
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
		public Task<bool?> GetDeletedStatusAsync(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");
			if (objectId <= 0)
				throw new ArgumentException("The object id cannot be less than or equal to zero");

			// Use the other overload.
			return this.GetDeletedStatusAsync(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId
			}, token);
		}

		/// <summary>
		/// Retrieves an object's deleted status.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public bool? GetDeletedStatus(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetDeletedStatusAsync(objectTypeId, objectId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves an object's deleted status.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public async Task<bool?> GetDeletedStatusAsync(ObjID objId, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Extract the URI elements.
			int objectTypeId;
			string objectId;
			objId.GetUriParameters(out objectTypeId, out objectId);

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/deleted");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<PrimitiveType<bool>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data?.Value;
		}

		/// <summary>
		/// Retrieves an object's deleted status.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public bool? GetDeletedStatus(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetDeletedStatusAsync(objId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

		#region Undeleting objects

		/// <summary>
		/// Undeletes an object.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The ObjectVersion, if it is still visible to the user.</returns>
		public async Task<ObjectVersion> UndeleteObjectAsync(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Extract the URI elements.
			int objectTypeId;
			string objectId;
			objId.GetUriParameters(out objectTypeId, out objectId);

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/deleted");
			request.Method = Method.PUT;

			// Add the body.
			request.AddJsonBody(new PrimitiveType<bool>() { Value = false });

			// Make the request and get the response.
			var response = await this.MFWSClient.Put<ObjectVersion>(request, token)
				.ConfigureAwait(false);

			// If success, returns 204.
			return response.Data;
		}

		/// <summary>
		/// Undeletes an object.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The ObjectVersion, if it is still visible to the user.</returns>
		public ObjectVersion UndeleteObject(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.UndeleteObjectAsync(objId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Undeletes an object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The ObjectVersion, if it is still visible to the user.</returns>
		public ObjectVersion UndeleteObject(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.UndeleteObjectAsync(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId,
			}, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

		#region Deleting objects

		/// <summary>
		/// Deletes an object.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The ObjectVersion, if it is still visible to the user.</returns>
		public async Task<ObjectVersion> DeleteObjectAsync(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Extract the URI elements.
			int objectTypeId;
			string objectId;
			objId.GetUriParameters(out objectTypeId, out objectId);

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/deleted");
			request.Method = Method.PUT;
			
			// Add the body.
			request.AddJsonBody(new PrimitiveType<bool>() { Value = true });

			// Make the request and get the response.
			var response = await this.MFWSClient.Put<ObjectVersion>(request, token)
				.ConfigureAwait(false);

			// If success, returns 204.
			return response.Data;
		}

		/// <summary>
		/// Deletes an object.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The ObjectVersion, if it is still visible to the user.</returns>
		public ObjectVersion DeleteObject(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.DeleteObjectAsync(objId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Deletes an object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The ObjectVersion, if it is still visible to the user.</returns>
		public ObjectVersion DeleteObject(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.DeleteObjectAsync(new ObjID()
				{
					ID = objectId,
					Type = objectTypeId,
				}, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

		#region Destroying objects

		/// <summary>
		/// Destroys all versions of an object.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="version">The version of the object. Must be -1.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <param name="destroyAllVersions">Whether to destroy all versions.  Must be true.</param>
		/// <returns>True if 204 was received from the server.</returns>
		/// <remarks>Unlike the COM API method (https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultObjectOperations~DestroyObject.html),
		/// the REST API only supports destroying all versions.</remarks>
		public async Task<bool> DestroyObjectAsync(ObjID objId, bool destroyAllVersions, int version, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));
			if (false == destroyAllVersions)
				throw new ArgumentException($"{nameof(destroyAllVersions)} must be true.", nameof(destroyAllVersions));
			if (-1 != version)
				throw new ArgumentException($"{nameof(version)} must be -1.", nameof(version));

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objId.Type}/{objId.ID}/latest?allVersions=true");
			request.Method = Method.DELETE;

			// Make the request and get the response.
			var response = await this.MFWSClient.Delete<ObjectVersion>(request, token)
				.ConfigureAwait(false);

			// If success, returns 204.
			return response.StatusCode == HttpStatusCode.NoContent;
		}

		/// <summary>
		/// Destroys all versions of an object.
		/// </summary>
		/// <param name="objId">The Id of the object.</param>
		/// <param name="version">The version of the object. Must be -1.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <param name="destroyAllVersions">Whether to destroy all versions.  Must be true.</param>
		/// <returns>True if 204 was received from the server.</returns>
		/// <remarks>Unlike the COM API method (https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultObjectOperations~DestroyObject.html),
		/// the REST API only supports destroying all versions.</remarks>
		public bool DestroyObject(ObjID objId, bool destroyAllVersions, int version, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.DestroyObjectAsync(objId, destroyAllVersions, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Destroys all versions of an object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version of the object. Must be -1.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <param name="destroyAllVersions">Whether to destroy all versions.  Must be true.</param>
		/// <returns>True if 204 was received from the server.</returns>
		/// <remarks>Unlike the COM API method (https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultObjectOperations~DestroyObject.html),
		/// the REST API only supports destroying all versions.</remarks>
		public bool DestroyObject(int objectTypeId, int objectId, bool destroyAllVersions, int version, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.DestroyObjectAsync(new ObjID()
				{
					ID = objectId,
					Type = objectTypeId,
				}, destroyAllVersions, version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
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
		public Task<List<ObjectVersion>> GetHistoryAsync(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			return this.GetHistoryAsync(new ObjID()
			{
				Type = objectTypeId,
				ID = objectId
			}, token);

		}

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of <see cref="ObjectVersion"/>s representing the object history.</returns>
		/// <remarks>Note that not all versions may be shown: http://www.m-files.com/mfws/resources/objects/type/objectid/history.html</remarks>
		public List<ObjectVersion> GetHistory(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetHistoryAsync(objectTypeId, objectId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objID">The object to retrieve the history from.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of <see cref="ObjectVersion"/>s representing the object history.</returns>
		/// <remarks>Note that not all versions may be shown: http://www.m-files.com/mfws/resources/objects/type/objectid/history.html</remarks>
		public async Task<List<ObjectVersion>> GetHistoryAsync(ObjID objID, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objID)
				throw new ArgumentNullException(nameof(ObjID));

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objID.Type}/{objID.ID}/history");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<ObjectVersion>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;

		}

		/// <summary>
		/// Retrieves the properties of multiple objects.
		/// </summary>
		/// <param name="objID">The object to retrieve the history from.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of <see cref="ObjectVersion"/>s representing the object history.</returns>
		/// <remarks>Note that not all versions may be shown: http://www.m-files.com/mfws/resources/objects/type/objectid/history.html</remarks>
		public List<ObjectVersion> GetHistory(ObjID objID, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetHistoryAsync(objID, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();

		}

		#endregion

		#region Favourites

		/// <summary>
		/// Adds the supplied item to the favorites.
		/// </summary>
		/// <param name="objId">The <see cref="ObjID"/> of the item to add to the favourites.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The item that was added.</returns>
		public async Task<ExtendedObjectVersion> AddToFavoritesAsync(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Create the request.
			var request = new RestRequest("/REST/favorites");
			request.AddJsonBody(objId);

			// Make the request and get the response.
			var response = await this.MFWSClient.Post<ExtendedObjectVersion>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Adds the supplied item to the favorites.
		/// </summary>
		/// <param name="objId">The <see cref="ObjID"/> of the item to add to the favourites.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The item that was added.</returns>
		public ExtendedObjectVersion AddToFavorites(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.AddToFavoritesAsync(objId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Adds the supplied item to the favorites.
		/// </summary>
		/// <param name="objectTypeId">The id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The item that was added.</returns>
		public Task<ExtendedObjectVersion> AddToFavoritesAsync(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			return this.AddToFavoritesAsync(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId
			}, token);
		}

		/// <summary>
		/// Adds the supplied item to the favorites.
		/// </summary>
		/// <param name="objectTypeId">The id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The item that was added.</returns>
		public ExtendedObjectVersion AddToFavorites(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.AddToFavoritesAsync(objectTypeId, objectId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Removes the supplied item to the favorites.
		/// </summary>
		/// <param name="objId">The <see cref="ObjID"/> of the item to remove from the favourites.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The item that was removed.</returns>
		public async Task<ExtendedObjectVersion> RemoveFromFavoritesAsync(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));

			// Create the request.
			var request = new RestRequest($"/REST/favorites/{objId.Type}/{objId.ID}");

			// Make the request and get the response.
			var response = await this.MFWSClient.Delete<ExtendedObjectVersion>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Removes the supplied item to the favorites.
		/// </summary>
		/// <param name="objId">The <see cref="ObjID"/> of the item to remove from the favourites.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The item that was removed.</returns>
		public ExtendedObjectVersion RemoveFromFavorites(ObjID objId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.RemoveFromFavoritesAsync(objId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Removes the supplied item to the favorites.
		/// </summary>
		/// <param name="objectTypeId">The id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The item that was removed.</returns>
		public Task<ExtendedObjectVersion> RemoveFromFavoritesAsync(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			return this.RemoveFromFavoritesAsync(new ObjID()
			{
				ID = objectId,
				Type = objectTypeId
			}, token);
		}

		/// <summary>
		/// Removes the supplied item to the favorites.
		/// </summary>
		/// <param name="objectTypeId">The id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The item that was removed.</returns>
		public ExtendedObjectVersion RemoveFromFavorites(int objectTypeId, int objectId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.RemoveFromFavoritesAsync(objectTypeId, objectId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

	}
	
}
