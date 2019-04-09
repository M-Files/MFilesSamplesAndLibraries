using System;
using Newtonsoft.Json;

namespace JsonDataProvider
{
	/// <summary>
	/// A post within the system.
	/// </summary>
	/// <remarks>Reflects the JSON structure at http://jsonplaceholder.typicode.com/posts </remarks>
	public class Post
		: IEntity
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("userId")]
		public int UserId { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("body")]
		public string Body { get; set; }
	}
}
