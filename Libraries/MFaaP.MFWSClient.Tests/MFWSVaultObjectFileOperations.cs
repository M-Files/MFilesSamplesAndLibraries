using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace MFaaP.MFWSClient.Tests
{
	[TestClass]
	public class MFWSVaultObjectFileOperations
	{
		/// <summary>
		/// Wrapper around <see cref="RestApiTestRunner{T}"/> to facilitate checking file data.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		private class FileRestApiTestRunner<T>
			: RestApiTestRunner<T>
			where T : class, new()
		{
			/// <inheritdoc />
			public FileRestApiTestRunner(Method expectedMethod, string expectedResourceAddress)
				: base(expectedMethod, expectedResourceAddress)
			{
			}

			public List<FileParameter> RequestFiles { get; private set; }
				= new List<FileParameter>();

			#region Overrides of RestApiTestRunner

			/// <inheritdoc />
			protected override void HandleCallback(IRestRequest r)
			{
				this.RequestFiles = r?.Files;
				base.HandleCallback(r);
			}

			#endregion
		}

		#region Uploading files

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.UploadFileAsync"/>
		/// requests the correct resource address using the correct method, with the correct request body.
		/// </summary>
		[TestMethod]
		public async Task UploadFileAsync()
		{
			// Create our test runner.
			var runner = new FileRestApiTestRunner<ExtendedObjectVersion>(Method.PUT, "/REST/objects/456/123/9/files/123/content.aspx");

			// Create a temporary file.
			var tempFile = new FileInfo(@"test.txt");
			if (false == tempFile.Exists)
			{
				tempFile.Create();

				// NOTE: If test.txt did not exist the first time the test was run then 'file.Length' will throw an exception
				// of type 'System.IO.FileNotFoundException' in MFWSVaultObjectFileOperations.UploadFilesAsync.
				tempFile = new FileInfo(@"test.txt");
			}

			// Execute.
			await runner.MFWSClient.ObjectFileOperations.UploadFileAsync(new ObjVer()
			{
				Type = 456,
				ID = 123,
				Version = 9
			}, new FileVer()
			{
				ID = 123
			}, tempFile.FullName);

			// Verify.
			runner.Verify();

			// Ensure the file data was passed.
			Assert.IsNotNull(runner.RequestFiles);
			Assert.AreEqual(1, runner.RequestFiles.Count);
			Assert.AreEqual(tempFile.Name, runner.RequestFiles[0].Name);
			Assert.AreEqual(tempFile.Length, runner.RequestFiles[0].ContentLength);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.UploadFileAsync"/>
		/// requests the correct resource address using the correct method, with the correct request body.
		/// </summary>
		[TestMethod]
		public async Task UploadFileAsync_Latest()
		{
			// Create our test runner.
			var runner = new FileRestApiTestRunner<ExtendedObjectVersion>(Method.PUT, "/REST/objects/456/123/latest/files/123/content.aspx");

			// Create a temporary file.
			var tempFile = new FileInfo(@"test.txt");
			if (false == tempFile.Exists)
			{
				tempFile.Create();

				// NOTE: If test.txt did not exist the first time the test was run then 'file.Length' will throw an exception
				// of type 'System.IO.FileNotFoundException' in MFWSVaultObjectFileOperations.UploadFilesAsync.
				tempFile = new FileInfo(@"test.txt");
			}

			// Execute.
			await runner.MFWSClient.ObjectFileOperations.UploadFileAsync(new ObjVer()
			{
				Type = 456,
				ID = 123
			}, new FileVer()
			{
				ID = 123
			}, tempFile.FullName);

			// Verify.
			runner.Verify();

			// Ensure the file data was passed.
			Assert.IsNotNull(runner.RequestFiles);
			Assert.AreEqual(1, runner.RequestFiles.Count);
			Assert.AreEqual(tempFile.Name, runner.RequestFiles[0].Name);
			Assert.AreEqual(tempFile.Length, runner.RequestFiles[0].ContentLength);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.UploadFileAsync"/>
		/// requests the correct resource address using the correct method, with the correct request body.
		/// </summary>
		[TestMethod]
		public async Task UploadFileAsync_Unmanaged()
		{
			// Create our test runner.
			var runner = new FileRestApiTestRunner<ExtendedObjectVersion>(Method.PUT, "/REST/objects/0/umy%2Brepository%3Amy%2Bobject/uversion%2B1/files/umy%2Bfile/content.aspx");

			// Create a temporary file.
			var tempFile = new FileInfo(@"test.txt");
			if (false == tempFile.Exists)
			{
				tempFile.Create();

				// NOTE: If test.txt did not exist the first time the test was run then 'file.Length' will throw an exception
				// of type 'System.IO.FileNotFoundException' in MFWSVaultObjectFileOperations.UploadFilesAsync.
				tempFile = new FileInfo(@"test.txt");
			}

			// Execute.
			await runner.MFWSClient.ObjectFileOperations.UploadFileAsync(new ObjVer()
			{
				Type = 0,
				ExternalRepositoryName = "my repository",
				ExternalRepositoryObjectID = "my object",
				ExternalRepositoryObjectVersionID = "version 1"
			}, new FileVer()
			{
				ExternalRepositoryFileID = "my file",
				ExternalRepositoryFileVersionID = "version 2"
			}, tempFile.FullName);

			// Verify.
			runner.Verify();

			// Ensure the file data was passed.
			Assert.IsNotNull(runner.RequestFiles);
			Assert.AreEqual(1, runner.RequestFiles.Count);
			Assert.AreEqual(tempFile.Name, runner.RequestFiles[0].Name);
			Assert.AreEqual(tempFile.Length, runner.RequestFiles[0].ContentLength);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.UploadFilesAsync(System.IO.FileInfo[])"/>
		/// requests the correct resource address using the correct method, with the correct request body.
		/// </summary>
		[TestMethod]
		public async Task UploadFilesAsync()
		{
			// Create our test runner.
			var runner = new FileRestApiTestRunner<List<UploadInfo>>(Method.POST, "/REST/files.aspx");

			// Create a temporary file.
			var tempFile = new FileInfo(@"test.txt");
			if (false == tempFile.Exists)
			{
				tempFile.Create();

				// NOTE: If test.txt did not exist the first time the test was run then 'file.Length' will throw an exception
				// of type 'System.IO.FileNotFoundException' in MFWSVaultObjectFileOperations.UploadFilesAsync.
				tempFile = new FileInfo(@"test.txt");
			}

			// When the execute method is called, return dummy upload information.
			runner.ResponseData = new[]
			{
				new UploadInfo()
				{
					UploadID = 1
				}
			}.ToList();

			// Execute.
			await runner.MFWSClient.ObjectFileOperations.UploadFilesAsync(tempFile);

			// Verify.
			runner.Verify();

			// Ensure the file data was passed.
			Assert.IsNotNull(runner.RequestFiles);
			Assert.AreEqual(1, runner.RequestFiles.Count);
			Assert.AreEqual(tempFile.Name, runner.RequestFiles[0].Name);
			Assert.AreEqual(tempFile.Length, runner.RequestFiles[0].ContentLength);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.UploadFiles(System.IO.FileInfo[])"/>
		/// requests the correct resource address using the correct method, with the correct request body.
		/// </summary>
		[TestMethod]
		public void UploadFiles()
		{
			// Create our test runner.
			var runner = new FileRestApiTestRunner<List<UploadInfo>>(Method.POST, "/REST/files.aspx");

			// Create a temporary file.
			var tempFile = new FileInfo(@"test.txt");
			if (false == tempFile.Exists)
			{
				tempFile.Create();

				// NOTE: If test.txt did not exist the first time the test was run then 'file.Length' will throw an exception
				// of type 'System.IO.FileNotFoundException' in MFWSVaultObjectFileOperations.UploadFiles.
				tempFile = new FileInfo(@"test.txt");
			}

			// When the execute method is called, return dummy upload information.
			runner.ResponseData = new[]
			{
				new UploadInfo()
				{
					UploadID = 1
				}
			}.ToList();

			// Execute.
			runner.MFWSClient.ObjectFileOperations.UploadFiles(tempFile);

			// Verify.
			runner.Verify();

			// Ensure the file data was passed.
			Assert.IsNotNull(runner.RequestFiles);
			Assert.AreEqual(1, runner.RequestFiles.Count);
			Assert.AreEqual(tempFile.Name, runner.RequestFiles[0].Name);
			Assert.AreEqual(tempFile.Length, runner.RequestFiles[0].ContentLength);
		}

		#endregion

		#region Downloading files

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.DownloadFileAsync(int,int,int,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public async Task DownloadFileAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner(Method.GET, "/REST/objects/1/2/4/files/3/content");

			// Execute.
			await runner.MFWSClient.ObjectFileOperations.DownloadFileAsync(1, 2, 3, 4);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.DownloadFile(int,int,int,System.Nullable{int},System.Threading.CancellationToken)"/>
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public void DownloadFile()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner(Method.GET, "/REST/objects/1/2/4/files/3/content");

			// Execute.
			runner.MFWSClient.ObjectFileOperations.DownloadFile(1, 2, 3, 4);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.DownloadFileAsync(ObjVer, FileVer, CancellationToken)"/>
		/// when using an unmanaged object data
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public async Task DownloadFileAsync_Umanaged()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner(Method.GET, "/REST/objects/0/umy%2Brepository%3Amy%2Bobject/uversion%2B1/files/umy%2Bfile/content");

			// Execute.
			await runner.MFWSClient.ObjectFileOperations.DownloadFileAsync(new ObjVer()
			{
				Type = 0,
				ExternalRepositoryName = "my repository", // Will be double-encoded.
				ExternalRepositoryObjectID = "my object", // Will be double-encoded.
				ExternalRepositoryObjectVersionID = "version 1" // Will be double-encoded.
			}, new FileVer()
			{
				ExternalRepositoryFileID = "my file" // Will be double-encoded.
			});

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.DownloadFile(ObjVer, FileVer, CancellationToken)"/>
		/// when using an unmanaged object data
		/// requests the correct resource address and HTTP method.
		/// </summary>
		[TestMethod]
		public void DownloadFile_Unmanaged()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner(Method.GET, "/REST/objects/0/umy%2Brepository%3Amy%2Bobject/uversion%2B1/files/umy%2Bfile/content");

			// Execute.
			runner.MFWSClient.ObjectFileOperations.DownloadFile(new ObjVer()
			{
				Type = 0,
				ExternalRepositoryName = "my repository", // Will be double-encoded.
				ExternalRepositoryObjectID = "my object", // Will be double-encoded.
				ExternalRepositoryObjectVersionID = "version 1" // Will be double-encoded.
			}, new FileVer()
			{
				ExternalRepositoryFileID = "my file" // Will be double-encoded.
			});

			// Verify.
			runner.Verify();
		}

		#endregion

		#region Adding files

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.AddFilesAsync(int,int,System.Nullable{int},System.Threading.CancellationToken,System.IO.FileInfo[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public async Task AddFilesAsync_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create a temporary file.
			var tempFile = new FileInfo(@"test.txt");
			if (false == tempFile.Exists)
				tempFile.Create();

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			// We also need to handle the upload file call or our tests will except.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<UploadInfo>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<UploadInfo>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new[]
						{
							new UploadInfo()
							{
								UploadID = 1
							}
						}.ToList());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectFileOperations.AddFilesAsync(0, 1, 2, tempFile);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects/0/1/2/files/upload", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.AddFiles(int,int,System.Nullable{int},System.Threading.CancellationToken,System.IO.FileInfo[])"/>
		/// requests the correct resource address.
		/// </summary>
		[TestMethod]
		public void AddFiles_CorrectResource()
		{
			/* Arrange */

			// The actual requested address.
			var resourceAddress = "";

			// Create a temporary file.
			var tempFile = new FileInfo(@"test.txt");
			if (false == tempFile.Exists)
				tempFile.Create();

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					resourceAddress = r.Resource;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			// We also need to handle the upload file call or our tests will except.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<UploadInfo>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<UploadInfo>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new[]
						{
							new UploadInfo()
							{
								UploadID = 1
							}
						}.ToList());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});


			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectFileOperations.AddFiles(0, 1, 2, tempFile);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Resource must be correct.
			Assert.AreEqual("/REST/objects/0/1/2/files/upload", resourceAddress);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.AddFilesAsync(int,int,System.Nullable{int},System.Threading.CancellationToken,System.IO.FileInfo[])"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public async Task AddFilesAsync_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create a temporary file.
			var tempFile = new FileInfo(@"test.txt");
			if (false == tempFile.Exists)
				tempFile.Create();

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			// We also need to handle the upload file call or our tests will except.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<UploadInfo>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<UploadInfo>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new[]
						{
							new UploadInfo()
							{
								UploadID = 1
							}
						}.ToList());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			await mfwsClient.ObjectFileOperations.AddFilesAsync(0, 1, 2, tempFile);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.AddFiles(int,int,System.Nullable{int},System.Threading.CancellationToken,System.IO.FileInfo[])"/>
		/// uses the correct Http method.
		/// </summary>
		[TestMethod]
		public void AddFiles_CorrectMethod()
		{
			/* Arrange */

			// The method.
			Method? methodUsed = null;

			// Create a temporary file.
			var tempFile = new FileInfo(@"test.txt");
			if (false == tempFile.Exists)
				tempFile.Create();

			// Create our restsharp mock.
			var mock = new Mock<IRestClient>();

			// When the execute method is called, log the resource requested.
			mock
				.Setup(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				.Callback((IRestRequest r, CancellationToken t) => {
					methodUsed = r.Method;
				})
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<ExtendedObjectVersion>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new ExtendedObjectVersion());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			// We also need to handle the upload file call or our tests will except.
			mock
				.Setup(c => c.ExecuteTaskAsync<List<UploadInfo>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
				// Return a mock response.
				.Returns(() =>
				{
					// Create the mock response.
					var response = new Mock<IRestResponse<List<UploadInfo>>>();

					// Setup the return data.
					response.SetupGet(r => r.Data)
						.Returns(new[]
						{
							new UploadInfo()
							{
								UploadID = 1
							}
						}.ToList());

					//Return the mock object.
					return Task.FromResult(response.Object);
				});

			/* Act */

			// Create our MFWSClient.
			var mfwsClient = MFWSClient.GetMFWSClient(mock);

			// Execute.
			mfwsClient.ObjectFileOperations.AddFiles(0, 1, 2, tempFile);

			/* Assert */

			// Execute must be called once.
			mock.Verify(c => c.ExecuteTaskAsync<ExtendedObjectVersion>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

			// Method must be correct.
			Assert.AreEqual(Method.POST, methodUsed);
		}

		#endregion

		#region Renaming files

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.RenameFileAsync(int, int, int, string, int?, int?, CancellationToken)"/>
		/// requests the correct resource address using the correct method..
		/// </summary>
		[TestMethod]
		public async Task RenameFileAsync()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, "/REST/objects/1/2/latest/files/3/latest/title");

			// Create the object to send in the body.
			var body = new PrimitiveType<string>()
			{
				Value = "renamed.pdf"
			};

			// Set the expected request body.
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ObjectFileOperations.RenameFileAsync(1, 2, 3, "renamed.pdf");

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.RenameFileAsync(int, int, int, string, int?, int?, CancellationToken)"/>
		/// requests the correct resource address using the correct method..
		/// </summary>
		[TestMethod]
		public async Task RenameFileAsync_WithVersionData()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, "/REST/objects/1/2/4/files/3/5/title");

			// Create the object to send in the body.
			var body = new PrimitiveType<string>()
			{
				Value = "renamed.pdf"
			};

			// Set the expected request body.
			runner.SetExpectedRequestBody(body);

			// Execute.
			await runner.MFWSClient.ObjectFileOperations.RenameFileAsync(1, 2, 3, "renamed.pdf", 4, 5);

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.RenameFile(int, int, int, string, int?, int?, CancellationToken)"/>
		/// requests the correct resource address using the correct method..
		/// </summary>
		[TestMethod]
		public void RenameFile()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, "/REST/objects/4/5/latest/files/6/latest/title");

			// Create the object to send in the body.
			var body = new PrimitiveType<string>()
			{
				Value = "renamed.pdf"
			};

			// Set the expected request body.
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ObjectFileOperations.RenameFile(4, 5, 6, "renamed.pdf");

			// Verify.
			runner.Verify();
		}

		/// <summary>
		/// Ensures that a call to <see cref="MFaaP.MFWSClient.MFWSVaultObjectFileOperations.RenameFile(int, int, int, string, int?, int?, CancellationToken)"/>
		/// requests the correct resource address using the correct method..
		/// </summary>
		[TestMethod]
		public void RenameFileWithVersionData()
		{
			// Create our test runner.
			var runner = new RestApiTestRunner<ObjectVersion>(Method.PUT, "/REST/objects/4/5/7/files/6/8/title");

			// Create the object to send in the body.
			var body = new PrimitiveType<string>()
			{
				Value = "renamed.pdf"
			};

			// Set the expected request body.
			runner.SetExpectedRequestBody(body);

			// Execute.
			runner.MFWSClient.ObjectFileOperations.RenameFile(4, 5, 6, "renamed.pdf", 7, 8);

			// Verify.
			runner.Verify();
		}

		#endregion

	}
}
