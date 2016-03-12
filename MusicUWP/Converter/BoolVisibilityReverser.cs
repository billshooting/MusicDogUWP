using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MusicUWP.Converter
{
    public class BoolVisibilityReverser : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool state = (bool)value;
            if (state)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
