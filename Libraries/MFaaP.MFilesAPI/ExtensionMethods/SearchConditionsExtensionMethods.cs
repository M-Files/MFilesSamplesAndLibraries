using System;
using MFilesAPI;

namespace MFaaP.MFilesAPI.ExtensionMethods
{
	/// <summary>
	/// Provides extension methods for the <see cref="SearchConditions"/> class.
	/// </summary>
	public static class SearchConditionsExtensionMethods
	{
		/// <summary>
		/// Search condition for searching not deleted objects.
		/// </summary>
		/// <param name="searchConditions">The search conditions to add the condition to.</param>
		/// <param name="index">The index at which to add the search condition to the collection.</param>
		public static void AddNotDeletedSearchCondition(this SearchConditions searchConditions, int index = -1)
		{
			// Sanity.
			if (null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));

			// Create the search condition.
			SearchCondition condition = new SearchCondition
			{
				ConditionType = MFConditionType.MFConditionTypeEqual
			};
			condition.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeDeleted, null);
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
		public static void AddObjectTypeIdSearchCondition(this SearchConditions searchConditions, int objTypeId, int index = -1)
		{
			// Sanity.
			if (null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));

			// Create the search condition.
			SearchCondition condition = new SearchCondition
			{
				ConditionType = MFConditionType.MFConditionTypeEqual
			};
			condition.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeObjectTypeID, null);
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
		public static void AddMinimumObjectIdSearchCondition(this SearchConditions searchConditions, int objId, int index = -1)
		{
			// Sanity.
			if (null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));

			// Create the search condition.
			SearchCondition condition = new SearchCondition
			{
				ConditionType = MFConditionType.MFConditionTypeGreaterThanOrEqual
			};
			condition.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeObjectID, null);
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
		/// A <see cref="segment"/> of 0 and an <see cref="itemsPerSegment"/> of 1000 returns items with Ids between 0 and 999 (inclusive).
		/// A <see cref="segment"/> of 1 and an <see cref="itemsPerSegment"/> of 1000 returns items with Ids between 1000 and 1999 (inclusive).
		/// </remarks>
		public static void AddObjectIdSegmentSearchCondition(this SearchConditions searchConditions, int segment, int itemsPerSegment, int index = -1)
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
		public static void AddDisplayIdSearchCondition(this SearchConditions searchConditions, string displayId, int index = -1)
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

	}
}
