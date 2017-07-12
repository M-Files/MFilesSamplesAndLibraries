using System;
using System.Globalization;
using System.Windows.Data;

namespace GenerateApplicationPreApprovalKey.App.ValueConverters
{
	/// <summary>
	/// An implementation of <see cref="IValueConverter"/>
	/// that returns true if the item is null or false otherwise.
	/// Note that a boolean parameter is supported to invert the logic.
	/// </summary>
	public class IsNullValueConverter
		: IValueConverter
	{
		/// <summary>
		/// Converts an object to true (if null) or false (if not null).
		/// </summary>
		/// <param name="value">The object to check.</param>
		/// <param name="targetType">Not used.</param>
		/// <param name="parameter">Passing true (boolean) will invert the logic.</param>
		/// <param name="culture">Not used.</param>
		/// <returns>True if <see cref="value"/> is null, false otherwise (unless inverted with <see cref="parameter"/>).</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool invert = false;
			Boolean.TryParse(parameter?.ToString() ?? "", out invert);

			return false == invert
				? null == value
				: null != value;
		}

		/// <inheritdoc />
		/// <remarks>Not implemented.  Throws an exception.</remarks>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
