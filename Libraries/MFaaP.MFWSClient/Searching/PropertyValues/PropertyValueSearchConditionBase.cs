using System;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Represents a search condition that restricts by a property value.
	/// </summary>
	[Serializable]
	public abstract class PropertyValueSearchConditionBase<T>
		: SearchConditionBase<T>
	{
		/// <summary>
		/// The Id of the property definition to search by.
		/// </summary>
		public int PropertyDefId { get; set; }

		/// <summary>
		/// Creates a <see cref="PropertyValueSearchConditionBase{T}"/>, searching for the value supplied.
		/// </summary>
		/// <param name="propertyDefId">The Id of the property def to search by.</param>
		/// <param name="value">The value to search by.</param>
		/// <param name="searchConditionOperator">The operator to use (defaults to Equals if not provided).</param>
		/// <param name="invertOperator">Whether to invert the search operator (defaults to false if not provided).</param>
		protected PropertyValueSearchConditionBase(
			int propertyDefId,
			T value,
			SearchConditionOperators searchConditionOperator = SearchConditionOperators.Equals,
			bool invertOperator = false)
			: base("p", searchConditionOperator, value, invertOperator)
		{
			this.PropertyDefId = propertyDefId;
		}

		/// <inheritdoc />
		public override string Expression => $"{base.Expression}{this.PropertyDefId}";

		/// <inheritdoc />
		public override string EncodedValue => this.Value.ToString();
	}
}