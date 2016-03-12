using System;
using Windows.UI.Xaml.Data;

namespace MusicUWP.Converter
{
    public class TimeIntStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int seconds = (int)value;
            return (seconds / 60).ToString().PadLeft(2, '0') + ":" + (seconds % 60).ToString().PadLeft(2, '0');
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
