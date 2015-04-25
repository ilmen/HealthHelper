using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HealthWorkHelper.Classes.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        private static TimeSpanToStringConverter Instance = new TimeSpanToStringConverter();

        public static string ConvertToString(TimeSpan timeSpan)
        {
            return (string)TimeSpanToStringConverter.Instance.Convert(
                timeSpan,
                typeof(string), null, System.Globalization.CultureInfo.CurrentCulture);
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is TimeSpan)) throw new InvalidCastException("Конвертер поддерживает преобразование только TimeSpan!");

            var span = (TimeSpan)value;
            if (Math.Abs(span.TotalMinutes) < 1)
            {
                var count = (int)span.TotalSeconds;
                return count + " " + SmartEnding.GetNounEnding(count, "секунда", "секунды", "секунд");
            }
            else
            {
                var count = (int)span.TotalMinutes;
                return count + " " + SmartEnding.GetNounEnding(count, "минута", "минуты", "минут");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Обратное преобразование не поддерживается!");
        }
    }
}
