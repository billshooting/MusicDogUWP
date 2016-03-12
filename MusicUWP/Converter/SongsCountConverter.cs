using System;
using Windows.UI.Xaml.Data;

namespace MusicUWP.Converter
{
    public class SongsCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int count = (int)value;
            return "共" + count.ToString() + "首";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
