using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultObjectTypeOperations
	{

		#region Object type alias to ID resolution

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectTypeOperations.GetObjectTypeIDByAliasAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetObjectTypeIDByAliasAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Dictionary<string, int>>(Method.POST, "/REST/structure/objecttypes/itemidbyalias.aspx");

			// Set up the expected body.
			var body = new JsonArray { "hello world" };
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ObjectTypeOperations.GetObjectTypeIDByAliasAsync("hello world");

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectTypeOperations.GetObjectTypeIDsByAliasesAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetObjectTypeIDsByAliasesAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Dictionary<string, int>>(Method.POST, "/REST/structure/objecttypes/itemidbyalias.aspx");

			// Set up the expected body.
			var body = new JsonArray { "hello", "world", "third option" };
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ObjectTypeOperations.GetObjectTypeIDsByAliasesAsync(aliases: new string[] { "hello", "world", "third option" });

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectTypeOperations.GetObjectTypeIDsByAliases"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetObjectTypeIDsByAliases()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Dictionary<string, int>>(Method.POST, "/REST/structure/objecttypes/itemidbyalias.aspx");

			// Set up the expected body.
			var body = new JsonArray { "hello", "world", "third option" };
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ObjectTypeOperations.GetObjectTypeIDsByAliases(aliases: new string[] { "hello", "world", "third option" });

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectTypeOperations.GetObjectTypeIDByAlias"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetObjectTypeIDByAlias()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Dictionary<string, int>>(Method.POST, "/REST/structure/objecttypes/itemidbyalias.aspx");

			// Set up the expected body.
			var body = new JsonArray { "hello world" };
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ObjectTypeOperations.GetObjectTypeIDByAlias("hello world");

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetObjectTypes

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectTypeOperations.GetObjectTypesAsync"/>
		/// requests the correct resource address using the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetObjectTypesAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ObjType>>(Method.GET, "/REST/structure/objecttypes.aspx");

			// Execute.
			await runner.MFWSClient.ObjectTypeOperations.GetObjectTypesAsync();

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectTypeOperations.GetObjectTypes"/>
		/// requests the correct resource address using the correct method.
		/// </summary>
		[TestMethod]
		public void GetObjectTypes()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ObjType>>(Method.GET, "/REST/structure/objecttypes.aspx");

			// Execute.
			runner.MFWSClient.ObjectTypeOperations.GetObjectTypes();

			// Verify.
			runner.Verify();
		}

		#endregion

	}
}
