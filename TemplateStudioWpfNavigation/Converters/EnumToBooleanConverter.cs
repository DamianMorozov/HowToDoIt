// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Globalization;
using System.Windows.Data;

namespace TemplateStudioWpfNavigation.Converters;

public class EnumToBooleanConverter : IValueConverter
{
	public Type EnumType { get; set; }

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (parameter is string enumString)
		{
			if (Enum.IsDefined(EnumType, value))
			{
				var enumValue = Enum.Parse(EnumType, enumString);

				return enumValue.Equals(value);
			}
		}

		return false;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (parameter is string enumString)
		{
			return Enum.Parse(EnumType, enumString);
		}

		return null;
	}
}