using System;
using Windows.UI.Xaml.Data;
using MusicUWP.ViewModels;
namespace MusicUWP.Converter
{
    public class PlayModeIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var mode = (PlayMode)value;
            switch(mode)
            {
                case PlayMode.ListCycle:
                    return "\xe14C";
                case PlayMode.Order:
                    return "\xe149";
                case PlayMode.Random:
                    return "\xe14B";
                case PlayMode.SingleCycle:
                    return "\xe14A";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
