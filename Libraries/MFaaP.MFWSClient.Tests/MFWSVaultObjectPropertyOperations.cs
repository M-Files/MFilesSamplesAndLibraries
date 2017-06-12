using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MFaaP.MFWSClient.Tests.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultObjectPropertyOperations
	{

		#region GetProperties

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetProperties"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task GetObjectPropertyValuesAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<List<PropertyValue>>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<List<PropertyValue>>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new[]
						{
							new []
							{
								new PropertyValue()
							}.ToList()
						}.ToList());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectPropertyOperations.GetPropertiesAsync(new ObjVer()
			{
				ID = 2,
				Type = 1
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<List<PropertyValue>>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects/properties;1/2/0", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.GetProperties"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void GetObjectPropertyValues_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<List<PropertyValue>>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<List<PropertyValue>>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new[]
						{
							new []
							{
								new PropertyValue()
							}.ToList()
						}.ToList());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectPropertyOperations.GetProperties(new ObjVer()
			{
				ID = 2,
				Type = 1
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<List<PropertyValue>>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects/properties;1/2/0", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetObjectPropertyValues(MFaaP.MFWSClient.ObjVer[])"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task GetObjectPropertyValuesAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<List<PropertyValue>>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<List<PropertyValue>>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new[]
						{
							new []
							{
								new PropertyValue()
							}.ToList()
						}.ToList());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectPropertyOperations.GetPropertiesAsync(new ObjVer()
			{
				ID = 2,
				Type = 1
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<List<PropertyValue>>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetObjectPropertyValues(MFaaP.MFWSClient.ObjVer[])"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void GetObjectPropertyValues_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<List<PropertyValue>>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<List<PropertyValue>>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new[]
						{
							new []
							{
								new PropertyValue()
							}.ToList()
						}.ToList());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectPropertyOperations.GetProperties(new ObjVer()
			{
				ID = 2,
				Type = 1
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<List<PropertyValue>>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

		#region SetProperty

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetProperty(int,int,MFaaP.MFWSClient.PropertyValue,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void SetProperty_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectPropertyOperations.SetProperty(1, 2, new PropertyValue()
			{
				PropertyDef = 0,
				TypedValue = new TypedValue()
				{
					DataType = MFDataType.Text,
					Value = "hello world"
				}
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects/1/2/latest/properties/0", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetProperty(int,int,MFaaP.MFWSClient.PropertyValue,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void SetProperty_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectPropertyOperations.SetProperty(1, 2, new PropertyValue()
			{
				PropertyDef = 0,
				TypedValue = new TypedValue()
				{
					DataType = MFDataType.Text,
					Value = "hello world"
				}
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.PUT, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.SetProperty(int,int,MFaaP.MFWSClient.PropertyValue,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void SetProperty_CorrectBody()
		{
			/* Arrange */

			// The method.
			PropertyValue body = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					body = r.DeserializeBody<PropertyValue>();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectPropertyOperations.SetProperty(1, 2, new PropertyValue()
			{
				PropertyDef = 0,
				TypedValue = new TypedValue()
				{
					DataType = MFDataType.Text,
					Value = "hello world"
				}
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Body must be correct.
			Assert.IsNotNull(body);
			Assert.AreEqual(0, body.PropertyDef);
			Assert.IsNotNull(body.TypedValue);
			Assert.AreEqual(MFDataType.Text, body.TypedValue.DataType);
			Assert.AreEqual("hello world", body.TypedValue.Value);
		}

		#endregion

		#region RemoveProperty

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.RemoveProperty(int,int,int,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void RemoveProperty_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectPropertyOperations.RemoveProperty(1, 2, 0);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects/1/2/latest/properties/0", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectPropertyOperations.RemoveProperty(int,int,int,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void RemoveProperty_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectPropertyOperations.RemoveProperty(1, 2, 0);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.DELETE, methodUsed);
		}

		#endregion

	}
}
