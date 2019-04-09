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
		public override bool CanUpdate()
		{
			// We support updates.
			return true;
		}

		/// <inheritdoc />
		public override bool ValidateUpdateStatemet(string updateStatement, IList<ColumnDefinition> updatedColumns)
		{
			// We won't use the update statement here.
			return true;
		}

		/// <inheritdoc />
		public override void UpdateItem(string updateStatement, string extid, IList<ColumnValue> values, IList<ColumnValue> previousValues)
		{
			// Retrieve the id of the item.
			int itemId;
			try
			{
				itemId = Int32.Parse(extid);
			}
			catch
			{
				throw new ArgumentException("The external Id provided could not be converted to a valid integer", nameof(extid));
			}

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
						Id = itemId,
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

					// Update the post.
					repository
						.GetDataProvider<Post>()
						.Update(itemId, post);

					break;

				// Users?
				case "user":
				case "users":

					// Create the user and pull the data from the values provided.
					var user = new User
					{
						Id = itemId,
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

					// Update the user
					repository
						.GetDataProvider<User>()
						.Update(itemId, user);

					break;

				// Comments?
				case "comment":
				case "comments":

					// Create the comment and pull the data from the values provided.
					var comment = new Comment
					{
						Id = itemId,
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

					// Update the comment.
					repository
						.GetDataProvider<Comment>()
						.Update(itemId, comment);

					break;

				default:
					// Type name not recognised.
					throw new InvalidOperationException($"The type value ({type}) is not supported.");
			}
		}
	}
}
