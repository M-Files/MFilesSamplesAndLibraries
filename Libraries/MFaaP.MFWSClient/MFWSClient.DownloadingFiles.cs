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
		/// <returns>the raw response from the HTTP request.</returns>
		public byte[] DownloadFile(int objectType, int objectId, int fileId, int? objectVersion = null)
		{
			// Create the version string to be used for the uri segment.
			var versionString = objectVersion?.ToString() ?? "latest";

			// Build up the request.
			var request = new RestRequest($"/REST/objects/{objectType}/{objectId}/{versionString}/files/{fileId}/content");

			// Execute the request.
			var response = this.Get(request);

			// Return the content.
			return response?.RawBytes;
		}

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objectType">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="fileId">The Id of the file.</param>
		/// <param name="outputFileName">The full path to the file to output to.</param>
		/// <param name="objectVersion">The verison of the object, or null for the latest.</param>
		/// <returns>the raw response from the HTTP request.</returns>
		public void DownloadFile(int objectType, int objectId, int fileId, string outputFileName, int? objectVersion = null)
		{
			// Open a stream to the file.
			using (var stream = System.IO.File.OpenWrite(outputFileName))
			{
				// Get the raw data.
				var data = this.DownloadFile(objectType, objectId, fileId, objectVersion);

				// Save the content to disk.
				stream.Write(data, 0, data.Length);
			}
		}
	}
}
