using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HealthWorkHelper.Classes.Converters
{
    public class TimeToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var time = value as DateTime?;
            if (time == null) throw new ArgumentException("Неверный тип аргумента!");

            return DateTime.Now - (DateTime)time;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Обратное преобразование не поддерживается!");
        }
    }
}
