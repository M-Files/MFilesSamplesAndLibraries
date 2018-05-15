using System;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Represents a search condition that includes deleted objects.
	/// By default the M-Files Web Service excludes deleted objects.
	/// </summary>
	[Serializable]
	public class IncludeDeletedObjectsSearchCondition
		: SearchConditionBase
	{
		/// <summary>
		/// Creates a <see cref="IncludeDeletedObjectsSearchCondition"/>.
		/// Note that this search condition is a "flag"; by including it the value is irrelevant and will include deleted objects.
		/// </summary>
		public IncludeDeletedObjectsSearchCondition()
			: base("d", SearchConditionOperators.Equals)
		{
		}

		/// <inheritdoc />
		public override string EncodedValue => "include";
	}
}