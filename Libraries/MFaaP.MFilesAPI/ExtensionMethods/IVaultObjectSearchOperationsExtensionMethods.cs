using System;
using System.Collections.Generic;
using System.Linq;
using MFilesAPI;

namespace MFaaP.MFilesAPI.ExtensionMethods
{
	// ReSharper disable once InconsistentNaming
	/// <summary>
	/// Extension methods for objects implementing <see cref="IVaultObjectSearchOperations"/>.
	/// </summary>
	public static class IVaultObjectSearchOperationsExtensionMethods
	{
		/// <summary>
		/// The number of items to match in one segment.
		/// </summary>
		/// <remarks>Making it larger will reduce queries to M-Files, but higher chance of timeout.</remarks>
		public const int DefaultNumberOfItemsInSegment = 1000;

		/// <summary>
		/// The maximum index segment (must be greater than zero).
		/// </summary>
		public const int MaximumSegmentIndex = 100001;

		/// <summary>
		/// Searches for objects matching the search conditions, in segments by M-Files Object Id.
		/// Returns an enumerable of object versions, flattening out the segment data.
		/// Using this approach (e.g. searching for IDs 1-1000, then 1001-2000, etc.) bypasses the return count limitations.
		/// </summary>
		/// <param name="vaultObjectSearchOperations">The search operations object to query with.</param>
		/// <param name="searchConditions">The search conditions to execute.</param>
		/// <param name="searchFlags">Any search flags to execute (note that the order will not be respected).</param>
		/// <param name="startSegment">The (zero-based) index of the segment to start at.</param>
		/// <param name="itemsPerSegment">The number of items to include in each segment. See <see cref="DefaultNumberOfItemsInSegment"/>.</param>
		/// <param name="maximumSegmentIndex"></param>
		/// <returns></returns>
		/// <remarks>Be aware that calling this method will return all matching items, which may cause significant load on the M-Files server.</remarks>
		public static IEnumerable<ObjectVersion> SearchForObjectsByConditionsSegmented_Flat(this IVaultObjectSearchOperations vaultObjectSearchOperations,
			SearchConditions searchConditions,
			MFSearchFlags searchFlags = MFSearchFlags.MFSearchFlagDisableRelevancyRanking,
			int startSegment = 0,
			int itemsPerSegment = IVaultObjectSearchOperationsExtensionMethods.DefaultNumberOfItemsInSegment,
			int maximumSegmentIndex = IVaultObjectSearchOperationsExtensionMethods.MaximumSegmentIndex)
		{
			// Sanity.
			if (null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));
			if (null == vaultObjectSearchOperations)
				throw new ArgumentNullException(nameof(vaultObjectSearchOperations));
			if (startSegment < 0)
				startSegment = IVaultObjectSearchOperationsExtensionMethods.DefaultNumberOfItemsInSegment;
			if (maximumSegmentIndex < 0)
				maximumSegmentIndex = IVaultObjectSearchOperationsExtensionMethods.MaximumSegmentIndex;

			// Iterate over each segment to return results.
			foreach (
				var segment in
				vaultObjectSearchOperations.SearchForObjectsByConditionsSegmented(searchConditions, searchFlags, startSegment,
					itemsPerSegment, maximumSegmentIndex))
			{
				// Iterate over each result.
				foreach (var result in segment.ObjectVersions.Cast<ObjectVersion>())
				{
					yield return result;
				}
			}

		}

		/// <summary>
		/// Searches for objects matching the search conditions, in segments by M-Files Object Id.
		/// Returns an enumerable of each segment's search results.
		/// Using this approach (e.g. searching for IDs 1-1000, then 1001-2000, etc.) bypasses the return count limitations.
		/// </summary>
		/// <param name="vaultObjectSearchOperations">The search operations object to query with.</param>
		/// <param name="searchConditions">The search conditions to execute.</param>
		/// <param name="searchFlags">Any search flags to execute (note that the order will not be respected).</param>
		/// <param name="startSegment">The (zero-based) index of the segment to start at.</param>
		/// <param name="itemsPerSegment">The number of items to include in each segment. See <see cref="DefaultNumberOfItemsInSegment"/>.</param>
		/// <param name="maximumSegmentIndex"></param>
		/// <returns></returns>
		/// <remarks>Be aware that calling this method will return all matching items, which may cause significant load on the M-Files server.</remarks>
		public static IEnumerable<IObjectSearchResults> SearchForObjectsByConditionsSegmented(this IVaultObjectSearchOperations vaultObjectSearchOperations,
			SearchConditions searchConditions, 
			MFSearchFlags searchFlags = MFSearchFlags.MFSearchFlagDisableRelevancyRanking, 
			int startSegment = 0,
			int itemsPerSegment = IVaultObjectSearchOperationsExtensionMethods.DefaultNumberOfItemsInSegment,
			int maximumSegmentIndex = IVaultObjectSearchOperationsExtensionMethods.MaximumSegmentIndex)
		{
			// Sanity.
			if (null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));
			if (null == vaultObjectSearchOperations)
				throw new ArgumentNullException(nameof(vaultObjectSearchOperations));
			if (startSegment < 0)
				startSegment = IVaultObjectSearchOperationsExtensionMethods.DefaultNumberOfItemsInSegment;
			if (maximumSegmentIndex < 0)
				maximumSegmentIndex = IVaultObjectSearchOperationsExtensionMethods.MaximumSegmentIndex;

			// No point having relevancy ranking as we're throwing it away.
			// ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
			searchFlags |= MFSearchFlags.MFSearchFlagDisableRelevancyRanking;

			// A segment has a start index and a number of items in it.
			// Segment 0 = object Ids 0 through 999.
			// Segment 1 = object Ids 1000 through 1999.
			// ....

			// Start at segment zero.
			int segment = startSegment;

			// Let's have a sanity counter, just in case.
			int sanity = maximumSegmentIndex;

			// While there are objects with a higher ID than the segment we're in, continue searching.
			while ((sanity > 0) && vaultObjectSearchOperations.HasMoreResults(searchConditions, segment, searchFlags, itemsPerSegment))
			{
				// Clone the search conditions (so we can add current-segment condition).
				var internalSearchConditions = searchConditions.Clone();

				// Add search condition:
				//   Id within the range: (segment - itemsPerSegment) to ((segment + 1) * itemsPerSegment)
				internalSearchConditions.AddObjectIdSegmentSearchCondition(segment, itemsPerSegment);

				// Return the results.
				yield return vaultObjectSearchOperations
					.SearchForObjectsByConditionsEx(internalSearchConditions, searchFlags, SortResults: false, MaxResultCount: 0,
						SearchTimeoutInSeconds: 0);

				// Move to the next segment.
				segment++;

				// Reduce the sanity value (if we hit zero we will exit).
				sanity--;

			}

		}

		/// <summary>
		/// Returns whether there are objects in the vault that have a higher internal Id than
		/// the current segment.
		/// </summary>
		/// <param name="vaultObjectSearchOperations">The search operations object to query with.</param>
		/// <param name="searchConditions">The search conditions to execute.</param>
		/// <param name="searchFlags">Any search flags to execute (note that the order will not be respected).</param>
		/// <param name="segment">The zero-based index of the segment to return data for.</param>
		/// <param name="itemsPerSegment">The number of items to include in each segment. See <see cref="DefaultNumberOfItemsInSegment"/>.</param>
		/// <returns>true if there is at least one, false if there are none.</returns>
		private static bool HasMoreResults(this IVaultObjectSearchOperations vaultObjectSearchOperations,
			SearchConditions searchConditions, 
			int segment,
			MFSearchFlags searchFlags = MFSearchFlags.MFSearchFlagDisableRelevancyRanking,
			int itemsPerSegment = IVaultObjectSearchOperationsExtensionMethods.DefaultNumberOfItemsInSegment)
		{
			// Sanity.
			if (null == vaultObjectSearchOperations)
				throw new ArgumentNullException(nameof(vaultObjectSearchOperations));
			if (null == searchConditions)
				throw new ArgumentNullException(nameof(searchConditions));
			if (segment <= 0)
				segment = 0;
			if (itemsPerSegment <= 0)
				itemsPerSegment = IVaultObjectSearchOperationsExtensionMethods.DefaultNumberOfItemsInSegment;

			// There are more results if there are any objects that meet the search criteria, with
			// an Id greater than the current segment range.

			// Clone the search conditions (so we can add object id condition).
			var internalSearchConditions = searchConditions.Clone();

			// Add search condition:
			//   Id at least (segment * itemsPerSegment)
			internalSearchConditions.AddMinimumObjectIdSearchCondition(segment * itemsPerSegment);

			// If we get zero items then there are no more results.
			var results = vaultObjectSearchOperations.SearchForObjectsByConditionsEx(
				internalSearchConditions, // Our search conditions.
				searchFlags,
				SortResults: false, // Don't bother attempting to sort them.
				MaxResultCount: 1, // We only need to know if there is at least one, nothing more.
				SearchTimeoutInSeconds: 0);

			// Did we only get one?
			return 0 != results.Count;

		}

	}
}
