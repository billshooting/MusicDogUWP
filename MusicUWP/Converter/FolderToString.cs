using System;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace MusicUWP.Converter
{
    public class FolderToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string str = (string)value;
            if (!string.IsNullOrEmpty(str))
            {
                return str;
            }
            else
                return "音 乐 库";
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
