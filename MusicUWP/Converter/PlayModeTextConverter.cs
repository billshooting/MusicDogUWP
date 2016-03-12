using MusicUWP.ViewModels;
using System;
using Windows.UI.Xaml.Data;

namespace MusicUWP.Converter
{
    public class PlayModeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var mode = (PlayMode)value;
            switch (mode)
            {
                case PlayMode.ListCycle:
                    return "列表循环";
                case PlayMode.Order:
                    return "顺序播放";
                case PlayMode.Random:
                    return "随机播放";
                case PlayMode.SingleCycle:
                    return "单曲循环";
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
