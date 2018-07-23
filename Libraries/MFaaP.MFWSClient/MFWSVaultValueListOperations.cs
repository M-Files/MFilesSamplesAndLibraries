using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// The VaultValueListOperations class represents the available value list operations.
	/// </summary>
	/// <remarks>ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultValueListOperations.html </remarks>
	public class MFWSVaultValueListOperations
		: MFWSVaultOperationsBase
	{
		/// <inheritdoc />
		internal MFWSVaultValueListOperations(MFWSClientBase client)
			: base(client)
		{
		}

		#region Value list alias to ID resolution

		/// <summary>
		/// Retrieves the ID for a value list with a given alias in the vault.
		/// </summary>
		/// <param name="alias">The alias for the value list.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no value lists have the alias, or more than one does).</remarks>
		/// <remarks>Only available in M-Files 12.0.6768.0 upwards.</remarks>
		public async Task<int> GetValueListIDByAliasAsync(string alias, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			var output = await this.GetValueListIDsByAliasesAsync(token, aliases: new string[] { alias });
			return output?.Count == 1
				? output[0]
				: -1;
		}

		/// <summary>
		/// Retrieves the IDs for value lists with given aliases in the vault.
		/// </summary>
		/// <param name="aliases">The aliases for the value list.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no value lists have the alias, or more than one does).</remarks>
		/// <remarks>Only available in M-Files 12.0.6768.0 upwards.</remarks>
		public async Task<List<int>> GetValueListIDsByAliasesAsync(CancellationToken token = default(CancellationToken), params string[] aliases)
		{
			// Sanity.
			if (null == aliases)
				throw new ArgumentNullException(nameof(aliases));
			if (0 == aliases.Length)
				return new List<int>();

			// Create the request.
			var request = new RestRequest($"/REST/structure/valuelists/itemidbyalias.aspx");

			// Assign the body.
			request.AddJsonBody(aliases);

			// Make the request and get the response.
			var response = await this.MFWSClient.Post<Dictionary<string, int>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return aliases.Select(alias => response.Data.ContainsKey(alias) ? response.Data[alias] : -1).ToList();
		}

		/// <summary>
		/// Retrieves the IDs for value lists with given aliases in the vault.
		/// </summary>
		/// <param name="aliases">The aliases for the value list.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no value lists have the alias, or more than one does).</remarks>
		public List<int> GetValueListIDsByAliases(CancellationToken token = default(CancellationToken), params string[] aliases)
		{
			// Execute the async method.
			return this.GetValueListIDsByAliasesAsync(token, aliases)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves the ID for a value list with a given alias in the vault.
		/// </summary>
		/// <param name="alias">The alias for the value list.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no value lists have the alias, or more than one does).</remarks>
		/// <remarks>Only available in M-Files 12.0.6768.0 upwards.</remarks>
		public int GetValueListIDByAlias(string alias, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			var output = this.GetValueListIDsByAliases(token, aliases: new string[] { alias });
			return output?.Count == 1
				? output[0]
				: -1;
		}

		#endregion

		/// <summary>
		/// Gets a list of all value lists in the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All value lists in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public async Task<List<ObjType>> GetValueListsAsync(CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/valuelists.aspx");

			// Make the request and get the response.
			var response = await this.MFWSClient.Get<List<ObjType>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;
		}

		/// <summary>
		/// Gets a list of all value lists in the vault.
		/// </summary>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>All value lists in the vault.</returns>
		/// <remarks>This may be filtered by the user's permissions.</remarks>
		public List<ObjType> GetValueLists(CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.GetValueListsAsync(token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}
	}
}
