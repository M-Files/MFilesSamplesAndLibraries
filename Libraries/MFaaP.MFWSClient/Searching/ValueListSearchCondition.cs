using System;
using System.Linq;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Represents a search condition that restricts by a property reference to a value list item (any property).
	/// </summary>
	/// <remarks>Can be used for both value lists and object types.</remarks>
	[Serializable]
	public class ValueListSearchCondition
		: SearchConditionBase
	{
		/// <summary>
		/// The Id of the value list to search by.
		/// </summary>
		public int ValueListId { get; set; }

		/// <summary>
		/// The (M-Files internal) Ids of value list items that must be
		/// referenced by an object for it to match.
		/// </summary>
		/// <remarks>Only used if <see cref="Type"/> is <see cref="ValueListItemIdType.Id"/>.</remarks>
		public int[] InternalIds { get; set; }

		/// <summary>
		/// The external Ids of value list items that must be
		/// referenced by an object for it to match.
		/// </summary>
		/// <remarks>Only used if <see cref="Type"/> is <see cref="ValueListItemIdType.ExternalId"/>.</remarks>
		public string[] ExternalIds { get; set; }

		/// <summary>
		/// Gets the type of search condition (internal ID or external ID) which is being used.
		/// </summary>
		public ValueListItemIdType Type { get; set; }
			= ValueListItemIdType.Id;

		/// <summary>
		/// Creates a <see cref="ValueListSearchCondition"/>, searching for the value supplied.
		/// </summary>
		/// <param name="valueListId">The Id of the property def to search by.</param>
		/// <param name="internalValueListItemIds">The Ids of the value list items to match.</param>
		public ValueListSearchCondition(
			int valueListId,
			params int[] internalValueListItemIds)
			: base("vl", SearchConditionOperators.Equals, false)
		{
			this.ValueListId = valueListId;
			this.InternalIds = internalValueListItemIds;
			this.Type = ValueListItemIdType.Id;
		}

		/// <summary>
		/// Creates a <see cref="ValueListSearchCondition"/>, searching for the value supplied.
		/// </summary>
		/// <param name="valueListId">The Id of the property def to search by.</param>
		/// <param name="externalValueListItemIds">The Ids of the value list items to match.</param>
		public ValueListSearchCondition(
			int valueListId,
			params string[] externalValueListItemIds)
			: base("vl", SearchConditionOperators.Equals, false)
		{
			this.ValueListId = valueListId;
			this.ExternalIds = externalValueListItemIds;
			this.Type = ValueListItemIdType.ExternalId;
		}

		/// <inheritdoc />
		public override string Expression => $"{base.Expression}{this.ValueListId}";

		/// <inheritdoc />
		public override string EncodedValue =>
			this.Type == ValueListItemIdType.Id
			? String.Join(",", this.InternalIds)
			: String.Join(",", this.ExternalIds.Select( i => "e" + i))
			;
	}

	/// <summary>
	/// The type of Id to match against.
	/// </summary>
	public enum ValueListItemIdType
	{
		/// <summary>
		/// Notes that the matched value is the internal M-Files Id of the value list item.
		/// </summary>
		Id = 0,

		/// <summary>
		/// Notes that the matched value is the external Id of the value list item (when using external object types or value lists).
		/// </summary>
		ExternalId = 1
	}
}
