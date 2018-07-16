using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// The VaultWorkflowOperations class represents the available workflow operations.
	/// </summary>
	/// <remarks>ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultWorkflowOperations.html </remarks>
	public class MFWSVaultWorkflowOperations
		: MFWSVaultOperationsBase
	{

		/// <inheritdoc />
		public MFWSVaultWorkflowOperations(MFWSClientBase client)
			: base(client)
		{
		}

		#region Workflow alias to ID resolution

		/// <summary>
		/// Retrieves the ID for a workflow with a given alias in the vault.
		/// </summary>
		/// <param name="alias">The alias for the workflow.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no workflows have the alias, or more than one does).</remarks>
		public async Task<int> GetWorkflowIDByAliasAsync(string alias, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			var output = await this.GetWorkflowIDsByAliasesAsync(token, aliases: new string[] { alias });
			return output?.Count == 1
				? output[0]
				: -1;
		}

		/// <summary>
		/// Retrieves the IDs for workflows with given aliases in the vault.
		/// </summary>
		/// <param name="aliases">The aliases for the workflow.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no object type have the alias, or more than one does).</remarks>
		public async Task<List<int>> GetWorkflowIDsByAliasesAsync(CancellationToken token = default(CancellationToken), params string[] aliases)
		{
			// Sanity.
			if (null == aliases)
				throw new ArgumentNullException(nameof(aliases));
			if (0 == aliases.Length)
				return new List<int>();

			// Create the request.
			var request = new RestRequest($"/REST/structure/workflows/itemidbyalias.aspx");

			// Assign the body.
			request.AddJsonBody(aliases);

			// Make the request and get the response.
			var response = await this.MFWSClient.Post<Dictionary<string, int>>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return aliases.Select(alias => response.Data.ContainsKey(alias) ? response.Data[alias] : -1).ToList();
		}

		/// <summary>
		/// Retrieves the IDs for workflows with given aliases in the vault.
		/// </summary>
		/// <param name="aliases">The aliases for the workflow.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no object type have the alias, or more than one does).</remarks>
		public List<int> GetWorkflowIDsByAliases(CancellationToken token = default(CancellationToken), params string[] aliases)
		{
			// Execute the async method.
			return this.GetWorkflowIDsByAliasesAsync(token, aliases)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// Retrieves the ID for a workflow with a given alias in the vault.
		/// </summary>
		/// <param name="alias">The alias for the workflow.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the request.</returns>
		/// <remarks>Returns -1 if the alias cannot be resolved (e.g. no workflows have the alias, or more than one does).</remarks>
		public int GetWorkflowIDByAlias(string alias, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			var output = this.GetWorkflowIDsByAliases(token, aliases: new string[] { alias });
			return output?.Count == 1
				? output[0]
				: -1;
		}

		#endregion

	}
}
