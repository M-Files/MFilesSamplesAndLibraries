using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultAutomaticMetadataOperations
	{

		#region GetAutomaticMetadataAsync

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultAutomaticMetadataOperations.GetAutomaticMetadataAsync(MFaaP.MFWSClient.AutomaticMetadataRequestInfo,System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetAutomaticMetadataAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValueSuggestion>>(Method.POST, "/REST/objects/automaticmetadata.aspx");

			// Execute.
			await runner.MFWSClient.AutomaticMetadataOperations.GetAutomaticMetadataAsync();

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultAutomaticMetadataOperations.GetAutomaticMetadataAsync(MFaaP.MFWSClient.AutomaticMetadataRequestInfo,System.Threading.CancellationToken)"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetAutomaticMetadata()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValueSuggestion>>(Method.POST, "/REST/objects/automaticmetadata.aspx");

			// Execute.
			runner.MFWSClient.AutomaticMetadataOperations.GetAutomaticMetadata();

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetAutomaticMetadataForObject

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultAutomaticMetadataOperations.GetAutomaticMetadataForObjectAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetAutomaticMetadataForObjectAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValueSuggestion>>(Method.POST, "/REST/objects/automaticmetadata.aspx");

			// Set up the expected body.
			var body = new AutomaticMetadataRequestInfo()
			{
				ObjVer = new ObjVer()
				{
					Type = 0,
					ID = 43,
					Version = 0
				}
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.AutomaticMetadataOperations.GetAutomaticMetadataForObjectAsync(body.ObjVer);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultAutomaticMetadataOperations.GetAutomaticMetadataForObject"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetAutomaticMetadataForObject()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValueSuggestion>>(Method.POST, "/REST/objects/automaticmetadata.aspx");

			// Set up the expected body.
			var body = new AutomaticMetadataRequestInfo()
			{
				ObjVer = new ObjVer()
				{
					Type = 0,
					ID = 43,
					Version = 0
				}
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.AutomaticMetadataOperations.GetAutomaticMetadataForObject(body.ObjVer);

			// Verify.
			runner.Verify();
		}

		#endregion

		#region GetAutomaticMetadataForTemporaryFiles

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultAutomaticMetadataOperations.GetAutomaticMetadataForTemporaryFileAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetAutomaticMetadataForTemporaryFileAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValueSuggestion>>(Method.POST, "/REST/objects/automaticmetadata.aspx");

			// Set up the expected body.
			var body = new AutomaticMetadataRequestInfo()
			{
				UploadIds = new List<int>() { 123 }
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.AutomaticMetadataOperations.GetAutomaticMetadataForTemporaryFileAsync(body.UploadIds.First());

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultAutomaticMetadataOperations.GetAutomaticMetadataForTemporaryFile"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetAutomaticMetadataForTemporaryFile()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValueSuggestion>>(Method.POST, "/REST/objects/automaticmetadata.aspx");

			// Set up the expected body.
			var body = new AutomaticMetadataRequestInfo()
			{
				UploadIds = new List<int>() { 123 }
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.AutomaticMetadataOperations.GetAutomaticMetadataForTemporaryFile(body.UploadIds.First());

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultAutomaticMetadataOperations.GetAutomaticMetadataForTemporaryFilesAsync"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public async Task GetAutomaticMetadataForTemporaryFilesAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<List<PropertyValueSuggestion>>(Method.POST, "/REST/objects/automaticmetadata.aspx");

			// Set up the expected body.
			var body = new AutomaticMetadataRequestInfo()
			{
				UploadIds = new List<int>() { 123, 456 }
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.AutomaticMetadataOperations.GetAutomaticMetadataForTemporaryFilesAsync(temporaryFileIds: body.UploadIds.ToArray());

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultAutomaticMetadataOperations.GetAutomaticMetadataForTemporaryFiles"/>
		/// requests the correct resource address with the correct method.
		/// </summary>
		[TestMethod]
		public void GetAutomaticMetadataForTemporaryFiles()
		{
			// Create our test runner.
			var runner =
				new RestApiTestRunner<List<PropertyValueSuggestion>>(Method.POST, "/REST/objects/automaticmetadata.aspx");

			// Set up the expected body.
			var body = new AutomaticMetadataRequestInfo()
			{
				UploadIds = new List<int>() { 123, 456 }
			};
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.AutomaticMetadataOperations.GetAutomaticMetadataForTemporaryFiles(temporaryFileIds: body.UploadIds.ToArray());

			// Verify.
			runner.Verify();
		}

		#endregion

	}
}
