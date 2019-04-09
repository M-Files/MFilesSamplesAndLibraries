using System;
using System.Collections.Generic;
using JsonDataProvider;
using MFiles.Server.Extensions;

namespace ExtOTDS.Json
{
	public partial class DataSourceConnection
	{
		/// <summary>
		/// The settings from the select statement.
		/// </summary>
		public Settings SelectStatementSettings { get; set; }
			= new Settings();

		/// <inheritdoc />
		public override void PrepareForDataRetrieval(string selectStatement)
		{
			// Take the "select statement" shown in the user interface and parse into our settings object.
			// We will use this to identify what to load.
			this.SelectStatementSettings.UpdateFromString(selectStatement);
		}

		/// <inheritdoc />
		public override IEnumerable<ColumnDefinition> GetAvailableColumns()
		{
			// Which data type did the connection string say?
			var type = this.DataSource.GetElementType();
			switch (type)
			{
				// Posts?
				case "post":
				case "posts":
					return this.GetAvailableColumns(typeof(Post));

				// Users?
				case "user":
				case "users":

					return this.GetAvailableColumns(typeof(User));

				// Comments?
				case "comment":
				case "comments":

					return this.GetAvailableColumns(typeof(Comment));

				default:
					// Type name not recognised.
					throw new InvalidOperationException($"The type value ({type}) is not supported.");
			}

		}

		/// <inheritdoc />
		public override IEnumerable<DataItem> GetItems()
		{
			// Create our repository which we will load the data provider from.
			var repository = new DataProviderRepository();
			
			// Which data type did the connection string say?
			var type = this.DataSource.GetElementType();
			switch (type)
			{
				// Posts?
				case "post":
				case "posts":

					// Load all posts from the data provider.
					foreach (var item in repository
						.GetDataProvider<Post>()
						.GetAll())
					{
						// Iterate over the column definitions we expect and produce a dictionary of
						// properties for provision to the M-Files client.
						var properties = new Dictionary<int, object>();
						foreach (var column in this.GetAvailableColumns())
						{
							// Retrieve the value from the object via reflection.
							properties.Add(column.Ordinal, this.GetValue(item, column.Name));
						}

						// Return the specific object in the JSON file, with its populated set of properties.
						yield return new DataItemSimple(properties);
					}

					break;

				// Users?
				case "user":
				case "users":

					// Load all posts from the data provider.
					foreach (var item in repository
						.GetDataProvider<User>()
						.GetAll())
					{
						// Iterate over the column definitions we expect and produce a dictionary of
						// properties for provision to the M-Files client.
						var properties = new Dictionary<int, object>();
						foreach (var column in this.GetAvailableColumns())
						{
							// Retrieve the value from the object via reflection.
							properties.Add(column.Ordinal, this.GetValue(item, column.Name));
						}

						// Return the specific object in the JSON file, with its populated set of properties.
						yield return new DataItemSimple(properties);
					}

					break;

				// Comments?
				case "comment":
				case "comments":

					// Load all posts from the data provider.
					foreach (var item in repository
						.GetDataProvider<Comment>()
						.GetAll())
					{
						// Iterate over the column definitions we expect and produce a dictionary of
						// properties for provision to the M-Files client.
						var properties = new Dictionary<int, object>();
						foreach (var column in this.GetAvailableColumns())
						{
							// Retrieve the value from the object via reflection.
							properties.Add(column.Ordinal, this.GetValue(item, column.Name));
						}

						// Return the specific object in the JSON file, with its populated set of properties.
						yield return new DataItemSimple(properties);
					}

					break;

				default:
					// Type name not recognised.
					throw new InvalidOperationException($"The type value ({type}) is not supported.");
			}
		}
	}
}
