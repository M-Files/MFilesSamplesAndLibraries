using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MFaaP.MFWSClient;
using Newtonsoft.Json;

namespace MFWSDownloading
{
	/// <summary>
	/// A example of downloading a file from the M-Files Web Service.
	/// </summary>
	class Program
	{
		/// <summary>
		/// The term to search for.
		/// </summary>
		private const string queryTerm = "mfws";

		/// <summary>
		/// The folder that the download will be placed in.
		/// </summary>
		private const string downloadFolder = @"c:\temp\mfws-downloads\";

		static void Main(string[] args)
		{
			// Download the items using the library.
			Console.WriteLine($"Downloading items using the library.");
			Task.WaitAll(Program.UseLibrary());
			Console.WriteLine("Complete.  Press enter to continue.");
			Console.ReadLine();

			// Download the items without using the library.
			Console.WriteLine($"Downloading items using the API directly.");
			Task.WaitAll(Program.UseApiDirectly());
			Console.WriteLine("Complete.  Press enter to continue.");
			Console.ReadLine();
		}

		/// <summary>
		/// Uses the helper library to execute a search.
		/// </summary>
		static async Task UseLibrary()
		{

			// Connect to the online knowledgebase.
			// Note that this doesn't require authentication.
			var client = new MFWSClient("http://kb.cloudvault.m-files.com");

			// Execute a quick search for the query term.
			var results = await client.ObjectSearchOperations.SearchForObjectsByString(Program.queryTerm);

			// Iterate over the results and output them.
			Console.WriteLine($"There were {results.Length} results returned.");
			foreach (var objectVersion in results)
			{
				Console.WriteLine($"\t{objectVersion.Title}");
				Console.WriteLine($"\t\tType: {objectVersion.ObjVer.Type}, ID: {objectVersion.ObjVer.ID}");

				// Create a folder for the files to go in.
				var folderPath =
					new System.IO.DirectoryInfo(System.IO.Path.Combine(Program.downloadFolder, objectVersion.ObjVer.ID.ToString()));
				if (false == folderPath.Exists)
					folderPath.Create();

				// Download the files.
				foreach (var file in objectVersion.Files)
				{
					// Generate a unique file name.
					var fileName = System.IO.Path.Combine(folderPath.FullName, file.ID + "." + file.Extension);

					// Download the file data.
					await client.ObjectFileOperations.DownloadFile(objectVersion.ObjVer.Type,
						objectVersion.ObjVer.ID,
						objectVersion.Files[0].ID,
						fileName,
						objectVersion.ObjVer.Version);

					Console.WriteLine($"\t\t\tFile: {file.Name} output to {fileName}");
				}

			}
			
		}

		/// <summary>
		/// Executes a search using the endpoint directly.
		/// </summary>
		static async Task UseApiDirectly()
		{
			// Build the url to request (note to encode the query term).
			var url =
				new Uri("http://kb.cloudvault.m-files.com/REST/objects?q=" + System.Net.WebUtility.UrlEncode(Program.queryTerm));

			// Build the request.
			var httpClient = new HttpClient();

			// Start the request.
			string responseBody = await httpClient.GetStringAsync(url);
			
			// Output the body.
			// System.Console.WriteLine($"Raw content returned: {responseBody}.");

			// Attempt to parse it.  For this we will use the Json.NET library, but you could use others.
			var results = JsonConvert.DeserializeObject<Results<ObjectVersion>>(responseBody);

			// Iterate over the results and output them.
			Console.WriteLine($"There were {results.Items.Count} results returned.");
			foreach (var objectVersion in results.Items)
			{
				Console.WriteLine($"\t{objectVersion.Title}");
				Console.WriteLine($"\t\tType: {objectVersion.ObjVer.Type}, ID: {objectVersion.ObjVer.ID}");

				// Create a folder for the files to go in.
				var folderPath =
					new System.IO.DirectoryInfo(System.IO.Path.Combine(Program.downloadFolder, objectVersion.ObjVer.ID.ToString()));
				if (false == folderPath.Exists)
					folderPath.Create();

				// Download the files.
				foreach (var file in objectVersion.Files)
				{
					// Generate a unique file name.
					var fileName = System.IO.Path.Combine(folderPath.FullName, file.ID + "." + file.Extension);

					// Download the file data.
					var data = await
						httpClient.GetByteArrayAsync(
							$"http://kb.cloudvault.m-files.com/REST/objects/{objectVersion.ObjVer.Type}/{objectVersion.ObjVer.ID}/{objectVersion.ObjVer.Version}/files/{file.ID}/content");

					// Open a stream to the file.
					using (var stream = System.IO.File.OpenWrite(fileName))
					{
						// Save the content to disk.
						stream.Write(data, 0, data.Length);
					}

					// Log.
					Console.WriteLine($"\t\t\tFile: {file.Name} output to {fileName}");

				}
			}

		}

		#region Classes for deserialisation (only used with the direct API calls)

		/// <summary>
		/// Results of a query which might leave only a partial set of items.
		/// </summary>
		/// <remarks>Copied from MFaaP.MFWSClient project, only used for deserialisation with the direct API calls.</remarks>
		private class Results<T>
		{
			public Results()
			{
			}

			/// <summary>
			/// Contains results of a query
			/// </summary>
			public List<T> Items { get; set; }

			/// <summary>
			/// True if there were more results which were left out.
			/// </summary>
			public bool MoreResults { get; set; }

		}

		/// <summary>
		/// Based on M-Files API.
		/// </summary>
		/// <remarks>Copied from MFaaP.MFWSClient project, only used for deserialisation with the direct API calls.</remarks>
		private class ObjectVersion
		{

			public ObjectVersion()
			{
			}

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public DateTime AccessedByMeUtc { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public DateTime CheckedOutAtUtc { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int CheckedOutTo { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public string CheckedOutToUserName { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int Class { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public DateTime CreatedUtc { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool Deleted { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public string DisplayID { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public List<ObjectFile> Files { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool HasAssignments { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool HasRelationshipsFromThis { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool HasRelationshipsToThis { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool IsStub { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public DateTime LastModifiedUtc { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool ObjectCheckedOut { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool ObjectCheckedOutToThisUser { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public MFObjectVersionFlag ObjectVersionFlags { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public ObjVer ObjVer { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool SingleFile { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool ThisVersionLatestToThisUser { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public string Title { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public bool VisibleAfterOperation { get; set; }

		}



		/// <summary>
		/// Based on M-Files API.
		/// </summary>
		/// <remarks>Copied from MFaaP.MFWSClient project, only used for deserialisation with the direct API calls.</remarks>
		private class ObjectFile
		{

			public ObjectFile()
			{
			}

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public DateTime ChangeTimeUtc { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public string Extension { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int ID { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int Version { get; set; }

		}



		/// <summary>
		/// Based on M-Files API.
		/// </summary>
		/// <remarks>Copied from MFaaP.MFWSClient project, only used for deserialisation with the direct API calls.</remarks>
		private class ObjVer
		{

			public ObjVer()
			{
			}

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int ID { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int Type { get; set; }

			/// <summary>
			/// Based on M-Files API.
			/// </summary>
			public int Version { get; set; }

		}

		#endregion

	}
}
