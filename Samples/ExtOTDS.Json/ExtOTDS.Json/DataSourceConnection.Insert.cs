using System;
using System.Collections.Generic;
using System.Linq;
using JsonDataProvider;
using MFiles.Server.Extensions;

namespace ExtOTDS.Json
{
	public partial class DataSourceConnection
	{
		/// <inheritdoc />
		public override bool CanReturnIdOnInsert()
		{
			// Our insert call will return the Id of the item it inserts.
			return true;
		}

		/// <inheritdoc />
		public override bool CanInsert()
		{
			// We can insert!
			return true;
		}

		/// <inheritdoc />
		public override bool ValidateInsertStatemet(string insertStatement, IList<ColumnDefinition> insertedColumns)
		{
			// We don't care about the insert statement, as we will serialise the column data directly.
			return true;
		}

		/// <inheritdoc />
		public override string InsertItem(string insertStatement, IList<ColumnValue> values)
		{
			// Create a variable to hold our new item Id.
			string newItemId = null;

			// Create our repository which we will load the data provider from.
			var repository = new DataProviderRepository();

			// Which data type did the connection string say?
			var type = this.DataSource.GetElementType();
			switch (type)
			{
				// Posts?
				case "post":
				case "posts":

					// Create the post and pull the data from the values provided.
					var post = new Post
					{
						Body = values
										   .Where(v => v.Definition.Name == nameof(Post.Body))
										   .Select(v => v.Value as string)
										   .FirstOrDefault() ?? "",
						Title = values
								   .Where(v => v.Definition.Name == nameof(Post.Title))
								   .Select(v => v.Value as string)
								   .FirstOrDefault() ?? "",
						UserId = values
									   .Where(v => v.Definition.Name == nameof(Post.UserId))
									   .Select(v => string.IsNullOrWhiteSpace(v.Value as string) ? (int?)null : Int32.Parse(v.Value as string))
									   .FirstOrDefault() ?? 0
					};

					// Insert the item and save the id.
					newItemId = repository
						.GetDataProvider<Post>()
						.Insert(post)
						.ToString();

					break;

				// Users?
				case "user":
				case "users":

					// Create the user and pull the data from the values provided.
					var user = new User
					{
						EmailAddress = values
										   .Where(v => v.Definition.Name == nameof(User.EmailAddress))
										   .Select(v => v.Value as string)
										   .FirstOrDefault() ?? "",
						Name = values
								   .Where(v => v.Definition.Name == nameof(User.Name))
								   .Select(v => v.Value as string)
								   .FirstOrDefault() ?? "",
						Username = values
									   .Where(v => v.Definition.Name == nameof(User.Username))
									   .Select(v => v.Value as string)
									   .FirstOrDefault() ?? "",
						PhoneNumber = values
										  .Where(v => v.Definition.Name == nameof(User.PhoneNumber))
										  .Select(v => v.Value as string)
										  .FirstOrDefault() ?? "",
						Website = values
									  .Where(v => v.Definition.Name == nameof(User.Website))
									  .Select(v => v.Value as string)
									  .FirstOrDefault() ?? ""
					};

					// Insert the item and save the id.
					newItemId = repository
						.GetDataProvider<User>()
						.Insert(user)
						.ToString();

					break;

				// Comments?
				case "comment":
				case "comments":

					// Create the comment and pull the data from the values provided.
					var comment = new Comment
					{
						Body = values
										   .Where(v => v.Definition.Name == nameof(Comment.Body))
										   .Select(v => v.Value as string)
										   .FirstOrDefault() ?? "",
						Name = values
								   .Where(v => v.Definition.Name == nameof(Comment.Name))
								   .Select(v => v.Value as string)
								   .FirstOrDefault() ?? "",
						EmailAddress = values
								   .Where(v => v.Definition.Name == nameof(Comment.EmailAddress))
								   .Select(v => v.Value as string)
								   .FirstOrDefault() ?? "",
						PostId = values
									   .Where(v => v.Definition.Name == nameof(Comment.PostId))
									   .Select(v => string.IsNullOrWhiteSpace(v.Value as string) ? (int?)null : Int32.Parse(v.Value as string))
									   .FirstOrDefault() ?? 0
					};

					// Insert the item and save the id.
					newItemId = repository
						.GetDataProvider<Comment>()
						.Insert(comment)
						.ToString();

					break;

				default:
					// Type name not recognised.
					throw new InvalidOperationException($"The type value ({type}) is not supported.");
			}

			// Return the id of the new item.
			return newItemId;
		}
	}
}
