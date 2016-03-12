using System;
using Windows.UI.Xaml.Data;

namespace MusicUWP.Converter
{
    public class DoubleStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double val = (double)value;
            int vl = (int)val;
            return string.Format("{0}:{1}", (vl / 60).ToString().PadLeft(2, '0'), (vl % 60).ToString().PadLeft(2, '0'));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
