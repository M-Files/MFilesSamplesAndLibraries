using System;
using System.IO;
using System.Reflection;

namespace JsonDataProvider
{
	public class DataProviderRepository
	{
		public DirectoryInfo JsonDirectory { get; set; }
			= new DirectoryInfo(Path.GetTempPath());

		public DataProvider<T> GetDataProvider<T>()
			where T : IEntity
		{
			// Get the expected file.
			FileInfo fileInfo;
			if (typeof(T) == typeof(User))
			{
				fileInfo = new FileInfo(System.IO.Path.Combine(this.JsonDirectory.FullName, "users.json"));
			}
			else if (typeof(T) == typeof(Comment))
			{
				fileInfo = new FileInfo(System.IO.Path.Combine(this.JsonDirectory.FullName, "comments.json"));
			}
			else if (typeof(T) == typeof(Post))
			{
				fileInfo = new FileInfo(System.IO.Path.Combine(this.JsonDirectory.FullName, "posts.json"));
			}
			else
			{
				throw new NotSupportedException("A provider cannot be found for the type supplied.");
			}

			// If the file does not exist, create it using the sample data.
			if (false == fileInfo.Exists)
			{
				// Get all the embedded resources (sample data).
				var executingAssembly = Assembly.GetExecutingAssembly();
				foreach (var resourceName in executingAssembly.GetManifestResourceNames())
				{
					// Skip ones that aren't the one we want.
					if (false == resourceName.EndsWith($".{fileInfo.Name}"))
						continue;

					// Save the resource out to the expected location.
					using (var resourceStream = executingAssembly.GetManifestResourceStream(resourceName))
					{
						using (var fileStream = fileInfo.OpenWrite())
						{
							// Copy the embedded resource to the file stream.
							resourceStream.CopyTo(fileStream);
							fileStream.Close();
						}
						resourceStream.Close();

						// Break out the foreach.
						break;
					}
				}

				// Update the ".Exists" property on our file info.
				fileInfo = new FileInfo(fileInfo.FullName);
			}

			// Return a provider.
			return new DataProvider<T>(fileInfo);

		}

	}
}
