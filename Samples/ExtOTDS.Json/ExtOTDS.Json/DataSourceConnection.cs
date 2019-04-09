using System;
using System.Collections.Generic;
using MFiles.Server.Extensions;

namespace ExtOTDS.Json
{
	/// <summary>
	/// An implementation of IDataSourceConnection and IDataAlteration,
	/// uisng a JsonDataProvider for the actual data storage/retrieval.
	/// </summary>
	/// 
	// Note that implementations for reading data, inserting data, updating data, and deleting data
	// have been split into separate partial classes:
	// DataSourceConnection.cs (helper methods)
	// DataSourceConnection.Read.cs (methods for reading data)
	// DataSourceConnection.Insert.cs (methods for inserting data)
	// DataSourceConnection.Update.cs (methods for updating data)
	// DataSourceConnection.Delete.cs (methods for deleting data)
	public partial class DataSourceConnection
		: BasicDataSourceConnection
	{

		protected DataSource DataSource { get; private set; }

		/// <summary>
		/// Instantiates a DataSourceConnection for a specific data source.
		/// </summary>
		/// <param name="dataSource">The data source this connection is for.</param>
		public DataSourceConnection(DataSource dataSource)
		{
			// Sanity.
			if (null == dataSource)
				throw new ArgumentNullException(nameof(dataSource));
			this.DataSource = dataSource;
		}

		/// <summary>
		/// Uses reflection to get the value of a property or field on an object.
		/// </summary>
		/// <param name="obj">The object to look up the value on.</param>
		/// <param name="propertyOrFieldName">The name of the property or field.</param>
		/// <returns>The value of the property or field.</returns>
		protected object GetValue(object obj, string propertyOrFieldName)
		{

			// Sanity.
			if (null == obj)
				throw new ArgumentNullException(nameof(obj));
			if(string.IsNullOrWhiteSpace(propertyOrFieldName))
				throw new ArgumentException("The property or field name cannot be null or whitespace.", nameof(propertyOrFieldName));

			// Iterate over the property definitions.
			foreach (var property in obj.GetType().GetProperties())
			{
				// Is this the right property?
				if (property.Name != propertyOrFieldName)
					continue;

				// Try and read the value.
				try
				{
					return property.GetValue(obj);

				}
				catch (Exception e)
				{
					// TODO: Exception logging!
					return null;
				}
			}

			// Iterate over the field definitions.
			foreach (var field in obj.GetType().GetFields())
			{
				// Is this the right field?
				if (field.Name != propertyOrFieldName)
					continue;

				// Try and read the value.
				try
				{
					return field.GetValue(obj);

				}
				catch (Exception e)
				{
					// TODO: Exception logging!
					return null;
				}
			}

			// Could not find the property or field.
			throw new ArgumentException("The property or field could not be found", nameof(propertyOrFieldName));

		}

		/// <summary>
		/// A helper function to extract properties and fields from the given type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		protected IEnumerable<ColumnDefinition> GetAvailableColumns(Type type)
		{
			// Use a counter for our column index.
			var counter = 0;

			// Iterate over the property definitions.
			foreach (var property in type.GetProperties())
			{
				// Skip write-only.
				if (false == property.CanRead)
					continue;

				// Return this column definition.
				yield return new ColumnDefinition()
				{
					Type = property.PropertyType,
					Name = property.Name,
					Ordinal = counter
				};

				// Increment the column index.
				counter++;
			}

			// Iterate over the field definitions.
			foreach (var field in type.GetFields())
			{
				// Return this column definition.
				yield return new ColumnDefinition()
				{
					Type = field.FieldType,
					Name = field.Name,
					Ordinal = counter
				};

				// Increment the column index.
				counter++;
			}

		}
	}
}
