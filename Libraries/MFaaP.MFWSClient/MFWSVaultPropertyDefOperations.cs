using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// A group of methods for creating and modifying property definitions.
	/// </summary>
	/// <remarks>ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultPropertyDefOperations.html </remarks>
	public class MFWSVaultPropertyDefOperations
		: MFWSVaultOperationsBase
	{

		/// <inheritdoc />
		public MFWSVaultPropertyDefOperations(MFWSClientBase client)
			: base(client)
		{
		}

		#region Property definition alias to ID resolution

		/// <summary>
		/// Retrieves the ID for a property definition with a given alias in the vault.
		/// </summary>
		/// <param name="alias">The alias for the property definition.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no property definitions have the alias, or more than one does).</remarks>
		/// <remarks>Only available in M-Files 12.0.6768.0 upwards.</remarks>
		public async Task<int> GetPropertyDefIDByAliasAsync(string alias, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			var output = await this.GetPropertyDefIDsByAliasesAsync(token, aliases: new string[] { alias });
			return output?.Count == 1
				? output[0]
				: -1;
		}

		/// <summary>
		/// Retrieves the ID for a property definition with a given alias in the vault.
		/// </summary>
		/// <param name="aliases">The aliases for the property definition.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no property definitions have the alias, or more than one does).</remarks>
		/// <remarks>Only available in M-Files 12.0.6768.0 upwards.</remarks>
		public async Task<List<int>> GetPropertyDefIDsByAliasesAsync(CancellationToken token = default(CancellationToken), params string[] aliases)
		{
			// Sanity.
			if(null == aliases)
				throw new ArgumentNullException(nameof(aliases));
			if (0 == aliases.Length)
				return new List<int>();

			// Create the request.
			var request = new RestRequest($"/REST/structure/properties/itemidbyalias.aspx");
			
			// Assign the body.
			request.AddJsonBody(aliases);

			// Make the request and get the response.
			var response = await this.MFWSClient.Post<Dictionary<string, int>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return aliases.Select(alias => response.Data.ContainsKey(alias) ? response.Data[alias] : -1).ToList();
		}

		/// <summary>
		/// Retrieves the ID for a property definition with a given alias in the vault.
		/// </summary>
		/// <param name="aliases">The aliases for the property definition.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>A collection of resolved IDs.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no property definitions have the alias, or more than one does).</remarks>
		public List<int> GetPropertyDefIDsByAliases(CancellationToken token = default(CancellationToken), params string[] aliases)
		{
			// Execute the async method.
			return this.GetPropertyDefIDsByAliasesAsync(token, aliases)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves the ID for a property definition with a given alias in the vault.
		/// </summary>
		/// <param name="alias">The alias for the property definition.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no property definitions have the alias, or more than one does).</remarks>
		/// <remarks>Only available in M-Files 12.0.6768.0 upwards.</remarks>
		public int GetPropertyDefIDByAlias(string alias, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			var output = this.GetPropertyDefIDsByAliases(token, aliases: new string[] { alias });
			return output?.Count == 1
				? output[0]
				: -1;
		}

		#endregion

		/// <summary>
		/// Gets all property definitions in the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All property definitions in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public async Task<List<PropertyDef>> GetPropertyDefsAsync(CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/structure/properties");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<PropertyDef>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets all property definitions in the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All property definitions in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public List<PropertyDef> GetPropertyDefs(CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetPropertyDefsAsync(token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Gets a single property definition in the vault.
		/// </summary>
		/// <param name="propertyDefId">The Id of the property definition to retrieve.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The property definition.</returns>
		/// <remarks>This may be affected by the user's permissions.</remarks>
		public async Task<PropertyDef> GetPropertyDefAsync(int propertyDefId, CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/structure/properties/{propertyDefId}");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<PropertyDef>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets a single property definition in the vault.
		/// </summary>
		/// <param name="propertyDefId">The Id of the property definition to retrieve.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The property definition.</returns>
		/// <remarks>This may be affected by the user's permissions.</remarks>
		public PropertyDef GetPropertyDef(int propertyDefId, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetPropertyDefAsync(propertyDefId, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}
    }
}
