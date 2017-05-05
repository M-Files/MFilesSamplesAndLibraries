using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	public partial class MFWSClient
	{

		#region GetCheckoutStatus

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetCheckoutStatus(int,int,System.Nullable{int})"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task GetCheckoutStatus_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PrimitiveType<MFCheckOutStatus>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PrimitiveType<MFCheckOutStatus>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PrimitiveType<MFCheckOutStatus>()
						{
							Value = MFCheckOutStatus.CheckedOutToMe
						});

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetCheckoutStatus(0, 1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PrimitiveType<MFCheckOutStatus>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/2/checkedout", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetCheckoutStatus(int,int,System.Nullable{int})"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task GetCheckoutStatus_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<PrimitiveType<MFCheckOutStatus>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<PrimitiveType<MFCheckOutStatus>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new PrimitiveType<MFCheckOutStatus>()
						{
							Value = MFCheckOutStatus.CheckedOutToMe
						});

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetCheckoutStatus(0, 1, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<PrimitiveType<MFCheckOutStatus>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

		#region SetCheckoutStatus

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int})"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SetCheckoutStatus_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.SetCheckoutStatus(0, 1, MFCheckOutStatus.CheckedOutToMe, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/2/checkedout", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int})"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task SetCheckoutStatus_CorrectResource_LatestVersion()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.SetCheckoutStatus(0, 1, MFCheckOutStatus.CheckedOutToMe);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual($"/REST/objects/0/1/latest/checkedout", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int})"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task SetCheckoutStatus_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.SetCheckoutStatus(0, 1, MFCheckOutStatus.CheckedOutToMe, 2);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.PUT, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.SetCheckoutStatus(int,int,MFaaP.MFWSClient.MFCheckOutStatus,System.Nullable{int})"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task SetCheckoutStatus_CorrectMethod_LatestVersion()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.SetCheckoutStatus(0, 1, MFCheckOutStatus.CheckedOutToMe);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.PUT, methodUsed);
		}

		#endregion

		#region GetProperties

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetObjectPropertyValues(MFaaP.MFWSClient.ObjVer[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task GetObjectPropertyValues_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<List<PropertyValue>>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetObjectPropertyValues(new ObjVer()
			{
				ID = 2,
				Type = 1
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<List<PropertyValue>>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects/properties;1/2/0", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSClient.GetObjectPropertyValues(MFaaP.MFWSClient.ObjVer[])"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task GetObjectPropertyValues_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<List<PropertyValue>>>(It.IsAny<IRestRequest>()))
				.Callback((IRestRequest r) => {
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
			var mfwsClient = this.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.GetObjectPropertyValues(new ObjVer()
			{
				ID = 2,
				Type = 1
			});

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<List<List<PropertyValue>>>(It.IsAny<IRestRequest>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.GET, methodUsed);
		}

		#endregion

	}
}
