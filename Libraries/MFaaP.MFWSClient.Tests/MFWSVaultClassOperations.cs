using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultClassOperations
	{

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
