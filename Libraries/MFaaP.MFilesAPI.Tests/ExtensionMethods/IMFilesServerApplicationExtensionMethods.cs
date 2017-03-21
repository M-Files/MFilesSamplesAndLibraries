using System;
using MFilesAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MFaaP.MFilesAPI.ExtensionMethods;
using Moq;

namespace MFaaP.MFilesAPI.Tests.ExtensionMethods
{
	[TestClass]
	// ReSharper disable once InconsistentNaming
	public class IMFilesServerApplicationExtensionMethods
	{

		#region Connect

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Connect_ThrowsWith_NullServerApplication()
		{

			// Arrange.
			var connectionDetails = new ConnectionDetails();

			// Act.
			((IMFilesServerApplication)null).Connect(connectionDetails);

		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Connect_ThrowsWith_NullConnectionDetailsApplication()
		{

			// Arrange.
			var mock = new Mock<IMFilesServerApplication>();

			// Act.
			mock.Object.Connect((ConnectionDetails)null);

		}

		[TestMethod]
		public void Connect_CallsConnectEx4_ExactlyOnce()
		{

			// Arrange.
			var mock = new Mock<IMFilesServerApplication>();

			// Act.
			mock.Object.Connect(this.CreateConnectionDetails());

			// Assert.
			mock.Verify(a => a.ConnectEx4(It.IsAny<TimeZoneInformation>(),
					It.IsAny<MFAuthType>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<string>()), Times.Exactly(1));

		}

		[TestMethod]
		public void Connect_CorrectTimeZoneInformation()
		{

			// Arrange.
			var mock = new Mock<IMFilesServerApplication>();
			mock.Setup(a => a.ConnectEx4(It.IsAny<TimeZoneInformation>(),
					It.IsAny<MFAuthType>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<string>()))
				.Callback((TimeZoneInformation timeZoneInformation, MFAuthType authType, object username, object password, object domain, object spn, string protocolSequence, string networkAddress, string endPoint, bool encryptedConnection, string localComputerName, bool allowAnonymousConnection, bool allowAuthenticationUsingPlugins, string logicalTargetServer, string clientCulture) =>
				{
					// Assert.
					Assert.AreEqual("UTC", timeZoneInformation.StandardName);
				});
			var connectionDetails = this.CreateConnectionDetails();
			connectionDetails.TimeZoneInformation.LoadTimeZoneByName("UTC");

			// Act.
			mock.Object.Connect(connectionDetails);

		}

		[TestMethod]
		public void Connect_CorrectAuthType_Default()
		{

			// Arrange.
			var mock = new Mock<IMFilesServerApplication>();
			mock.Setup(a => a.ConnectEx4(It.IsAny<TimeZoneInformation>(),
					It.IsAny<MFAuthType>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<string>()))
				.Callback((TimeZoneInformation timeZoneInformation, MFAuthType authType, object username, object password, object domain, object spn, string protocolSequence, string networkAddress, string endPoint, bool encryptedConnection, string localComputerName, bool allowAnonymousConnection, bool allowAuthenticationUsingPlugins, string logicalTargetServer, string clientCulture) =>
				{
					// Assert.
					Assert.AreEqual(MFAuthType.MFAuthTypeLoggedOnWindowsUser, authType);
				});
			var connectionDetails = this.CreateConnectionDetails();

			// Act.
			mock.Object.Connect(connectionDetails);

		}

		[TestMethod]
		public void Connect_CorrectAuthType_CurrentWindowsUser()
		{

			// Arrange.
			var mock = new Mock<IMFilesServerApplication>();
			mock.Setup(a => a.ConnectEx4(It.IsAny<TimeZoneInformation>(),
					It.IsAny<MFAuthType>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<string>()))
				.Callback((TimeZoneInformation timeZoneInformation, MFAuthType authType, object username, object password, object domain, object spn, string protocolSequence, string networkAddress, string endPoint, bool encryptedConnection, string localComputerName, bool allowAnonymousConnection, bool allowAuthenticationUsingPlugins, string logicalTargetServer, string clientCulture) =>
				{
					// Assert.
					Assert.AreEqual(MFAuthType.MFAuthTypeLoggedOnWindowsUser, authType);
				});
			var connectionDetails = this.CreateConnectionDetails();
			connectionDetails.AuthenticationDetails = AuthenticationDetails.CreateForLoggedOnWindowsUser();

			// Act.
			mock.Object.Connect(connectionDetails);

		}

		[TestMethod]
		public void Connect_CorrectAuthType_SpecificMFilesUser()
		{

			// Arrange.
			var mock = new Mock<IMFilesServerApplication>();
			mock.Setup(a => a.ConnectEx4(It.IsAny<TimeZoneInformation>(),
					It.IsAny<MFAuthType>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<string>()))
				.Callback((TimeZoneInformation timeZoneInformation, MFAuthType authType, object username, object password, object domain, object spn, string protocolSequence, string networkAddress, string endPoint, bool encryptedConnection, string localComputerName, bool allowAnonymousConnection, bool allowAuthenticationUsingPlugins, string logicalTargetServer, string clientCulture) =>
				{
					// Assert.
					Assert.AreEqual(MFAuthType.MFAuthTypeSpecificMFilesUser, authType);
					Assert.AreEqual("hello", username);
					Assert.AreEqual("world", password);
				});
			var connectionDetails = this.CreateConnectionDetails();
			connectionDetails.AuthenticationDetails = AuthenticationDetails.CreateForSpecificMFilesUser("hello", "world");

			// Act.
			mock.Object.Connect(connectionDetails);

		}

		[TestMethod]
		public void Connect_CorrectAuthType_SpecificWindowsUser()
		{

			// Arrange.
			var mock = new Mock<IMFilesServerApplication>();
			mock.Setup(a => a.ConnectEx4(It.IsAny<TimeZoneInformation>(),
					It.IsAny<MFAuthType>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<string>()))
				.Callback((TimeZoneInformation timeZoneInformation, MFAuthType authType, object username, object password, object domain, object spn, string protocolSequence, string networkAddress, string endPoint, bool encryptedConnection, string localComputerName, bool allowAnonymousConnection, bool allowAuthenticationUsingPlugins, string logicalTargetServer, string clientCulture) =>
				{
					// Assert.
					Assert.AreEqual(MFAuthType.MFAuthTypeSpecificWindowsUser, authType);
					Assert.AreEqual("hello", username);
					Assert.AreEqual("world", password);
					Assert.AreEqual("domain", domain);
					Assert.AreEqual("spn", spn);
				});
			var connectionDetails = this.CreateConnectionDetails();
			connectionDetails.AuthenticationDetails = AuthenticationDetails.CreateForSpecificWindowsUser("hello", "world", "domain", "spn");

			// Act.
			mock.Object.Connect(connectionDetails);

		}

		[TestMethod]
		public void Connect_CorrectServerDetails_TCPIP()
		{

			// Arrange.
			var mock = new Mock<IMFilesServerApplication>();
			mock.Setup(a => a.ConnectEx4(It.IsAny<TimeZoneInformation>(),
					It.IsAny<MFAuthType>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<string>()))
				.Callback((TimeZoneInformation timeZoneInformation, MFAuthType authType, object username, object password, object domain, object spn, string protocolSequence, string networkAddress, string endPoint, bool encryptedConnection, string localComputerName, bool allowAnonymousConnection, bool allowAuthenticationUsingPlugins, string logicalTargetServer, string clientCulture) =>
				{
					// Assert.;
					Assert.AreEqual("ncacn_ip_tcp", protocolSequence);
					Assert.AreEqual("serverName", networkAddress);
					Assert.AreEqual("12345", endPoint);
				});
			var connectionDetails = this.CreateConnectionDetails();
			connectionDetails.ServerDetails = ServerDetails.CreateForTcpIp("serverName", 12345);

			// Act.
			mock.Object.Connect(connectionDetails);

		}

		[TestMethod]
		public void Connect_CorrectServerDetails_HTTPS()
		{

			// Arrange.
			var mock = new Mock<IMFilesServerApplication>();
			mock.Setup(a => a.ConnectEx4(It.IsAny<TimeZoneInformation>(),
					It.IsAny<MFAuthType>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<string>()))
				.Callback((TimeZoneInformation timeZoneInformation, MFAuthType authType, object username, object password, object domain, object spn, string protocolSequence, string networkAddress, string endPoint, bool encryptedConnection, string localComputerName, bool allowAnonymousConnection, bool allowAuthenticationUsingPlugins, string logicalTargetServer, string clientCulture) =>
				{
					// Assert.;
					Assert.AreEqual("ncacn_http", protocolSequence);
					Assert.AreEqual("m-files.mycompany.com", networkAddress);
					Assert.AreEqual("123", endPoint);
				});
			var connectionDetails = this.CreateConnectionDetails();
			connectionDetails.ServerDetails = ServerDetails.CreateForHttps("m-files.mycompany.com", 123);

			// Act.
			mock.Object.Connect(connectionDetails);

		}

		[TestMethod]
		public void Connect_CorrectServerDetails_LPC()
		{

			// Arrange.
			var mock = new Mock<IMFilesServerApplication>();
			mock.Setup(a => a.ConnectEx4(It.IsAny<TimeZoneInformation>(),
					It.IsAny<MFAuthType>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<object>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<bool>(),
					It.IsAny<bool>(),
					It.IsAny<string>(),
					It.IsAny<string>()))
				.Callback((TimeZoneInformation timeZoneInformation, MFAuthType authType, object username, object password, object domain, object spn, string protocolSequence, string networkAddress, string endPoint, bool encryptedConnection, string localComputerName, bool allowAnonymousConnection, bool allowAuthenticationUsingPlugins, string logicalTargetServer, string clientCulture) =>
				{
					// Assert.;
					Assert.AreEqual("ncalrpc", protocolSequence);
				});
			var connectionDetails = this.CreateConnectionDetails();
			connectionDetails.ServerDetails = ServerDetails.CreateForLpc();

			// Act.
			mock.Object.Connect(connectionDetails);

		}

		/// <summary>
		/// Creates a <see cref="ConnectionDetails"/> object
		/// with a mock timezone (otherwise we cannot use <see cref="Mock{T}.Setup"/> to mock the connect calls).
		/// </summary>
		/// <returns>The connection details object.</returns>
		private ConnectionDetails CreateConnectionDetails()
		{
			// Create a mock of the timezone information.
			var timeZoneName = "";
			var timeZoneInformation = new Mock<TimeZoneInformation>();
			timeZoneInformation
				.Setup(t => t.LoadTimeZoneByName(It.IsAny<string>()))
				.Callback((string input) =>
				{
					timeZoneName = input;
				});
			timeZoneInformation
				.SetupGet(t => t.StandardName)
				.Returns(() => timeZoneName);

			// Return a new connection details.
			return new ConnectionDetails()
			{
				TimeZoneInformation = timeZoneInformation.Object
			};
		}

		#endregion

		#region Disconnect

		[TestMethod]
		public void Disconnect_DoesNotThrow_NullServerApplication()
		{

			((IMFilesServerApplication)null).Disconnect(Mock.Of<Vault>());

		}

		[TestMethod]
		public void Disconnect_DoesNotThrow_NullVault()
		{

			Mock.Of<IMFilesServerApplication>().Disconnect((Vault)null);

		}

		#endregion

	}
}
