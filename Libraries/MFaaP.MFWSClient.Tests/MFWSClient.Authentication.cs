using System;
using System.Collections.Generic;
using System.Linq;
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
		public void AuthenticateUsingSingleSignOn_CorrectResource()
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
				.Setup(c => c.Execute(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(new Mock<IRestResponse>().Object);

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			mfwsClient.AuthenticateUsingSingleSignOn(guid);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.Execute(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/WebServiceSSO.aspx?popup=1&vault={guid.ToString("D")}", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.AuthenticateUsingSingleSignOn"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void AuthenticateUsingSingleSignOn_CorrectMethod()
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
				.Setup(c => c.Execute(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(new Mock<IRestResponse>().Object);

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			mfwsClient.AuthenticateUsingSingleSignOn(guid);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.Execute(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

		#region AuthenticateUsingCredentials

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.AuthenticateUsingCredentials"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void AuthenticateUsingCredentials_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.Execute<PrimitiveType<string>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
					return response.Object;
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			mfwsClient.AuthenticateUsingCredentials(Guid.NewGuid(), "my username", "my password");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.Execute<PrimitiveType<string>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/server/authenticationtokens", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.AuthenticateUsingCredentials"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void AuthenticateUsingCredentials_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.Execute<PrimitiveType<string>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
					return response.Object;
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			mfwsClient.AuthenticateUsingCredentials(Guid.NewGuid(), "my username", "my password");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.Execute<PrimitiveType<string>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.AuthenticateUsingCredentials"/>
		/// uses the correct body.
		/// </summary>
		[TestMethod]
		public void AuthenticateUsingCredentials_CorrectBody()
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
				.Setup(c => c.Execute<PrimitiveType<string>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
					return response.Object;
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			mfwsClient.AuthenticateUsingCredentials(vaultGuid, "my username", "my password");

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.Execute<PrimitiveType<string>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Body must be correct.
			Assert.AreEqual($"{{\"Username\":\"my username\",\"Password\":\"my password\",\"Domain\":null,\"WindowsUser\":false,\"ComputerName\":null,\"VaultGuid\":\"{vaultGuid.ToString("D")}\",\"Expiration\":null,\"ReadOnly\":false,\"URL\":null,\"Method\":null}}", requestBody);
		}

		#endregion

		#region GetOnlineVaults

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetOnlineVaults"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void GetOnlineVaults_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.Execute<List<Vault>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
					return response.Object;
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			mfwsClient.GetOnlineVaults();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.Execute<List<Vault>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/server/vaults?online=true", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetOnlineVaults"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void GetOnlineVaults_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.Execute<List<Vault>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
					return response.Object;
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			mfwsClient.GetOnlineVaults();

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.Execute<List<Vault>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

	}
}
