using System;
using JsonDataProvider;
using MFiles.Server.Extensions;

namespace ExtOTDS.Json
{
	public partial class DataSourceConnection
	{
		/// <inheritdoc />
		public override bool CanDelete()
		{
			// We support deletion.
			return true;
		}

		/// <inheritdoc />
		public override bool ValidateDeleteStatemet(string deleteStatement, ColumnDefinition extidColumn)
		{
			// We won't use the delete statement.
			return true;
		}

		/// <inheritdoc />
		public override void DeleteItem(string deleteStatement, ColumnValue extid)
		{
			// Retrieve the id of the item.
			int itemId;
			try
			{
				itemId = Int32.Parse(extid.Value as string);
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

					// Delete the post.
					repository
						.GetDataProvider<Post>()
						.Delete(itemId);

					break;

				// Users?
				case "user":
				case "users":

					// Delete the user.
					repository
						.GetDataProvider<User>()
						.Delete(itemId);

					break;

				// Comments?
				case "comment":
				case "comments":

					// Delete the comment.
					repository
						.GetDataProvider<Comment>()
						.Delete(itemId);

					break;

				default:
					// Type name not recognised.
					throw new InvalidOperationException($"The type value ({type}) is not supported.");
			}
		}
	}
}
