using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using MFilesAPI;

namespace GenerateApplicationPreApprovalKey.App.ValueConverters
{
	/// <summary>
	/// An implementation of <see cref="IValueConverter"/>
	/// which converts a given <see cref="MFAuthType"/> to a string, and back again.
	/// </summary>
	// ReSharper disable once InconsistentNaming
	public class MFAuthTypeToStringValueConverter
		: IValueConverter
	{
		/// <summary>
		/// Converts an <see cref="MFAuthType"/> to a string.
		/// </summary>
		/// <param name="value">The <see cref="MFAuthType"/> to convert.</param>
		/// <param name="targetType">Not used.</param>
		/// <param name="parameter">Not used.</param>
		/// <param name="culture">Not used.</param>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((MFAuthType) value).ToString();
		}

		/// <summary>
		/// Converts a string to an <see cref="MFAuthType"/>.
		/// Excepts if the value cannot be converted.
		/// </summary>
		/// <param name="value">The string to convert.</param>
		/// <param name="targetType">Not used.</param>
		/// <param name="parameter">Not used.</param>
		/// <param name="culture">Not used.</param>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch (value?.ToString())
			{
				case "MFAuthTypeLoggedOnWindowsUser":
					return MFAuthType.MFAuthTypeLoggedOnWindowsUser;
				case "MFAuthTypeSpecificMFilesUser":
					return MFAuthType.MFAuthTypeSpecificMFilesUser;
				case "MFAuthTypeSpecificWindowsUser":
					return MFAuthType.MFAuthTypeSpecificWindowsUser;
				case "MFAuthTypeUnknown":
					return MFAuthType.MFAuthTypeUnknown;
			}
			throw new Exception();
		}
	}
}
