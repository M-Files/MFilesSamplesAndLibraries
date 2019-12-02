using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Extensions;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Object file operations.
	/// </summary>
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
		/// <param name="objVer">The object that contains the file to download.</param>
		/// <param name="fileVer">The file to download.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The raw response from the HTTP request.</returns>
		public async Task<byte[]> DownloadFileAsync(ObjVer objVer, FileVer fileVer, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));
			if (null == fileVer)
				throw new ArgumentNullException(nameof(fileVer));

			// Extract the URI elements.
			int objectTypeId;
			string objectId, objectVersionId;
			objVer.GetUriParameters(out objectTypeId, out objectId, out objectVersionId);
			string fileId, fileVersionId;
			fileVer.GetUriParameters(out fileId, out fileVersionId);

			// Build up the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/{objectVersionId}/files/{fileId}/content");

			// Execute the request.
			var response = await this.MFWSClient.Get(request, token)
				.ConfigureAwait(false);

			// Return the content.
			return response?.RawBytes;
		}

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objVer">The object that contains the file to download.</param>
		/// <param name="fileVer">The file to download.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The raw response from the HTTP request.</returns>
		public byte[] DownloadFile(ObjVer objVer, FileVer fileVer, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.DownloadFileAsync(objVer, fileVer, token)
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
		/// <param name="objectVersion">The version of the object, or null for the latest.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The raw response from the HTTP request.</returns>
		public Task<byte[]> DownloadFileAsync(int objectType, int objectId, int fileId, int? objectVersion = null, CancellationToken token = default(CancellationToken))
        {
			// Use the other overload.
			return this.DownloadFileAsync(new ObjVer()
			{
				Type = objectType,
				ID = objectId,
				Version =  objectVersion ?? 0
			}, new FileVer()
			{
				ID = fileId
			}, token);
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
		/// <param name="objVer">The object that contains the file to download.</param>
		/// <param name="fileVer">The file to download.</param>
		/// <param name="outputStream">The output stream for the response to be written to.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The raw response from the HTTP request.</returns>
		public async Task DownloadFileAsync(ObjVer objVer, FileVer fileVer, System.IO.Stream outputStream, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));
			if (null == fileVer)
				throw new ArgumentNullException(nameof(fileVer));

			// Extract the URI elements.
			int objectTypeId;
			string objectId, objectVersionId;
			objVer.GetUriParameters(out objectTypeId, out objectId, out objectVersionId);
			string fileId, fileVersionId;
			fileVer.GetUriParameters(out fileId, out fileVersionId);

			// Build up the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/{objectVersionId}/files/{fileId}/content");

			// Output the response to the given stream.
			request.ResponseWriter = (responseStream) => responseStream.CopyTo(outputStream);

			// Execute the request.
			await this.MFWSClient.Get(request, token)
				.ConfigureAwait(false);
		}

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objVer">The object that contains the file to download.</param>
		/// <param name="fileVer">The file to download.</param>
		/// <param name="outputStream">The output stream for the response to be written to.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The raw response from the HTTP request.</returns>
		public void DownloadFile(ObjVer objVer, FileVer fileVer, System.IO.Stream outputStream, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			this.DownloadFileAsync(objVer, fileVer, outputStream, token)
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
		public Task DownloadFileAsync(int objectType, int objectId, int fileId, System.IO.Stream outputStream, int? objectVersion = null, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.DownloadFileAsync(new ObjVer()
				{
					Type = objectType,
					ID = objectId,
					Version = objectVersion ?? 0
				}, new FileVer()
				{
					ID = fileId
				},
				outputStream,
				token);
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
		/// <param name="objVer">The object that contains the file to download.</param>
		/// <param name="fileVer">The file to download.</param>
		/// <param name="outputFileName">The full path to the file to output to.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The raw response from the HTTP request.</returns>
		public void DownloadFile(ObjVer objVer, FileVer fileVer, string outputFileName, CancellationToken token = default(CancellationToken))
		{
			// Create a FileInfo for the output.
			var outputFileInfo = new System.IO.FileInfo(outputFileName);

			// Execute the async method.
			this.DownloadFileAsync(objVer, fileVer, outputFileInfo, token)
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
        public Task DownloadFileAsync(int objectType, int objectId, int fileId, System.IO.FileInfo outputFileInfo, int? objectVersion = null,
            CancellationToken token = default(CancellationToken))
        {
            // Sanity.
            if (null == outputFileInfo)
                throw new ArgumentNullException(nameof(outputFileInfo));

			// Use the other overload.
			return this.DownloadFileAsync(new ObjVer()
			{
				Type = objectType,
				ID = objectId,
				Version = objectVersion ?? 0
			}, new FileVer()
			{
				ID = fileId
			}, outputFileInfo, token);
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

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objVer">The object that contains the file to download.</param>
		/// <param name="fileVer">The file to download.</param>
		/// <param name="outputFileInfo">The file to output the content to (will be overwritten if exists).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>An awaitable task for the download process.</returns>
		public async Task DownloadFileAsync(ObjVer objVer, FileVer fileVer, System.IO.FileInfo outputFileInfo,
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
				await this.DownloadFileAsync(objVer, fileVer, stream, token)
					.ConfigureAwait(false);
			}
		}

		/// <summary>
		/// Initiates the download of a specific file.
		/// </summary>
		/// <param name="objVer">The object that contains the file to download.</param>
		/// <param name="fileVer">The file to download.</param>
		/// <param name="outputFileInfo">The file to output the content to (will be overwritten if exists).</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The raw response from the HTTP request.</returns>
		public void DownloadFile(ObjVer objVer, FileVer fileVer, System.IO.FileInfo outputFileInfo, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			this.DownloadFileAsync(objVer, fileVer, outputFileInfo, token)
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

		/// <summary>
		/// Replaces the content of a(n existing) single file.
		/// </summary>
		/// <param name="objVer">The object that the file belongs to.</param>
		/// <param name="fileVer">The file to replace.</param>
		/// <param name="filePath">The path to the file to upload in its replacement.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The updated object version.</returns>
		public async Task<ExtendedObjectVersion> UploadFileAsync(ObjVer objVer, FileVer fileVer, string filePath, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == objVer)
				throw new ArgumentNullException(nameof(objVer));
			if (null == fileVer)
				throw new ArgumentNullException(nameof(fileVer));
			try
			{
				if (false == System.IO.File.Exists(filePath))
					throw new ArgumentException("The file path must exist.", nameof(filePath));
			}
			catch (Exception e)
			{
				throw new ArgumentException("Could not confirm file path location.", nameof(filePath), e);
			}

			// Extract the URI elements.
			int objectTypeId;
			string objectId, objectVersionId;
			objVer.GetUriParameters(out objectTypeId, out objectId, out objectVersionId);
			string fileId, fileVersionId;
			fileVer.GetUriParameters(out fileId, out fileVersionId);

			// Build up the request.
			var request = new RestRequest($"/REST/objects/{objectTypeId}/{objectId}/{objectVersionId}/files/{fileId}/content.aspx");

			// Add the file as a body.
			var fileInfo = new System.IO.FileInfo(filePath);
			request.AddFile(fileInfo.Name, fileInfo.FullName);

			// Execute the request.
			var response = await this.MFWSClient.Put<ExtendedObjectVersion>(request, token)
				.ConfigureAwait(false);

			// Return the data.
			return response.Data;

		}

		/// <summary>
		/// Replaces the content of a(n existing) single file.
		/// </summary>
		/// <param name="objVer">The object that the file belongs to.</param>
		/// <param name="fileVer">The file to replace.</param>
		/// <param name="filePath">The path to the file to upload in its replacement.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>The updated object version.</returns>
		public ExtendedObjectVersion UploadFile(ObjVer objVer, FileVer fileVer, string filePath, CancellationToken token = default(CancellationToken))
		{
			// Use the other overload.
			return this.UploadFileAsync(objVer, fileVer, filePath, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
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

		#region Uploading files in chunks

		#region Start the temporary file upload

		/// <summary>
		/// Begins the upload of a file to a temporary location.
		/// </summary>
		/// <param name="fileInfo">The file to upload.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the upload.</returns>
		/// <remarks>THIS METHOD IS NOT DOCUMENTED SO DO NOT USE IN PRODUCTION ENVIRONMENTS.</remarks>
		public async Task<UploadInfo> UploadTemporaryFileBlockBeginAsync(FileInfo fileInfo, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if(null == fileInfo)
				throw new ArgumentNullException(nameof(fileInfo));
			if(false == fileInfo.Exists)
				throw new ArgumentException("The file must exist.", nameof(fileInfo));

			// Create the request.
			var request = new RestRequest("/REST/files/upload.aspx");

			// Add the request body.
			var uploadInfo = new UploadInfo()
			{
				Extension = fileInfo.Extension.Substring(1), // Remove the dot.
				Size = fileInfo.Length,
				Title = fileInfo.Name
			};
			request.AddJsonBody(uploadInfo);

			// Execute the request.
			var response = await this.MFWSClient.Post<UploadInfo>(request, token)
				.ConfigureAwait(false);

			// Return the deserialised response data.
			return response.Data;

		}

		/// <summary>
		/// Begins the upload of a file to a temporary location.
		/// </summary>
		/// <param name="fileInfo">The file to upload.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the upload.</returns>
		/// <remarks>THIS METHOD IS NOT DOCUMENTED SO DO NOT USE IN PRODUCTION ENVIRONMENTS.</remarks>
		public UploadInfo UploadTemporaryFileBlockBegin(FileInfo fileInfo, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.UploadTemporaryFileBlockBeginAsync(fileInfo, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();

		}

		#endregion

		#region Upload blocks of data to an existing temporary file

		/// <summary>
		/// Uploads a block of data to a temporary file.
		/// </summary>
		/// <param name="uploadId">The ID of the temporary upload returned from <see cref="UploadTemporaryFileBlockBeginAsync"/>.</param>
		/// <param name="totalSizeInBytes">The total size of the file (in bytes).</param>
		/// <param name="offset">The offset (in bytes) to start writing this chunk.</param>
		/// <param name="block">The binary data for the chunk.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the upload.</returns>
		/// <remarks>THIS METHOD IS NOT DOCUMENTED SO DO NOT USE IN PRODUCTION ENVIRONMENTS.</remarks>
		public async Task<UploadInfo> UploadTemporaryFileBlockAsync(int uploadId, long totalSizeInBytes, long offset, byte[] block,
			CancellationToken token = default(CancellationToken))
		{
			// Create the request.
			var request = new RestRequest($"/REST/files/upload/{uploadId}.aspx");

			// Calculate the block data.
			var chunkSize = block.Length; // All chunks must be the same size, aside from the last one.
			var resumableChunkNumber = offset / chunkSize + 1; // Chunks start at 1.
			var totalChunks = (totalSizeInBytes / chunkSize) + 1; // Chunks start at 1.

			// Add the binary data to the request body.
			request.AddParameter("application/octet-stream", block, ParameterType.RequestBody);

			// Add the querystring arguments.
			request.AddQueryParameter("resumableChunkNumber", resumableChunkNumber.ToString());
			request.AddQueryParameter("resumableTotalChunks", totalChunks.ToString());
			if (resumableChunkNumber > 0)
			{
				request.AddQueryParameter("resumableChunkSize", chunkSize.ToString());
			}

			// Execute the request.
			var response = await this.MFWSClient.Post<UploadInfo>(request, token)
				.ConfigureAwait(false);

			// Return the deserialised response data.
			return response.Data;

		}

		/// <summary>
		/// Uploads a block of data to a temporary file.
		/// </summary>
		/// <param name="uploadId">The ID of the temporary upload returned from <see cref="UploadTemporaryFileBlockBeginAsync"/>.</param>
		/// <param name="totalSizeInBytes">The total size of the file (in bytes).</param>
		/// <param name="offset">The offset (in bytes) to start writing this chunk.</param>
		/// <param name="block">The binary data for the chunk.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the upload.</returns>
		/// <remarks>THIS METHOD IS NOT DOCUMENTED SO DO NOT USE IN PRODUCTION ENVIRONMENTS.</remarks>
		public UploadInfo UploadTemporaryFileBlock(int uploadId, long totalSizeInBytes, long offset, byte[] block,
			CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.UploadTemporaryFileBlockAsync(uploadId, totalSizeInBytes, offset, block, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();

		}

		#endregion

		#region Complete/commit temporary file

		/// <summary>
		/// Marks the temporary file as committed so that it can be used from other processes.
		/// </summary>
		/// <param name="uploadInfo">The upload to commit.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the upload.</returns>
		/// <remarks>THIS METHOD IS NOT DOCUMENTED SO DO NOT USE IN PRODUCTION ENVIRONMENTS.</remarks>
		public async Task<UploadInfo> UploadTemporaryFileCommitAsync(UploadInfo uploadInfo, CancellationToken token = default(CancellationToken))
		{
			// Sanity.
			if (null == uploadInfo)
				throw new ArgumentNullException(nameof(uploadInfo));
			if (0 >= uploadInfo.Size)
				throw new ArgumentException("The file must have a size.", nameof(uploadInfo));

			// Create the request.
			var request = new RestRequest($"/REST/files/{uploadInfo.UploadID}/{uploadInfo.Size}/uploadtemporarycommit.aspx");

			// Execute the request.
			var response = await this.MFWSClient.Post<int>(request, token)
				.ConfigureAwait(false);

			// Return the deserialised response data.
			return uploadInfo;

		}

		/// <summary>
		/// Marks the temporary file as committed so that it can be used from other processes.
		/// </summary>
		/// <param name="uploadInfo">The upload to commit.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <returns>Information on the upload.</returns>
		/// <remarks>THIS METHOD IS NOT DOCUMENTED SO DO NOT USE IN PRODUCTION ENVIRONMENTS.</remarks>
		public UploadInfo UploadTemporaryFileCommit(UploadInfo uploadInfo, CancellationToken token = default(CancellationToken))
		{
			// Execute the async method.
			return this.UploadTemporaryFileCommitAsync(uploadInfo, token)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();

		}

		#endregion

		#region Helper for uploading files in blocks

		/// <summary>
		/// Uploads a temporary file to M-Files using blocks of the given size.
		/// </summary>
		/// <param name="fileToUpload">The file to upload.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <param name="blockSize">The chunk size (bytes)to upload in.  Defaults to 2MB.</param>
		/// <returns>Information on the upload.</returns>
		/// <remarks>THIS METHOD IS NOT DOCUMENTED SO DO NOT USE IN PRODUCTION ENVIRONMENTS.</remarks>
		public async Task<UploadInfo> UploadTemporaryFileInBlocksAsync(
			FileInfo fileToUpload,
			CancellationToken token = default(CancellationToken),
			int blockSize = 1024 * 1024 * 2)
		{
			// Sanity.
			if (null == fileToUpload)
				throw new ArgumentNullException(nameof(fileToUpload));
			if (false == fileToUpload.Exists)
				throw new ArgumentException("The file must exist.", nameof(fileToUpload));
			if(blockSize <= 0)
				throw new ArgumentOutOfRangeException(nameof(blockSize), "The block size must be greater than zero.");

			// Start the upload.
			var uploadInfo = await this.UploadTemporaryFileBlockBeginAsync(fileToUpload, token);

			// Iterate over the file blocks and upload them.
			long offset = 0;
			long totalSizeInBytes = fileToUpload.Length;
			if (blockSize > totalSizeInBytes)
				blockSize = (int)totalSizeInBytes;
			using (var stream = fileToUpload.OpenRead())
			{
				var buffer = new byte[blockSize];
				do
				{
					// Read the data from the stream.
					var bytesRead = await stream.ReadAsync(buffer, 0, blockSize, token);

					// Upload the block.
					await this.UploadTemporaryFileBlockAsync(uploadInfo.UploadID, totalSizeInBytes, offset, buffer, token);

					// Move onwards.
					offset += blockSize;
				} while (offset < totalSizeInBytes);
			}

			// Return the upload information.
			return uploadInfo;

		}

		/// <summary>
		/// Uploads a temporary file to M-Files using blocks of the given size.
		/// </summary>
		/// <param name="fileToUpload">The file to upload.</param>
		/// <param name="token">A cancellation token for the request.</param>
		/// <param name="blockSize">The chunk size (bytes)to upload in.  Defaults to 2MB.</param>
		/// <returns>Information on the upload.</returns>
		/// <remarks>THIS METHOD IS NOT DOCUMENTED SO DO NOT USE IN PRODUCTION ENVIRONMENTS.</remarks>
		public UploadInfo UploadTemporaryFileInBlocks(
			FileInfo fileToUpload,
			CancellationToken token = default(CancellationToken),
			int blockSize = 1024 * 1024 * 2)
		{
			// Execute the async method.
			return this.UploadTemporaryFileInBlocksAsync(fileToUpload, token, blockSize)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		#endregion

		#endregion
	}
}
