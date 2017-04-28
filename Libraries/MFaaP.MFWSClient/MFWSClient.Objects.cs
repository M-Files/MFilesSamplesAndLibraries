using System;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{

		/// <summary>
		/// Sets an object checkout status.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <param name="status">The checkout status.</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public async Task<ObjectVersion> SetCheckoutStatus(int objectTypeId, int objectId, MFCheckOutStatus status, int? version = null)
		{

			// Sanity.
			if (objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");
			if (objectId <= 0)
				throw new ArgumentException("The object id cannot be less than or equal to zero");

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/{version?.ToString() ?? "latest"}/checkedout");
			request.AddJsonBody(status);

			// Make the request and get the response.
			var response = await this.Put<ObjectVersion>(request);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Retrieves an object' checkout status.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public async Task<MFCheckOutStatus?> GetCheckoutStatus(int objectTypeId, int objectId, int? version = null)
		{

			// Sanity.
			if (objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");
			if (objectId <= 0)
				throw new ArgumentException("The object id cannot be less than or equal to zero");

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/{version?.ToString() ?? "latest"}/checkedout");

			// Make the request and get the response.
			var response = await this.Get<PrimitiveType<MFCheckOutStatus>>(request);

			// Return the data.
			return response.Data?.Value;
		}

		/// <summary>
		/// Checks out an object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<ObjectVersion> CheckOut(int objectTypeId, int objectId, int? version = null)
		{
			return this.SetCheckoutStatus(objectTypeId, objectId, MFCheckOutStatus.CheckedOutToMe, version);
		}

		/// <summary>
		/// Checks in an object.
		/// </summary>
		/// <param name="objectTypeId">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="version">The version (or null for latest).</param>
		/// <returns>A representation of the checked-in object version/</returns>
		public Task<ObjectVersion> CheckIn(int objectTypeId, int objectId, int? version = null)
		{
			return this.SetCheckoutStatus(objectTypeId, objectId, MFCheckOutStatus.CheckedIn, version);
		}

	}
	
}
