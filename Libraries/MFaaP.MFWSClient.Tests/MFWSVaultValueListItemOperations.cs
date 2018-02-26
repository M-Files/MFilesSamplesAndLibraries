using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultValueListItemOperations
	{

		#region GetValueListItems

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultValueListItemOperations.GetValueListItemsAsync"/>
		/// requests the correct resource using the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetValueListItemsAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Results<ValueListItem>>(Method.GET, "/REST/valuelists/1/items");

			// Execute.
			await runner.MFWSClient.ValueListItemOperations.GetValueListItemsAsync(1);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultValueListItemOperations.GetValueListItems"/>
		/// requests the correct resource using the correct method.
		/// </summary>
		[TestMethod]
		public void GetValueListItems()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Results<ValueListItem>>(Method.GET, "/REST/valuelists/1/items");

			// Execute.
			runner.MFWSClient.ValueListItemOperations.GetValueListItems(1);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultValueListItemOperations.GetValueListItemsAsync"/>
		/// requests the correct resource using the correct method (when using a name filter).
		/// </summary>
		[TestMethod]
		public async Task GetValueListItemsAsync_WithNameFilter()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Results<ValueListItem>>(Method.GET, "/REST/valuelists/1/items?filter=hello");

			// Execute.
			await runner.MFWSClient.ValueListItemOperations.GetValueListItemsAsync(1, "hello");

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultValueListItemOperations.GetValueListItems"/>
		/// requests the correct resource using the correct method (when using a name filter).
		/// </summary>
		[TestMethod]
		public void GetValueListItems_WithNameFilter()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Results<ValueListItem>>(Method.GET, "/REST/valuelists/1/items?filter=hello");

			// Execute.
			runner.MFWSClient.ValueListItemOperations.GetValueListItems(1, "hello");

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultValueListItemOperations.GetValueListItemsAsync"/>
		/// requests the correct resource using the correct method (when using a limit).
		/// </summary>
		[TestMethod]
		public async Task GetValueListItemsAsync_WithLimit()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Results<ValueListItem>>(Method.GET, "/REST/valuelists/1/items?limit=501");

			// Execute.
			await runner.MFWSClient.ValueListItemOperations.GetValueListItemsAsync(1, limit: 501);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultValueListItemOperations.GetValueListItems"/>
		/// requests the correct resource using the correct method (when using a limit).
		/// </summary>
		[TestMethod]
		public void GetValueListItems_WithLimit()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Results<ValueListItem>>(Method.GET, "/REST/valuelists/1/items?limit=501");

			// Execute.
			runner.MFWSClient.ValueListItemOperations.GetValueListItems(1, limit: 501);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultValueListItemOperations.GetValueListItemsAsync"/>
		/// requests the correct resource using the correct method (when using a name filter and limit).
		/// </summary>
		[TestMethod]
		public async Task GetValueListItemsAsync_WithLimitAndNameFilter()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Results<ValueListItem>>(Method.GET, "/REST/valuelists/1/items?filter=hello&limit=501");

			// Execute.
			await runner.MFWSClient.ValueListItemOperations.GetValueListItemsAsync(1, "hello", limit: 501);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultValueListItemOperations.GetValueListItems"/>
		/// requests the correct resource using the correct method (when using a name filter and limit).
		/// </summary>
		[TestMethod]
		public void GetValueListItems_WithLimitAndNameFilter()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Results<ValueListItem>>(Method.GET, "/REST/valuelists/1/items?filter=hello&limit=501");

			// Execute.
			runner.MFWSClient.ValueListItemOperations.GetValueListItems(1, "hello", limit: 501);

			// Verify.
			runner.Verify();
		}

		#endregion

	}
}
