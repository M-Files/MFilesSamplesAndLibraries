using System;
using System.Linq;
using MFilesAPI;

namespace GenerateApplicationPreApprovalKey.App
{
	/// <summary>
	/// Class for easy binding enum items to a combo box.
	/// </summary>
	/// <remarks>From https://stackoverflow.com/questions/6145888/how-to-bind-an-enum-to-a-combobox-control-in-wpf, with thanks.</remarks>
	public class EnumToItemsSource
		: System.Windows.Markup.MarkupExtension
	{
		/// <summary>
		/// The enum type to expose items for.
		/// </summary>
		private readonly Type type;

		/// <summary>
		/// Instantiates an <see cref="EnumToItemsSource"/> for the provided type.
		/// </summary>
		/// <param name="type"></param>
		public EnumToItemsSource(Type type)
		{
			this.type = type;
		}

		/// <inheritdoc />
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Enum.GetValues(this.type)
				.Cast<object>()
				.Select(e => new { Value = e, DisplayName = e.ToString() });
		}
	}
}
