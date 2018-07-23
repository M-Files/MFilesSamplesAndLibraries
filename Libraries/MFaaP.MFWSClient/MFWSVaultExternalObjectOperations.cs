using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Methods to unteract with objects in external repositories.
	/// </summary>
	public class MFWSVaultExternalObjectOperations
		: MFWSVaultOperationsBase
	{
		/// <summary>
		/// Creates a new <see cref="MFWSVaultClassOperations"/> object.
		/// </summary>
		/// <param name="client">The client to interact with the server.</param>
		internal MFWSVaultExternalObjectOperations(MFWSClientBase client)
			: base(client)
		{
		}

		#region Demote objects

		/// <summary>
		/// Demotes managed objects to unmanaged objects.
		/// </summary>
		/// <param name="objectsToDemote">The Ids of the objects to demote.</param>
		/// <param name="token">A cancellation token for the request.</param>
		public async Task<List<ExtendedObjectVersion>> DemoteObjectsAsync(CancellationToken token = default(CancellationToken), params ObjID[] objectsToDemote)
		{
			// Sanity.
			if (null == objectsToDemote)
				throw new ArgumentNullException(nameof(objectsToDemote));
			if (objectsToDemote.Length == 0)
				return new List<ExtendedObjectVersion>();

			// Create the request.
			var request = new RestRequest($"/REST/objects/demotemultiobjects");

			// NOTE: If the ObjID collection contains external data then it fails; remove it.
			objectsToDemote = objectsToDemote
				.Select(oid => new ObjID()
				{
					Type = oid.Type,
					ID = oid.ID
				})
				.ToArray();

			// Set the request body.
			request.AddJsonBody(objectsToDemote);

			// Make the request and get the response.
			var response = await this.MFWSClient.Put<List<ExtendedObjectVersion>>(request, token)
				.ConfigureAwait(false);

			// Return the object data.
			return response.Data;
		}

		/// <summary>
		/// Demotes managed objects to unmanaged objects.
		/// </summary>
		/// <param name="objectsToDemote">The Ids of the objects to demote.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The ObjectVersion, if it is visible to the user.</returns>
		public List<ExtendedObjectVersion> DemoteObjects(CancellationToken token = default(CancellationToken), params ObjID[] objectsToDemote)
		{
			// Execute the async method.
			return this.DemoteObjectsAsync(token, objectsToDemote)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Demote a managed object to an unmanaged object.
		/// </summary>
		/// <param name="objID">The Id of the object to demote.</param>
		/// <param name="token">A cancellation token for the request.</param>
		public Task<List<ExtendedObjectVersion>> DemoteObjectAsync(ObjID objID, CancellationToken token = default(CancellationToken))
		{
			return this.DemoteObjectsAsync(token, objID);
		}

		/// <summary>
		/// Demote a managed object to an unmanaged object.
		/// </summary>
		/// <param name="objID">The Id of the object to demote.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The ObjectVersion, if it is visible to the user.</returns>
		public List<ExtendedObjectVersion> DemoteObject(ObjID objID, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.DemoteObjectAsync(objID, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

		#region Promote objects

		/// <summary>
		/// Promotes unmanaged objects to managed objects.
		/// </summary>
		/// <param name="objectVersionUpdateInformation">Information on the objects to promote.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <remarks>The property values must be valid for the class, as they would if an object were being created.</remarks>
		public Task<List<ExtendedObjectVersion>> PromoteObjectsAsync(CancellationToken token = default(CancellationToken), params ObjectVersionUpdateInformation[] objectVersionUpdateInformation)
		{
			// Use the "SetPropertiesOfMultipleObjects" method to perform this.
			return this.MFWSClient.ObjectPropertyOperations.SetPropertiesOfMultipleObjectsAsync(
				token,
				objectVersionUpdateInformation);
		}

		/// <summary>
		/// Promotes unmanaged objects to managed object.
		/// </summary>
		/// <param name="objectVersionUpdateInformation">Information on the objects to promote.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <remarks>The property values must be valid for the class, as they would if an object were being created.</remarks>
		public List<ExtendedObjectVersion> PromoteObjects(CancellationToken token = default(CancellationToken), params ObjectVersionUpdateInformation[] objectVersionUpdateInformation)
		{
			// Execute the async method.
			return this.PromoteObjectsAsync(token, objectVersionUpdateInformation)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Promotes an unmanaged object to a managed object.
		/// </summary>
		/// <param name="objVer">The object to promote.</param>
		/// <param name="propertyValues">The object's new properties.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <remarks>The property values must be valid for the class, as they would if an object were being created.</remarks>
		public async Task<ExtendedObjectVersion> PromoteObjectAsync(ObjVer objVer, PropertyValue[] propertyValues, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));
			if (null == propertyValues)
				throw new ArgumentNullException(nameof(propertyValues));

			// Use the other method.
			var response = await this.PromoteObjectsAsync(token, new ObjectVersionUpdateInformation()
			{
				ObjVer = objVer,
				Properties = propertyValues.ToList()
			});

			// Return the first item from the response.
			if(null == response || response.Count != 1)
				throw new InvalidOperationException($"The call to /objects/setmultipleobjproperties returned {response?.Count ?? 0} items, but 1 was expected");
			return response[0];
		}

		/// <summary>
		/// Promotes an unmanaged object to a managed object.
		/// </summary>
		/// <param name="objVer">The object to promote.</param>
		/// <param name="propertyValues">The object's new properties.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <remarks>The property values must be valid for the class, as they would if an object were being created.</remarks>
		public ExtendedObjectVersion PromoteObject(ObjVer objVer, PropertyValue[] propertyValues, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.PromoteObjectAsync(objVer, propertyValues, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

	}
}
