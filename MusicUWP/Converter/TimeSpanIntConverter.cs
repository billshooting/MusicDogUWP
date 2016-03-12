using System;
using Windows.UI.Xaml.Data;

namespace MusicUWP.Converter
{
    public class TimeSpanIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            TimeSpan time = (TimeSpan)value;
            double totalSeconds = time.TotalSeconds;
            return totalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
