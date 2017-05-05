using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultObjectPropertyOperations
	{

		#region GetProperties

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectOperations.GetObjectPropertyValues(MFaaP.MFWSClient.ObjVer[])"/>
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
			await mfwsClient.ObjectPropertyOperations.GetProperties(new ObjVer()
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
		public async Task GetObjectPropertyValues_CorrectMethod()
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
			await mfwsClient.ObjectPropertyOperations.GetProperties(new ObjVer()
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

	}
}
