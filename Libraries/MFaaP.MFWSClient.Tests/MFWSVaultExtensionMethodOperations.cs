using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultExtensionMethodOperations
	{

		#region Extension Method (no serialisation/deserialisation)

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod{TB}"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethodAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner(Method.POST, "/REST/vault/extensionmethod/HelloWorld.aspx");

			// Execute.
			await runner.MFWSClient.ExtensionMethodOperations.ExecuteVaultExtensionMethodAsync("HelloWorld");

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod{TB}"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void ExecuteExtensionMethod()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner(Method.POST, "/REST/vault/extensionmethod/HelloWorld.aspx");

			// Execute.
			runner.MFWSClient.ExtensionMethodOperations.ExecuteVaultExtensionMethod("HelloWorld");

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// includes the correct request body.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethodAsync_CorrectRequestBody()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner(Method.POST, "/REST/vault/extensionmethod/HelloWorld.aspx");

			// Set the request body.
			const string inputValue = "this is my test input value";
			runner.SetExpectedRequestBody(inputValue);

			// Execute.
			await runner.MFWSClient.ExtensionMethodOperations.ExecuteVaultExtensionMethodAsync("HelloWorld", input: inputValue);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// includes the correct request body.
		/// </summary>
		[TestMethod]
		public void ExecuteExtensionMethod_CorrectRequestBody()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner(Method.POST, "/REST/vault/extensionmethod/HelloWorld.aspx");

			// Set the request body.
			const string inputValue = "this is my test input value";
			runner.SetExpectedRequestBody(inputValue);

			// Execute.
			runner.MFWSClient.ExtensionMethodOperations.ExecuteVaultExtensionMethod("HelloWorld", input: inputValue);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// returns the correct response.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethodAsync_CorrectOutput()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner(Method.POST, "/REST/vault/extensionmethod/HelloWorld.aspx");

			// Set the request body.
			const string inputValue = "this is my test input value";
			runner.SetExpectedRequestBody(inputValue);

			// Set the response body.
			const string outputValue = "Return value";
			runner.ResponseData = outputValue;

			// Execute.
			var output = await runner.MFWSClient.ExtensionMethodOperations.ExecuteVaultExtensionMethodAsync("HelloWorld", input: inputValue);

			// Verify.
			runner.Verify();
			
			// Response body must be correct.
			Assert.AreEqual(outputValue, output);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// returns the correct response.
		/// </summary>
		[TestMethod]
		public void ExecuteExtensionMethod_CorrectOutput()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner(Method.POST, "/REST/vault/extensionmethod/HelloWorld.aspx");

			// Set the request body.
			const string inputValue = "this is my test input value";
			runner.SetExpectedRequestBody(inputValue);

			// Set the response body.
			const string outputValue = "Return value";
			runner.ResponseData = outputValue;

			// Execute.
			var output = runner.MFWSClient.ExtensionMethodOperations.ExecuteVaultExtensionMethod("HelloWorld", input: inputValue);

			// Verify.
			runner.Verify();

			// Response body must be correct.
			Assert.AreEqual(outputValue, output);
		}

		#endregion

		#region Extension Method (serialisation of input, no deserialisation of output)

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// includes the serialised request data.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethodAsync_InputSerialisation()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner(Method.POST, "/REST/vault/extensionmethod/HelloWorld.aspx");

			// Set the request body.
			var inputValue = new MySerialisableObject
			{
				a = "b",
				x = 7
			};
			runner.SetExpectedRequestBody(inputValue.ToSerializedString());

			// Set the response body.
			const string outputValue = "Return value";
			runner.ResponseData = outputValue;

			// Execute
			var output = await runner.MFWSClient.ExtensionMethodOperations.ExecuteVaultExtensionMethodAsync("HelloWorld", input: inputValue);

			// Verify.
			runner.Verify();

			// Response body must be correct.
			Assert.AreEqual(outputValue, output);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// includes the serialised request data.
		/// </summary>
		[TestMethod]
		public void ExecuteExtensionMethod_InputSerialisation()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner(Method.POST, "/REST/vault/extensionmethod/HelloWorld.aspx");

			// Set the request body.
			var inputValue = new MySerialisableObject
			{
				a = "b",
				x = 7
			};
			runner.SetExpectedRequestBody(inputValue.ToSerializedString());

			// Set the response body.
			const string outputValue = "Return value";
			runner.ResponseData = outputValue;

			// Execute
			var output = runner.MFWSClient.ExtensionMethodOperations.ExecuteVaultExtensionMethod("HelloWorld", input: inputValue);

			// Verify.
			runner.Verify();

			// Response body must be correct.
			Assert.AreEqual(outputValue, output);
		}

		#endregion

		#region Extension Method (serialisation and deserialisation)

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// includes the deserialised response data.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethodAsync_OutputDeserialisation()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<MySerialisableObject>(Method.POST, "/REST/vault/extensionmethod/HelloWorld.aspx");

			// Set the request body.
			var inputValue = new MySerialisableObject
			{
				a = "b",
				x = 7
			};
			runner.SetExpectedRequestBody(inputValue.ToSerializedString());

			// Set the response body.
			var outputValue = new MySerialisableObject
			{
				a = "c",
				x = 123
			};
			runner.ResponseData = outputValue;

			// Execute
			var output = await runner.MFWSClient
				.ExtensionMethodOperations
				.ExecuteVaultExtensionMethodAsync<MySerialisableObject, MySerialisableObject>("HelloWorld", input: inputValue);

			// Verify.
			runner.Verify();

			// Response must be correct.
			Assert.AreEqual(outputValue, output);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// includes the deserialised response data.
		/// </summary>
		[TestMethod]
		public void ExecuteExtensionMethod_OutputDeserialisation()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<MySerialisableObject>(Method.POST, "/REST/vault/extensionmethod/HelloWorld.aspx");

			// Set the request body.
			var inputValue = new MySerialisableObject
			{
				a = "b",
				x = 7
			};
			runner.SetExpectedRequestBody(inputValue.ToSerializedString());

			// Set the response body.
			var outputValue = new MySerialisableObject
			{
				a = "c",
				x = 123
			};
			runner.ResponseData = outputValue;

			// Execute
			var output = runner.MFWSClient
				.ExtensionMethodOperations
				.ExecuteVaultExtensionMethod<MySerialisableObject, MySerialisableObject>("HelloWorld", input: inputValue);

			// Verify.
			runner.Verify();

			// Response must be correct.
			Assert.AreEqual(outputValue, output);
		}

		#endregion

		public class MySerialisableObject
		{
			public string a = "b";
			public int x = 7;

			public string ToSerializedString()
			{
				return Newtonsoft.Json.JsonConvert.SerializeObject(this, Formatting.None);
			}
		}

	}
}
