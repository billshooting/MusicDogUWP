using System;
using Windows.UI.Xaml.Data;

namespace MusicUWP.Converter
{
    public class FavoriteIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isFavorite = (bool)value;
            if (isFavorite)
                return "\xe0A5";
            else
                return "\xe006";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
