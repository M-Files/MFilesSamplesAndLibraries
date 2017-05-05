using System;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{
		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objectType">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="fileId">The Id of the file.</param>
		/// <param name="objectVersion">The verison of the object, or null for the latest.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The raw response from the HTTP request.</returns>
		public async Task<byte[]> DownloadFile(int objectType, int objectId, int fileId, int? objectVersion = null, CancellationToken token = default(CancellationToken))
		{
			// Create the version string to be used for the uri segment.
			var versionString = objectVersion?.ToString() ?? "latest";

			// Build up the request.
			var request = new RestRequest($"/REST/objects/{objectType}/{objectId}/{versionString}/files/{fileId}/content");

			// Execute the request.
			var response = await this.Get(request, token);

			// Return the content.
			return response?.RawBytes;
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
		public async Task DownloadFile(int objectType, int objectId, int fileId, System.IO.Stream outputStream, int? objectVersion = null, CancellationToken token = default(CancellationToken))
		{
			// Create the version string to be used for the uri segment.
			var versionString = objectVersion?.ToString() ?? "latest";

			// Build up the request.
			var request = new RestRequest($"/REST/objects/{objectType}/{objectId}/{versionString}/files/{fileId}/content");

			// Output the response to the given stream.
			request.ResponseWriter = (responseStream) => responseStream.CopyTo(outputStream);

			// Execute the request.
			await this.Get(request, token);
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
		public Task DownloadFile(int objectType, int objectId, int fileId, string outputFileName, int? objectVersion = null, CancellationToken token = default(CancellationToken))
		{
			// Create a FileInfo for the output.
			var outputFileInfo = new System.IO.FileInfo(outputFileName);

			// Use the other overload to download it.
			return this.DownloadFile(objectType, objectId, fileId, outputFileInfo, objectVersion, token);
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
		public async Task DownloadFile(int objectType, int objectId, int fileId, System.IO.FileInfo outputFileInfo, int? objectVersion = null, 
			CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == outputFileInfo)
				throw new ArgumentNullException(nameof(outputFileInfo));

			// Delete the file if it already exists.
			if(outputFileInfo.Exists)
				outputFileInfo.Delete();

			// Open a stream to the file.
			using (var stream = outputFileInfo.Create())
			{
				// Download the file to disk.
				await this.DownloadFile(objectType, objectId, fileId, stream, objectVersion, token);
			}
		}
	}
}
