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
	/// This will display a decimal in a money string format
	/// </summary>
	[ValueConversion(typeof(bool), typeof(bool))]
	public class MoneyConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string moneyDisplay = ((decimal)value).ToString("C");
			return moneyDisplay;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string moneyString = (string)value;
			decimal moneyValue;
			if (decimal.TryParse(moneyString, out moneyValue))
				return moneyValue;
			else
				throw new FormatException("Value is not a decimal");
		}
	}
}
