using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace MusicUWP.Converter
{
    public class DurationPlayerBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var duration = (Windows.UI.Xaml.Duration)value;
            var time = duration.TimeSpan;
            return time.Minutes.ToString().PadLeft(2, '0') + ":" + time.Seconds.ToString().PadLeft(2, '0');
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
