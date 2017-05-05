using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	public partial class MFWSClient
	{

		#region AuthenticateUsingSingleSignOn

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.AuthenticateUsingSingleSignOn"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task AuthenticateUsingSingleSignOn_CorrectResource()
		{
			/* Arrange */

			// The vault Guid to request.
			var guid = Guid.NewGuid();

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
				.Returns(Task.FromResult(new Mock<IRestResponse>().Object));

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.AuthenticateUsingSingleSignOn(guid);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/WebServiceSSO.aspx?popup=1&vault={guid:D}", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.AuthenticateUsingSingleSignOn"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task AuthenticateUsingSingleSignOn_CorrectMethod()
		{
			/* Arrange */

			// The vault Guid to request.
			var guid = Guid.NewGuid();

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
				.Returns(Task.FromResult(new Mock<IRestResponse>().Object));

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.AuthenticateUsingSingleSignOn(guid);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.AuthenticateUsingSingleSignOn"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task AuthenticateUsingSingleSignOn_SessionIdSet()
		{
			/* Arrange */

			// The vault Guid to request.
			var guid = Guid.NewGuid();

			// The ASP.NET session Id (dummy value).
			var sessionId = Guid.NewGuid().ToString();

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();
			mock.SetupAllProperties();
			mock.SetupGet(c => c.BaseUrl)
				.Returns(new Uri("http://example.org/"));

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				// Return a mock response.
				.Returns(() =>
				{
					var response = new Mock<IRestResponse>();
					response.SetupGet(r => r.Cookies)
						.Returns(new List<RestResponseCookie>()
						{
							new RestResponseCookie() {
								Name = "ASP.NET_SessionId",
								Value = sessionId,
								Path = "/",
								Domain = "example.org"
							}
						});
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.AuthenticateUsingSingleSignOn(guid);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Ensure cookie is in default cookie container.
			var requestSessionCookie = mfwsClient
				.CookieContainer
				.GetCookies(new Uri("http://example.org"))
				.Cast<Cookie>()
				.FirstOrDefault(c => c.Name == "ASP.NET_SessionId");
			Assert.IsNotNull(requestSessionCookie);
			Assert.AreEqual(requestSessionCookie.Value, sessionId);
		}

		#endregion

		#region AuthenticateUsingCredentials

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.AuthenticateUsingCredentials"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task AuthenticateUsingCredentials_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PrimitiveType<string>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PrimitiveType<string>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PrimitiveType<string>()
						{
							Value = "hello world"
						});

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.AuthenticateUsingCredentials(Guid.NewGuid(), "my username", "my password");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PrimitiveType<string>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/server/authenticationtokens", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.AuthenticateUsingCredentials"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task AuthenticateUsingCredentials_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PrimitiveType<string>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PrimitiveType<string>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PrimitiveType<string>()
						{
							Value = "hello world"
						});

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.AuthenticateUsingCredentials(Guid.NewGuid(), "my username", "my password");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PrimitiveType<string>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.AuthenticateUsingCredentials"/>
		/// uses the correct body.
		/// </summary>
		[TestMethod]
		public async Task AuthenticateUsingCredentials_CorrectBody()
		{
			/* Arrange */

			// The vault guid.
			var vaultGuid = Guid.NewGuid();

			// The request body.
			var requestBody = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PrimitiveType<string>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					requestBody = r.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value?.ToString();
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PrimitiveType<string>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PrimitiveType<string>()
						{
							Value = "hello world"
						});

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.AuthenticateUsingCredentials(vaultGuid, "my username", "my password");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PrimitiveType<string>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Body must be correct.
			Assert.AreEqual($"{{\"Username\":\"my username\",\"Password\":\"my password\",\"Domain\":null,\"WindowsUser\":false,\"ComputerName\":null,\"VaultGuid\":\"{vaultGuid.ToString("D")}\",\"Expiration\":null,\"ReadOnly\":false,\"URL\":null,\"Method\":null}}", requestBody);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.AuthenticateUsingCredentials"/>
		/// sets the authentication header.
		/// </summary>
		[TestMethod]
		public async Task AuthenticateUsingCredentials_AuthenticationHeaderSet()
		{
			/* Arrange */

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PrimitiveType<string>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PrimitiveType<string>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PrimitiveType<string>()
						{
							Value = "hello world"
						});

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.AuthenticateUsingCredentials(Guid.NewGuid(), "my username", "my password");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PrimitiveType<string>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Authentication header must exist.
			var authenticationHeader = mock.Object
				.DefaultParameters
				.FirstOrDefault(h => h.Type == ParameterType.HttpHeader && h.Name == "X-Authentication");
			Assert.IsNotNull(authenticationHeader);
			Assert.AreEqual("hello world", authenticationHeader.Value);
		}

		#endregion

		#region GetOnlineVaults

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetOnlineVaults"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task GetOnlineVaults_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<Vault>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<Vault>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new List<Vault>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetOnlineVaults();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<Vault>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/server/vaults?online=true", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetOnlineVaults"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task GetOnlineVaults_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<Vault>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<Vault>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new List<Vault>());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetOnlineVaults();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<Vault>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

	}
}
