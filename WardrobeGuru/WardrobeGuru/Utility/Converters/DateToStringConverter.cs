using System;
using System.Globalization;
using Xamarin.Forms;

namespace WardrobeGuru.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = value is DateTime ? (DateTime)value : default;
            return date.ToString("MMMM yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}