using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public partial class MFWSClient
	{
		[TestMethod]
		public void EnsureExtensionsHeaderSetOnClientByDefault()
		{
			// Create the client and ensure that we have a default headers collection.
			var client = new MFaaP.MFWSClient.MFWSClient("http://localhost");
			Assert.IsNotNull(client.DefaultParameters);

			// Ensure that the "X-Extensions" header is set.
			var extensionsHeader = client
					.DefaultParameters
					.FirstOrDefault(dp => dp.Name == "X-Extensions" && dp.Type == ParameterType.HttpHeader);
			Assert.IsNotNull(extensionsHeader);

			// Extract the registered extensions.
			var extensionsValues = ((extensionsHeader.Value as string) ?? "").Split(",".ToCharArray());

			// Ensure that we have IML registered.
			if (1 != extensionsValues.Count(e => e.Trim() == "IML"))
			{
				Assert.Fail("IML is not in the default extensions HTTP header.");
			}
		}


		/// <summary>
		/// Creates a MFWSClient using the supplied mock request client.
		/// </summary>
		/// <param name="restClientMoq">The mocked rest client.</param>
		/// <returns></returns>
		public static MFaaP.MFWSClient.MFWSClient GetMFWSClient(Moq.Mock<IRestClient> restClientMoq)
		{
			// Sanity.
			if(null == restClientMoq)
				throw new ArgumentNullException(nameof(restClientMoq));

			// Ensure that we have a default parameter collection, if it's not been mocked already.
			if (null == restClientMoq.Object.DefaultParameters)
			{
				var defaultParameters = new List<Parameter>();
				restClientMoq
					.SetupGet(p => p.DefaultParameters)
					.Returns(defaultParameters);
			}

			// Return our proxy.
			return new MFWSClientProxy(restClientMoq.Object);
		}

		/// <summary>
		/// A proxy around <see cref="MFaaP.MFWSClient.MFWSClient"/>
		/// to allow provision of a mocked <see cref="IRestClient"/>.
		/// </summary>
		private class MFWSClientProxy
			: MFaaP.MFWSClient.MFWSClient
		{
			/// <inheritdoc />
			public MFWSClientProxy(IRestClient restClient) 
				: base(restClient)
			{
			}
			protected override void OnAfterExecuteRequest(IRestResponse e)
			{
				// If the response is null it's because we were testing for the wrong endpoint details.
				if (null == e)
				{
					Assert.Fail("Incorrect HTTP request (either method, endpoint address, or return type was invalid).");
					return;
				}

				// Base implementation.
				base.OnAfterExecuteRequest(e);
			}
		}
	}
}
