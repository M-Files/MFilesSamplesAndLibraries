using System;
using MFilesAPI;

namespace MFaaP.MFilesAPI.ExtensionMethods
{
	/// <summary>
	/// Provides extension methods for the <see cref="ISearchConditions"/> class.
	/// </summary>
	// ReSharper disable once InconsistentNaming
	public static class ISearchConditionsExtensionMethods
	{
		/// <summary>
		/// Search condition for searching not deleted objects.
		/// </summary>
		/// <param name="searchConditions">The search conditions to add the condition to.</param>
		/// <param name="index">The index at which to add the search condition to the collection.</param>
		public static void AddNotDeletedSearchCondition(this ISearchConditions searchConditions, int index = -1)
		{
			// Sanity.
			if (null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));

			// Create the search condition.
			SearchCondition condition = new SearchCondition
			{
				ConditionType = MFConditionType.MFConditionTypeEqual
			};
			condition.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeDeleted);
			condition.TypedValue.SetValue(MFDataType.MFDatatypeBoolean, false);

			// Add the condition at the index provided.
			searchConditions.Add(index, condition);
		}

		/// <summary>
		/// Search condition for searching specific object type.
		/// </summary>
		/// <param name="searchConditions">The search conditions to add the condition to.</param>
		/// <param name="objTypeId">The Id of the object type to restrict the search by.</param>
		/// <param name="index">The index at which to add the search condition to the collection.</param>
		public static void AddObjectTypeIdSearchCondition(this ISearchConditions searchConditions, int objTypeId, int index = -1)
		{
			// Sanity.
			if (null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));

			// Create the search condition.
			SearchCondition condition = new SearchCondition
			{
				ConditionType = MFConditionType.MFConditionTypeEqual
			};
			condition.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeObjectTypeID);
			condition.TypedValue.SetValue(MFDataType.MFDatatypeLookup, objTypeId);

			// Add the condition at the index provided.
			searchConditions.Add(index, condition);
		}

		/// <summary>
		/// Search condition, restricting down to objects with an Id greater than or equal to the parameter.
		/// </summary>
		/// <param name="searchConditions">The search conditions to add the condition to.</param>
		/// <param name="objId">The minimum object Id.</param>
		/// <param name="index">The index at which to add the search condition to the collection.</param>
		public static void AddMinimumObjectIdSearchCondition(this ISearchConditions searchConditions, int objId, int index = -1)
		{
			// Sanity.
			if (null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));

			// Create the search condition.
			SearchCondition condition = new SearchCondition
			{
				ConditionType = MFConditionType.MFConditionTypeGreaterThanOrEqual
			};
			condition.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeObjectID);
			condition.TypedValue.SetValue(MFDataType.MFDatatypeInteger, objId);

			// Add the condition at the index provided.
			searchConditions.Add(index, condition);
		}

		/// <summary>
		/// Search condition for searching for object Ids that sit within a segment.
		/// </summary>
		/// <param name="searchConditions">The search conditions to add the condition to.</param>
		/// <param name="segment">The zero-based index of the segment to return data for.</param>
		/// <param name="itemsPerSegment">The number of items to include in each segment.</param>
		/// <param name="index">The index at which to add the search condition to the collection.</param>
		/// <remarks>
		/// A <paramref name="segment"/> of 0 and an <paramref name="itemsPerSegment"/> of 1000 returns items with Ids between 0 and 999 (inclusive).
		/// A <paramref name="segment"/> of 1 and an <paramref name="itemsPerSegment"/> of 1000 returns items with Ids between 1000 and 1999 (inclusive).
		/// </remarks>
		public static void AddObjectIdSegmentSearchCondition(this ISearchConditions searchConditions, int segment, int itemsPerSegment, int index = -1)
		{
			// Sanity.
			if (null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));

			// Create the search condition.
			SearchCondition condition = new SearchCondition
			{
				ConditionType = MFConditionType.MFConditionTypeEqual
			};
			condition.Expression.SetObjectIDSegmentExpression(itemsPerSegment);
			condition.TypedValue.SetValue(MFDataType.MFDatatypeInteger, segment);

			// Add the condition at the index provided.
			searchConditions.Add(index, condition);
		}

		/// <summary>
		/// Adds a search condition for a given "display id" (or external id).
		/// </summary>
		/// <param name="searchConditions">The search conditions to add the condition to.</param>
		/// <param name="displayId">The display id of the item to search for.</param>
		/// <param name="index">The index at which to add the search condition to the collection.</param>
		public static void AddDisplayIdSearchCondition(this ISearchConditions searchConditions, string displayId, int index = -1)
		{
			// Sanity.
			if (null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));

			// Create the search condition.
			SearchCondition condition = new SearchCondition
			{
				ConditionType = MFConditionType.MFConditionTypeEqual,
			};
			condition.Expression.DataStatusValueType = MFStatusType.MFStatusTypeExtID;
			condition.TypedValue.SetValue(MFDataType.MFDatatypeText, displayId);

			// Add the condition at the index provided.
			searchConditions.Add(index, condition);
		}

		/// <summary>
		/// Adds a search condition for a full-text search for a text query.
		/// </summary>
		/// <param name="searchConditions">The search conditions to add the condition to.</param>
		/// <param name="query">The query to full-text-search for.</param>
		/// <param name="fullTextSearchFlags">What type of full-text-search to execute.  Defaults to searching in both file data and metadata.</param>
		/// <param name="index">The index at which to add the search condition to the collection.</param>
		public static void AddFullTextSearchCondition(this ISearchConditions searchConditions, string query,
			// ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
			MFFullTextSearchFlags fullTextSearchFlags = MFFullTextSearchFlags.MFFullTextSearchFlagsLookInFileData | MFFullTextSearchFlags.MFFullTextSearchFlagsLookInMetaData,
			int index = -1)
		{
			// Sanity.
			if (null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));

			// Create the search condition.
			SearchCondition condition = new SearchCondition
			{
				ConditionType = MFConditionType.MFConditionTypeContains,
			};
			condition.Expression.SetAnyFieldExpression(fullTextSearchFlags);
			condition.TypedValue.SetValue(MFDataType.MFDatatypeText, query);

			// Add the condition at the index provided.
			searchConditions.Add(index, condition);
		}



	}
}
