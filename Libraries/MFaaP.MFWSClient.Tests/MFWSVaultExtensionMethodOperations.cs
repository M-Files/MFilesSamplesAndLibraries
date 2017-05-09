using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultExtensionMethodOperations
	{

		#region Extension Method (no serialisation/deserialisation)

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod{TB}"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethod_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse>();

					// Setup the return data.
					response.SetupGet(r => r.Content)
						.Returns("returnValue");

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ExtensionMethodOperations.ExecuteVaultExtensionMethod("HelloWorld");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/vault/extensionmethod/HelloWorld", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod{TB}"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethod_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse>();

					// Setup the return data.
					response.SetupGet(r => r.Content)
						.Returns("returnValue");

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ExtensionMethodOperations.ExecuteVaultExtensionMethod("HelloWorld");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethod_CorrectRequestBody()
		{
			/* Arrange */

			// The input value.
			var inputValue = "this is my test input value";

			// The actual request body.
			var requestBody = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					requestBody = r.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value?.ToString();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse>();

					// Setup the return data.
					response.SetupGet(r => r.Content)
						.Returns("returnValue");

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ExtensionMethodOperations.ExecuteVaultExtensionMethod("HelloWorld", inputValue);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Request body must be correct.
			Assert.AreEqual(inputValue, requestBody);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethod_CorrectOutput()
		{
			/* Arrange */

			// The input value.
			var outputValue = "Return value";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse>();

					// Setup the return data.
					response.SetupGet(r => r.Content)
						.Returns(outputValue);

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			var output = await mfwsClient.ExtensionMethodOperations.ExecuteVaultExtensionMethod("HelloWorld", "this is my test input value");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Request body must be correct.
			Assert.AreEqual(outputValue, output);
		}

		#endregion

		#region Extension Method (serialisation of input, no deserialisation of output)

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethod_CorrectResource_InputSerialisation()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse>();

					// Setup the return data.
					response.SetupGet(r => r.Content)
						.Returns("returnValue");

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ExtensionMethodOperations.ExecuteVaultExtensionMethod("HelloWorld", new
			{
				a = "b",
				x = 7
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/vault/extensionmethod/HelloWorld", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethod_CorrectMethod_InputSerialisation()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse>();

					// Setup the return data.
					response.SetupGet(r => r.Content)
						.Returns("returnValue");

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ExtensionMethodOperations.ExecuteVaultExtensionMethod("HelloWorld", new
			{
				a = "b",
				x = 7
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethod_CorrectRequestBody_InputSerialisation()
		{
			/* Arrange */

			// The actual request body.
			var requestBody = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					requestBody = r.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value?.ToString();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse>();

					// Setup the return data.
					response.SetupGet(r => r.Content)
						.Returns("returnValue");

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ExtensionMethodOperations.ExecuteVaultExtensionMethod("HelloWorld", new
			{
				a = "b",
				x = 7
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Request body must be correct.
			Assert.AreEqual("{\"a\":\"b\",\"x\":7}", requestBody);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultExtensionMethodOperations.ExecuteVaultExtensionMethod"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task ExecuteExtensionMethod_CorrectOutput_InputSerialisation()
		{
			/* Arrange */

			// The input value.
			var outputValue = "Return value";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse>();

					// Setup the return data.
					response.SetupGet(r => r.Content)
						.Returns(outputValue);

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			var output = await mfwsClient.ExtensionMethodOperations.ExecuteVaultExtensionMethod("HelloWorld", new
			{
				a = "b",
				x = 7
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Request body must be correct.
			Assert.AreEqual(outputValue, output);
		}

		#endregion

	}
}
