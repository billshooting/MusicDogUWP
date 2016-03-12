using System;
using Windows.UI.Xaml.Data;

namespace MusicUWP.Converter
{
    public class VolumeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double sliderValue = (double)value;
            double volume = sliderValue / 100;
            return volume;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double volume = (double)value;
            return volume * 100;
        }
    }

}
