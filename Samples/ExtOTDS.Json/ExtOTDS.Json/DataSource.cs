using System;
using MFiles.Server.Extensions;

namespace ExtOTDS.Json
{
	public class DataSource
		: IDataSource
	{
		/// <summary>
		/// The key in the select statement string (e.g. in "type=posts", the key is "TYPE")
		/// that defines the type of object to load.  Must be in upper-case.
		/// </summary>
		public const string TypeKey = "TYPE";

		/// <summary>
		/// The settings from the connection string.
		/// </summary>
		public Settings ConnectionSettings { get; private set; }
			= new Settings();

		protected IDataSourceConnection DataSourceConnection { get; private set; }

		/// <inheritdoc />
		public IDataSourceConnection OpenConnection(string connectionString, Guid configurationId)
		{
			// Take the connection string shown in the user interface and parse into our settings object.
			// We could use this to identify what to load.
			this.ConnectionSettings.UpdateFromString(connectionString);

			// Instantiate our data source connection.
			this.DataSourceConnection = new DataSourceConnection(this);

			// We only have one data source connection, so we will return it here.
			return this.DataSourceConnection;
		}

		/// <inheritdoc />
		public bool CanAlterData()
		{
			// If our data source supports alteration then we do too.
			// Check whether our current data source implements IDataAlteration (if so,
			// it may support update/delete/add functionality).
			return this.DataSourceConnection is IDataAlteration;
		}

		/// <summary>
		/// Returns the element type data from the connection string.
		/// </summary>
		/// <returns></returns>
		public string GetElementType()
		{
			// Return the type data from the settings.
			return this.ConnectionSettings.ContainsKey(DataSource.TypeKey) 
				? this.ConnectionSettings[DataSource.TypeKey] 
				: null;
		}
	}
}
