using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
	{

		/// <summary>
		/// Creates an object.
		/// </summary>
		/// <param name="objectTypeId">The type of the object.</param>
		/// <param name="creationInfo">The creation information for the object.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the created object.</returns>
		public async Task<ObjectVersion> CreateObject(int objectTypeId, ObjectCreationInfo creationInfo, CancellationToken token = default(CancellationToken))
		{

			// Sanity.
			if (null == creationInfo)
				throw new ArgumentNullException();
			if(objectTypeId < 0)
				throw new ArgumentException("The object type id cannot be less than zero");

			// Create the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}");
			request.AddJsonBody(creationInfo);
			
			// Make the request and get the response.
			var response = await this.Post<ObjectVersion>(request, token);

			// Return the data.
			return response.Data;

		}

		/// <summary>
		/// Uploads files to the temporary location.
		/// </summary>
		/// <param name="files">The files to upload.</param>
		/// <returns>Information on the upload.</returns>
		public Task<UploadInfo[]> UploadFiles(params FileInfo[] files)
		{
			return this.UploadFiles(CancellationToken.None, files);
		}

		/// <summary>
		/// Uploads files to the temporary location.
		/// </summary>
		/// <param name="files">The files to upload.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the upload.</returns>
		public async Task<UploadInfo[]> UploadFiles(CancellationToken token, params FileInfo[] files)
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
			var response = await this.Post<List<UploadInfo>>(request, token);

			// Ensure the uploadinfo is updated.
			for (var i = 0; i < files.Length; i++)
			{
				var uploadInfo = response.Data[i];
				var file = files[i];
				uploadInfo.Title = file.Name;
				uploadInfo.Extension = file.Extension.Substring(1); // Remove the dot.
				uploadInfo.Size = file.Length;
			}

			// Return the data.
			return response.Data.ToArray();

		}

	}
	
}
