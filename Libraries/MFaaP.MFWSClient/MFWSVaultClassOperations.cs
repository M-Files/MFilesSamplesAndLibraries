using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Methods to create and modify objects.
	/// </summary>
	public class MFWSVaultClassOperations
		: MFWSVaultOperationsBase
	{
		/// <summary>
		/// Creates a new <see cref="MFWSVaultClassOperations"/> object.
		/// </summary>
		/// <param name="client">The client to interact with the server.</param>
		internal MFWSVaultClassOperations(MFWSClientBase client)
			: base(client)
		{
		}

		/// <summary>
		/// Gets a specific object class from the server, optionally including details on templates for that class.
		/// </summary>
		/// <param name="classId">The Id of the class to load.</param>
		/// <param name="includeTemplates">If true, information on the templates will be returned.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The class in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public async Task<ExtendedObjectClass> GetObjectClassAsync(int classId, bool includeTemplates = false, CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/structure/classes/{classId}");

			// Templates?
			if (includeTemplates)
			{
				request.Resource += "?include=templates";
			}

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<ExtendedObjectClass>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets a specific object class from the server, optionally including details on templates for that class.
		/// </summary>
		/// <param name="classId">The Id of the class to load.</param>
		/// <param name="includeTemplates">If true, information on the templates will be returned.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The class in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public ExtendedObjectClass GetObjectClass(int classId, bool includeTemplates = false, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetObjectClassAsync(classId, includeTemplates, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Gets a list of all classes in the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All classes in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public async Task<List<ExtendedObjectClass>> GetAllObjectClassesAsync(CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/structure/classes");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<ExtendedObjectClass>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets a list of all classes in the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All classes in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public List<ExtendedObjectClass> GetAllObjectClasses(CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetAllObjectClassesAsync(token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Gets a list of all classes in the vault for a given object type.
		/// </summary>
		/// <param name="objectTypeId">The type of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All classes in the vault for the supplied object type.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public async Task<List<ExtendedObjectClass>> GetObjectClassesAsync(int objectTypeId, CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/structure/classes?objtype={objectTypeId}");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<ExtendedObjectClass>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets a list of all classes in the vault for a given object type.
		/// </summary>
		/// <param name="objectTypeId">The type of the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All classes in the vault for the supplied object type.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public List<ExtendedObjectClass> GetObjectClasses(int objectTypeId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetObjectClassesAsync(objectTypeId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

	}
	
}
