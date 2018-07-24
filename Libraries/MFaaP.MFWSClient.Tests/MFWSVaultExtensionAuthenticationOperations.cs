using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Extensions.MonoHttp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultExtensionAuthenticationOperations
	{

		#region Retrieval of external repository information

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionAuthenticationOperations.GetExtensionAuthenticationTargetsAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetExtensionAuthenticationTargetsAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<RepositoryAuthenticationTarget>>(Method.GET, "/REST/repositories.aspx");

			// Execute.
			await runner.MFWSClient.ExtensionAuthenticationOperations.GetExtensionAuthenticationTargetsAsync();

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionAuthenticationOperations.GetExtensionAuthenticationTargets"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetExtensionAuthenticationTargets()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<RepositoryAuthenticationTarget>>(Method.GET, "/REST/repositories.aspx");

			// Execute.
			runner.MFWSClient.ExtensionAuthenticationOperations.GetExtensionAuthenticationTargets();

			// Verify.
			runner.Verify();
		}

		#endregion

		#region Logging into external repository connection

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionAuthenticationOperations.LogInWithExtensionAuthenticationAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task LogInWithExtensionAuthenticationAsync()
		{
			// Set the target ID.
			var targetID = "hello world";

			// Create our test runner.
			var runner = new RestApiTestRunner<RepositoryAuthenticationStatus>(Method.POST, $"/REST/repositories/{HttpUtility.UrlEncode(targetID)}/session.aspx");

			// Create the body.
			var authentication = new RepositoryAuthentication()
			{
				ConfigurationName = "hello"
			};

			// Set the expected body.
			runner.SetExpectedRequestBody(authentication);

			// Execute.
			await runner.MFWSClient.ExtensionAuthenticationOperations.LogInWithExtensionAuthenticationAsync(targetID, authentication);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionAuthenticationOperations.LogInWithExtensionAuthenticationAsync"/>
		/// with a null target throws an exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task LogInWithExtensionAuthenticationAsync_NullTarget()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<RepositoryAuthenticationStatus>(Method.POST, $"/REST/repositories//session.aspx");

			// Execute.
			await runner.MFWSClient.ExtensionAuthenticationOperations.LogInWithExtensionAuthenticationAsync(null, new RepositoryAuthentication());

			Assert.Fail("Expected exception not thrown.");
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionAuthenticationOperations.LogInWithExtensionAuthenticationAsync"/>
		/// with an empty target throws an exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public async Task LogInWithExtensionAuthenticationAsync_EmptyTarget()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<RepositoryAuthenticationStatus>(Method.POST, $"/REST/repositories//session.aspx");

			// Execute.
			await runner.MFWSClient.ExtensionAuthenticationOperations.LogInWithExtensionAuthenticationAsync("", new RepositoryAuthentication());

			Assert.Fail("Expected exception not thrown.");
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionAuthenticationOperations.LogInWithExtensionAuthenticationAsync"/>
		/// with null authentication throws an exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task LogInWithExtensionAuthenticationAsync_NullAuthentication()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<RepositoryAuthenticationStatus>(Method.POST, $"/REST/repositories//session.aspx");

			// Execute.
			await runner.MFWSClient.ExtensionAuthenticationOperations.LogInWithExtensionAuthenticationAsync("hello world", null);

			Assert.Fail("Expected exception not thrown.");
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionAuthenticationOperations.LogInWithExtensionAuthenticationAsync"/>
		/// with an empty/null authentication configuration name.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public async Task LogInWithExtensionAuthenticationAsync_EmptyAuthenticationConfiguration()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<RepositoryAuthenticationStatus>(Method.POST, $"/REST/repositories//session.aspx");

			// Execute.
			await runner.MFWSClient.ExtensionAuthenticationOperations.LogInWithExtensionAuthenticationAsync("hello world", new RepositoryAuthentication());

			Assert.Fail("Expected exception not thrown.");
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionAuthenticationOperations.LogInWithExtensionAuthentication"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void LogInWithExtensionAuthentication()
		{
			// Set the target ID.
			var targetID = "hello world";

			// Create our test runner.
			var runner = new RestApiTestRunner<RepositoryAuthenticationStatus>(Method.POST, $"/REST/repositories/{HttpUtility.UrlEncode(targetID)}/session.aspx");

			// Create the body.
			var authentication = new RepositoryAuthentication()
			{
				ConfigurationName = "hello"
			};

			// Set the expected body.
			runner.SetExpectedRequestBody(authentication);

			// Execute.
			runner.MFWSClient.ExtensionAuthenticationOperations.LogInWithExtensionAuthentication(targetID, authentication);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region Logging out of external repository connection

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionAuthenticationOperations.LogOutWithExtensionAuthenticationAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task LogOutWithExtensionAuthenticationAsync()
		{
			// Set the target ID.
			var targetID = "hello world";

			// Create our test runner.
			var runner = new RestApiTestRunner(Method.DELETE, $"/REST/repositories/{HttpUtility.UrlEncode(targetID)}/session.aspx");

			// Execute.
			await runner.MFWSClient.ExtensionAuthenticationOperations.LogOutWithExtensionAuthenticationAsync(targetID);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionAuthenticationOperations.LogOutWithExtensionAuthenticationAsync"/>
		/// with a null target throws an exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task LogOutWithExtensionAuthenticationAsync_NullTarget()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<RepositoryAuthenticationStatus>(Method.POST, $"/REST/repositories//session.aspx");

			// Execute.
			await runner.MFWSClient.ExtensionAuthenticationOperations.LogOutWithExtensionAuthenticationAsync(null);

			Assert.Fail("Expected exception not thrown.");
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionAuthenticationOperations.LogOutWithExtensionAuthenticationAsync"/>
		/// with an empty target throws an exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public async Task LogOutWithExtensionAuthenticationAsync_EmptyTarget()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<RepositoryAuthenticationStatus>(Method.POST, $"/REST/repositories//session.aspx");

			// Execute.
			await runner.MFWSClient.ExtensionAuthenticationOperations.LogOutWithExtensionAuthenticationAsync("");

			Assert.Fail("Expected exception not thrown.");
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionAuthenticationOperations.LogOutWithExtensionAuthentication"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void LogOutWithExtensionAuthentication()
		{
			// Set the target ID.
			var targetID = "hello world";

			// Create our test runner.
			var runner = new RestApiTestRunner(Method.DELETE, $"/REST/repositories/{HttpUtility.UrlEncode(targetID)}/session.aspx");

			// Execute.
			runner.MFWSClient.ExtensionAuthenticationOperations.LogOutWithExtensionAuthentication(targetID);

			// Verify.
			runner.Verify();
		}

		#endregion
	}
}
