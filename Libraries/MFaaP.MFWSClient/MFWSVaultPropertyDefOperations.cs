using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public class MFWSVaultPropertyDefOperations
		: MFWSVaultOperationsBase
	{

		/// <inheritdoc />
		public MFWSVaultPropertyDefOperations(MFWSClientBase client)
			: base(client)
		{
		}

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

        /// <summary>
        /// Gets a single property definition in the vault.
        /// </summary>
        /// <param name="propertyDefId">The Id of the property definition to retrieve.</param>
        /// <param name="token">A cancellation token for the request.</param>
        /// <returns>The property definition.</returns>
        /// <remarks>This may be affected by the user's permissions.</remarks>
        public async Task<ExtendedPropertyDef> GetExtendedPropertyDefAsync(int propertyDefId, CancellationToken token = default(CancellationToken))
        {
            // Create the request.
            var request = new RestRequest($"/REST/structure/properties/{propertyDefId}");

            // Make the request and get the response.
            var response = await this.MFWSClient.Get<ExtendedPropertyDef>(request, token)
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
        public ExtendedPropertyDef GetExtendedPropertyDef(int propertyDefId, CancellationToken token = default(CancellationToken))
        {
            // Execute the async method.
            return this.GetExtendedPropertyDefAsync(propertyDefId, token)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }
}
