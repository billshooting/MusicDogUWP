using System;
using Windows.UI.Xaml.Data;

namespace MusicUWP.Converter
{
    public class BoolIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool state = (bool)value;
            if (state)
            {
                return "\xe103";
            }
            else
            {
                return "\xe102";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
