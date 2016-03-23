using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MusicUWP.Models;
using System.Collections.ObjectModel;
using MusicUWP.ViewModels;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MusicUWP.ViewPage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FavoriteListPage : Page
    {
        public ObservableCollection<Song> FavoriteSongs { get; set; } = new ObservableCollection<Song>();

        private MainPage mainPage;
        private int _listSelectedIndex = -1;

        public FavoriteListPage()
        {
            this.InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SongsCountText.Text = "共" + FavoriteSongs.Count.ToString() + "首";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainPage = (MainPage)e.Parameter;
            FavoriteSongs = mainPage.FavoriteSongsList;
            base.OnNavigatedTo(e);
        }


        private async void FavMusicListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Song song = (Song)((ListViewItemPresenter)e.OriginalSource).Content;
            e.Handled = true;

            // 将选中的Song传给MainPage
            if (song.IsLoaclSong)
            {
                var nextSong = (Song)song;
                await mainPage.OpenLocalSongAsync(nextSong);
            }
            else
            {
                //从网络中打开歌曲
                var nextSong = (Song)song;
                mainPage.OpenWebSong(nextSong);
            }
        }

        private void FavMusicListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListView listview = (ListView)sender;
            Button button;
            //取消上一次点击的效果
            if (_listSelectedIndex >= 0)
            {
                button = (Button)((RelativePanel)(((Grid)((ListViewItem)listview.ItemsPanelRoot.Children[_listSelectedIndex]).ContentTemplateRoot).Children[2])).Children[1];//获取点击的条目的隐藏按钮
                button.Visibility = Visibility.Collapsed;
                button.IsEnabled = false;
            }
            base.OnTapped(e);
            e.Handled = true;
            button = (Button)((RelativePanel)(((Grid)((ListViewItem)listview.ItemsPanelRoot.Children[listview.SelectedIndex]).ContentTemplateRoot).Children[2])).Children[1];//获取点击的条目的隐藏按钮
            if (button.Visibility == Visibility.Collapsed)
            {
                button.Visibility = Visibility.Visible;
                button.IsEnabled = true;
            }
            else
            {
                button.Visibility = Visibility.Collapsed;
                button.IsEnabled = false;
            }
            _listSelectedIndex = listview.SelectedIndex;
        }

        private async void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Song song = (Song)((Grid)sender).DataContext;
            e.Handled = true;

            // 将选中的Song传给MainPage
            if (song.IsLoaclSong)
            {
                var nextSong = (Song)song;
                await mainPage.OpenLocalSongAsync(nextSong);
            }
            else
            {
                //从网络中打开歌曲
                var nextSong = (Song)song;
                mainPage.OpenWebSong(nextSong);
            }
        }

        private async void TextBlock_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Song song = (Song)((TextBlock)sender).DataContext;
            e.Handled = true;

            // 将选中的Song传给MainPage
            if (song.IsLoaclSong)
            {
                var nextSong = (Song)song;
                await mainPage.OpenLocalSongAsync(nextSong);
            }
            else
            {
                //从网络中打开歌曲
                var nextSong = (Song)song;
                mainPage.OpenWebSong(nextSong);
            }
        }

        private async void PlayMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            Song song = (Song)item.DataContext;

            // 将选中的Song传给MainPage
            if (song.IsLoaclSong)
            {
                var nextSong = (Song)song;
                await mainPage.OpenLocalSongAsync(nextSong);
            }
            else
            {
                //从网络中打开歌曲
                var nextSong = (Song)song;
                mainPage.OpenWebSong(nextSong);
            }
        }

        private void UnFavMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            Song song = (Song)item.DataContext;
            mainPage.UnFavorite(song);
        }

        private void AddPlayListMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            Song song = (Song)item.DataContext;
            mainPage.AddToPlayingList(song);
        }

        private async void Download_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            Song song = (Song)item.DataContext;
            if (song.IsLoaclSong)
                return; //最好提示它是本地歌曲
            else
            {
                Song websong = (Song)song;
                string url = websong.DownUrl;
                string title = websong.Title;
                await mainPage.HandleDownload(title, url);
            }
        }
    }
}
