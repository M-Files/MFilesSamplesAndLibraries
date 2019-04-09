using System;
using Newtonsoft.Json;

namespace JsonDataProvider
{
	/// <summary>
	/// A comment within the system.
	/// </summary>
	/// <remarks>Reflects the JSON structure at http://jsonplaceholder.typicode.com/commnets </remarks>
	public class Comment
		: IEntity
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("postID")]
		public int PostId { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("email")]
		public string EmailAddress { get; set; }

		[JsonProperty("body")]
		public string Body { get; set; }
	}
}
