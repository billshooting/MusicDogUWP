using System;
using Windows.UI.Xaml.Data;

namespace MusicUWP.Converter
{
    public class TimeSpanStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var time = (TimeSpan)value;
            return DurationToString(time);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private string DurationToString(TimeSpan duration)
        {
            return string.Format("{0}:{1}", duration.Minutes.ToString().PadLeft(2, '0'), duration.Seconds.ToString().PadLeft(2, '0'));
        }
    }
}
