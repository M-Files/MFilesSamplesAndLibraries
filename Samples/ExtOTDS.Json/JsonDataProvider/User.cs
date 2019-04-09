using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JsonDataProvider
{
	/// <summary>
	/// A user within the system.
	/// </summary>
	/// <remarks>Reflects the JSON structure at http://jsonplaceholder.typicode.com/users </remarks>
	public class User
		: IEntity
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("email")]
		public string EmailAddress { get; set; }

		[JsonProperty("phone")]
		public string PhoneNumber { get; set; }

		[JsonProperty("website")]
		public string Website { get; set; }
	}
}
