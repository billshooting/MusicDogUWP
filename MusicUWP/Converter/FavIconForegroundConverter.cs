using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MusicUWP.Converter
{
    public class FavIconForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isFav = (bool)value;
            if (isFav)
                return new SolidColorBrush(Windows.UI.Colors.IndianRed);
            else
                return new SolidColorBrush(Windows.UI.Colors.DarkGray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            SolidColorBrush brush = (SolidColorBrush)value;
            if (brush.Color == Windows.UI.Colors.IndianRed)
                return true;
            else if (brush.Color == Windows.UI.Colors.DarkGray)
                return false;
            else
                return false;
        }
    }
}
