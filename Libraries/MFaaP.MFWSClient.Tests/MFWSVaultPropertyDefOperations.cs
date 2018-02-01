using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultPropertyDefOperations
	{

		#region GetPropertyDefs

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultPropertyDefOperations.GetPropertyDefsAsync"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task GetPropertyDefsAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<PropertyDef>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<PropertyDef>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new List<PropertyDef>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.PropertyDefOperations.GetPropertyDefsAsync();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<PropertyDef>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/structure/properties", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultPropertyDefOperations.GetPropertyDefs"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void GetPropertyDefs_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<PropertyDef>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<PropertyDef>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new List<PropertyDef>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.PropertyDefOperations.GetPropertyDefs();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<PropertyDef>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/structure/properties", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultPropertyDefOperations.GetPropertyDefsAsync"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task GetPropertyDefsAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<PropertyDef>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<PropertyDef>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new List<PropertyDef>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.PropertyDefOperations.GetPropertyDefsAsync();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<PropertyDef>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultPropertyDefOperations.GetPropertyDefs"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void GetPropertyDefs_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<PropertyDef>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<PropertyDef>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new List<PropertyDef>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.PropertyDefOperations.GetPropertyDefs();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<PropertyDef>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

		#region GetPropertyDef

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultPropertyDefOperations.GetPropertyDefAsync"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task GetPropertyDefAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PropertyDef>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PropertyDef());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.PropertyDefOperations.GetPropertyDefAsync(123);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/structure/properties/123", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultPropertyDefOperations.GetPropertyDef"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void GetPropertyDef_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PropertyDef>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PropertyDef());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.PropertyDefOperations.GetPropertyDef(567);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/structure/properties/567", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultPropertyDefOperations.GetPropertyDefAsync"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task GetPropertyDefAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PropertyDef>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PropertyDef());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.PropertyDefOperations.GetPropertyDefAsync(123);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultPropertyDefOperations.GetPropertyDef"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void GetPropertyDef_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PropertyDef>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PropertyDef());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.PropertyDefOperations.GetPropertyDef(567);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

	}
}
