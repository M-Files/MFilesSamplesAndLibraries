namespace MFaaP.MFWSClient
{
	/// <summary>
	/// A base implementation of <see cref="ISearchCondition"/>.
	/// </summary>
	public abstract class SearchConditionBase
		: ISearchCondition
	{
		/// <inheritdoc />
		public virtual string Expression { get; }

		/// <inheritdoc />
		public bool InvertOperator { get; set; }

		/// <inheritdoc />
		public SearchConditionOperators Operator { get; set; }

		/// <inheritdoc />
		public abstract string EncodedValue { get; }

		protected SearchConditionBase(
			string expression,
			SearchConditionOperators op,
			bool invertOperator = false)
		{
			this.Expression = expression;
			this.InvertOperator = invertOperator;
			this.Operator = op;
		}
	}

	/// <summary>
	/// A base implementation of <see cref="ISearchCondition{T}"/>.
	/// </summary>
	public abstract class SearchConditionBase<T>
		: SearchConditionBase, ISearchCondition<T>
	{
		/// <inheritdoc />
		public T Value { get; set; }

		protected SearchConditionBase(
			string expression,
			SearchConditionOperators op,
			T value,
			bool invertOperator = false)
			: base(expression, op, invertOperator)
		{
			this.Value = value;
		}
	}
}