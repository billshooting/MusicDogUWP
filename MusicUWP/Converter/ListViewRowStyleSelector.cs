using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MusicUWP.Converter
{
    public class ListViewItemStyleSelector : StyleSelector
    {
        public new Style SelectStyle(object item,
            DependencyObject container)
        {
            Style st = new Style();
            st.TargetType = typeof(ListViewItem);
            Setter backGroundSetter = new Setter();
            backGroundSetter.Property = ListViewItem.BackgroundProperty;
            ListView listView =
                ItemsControl.ItemsControlFromItemContainer(container)
                  as ListView;
            int index =
                listView.ItemContainerGenerator.IndexFromContainer(container);
            if (index % 2 == 0)
            {
                backGroundSetter.Value = new SolidColorBrush(Colors.AliceBlue);
            }
            else
            {
                backGroundSetter.Value = new SolidColorBrush(Colors.White);
            }
            st.Setters.Add(backGroundSetter);
            return st;
        }
    }
}
