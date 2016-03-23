using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace MusicUWP.Converter
{
    public class UrlToBitMapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string url = value as string;
            if (url == null)
                url = "ms-appx:///Assets/Default/Default.jpg";
            BitmapImage albumCover = new BitmapImage();
            Uri uri = new Uri(url);
            albumCover.UriSource = uri;
            return albumCover;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
