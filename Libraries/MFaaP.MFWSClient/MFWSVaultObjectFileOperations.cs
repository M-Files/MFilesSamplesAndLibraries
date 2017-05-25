using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public class MFWSVaultObjectFileOperations
		: MFWSVaultOperationsBase
	{
		/// <inheritdoc />
		internal MFWSVaultObjectFileOperations(MFWSClientBase client)
			: base(client)
		{
		}

		#region Downloading files

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objectType">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="fileId">The Id of the file.</param>
		/// <param name="objectVersion">The verison of the object, or null for the latest.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The raw response from the HTTP request.</returns>
		public async Task<byte[]> DownloadFileAsync(int objectType, int objectId, int fileId, int? objectVersion = null, CancellationToken token = default(CancellationToken))
		{
			// Create the version string to be used for the uri segment.
			var versionString = objectVersion?.ToString() ?? "latest";

			// Build up the request.
			var request = new RestRequest($"/REST/objects/{objectType}/{objectId}/{versionString}/files/{fileId}/content");

			// Execute the request.
			var response = await this.MFWSClient.Get(request, token);

			// Return the content.
			return response?.RawBytes;
		}

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objectType">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="fileId">The Id of the file.</param>
		/// <param name="objectVersion">The verison of the object, or null for the latest.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The raw response from the HTTP request.</returns>
		public byte[] DownloadFile(int objectType, int objectId, int fileId, int? objectVersion = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			var task = this.DownloadFileAsync(objectType, objectId, fileId, objectVersion, token);
			Task.WaitAll(new Task[]
			{
				task
			}, token);
			return task.Result;
		}

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objectType">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="fileId">The Id of the file.</param>
		/// <param name="outputStream">The output stream for the response to be written to.</param>
		/// <param name="objectVersion">The verison of the object, or null for the latest.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The raw response from the HTTP request.</returns>
		public async Task DownloadFileAsync(int objectType, int objectId, int fileId, System.IO.Stream outputStream, int? objectVersion = null, CancellationToken token = default(CancellationToken))
		{
			// Create the version string to be used for the uri segment.
			var versionString = objectVersion?.ToString() ?? "latest";

			// Build up the request.
			var request = new RestRequest($"/REST/objects/{objectType}/{objectId}/{versionString}/files/{fileId}/content");

			// Output the response to the given stream.
			request.ResponseWriter = (responseStream) => responseStream.CopyTo(outputStream);

			// Execute the request.
			await this.MFWSClient.Get(request, token);
		}

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objectType">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="fileId">The Id of the file.</param>
		/// <param name="outputStream">The output stream for the response to be written to.</param>
		/// <param name="objectVersion">The verison of the object, or null for the latest.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The raw response from the HTTP request.</returns>
		public void DownloadFile(int objectType, int objectId, int fileId, System.IO.Stream outputStream, int? objectVersion = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			var task = this.DownloadFileAsync(objectType, objectId, fileId, outputStream, objectVersion, token);
			Task.WaitAll(new Task[]
			{
				task
			}, token);
		}

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objectType">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="fileId">The Id of the file.</param>
		/// <param name="outputFileName">The full path to the file to output to.</param>
		/// <param name="objectVersion">The verison of the object, or null for the latest.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the download process.</returns>
		public Task DownloadFileAsync(int objectType, int objectId, int fileId, string outputFileName, int? objectVersion = null, CancellationToken token = default(CancellationToken))
		{
			// Create a FileInfo for the output.
			var outputFileInfo = new System.IO.FileInfo(outputFileName);

			// Use the other overload to download it.
			return this.DownloadFileAsync(objectType, objectId, fileId, outputFileInfo, objectVersion, token);
		}

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objectType">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="fileId">The Id of the file.</param>
		/// <param name="outputFileName">The full path to the file to output to.</param>
		/// <param name="objectVersion">The verison of the object, or null for the latest.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the download process.</returns>
		public void DownloadFile(int objectType, int objectId, int fileId, string outputFileName, int? objectVersion = null, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			var task = this.DownloadFileAsync(objectType, objectId, fileId, outputFileName, objectVersion, token);
			Task.WaitAll(new Task[]
			{
				task
			}, token);
		}

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objectType">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="fileId">The Id of the file.</param>
		/// <param name="outputFileInfo">The file to output the content to (will be overwritten if exists).</param>
		/// <param name="objectVersion">The verison of the object, or null for the latest.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the download process.</returns>
		public async Task DownloadFileAsync(int objectType, int objectId, int fileId, System.IO.FileInfo outputFileInfo, int? objectVersion = null,
			CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == outputFileInfo)
				throw new ArgumentNullException(nameof(outputFileInfo));

			// Delete the file if it already exists.
			if (outputFileInfo.Exists)
				outputFileInfo.Delete();

			// Open a stream to the file.
			using (var stream = outputFileInfo.Create())
			{
				// Download the file to disk.
				await this.DownloadFileAsync(objectType, objectId, fileId, stream, objectVersion, token);
			}
		}

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objectType">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="fileId">The Id of the file.</param>
		/// <param name="outputFileInfo">The file to output the content to (will be overwritten if exists).</param>
		/// <param name="objectVersion">The verison of the object, or null for the latest.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the download process.</returns>
		public void DownloadFile(int objectType, int objectId, int fileId, System.IO.FileInfo outputFileInfo, int? objectVersion = null,
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			var task = this.DownloadFileAsync(objectType, objectId, fileId, outputFileInfo, objectVersion, token);
			Task.WaitAll(new Task[]
			{
				task
			}, token);
		}

		#endregion

		#region Uploading files

		/// <summary>
		/// Uploads files to the temporary location.
		/// </summary>
		/// <param name="files">The files to upload.</param>
		/// <returns>Information on the upload.</returns>
		public Task<UploadInfo[]> UploadFilesAsync(params FileInfo[] files)
		{
			return this.UploadFilesAsync(CancellationToken.None, files);
		}

		/// <summary>
		/// Uploads files to the temporary location.
		/// </summary>
		/// <param name="files">The files to upload.</param>
		/// <returns>Information on the upload.</returns>
		public UploadInfo[] UploadFiles(params FileInfo[] files)
		{
			// Execute the async method.
			var task = this.UploadFilesAsync(files);
			Task.WaitAll(new Task[]
			{
				task
			});
			return task.Result;
		}

		/// <summary>
		/// Uploads files to the temporary location.
		/// </summary>
		/// <param name="files">The files to upload.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the upload.</returns>
		public async Task<UploadInfo[]> UploadFilesAsync(CancellationToken token, params FileInfo[] files)
		{
			// Sanity.
			if (null == files)
				return null;

			// Create the request.
			// TODO: Possibly split this into multiple requests.
			// TODO: Can this be monitored?
			var request = new RestRequest("/REST/files");
			foreach (var file in files)
			{
				request.AddFile(file.Name, file.FullName);
			}

			// Make the request and get the response.
			var response = await this.MFWSClient.Post<List<UploadInfo>>(request, token);

			// Ensure the uploadinfo is updated.
			for (var i = 0; i < response.Data?.Count; i++)
			{
				var uploadInfo = response.Data[i];
				var file = files[i];
				uploadInfo.Title = file.Name;
				uploadInfo.Extension = file.Extension.Substring(1); // Remove the dot.
				uploadInfo.Size = file.Length;
			}

			// Return the data.
			return response.Data?.ToArray();

		}

		/// <summary>
		/// Uploads files to the temporary location.
		/// </summary>
		/// <param name="files">The files to upload.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the upload.</returns>
		public UploadInfo[] UploadFiles(CancellationToken token, params FileInfo[] files)
		{
			// Execute the async method.
			var task = this.UploadFilesAsync(token, files);
			Task.WaitAll(new Task[]
			{
				task
			}, token);
			return task.Result;

		}

		#endregion

	}
}
