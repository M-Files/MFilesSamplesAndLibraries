using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultClassOperations
	{

		#region Object type alias to ID resolution

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultClassOperations.GetObjectClassIDByAliasAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetObjectClassIDByAliasAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Dictionary<string, int>>(Method.POST, "/REST/structure/classes/itemidbyalias.aspx");

			// Set up the expected body.
			var body = new JsonArray { "hello world" };
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ClassOperations.GetObjectClassIDByAliasAsync("hello world");

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultClassOperations.GetObjectClassIDsByAliasesAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetObjectClassIDsByAliasesAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Dictionary<string, int>>(Method.POST, "/REST/structure/classes/itemidbyalias.aspx");

			// Set up the expected body.
			var body = new JsonArray { "hello", "world", "third option" };
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ClassOperations.GetObjectClassIDsByAliasesAsync(aliases: new string[] { "hello", "world", "third option" });

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultClassOperations.GetObjectClassIDsByAliases"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetObjectClassIDsByAliases()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Dictionary<string, int>>(Method.POST, "/REST/structure/classes/itemidbyalias.aspx");

			// Set up the expected body.
			var body = new JsonArray { "hello", "world", "third option" };
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ClassOperations.GetObjectClassIDsByAliases(aliases: new string[] { "hello", "world", "third option" });

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultClassOperations.GetObjectClassIDByAlias"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetObjectClassIDByAlias()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<Dictionary<string, int>>(Method.POST, "/REST/structure/classes/itemidbyalias.aspx");

			// Set up the expected body.
			var body = new JsonArray { "hello world" };
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ClassOperations.GetObjectClassIDByAlias("hello world");

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetAllObjectClasses

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultClassOperations.GetAllObjectClassesAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetAllObjectClassesAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ExtendedObjectClass>>(Method.GET, "/REST/structure/classes.aspx");

			// Execute.
			await runner.MFWSClient.ClassOperations.GetAllObjectClassesAsync();

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultClassOperations.GetAllObjectClasses"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetAllObjectClasses()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ExtendedObjectClass>>(Method.GET, "/REST/structure/classes.aspx");

			// Execute.
			runner.MFWSClient.ClassOperations.GetAllObjectClasses();

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetObjectClasses

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultClassOperations.GetObjectClassesAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetObjectClassesAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ExtendedObjectClass>>(Method.GET, "/REST/structure/classes.aspx?objtype=0");

			// Execute.
			await runner.MFWSClient.ClassOperations.GetObjectClassesAsync(0);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultClassOperations.GetObjectClasses"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetObjectClasses()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<ExtendedObjectClass>>(Method.GET, "/REST/structure/classes.aspx?objtype=0");

			// Execute.
			runner.MFWSClient.ClassOperations.GetObjectClasses(0);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetObjectClass

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultClassOperations.GetObjectClassAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetObjectClassAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectClass>(Method.GET, "/REST/structure/classes/0.aspx");

			// Execute.
			await runner.MFWSClient.ClassOperations.GetObjectClassAsync(classId: 0);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultClassOperations.GetObjectClass"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetObjectClass_CorrectResource()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectClass>(Method.GET, "/REST/structure/classes/0.aspx");

			// Execute.
			runner.MFWSClient.ClassOperations.GetObjectClass(classId: 0);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetObjectClass (with templates)

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultClassOperations.GetObjectClassAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetObjectClassAsync_WithTemplates()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectClass>(Method.GET, "/REST/structure/classes/0.aspx?include=templates");

			// Execute.
			await runner.MFWSClient.ClassOperations.GetObjectClassAsync(classId: 0, includeTemplates: true);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultClassOperations.GetObjectClass"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void GetObjectClass_CorrectResource_WithTemplates()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectClass>(Method.GET, "/REST/structure/classes/0.aspx?include=templates");

			// Execute.
			runner.MFWSClient.ClassOperations.GetObjectClass(classId: 0, includeTemplates: true);

			// Verify.
			runner.Verify();
		}

		#endregion

	}
}
