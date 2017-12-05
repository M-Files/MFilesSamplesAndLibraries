using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MFaaP.MFilesAPI.Tests.ExtensionMethods;
using MFaaP.MFWSClient.Tests.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultObjectPropertyOperations
		: TestBase
	{

		#region GetProperties

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetProperties"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		[RestApiTest(Method.POST, "/REST/objects/properties")]
		public void GetProperties()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<List<PropertyValue>>>();
			runner.Setup(this.TestContext);

			// Create the object to send in the body.
			var body = new ObjVer()
			{
				ID = 2,
				Type = 1
			};

			// We should post a collection of objvers (but only with this one in it).
			runner.SetExpectedRequestBody(new[] { body });

			// Execute.
			runner.MFWSClient.ObjectPropertyOperations.GetProperties(body);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetPropertiesAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		[RestApiTest(Method.POST, "/REST/objects/properties")]
		public async Task GetPropertiesAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<List<PropertyValue>>>();
			runner.Setup(this.TestContext);

			// Create the object to send in the body.
			var body = new ObjVer()
			{
				ID = 2,
				Type = 1
			};

			// We should post a collection of objvers (but only with this one in it).
			runner.SetExpectedRequestBody(new[] { body });

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.GetPropertiesAsync(body);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region SetProperty

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetProperty(int,int,MFaaP.MFWSClient.PropertyValue,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		[RestApiTest(Method.PUT, "/REST/objects/1/2/latest/properties/0")]
		public void SetProperty()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>();
			runner.Setup(this.TestContext);

			// Create the object to send in the body.
			var body = new PropertyValue()
			{
				PropertyDef = 0,
				TypedValue = new TypedValue()
				{
					DataType = MFDataType.Text,
					Value = "hello world"
				}
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ObjectPropertyOperations.SetProperty(1, 2, body);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetPropertyAsync(int,int,MFaaP.MFWSClient.PropertyValue,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		[RestApiTest(Method.PUT, "/REST/objects/1/2/latest/properties/0")]
		public async Task SetPropertyAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>();
			runner.Setup(this.TestContext);

			// Create the object to send in the body.
			var body = new PropertyValue()
			{
				PropertyDef = 0,
				TypedValue = new TypedValue()
				{
					DataType = MFDataType.Text,
					Value = "hello world"
				}
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.SetPropertyAsync(1, 2, body);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region RemoveProperty

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.RemoveProperty(int,int,int,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		[RestApiTest(Method.DELETE, "/REST/objects/1/2/latest/properties/0")]
		public void RemoveProperty()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>();
			runner.Setup(this.TestContext);

			// Execute.
			runner.MFWSClient.ObjectPropertyOperations.RemoveProperty(1, 2, 0);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.RemovePropertyAsync(int,int,int,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		[RestApiTest(Method.DELETE, "/REST/objects/1/2/latest/properties/0")]
		public async Task RemovePropertyAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ExtendedObjectVersion>();
			runner.Setup(this.TestContext);

			// Execute.
			await runner.MFWSClient.ObjectPropertyOperations.RemovePropertyAsync(1, 2, 0);

			// Verify.
			runner.Verify();
		}

		#endregion

	}
}
