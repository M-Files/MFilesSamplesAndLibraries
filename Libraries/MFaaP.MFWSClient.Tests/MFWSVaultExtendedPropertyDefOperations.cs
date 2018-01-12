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
	public class MFWSVaultExtendedPropertyDefOperations
    {

        #region GetExtendedPropertyDef

        /// <summary>
        /// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultPropertyDefOperations.GetExtendedPropertyDefAsync"/>
        /// requests the correct resource address.
        /// </summary>
        [TestMethod]
		public async Task GetExtendedPropertyDefAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedPropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedPropertyDef>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedPropertyDef());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.PropertyDefOperations.GetExtendedPropertyDefAsync(123);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedPropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/structure/properties/123", resourceAddress);
		}

        /// <summary>
        /// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultPropertyDefOperations.GetExtendedPropertyDef"/>
        /// requests the correct resource address.
        /// </summary>
        [TestMethod]
		public void GetExtendedPropertyDef_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedPropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedPropertyDef>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedPropertyDef());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.PropertyDefOperations.GetExtendedPropertyDef(567);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedPropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/structure/properties/567", resourceAddress);
		}

        /// <summary>
        /// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultPropertyDefOperations.GetExtendedPropertyDefAsync"/>
        /// uses the correct Http method.
        /// </summary>
        [TestMethod]
		public async Task GetExtendedPropertyDefAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedPropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedPropertyDef>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedPropertyDef());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.PropertyDefOperations.GetExtendedPropertyDefAsync(123);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedPropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

        /// <summary>
        /// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultPropertyDefOperations.GetExtendedPropertyDef"/>
        /// uses the correct Http method.
        /// </summary>
        [TestMethod]
		public void GetExtendedPropertyDef_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedPropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedPropertyDef>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedPropertyDef());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.PropertyDefOperations.GetExtendedPropertyDef(567);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedPropertyDef>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

	}
}
