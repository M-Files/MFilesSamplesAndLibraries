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

        /// <summary>0
        /// Initiates the download of a specific file.
        /// </summary>
        /// <param name="objectType">The Id of the object type.</param>
        /// <param name="objectId">The Id of the object.</param>
        /// <param name="fileId">The Id of the file.</param>
        /// <param name="objectVersion">The version of the object, or null for the latest.</param>
        /// <param name="token">A cancellation token for the request.</param>
        /// <returns>The raw response from the HTTP request.</returns>
        public async Task<byte[]> DownloadFileAsync(int objectType, int objectId, int fileId, int? objectVersion = null, CancellationToken token = default(CancellationToken))
        {
            // Create the version string to be used for the uri segment.
            var versionString = objectVersion?.ToString() ?? "latest";

            // Build up the request.
            var request = new RestRequest($"/REST/objects/{objectType}/{objectId}/{versionString}/files/{fileId}/content");

            // Execute the request.
            var response = await this.MFWSClient.Get(request, token)
                .ConfigureAwait(false);

            // Return the content.
            return response?.RawBytes;
        }

        /// <summary>
        /// Initiates the download of a specific file.
        /// </summary>
        /// <param name="objectType">The Id of the object type.</param>
        /// <param name="objectId">The Id of the object.</param>
        /// <param name="fileId">The Id of the file.</param>
        /// <param name="objectVersion">The version of the object, or null for the latest.</param>
        /// <param name="token">A cancellation token for the request.</param>
        /// <returns>The raw response from the HTTP request.</returns>
        public byte[] DownloadFile(int objectType, int objectId, int fileId, int? objectVersion = null, CancellationToken token = default(CancellationToken))
        {
            // Execute the async method.
            return this.DownloadFileAsync(objectType, objectId, fileId, objectVersion, token)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// Initiates the download of a specific file.
        /// </summary>
        /// <param name="objectType">The Id of the object type.</param>
        /// <param name="objectId">The Id of the object.</param>
        /// <param name="fileId">The Id of the file.</param>
        /// <param name="outputStream">The output stream for the response to be written to.</param>
        /// <param name="objectVersion">The version of the object, or null for the latest.</param>
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
            await this.MFWSClient.Get(request, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Initiates the download of a specific file.
        /// </summary>
        /// <param name="objectType">The Id of the object type.</param>
        /// <param name="objectId">The Id of the object.</param>
        /// <param name="fileId">The Id of the file.</param>
        /// <param name="outputStream">The output stream for the response to be written to.</param>
        /// <param name="objectVersion">The version of the object, or null for the latest.</param>
        /// <param name="token">A cancellation token for the request.</param>
        /// <returns>The raw response from the HTTP request.</returns>
        public void DownloadFile(int objectType, int objectId, int fileId, System.IO.Stream outputStream, int? objectVersion = null, CancellationToken token = default(CancellationToken))
        {
            // Execute the async method.
            this.DownloadFileAsync(objectType, objectId, fileId, outputStream, objectVersion, token)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// Initiates the download of a specific file.
        /// </summary>
        /// <param name="objectType">The Id of the object type.</param>
        /// <param name="objectId">The Id of the object.</param>
        /// <param name="fileId">The Id of the file.</param>
        /// <param name="outputFileName">The full path to the file to output to.</param>
        /// <param name="objectVersion">The version of the object, or null for the latest.</param>
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
        /// <param name="objectVersion">The version of the object, or null for the latest.</param>
        /// <param name="token">A cancellation token for the request.</param>
        /// <returns>An awaitable task for the download process.</returns>
        public void DownloadFile(int objectType, int objectId, int fileId, string outputFileName, int? objectVersion = null, CancellationToken token = default(CancellationToken))
        {
            // Execute the async method.
            this.DownloadFileAsync(objectType, objectId, fileId, outputFileName, objectVersion, token)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// Initiates the download of a specific file.
        /// </summary>
        /// <param name="objectType">The Id of the object type.</param>
        /// <param name="objectId">The Id of the object.</param>
        /// <param name="fileId">The Id of the file.</param>
        /// <param name="outputFileInfo">The file to output the content to (will be overwritten if exists).</param>
        /// <param name="objectVersion">The version of the object, or null for the latest.</param>
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
                await this.DownloadFileAsync(objectType, objectId, fileId, stream, objectVersion, token)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Initiates the download of a specific file.
        /// </summary>
        /// <param name="objectType">The Id of the object type.</param>
        /// <param name="objectId">The Id of the object.</param>
        /// <param name="fileId">The Id of the file.</param>
        /// <param name="outputFileInfo">The file to output the content to (will be overwritten if exists).</param>
        /// <param name="objectVersion">The version of the object, or null for the latest.</param>
        /// <param name="token">A cancellation token for the request.</param>
        /// <returns>An awaitable task for the download process.</returns>
        public void DownloadFile(int objectType, int objectId, int fileId, System.IO.FileInfo outputFileInfo, int? objectVersion = null,
            CancellationToken token = default(CancellationToken))
        {
            // Execute the async method.
            this.DownloadFileAsync(objectType, objectId, fileId, outputFileInfo, objectVersion, token)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
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
            return this.UploadFilesAsync(files)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
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
                throw new ArgumentNullException(nameof(files));
            if (files.Length == 0)
                return new UploadInfo[0];

            // Create the request.
            // TODO: Possibly split this into multiple requests.
            // TODO: Can this be monitored?
            var request = new RestRequest("/REST/files.aspx");
            foreach (var file in files)
            {
                request.AddFile(file.Name, file.FullName);
            }

            // Make the request and get the response.
            var response = await this.MFWSClient.Post<List<UploadInfo>>(request, token)
                .ConfigureAwait(false);

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
            return this.UploadFilesAsync(token, files)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        }

        #endregion

        #region Adding files to existing objects

        /// <summary>
        /// Adds files to an existing object.
        /// </summary>
        /// <param name="objectType">The Id of the object type.</param>
        /// <param name="objectId">The Id of the object.</param>
        /// <param name="files">The files to attach.</param>
        /// <param name="objectVersion">The version of the object, or null for the latest.</param>
        /// <param name="token">A cancellation token for the request.</param>
        /// <returns>The updated object version.</returns>
        public async Task<ExtendedObjectVersion> AddFilesAsync(int objectType, int objectId, int? objectVersion = null, CancellationToken token = default(CancellationToken), params FileInfo[] files)
        {
            // Sanity.
            if (null == files)
                throw new ArgumentNullException(nameof(files));

            // Firstly, upload the temporary files.
            var uploadInfo = await this.UploadFilesAsync(token, files);

            // Remove the extension from the item title if it exists.
            foreach (var item in uploadInfo)
            {
                // Sanity.
                if (string.IsNullOrWhiteSpace(item.Title) || string.IsNullOrWhiteSpace(item.Extension))
                    continue;

                // If the title ends with the extension then remove it.
                if (true == item.Title?.EndsWith("." + item.Extension))
                {
                    // Note the +1 is because we want to remove the dot as well.
                    item.Title = item.Title.Substring(0, item.Title.Length - (item.Extension.Length + 1));
                }
            }

            // Create the version string to be used for the uri segment.
            var versionString = objectVersion?.ToString() ?? "latest";

            // Build up the request.
            var request = new RestRequest($"/REST/objects/{objectType}/{objectId}/{versionString}/files/upload");
            request.AddJsonBody(uploadInfo);

            // Execute the request.
            var response = await this.MFWSClient.Post<ExtendedObjectVersion>(request, token)
                .ConfigureAwait(false);

            // Return the content.
            return response?.Data;
        }

        /// <summary>
        /// Adds files to an existing object.
        /// </summary>
        /// <param name="objectType">The Id of the object type.</param>
        /// <param name="objectId">The Id of the object.</param>
        /// <param name="files">The files to attach.</param>
        /// <param name="objectVersion">The version of the object, or null for the latest.</param>
        /// <param name="token">A cancellation token for the request.</param>
        /// <returns>The updated object version.</returns>
        public ExtendedObjectVersion AddFiles(int objectType, int objectId, int? objectVersion = null, CancellationToken token = default(CancellationToken), params FileInfo[] files)
        {
            // Execute the async method.
            return this.AddFilesAsync(objectType, objectId, objectVersion, token, files)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// Adds files to an existing object.
        /// </summary>
        /// <param name="objectType">The Id of the object type.</param>
        /// <param name="objectId">The Id of the object.</param>
        /// <param name="files">The files to attach.</param>
        /// <param name="objectVersion">The version of the object, or null for the latest.</param>
        /// <returns>The updated object version.</returns>
        public Task<ExtendedObjectVersion> AddFilesAsync(int objectType, int objectId, int? objectVersion = null, params FileInfo[] files)
        {
            // Execute the other overload.
            return this.AddFilesAsync(objectType, objectId, objectVersion, default(CancellationToken), files);
        }

        /// <summary>
        /// Adds files to an existing object.
        /// </summary>
        /// <param name="objectType">The Id of the object type.</param>
        /// <param name="objectId">The Id of the object.</param>
        /// <param name="files">The files to attach.</param>
        /// <param name="objectVersion">The version of the object, or null for the latest.</param>
        /// <returns>The updated object version.</returns>
        public ExtendedObjectVersion AddFiles(int objectType, int objectId, int? objectVersion = null, params FileInfo[] files)
        {
            // Execute the other overload.
            return this.AddFiles(objectType, objectId, objectVersion, default(CancellationToken), files);
        }

        /// <summary>
        /// Adds files to an existing object.
        /// </summary>
        /// <param name="objId">The object to add the file to.</param>
        /// <param name="files">The files to attach.</param>
        /// <returns>The updated object version.</returns>
        public Task<ExtendedObjectVersion> AddFilesAsync(ObjID objId, params FileInfo[] files)
        {
            // Sanity.
            if (null == objId)
                throw new ArgumentNullException(nameof(objId));

            // Execute the other overload.
            return this.AddFilesAsync(objId.Type, objId.ID, objectVersion: null, token: default(CancellationToken), files: files);
        }

        /// <summary>
        /// Adds files to an existing object.
        /// </summary>
        /// <param name="objId">The object to add the file to.</param>
        /// <param name="files">The files to attach.</param>
        /// <returns>The updated object version.</returns>
        public ExtendedObjectVersion AddFiles(ObjID objId, params FileInfo[] files)
        {
            // Sanity.
            if (null == objId)
                throw new ArgumentNullException(nameof(objId));

            // Execute the other overload.
            return this.AddFiles(objId.Type, objId.ID, objectVersion: null, token: default(CancellationToken), files: files);
        }

        /// <summary>
        /// Adds files to an existing object.
        /// </summary>
        /// <param name="objId">The object to add the file to.</param>
        /// <param name="token">A cancellation token for the request.</param>
        /// <param name="files">The files to attach.</param>
        /// <returns>The updated object version.</returns>
        public Task<ExtendedObjectVersion> AddFilesAsync(ObjID objId, CancellationToken token = default(CancellationToken), params FileInfo[] files)
        {
            // Sanity.
            if (null == objId)
                throw new ArgumentNullException(nameof(objId));

            // Execute the other overload.
            return this.AddFilesAsync(objId.Type, objId.ID, objectVersion: null, token: token, files: files);
        }

        /// <summary>
        /// Adds files to an existing object.
        /// </summary>
        /// <param name="objId">The object to add the file to.</param>
        /// <param name="token">A cancellation token for the request.</param>
        /// <param name="files">The files to attach.</param>
        /// <returns>The updated object version.</returns>
        public ExtendedObjectVersion AddFiles(ObjID objId, CancellationToken token = default(CancellationToken), params FileInfo[] files)
        {
            // Sanity.
            if (null == objId)
                throw new ArgumentNullException(nameof(objId));

            // Execute the other overload.
            return this.AddFiles(objId.Type, objId.ID, objectVersion: null, token: token, files: files);
        }

        /// <summary>
        /// Adds files to an existing object.
        /// </summary>
        /// <param name="objVer">The object to add the file to.</param>
        /// <param name="files">The files to attach.</param>
        /// <returns>The updated object version.</returns>
        public Task<ExtendedObjectVersion> AddFilesAsync(ObjVer objVer, params FileInfo[] files)
        {
            // Sanity.
            if (null == objVer)
                throw new ArgumentNullException(nameof(objVer));

            // Execute the other overload.
            return this.AddFilesAsync(objVer.Type, objVer.ID, objVer.Version, default(CancellationToken), files);
        }

        /// <summary>
        /// Adds files to an existing object.
        /// </summary>
        /// <param name="objVer">The object to add the file to.</param>
        /// <param name="files">The files to attach.</param>
        /// <returns>The updated object version.</returns>
        public ExtendedObjectVersion AddFiles(ObjVer objVer, params FileInfo[] files)
        {
            // Sanity.
            if (null == objVer)
                throw new ArgumentNullException(nameof(objVer));

            // Execute the other overload.
            return this.AddFiles(objVer.Type, objVer.ID, objVer.Version, default(CancellationToken), files);
        }

        /// <summary>
        /// Adds files to an existing object.
        /// </summary>
        /// <param name="objVer">The object to add the file to.</param>
        /// <param name="token">A cancellation token for the request.</param>
        /// <param name="files">The files to attach.</param>
        /// <returns>The updated object version.</returns>
        public Task<ExtendedObjectVersion> AddFilesAsync(ObjVer objVer, CancellationToken token = default(CancellationToken), params FileInfo[] files)
        {
            // Sanity.
            if (null == objVer)
                throw new ArgumentNullException(nameof(objVer));

            // Execute the other overload.
            return this.AddFilesAsync(objVer.Type, objVer.ID, objVer.Version, token, files);
        }

        /// <summary>
        /// Adds files to an existing object.
        /// </summary>
        /// <param name="objVer">The object to add the file to.</param>
        /// <param name="token">A cancellation token for the request.</param>
        /// <param name="files">The files to attach.</param>
        /// <returns>The updated object version.</returns>
        public ExtendedObjectVersion AddFiles(ObjVer objVer, CancellationToken token = default(CancellationToken), params FileInfo[] files)
        {
            // Sanity.
            if (null == objVer)
                throw new ArgumentNullException(nameof(objVer));

            // Execute the other overload.
            return this.AddFiles(objVer.Type, objVer.ID, objVer.Version, token, files);
        }

		#endregion

		#region Renaming a file

		/// <summary>
		/// Renames a specific file.
		/// </summary>
		/// <param name="objectType">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="fileId">The Id of the file to rename.</param>
		/// <param name="newFileName">The (new) file name.</param>
		/// <param name="objectVersion">The version of the object, or null for the latest.</param>
		/// <param name="fileVersion">The version of the file, or null for the latest.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The updated object version.</returns>
		public async Task<ObjectVersion> RenameFileAsync(int objectType,
			int objectId,
			int fileId,
			string newFileName,
			int? objectVersion = null,
			int? fileVersion = null,
			CancellationToken token = default(CancellationToken))
        {
			// Create the version strings to be used for the uri segment.
			var objectVersionString = objectVersion?.ToString() ?? "latest";
			var fileVersionString = fileVersion?.ToString() ?? "latest";

			// Build up the request.
			var request = new RestRequest($"/REST/objects/{objectType}/{objectId}/{objectVersionString}/files/{fileId}/{fileVersionString }/title");

			// Pass in the new file name.
			// ref: http://www.m-files.com/mfws/resources/objects/type/objectid/version/files/file/title.html
			request.AddJsonBody(new { Value = newFileName });
			
			// Execute the request.
			var response = await this.MFWSClient.Put<ObjectVersion>(request, token)
                .ConfigureAwait(false);

			// Return the content.
			return response?.Data;

		}

		/// <summary>
		/// Renames a specific file.
		/// </summary>
		/// <param name="objectType">The Id of the object type.</param>
		/// <param name="objectId">The Id of the object.</param>
		/// <param name="fileId">The Id of the file to rename.</param>
		/// <param name="newFileName">The (new) file name.</param>
		/// <param name="objectVersion">The version of the object, or null for the latest.</param>
		/// <param name="fileVersion">The version of the file, or null for the latest.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The updated object version.</returns>
		public ObjectVersion RenameFile(int objectType,
			int objectId,
			int fileId,
			string newFileName,
			int? objectVersion = null,
			int? fileVersion = null,
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.RenameFileAsync(objectType, objectId, fileId, newFileName, objectVersion, fileVersion, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();

		}

		/// <summary>
		/// Renames a specific file.
		/// </summary>
		/// <param name="objId">The object containing the file to rename.</param>
		/// <param name="fileVer">The file to rename.</param>
		/// <param name="newFileName">The (new) file name.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The updated object version.</returns>
		public ObjectVersion RenameFile(ObjID objId,
			FileVer fileVer,
			string newFileName,
			CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objId)
				throw new ArgumentNullException(nameof(objId));
			if (null == fileVer)
				throw new ArgumentNullException(nameof(fileVer));

			// Call the other overload.
			return this.RenameFileAsync(objId.Type, objId.ID, fileVer.ID, newFileName, null, fileVer.Version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();

		}

		/// <summary>
		/// Renames a specific file.
		/// </summary>
		/// <param name="objVer">The object containing the file to rename.</param>
		/// <param name="fileVer">The file to rename.</param>
		/// <param name="newFileName">The (new) file name.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The updated object version.</returns>
		public ObjectVersion RenameFile(ObjVer objVer,
			FileVer fileVer,
			string newFileName,
			CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));
			if (null == fileVer)
				throw new ArgumentNullException(nameof(fileVer));

			// Call the other overload.
			return this.RenameFileAsync(objVer.Type, objVer.ID, fileVer.ID, newFileName, objVer.Version, fileVer.Version, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();

		}

		#endregion
	}
}
