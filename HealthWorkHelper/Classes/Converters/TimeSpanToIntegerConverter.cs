using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HealthWorkHelper.Classes.Converters
{
    public class TimeSpanToIntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is TimeSpan)) throw new InvalidCastException("Конвертер поддерживает преобразование только TimeSpan!");

            var span = (TimeSpan)value;
            return Math.Abs(span.TotalMinutes) < 1 ? span.TotalSeconds : span.TotalMinutes;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Обратное преобразование не поддерживается!");
        }
    }
}
