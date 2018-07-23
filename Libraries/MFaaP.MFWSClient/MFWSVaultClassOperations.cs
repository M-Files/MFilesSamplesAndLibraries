using System;
using System.Collections.Generic;
using System.Linq;
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

		#region Class alias to ID resolution

		/// <summary>
		/// Retrieves the ID for a class with a given alias in the vault.
		/// </summary>
		/// <param name="alias">The alias for the class.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no classes have the alias, or more than one does).</remarks>
		/// <remarks>Only available in M-Files 12.0.6768.0 upwards.</remarks>
		public async Task<int> GetObjectClassIDByAliasAsync(string alias, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			var output = await this.GetObjectClassIDsByAliasesAsync(token, aliases: new string[] { alias });
			return output?.Count == 1
				? output[0]
				: -1;
		}

		/// <summary>
		/// Retrieves the IDs for classes with given aliases in the vault.
		/// </summary>
		/// <param name="aliases">The aliases for the class.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no object type have the alias, or more than one does).</remarks>
		/// <remarks>Only available in M-Files 12.0.6768.0 upwards.</remarks>
		public async Task<List<int>> GetObjectClassIDsByAliasesAsync(CancellationToken token = default(CancellationToken), params string[] aliases)
		{
			// Sanity.
			if (null == aliases)
				throw new ArgumentNullException(nameof(aliases));
			if (0 == aliases.Length)
				return new List<int>();

			// Create the request.
			var request = new RestRequest($"/REST/structure/classes/itemidbyalias.aspx");

			// Assign the body.
			request.AddJsonBody(aliases);

			// Make the request and get the response.
			var response = await this.MFWSClient.Post<Dictionary<string, int>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return aliases.Select(alias => response.Data.ContainsKey(alias) ? response.Data[alias] : -1).ToList();
		}

		/// <summary>
		/// Retrieves the IDs for classes with given aliases in the vault.
		/// </summary>
		/// <param name="aliases">The aliases for the class.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no object type have the alias, or more than one does).</remarks>
		public List<int> GetObjectClassIDsByAliases(CancellationToken token = default(CancellationToken), params string[] aliases)
		{
			// Execute the async method.
			return this.GetObjectClassIDsByAliasesAsync(token, aliases)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves the ID for a class with a given alias in the vault.
		/// </summary>
		/// <param name="alias">The alias for the class.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no classes have the alias, or more than one does).</remarks>
		/// <remarks>Only available in M-Files 12.0.6768.0 upwards.</remarks>
		public int GetObjectClassIDByAlias(string alias, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			var output = this.GetObjectClassIDsByAliases(token, aliases: new string[] { alias });
			return output?.Count == 1
				? output[0]
				: -1;
		}

		#endregion

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
			var request = new RestRequest($"/REST/structure/classes/{classId}.aspx");

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
			var request = new RestRequest($"/REST/structure/classes.aspx");

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
			var request = new RestRequest($"/REST/structure/classes.aspx?objtype={objectTypeId}");

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
