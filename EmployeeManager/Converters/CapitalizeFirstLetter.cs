using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EmployeeManager.Converters
{
	/// <summary>
	/// This will capitalize the first letter of a string.
	/// </summary>
	[ValueConversion(typeof(bool), typeof(bool))]
	public class CapitalizeFirstLetter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string)
			{
				var castValue = (string)value;
				if (castValue.Count() == 0)
					return castValue;
				else
					return char.ToUpper(castValue[0]) + castValue.Substring(1);
			}
			else
			{
				return value;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
