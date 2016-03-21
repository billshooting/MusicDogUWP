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
using Windows.Storage;
using MusicUWP.ViewModels;
using System.Collections.ObjectModel;
using Windows.Storage.Pickers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MusicUWP.ViewPage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DownloadPage : Page, INotifyPropertyChanged
    {
        private StorageFolder _storageFolder = KnownFolders.MusicLibrary;
        private MainPage mainPage;
        private int _listSelectedIndex = -1;

        public StorageFolder StorageFolder
        {
            get { return _storageFolder; }
            set
            {
                _storageFolder = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<LocalSong> DownloadedSongs { get; set; }
        public DownloadPage()
        {
            this.InitializeComponent();
        }


        private async void SavePathBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderPicker fp = new FolderPicker();
            fp.FileTypeFilter.Add(".mp3");
            StorageFolder = await fp.PickSingleFolderAsync();
            mainPage.DownloadFolder = _storageFolder;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private  void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var mainpage = (MainPage)e.Parameter;
            mainPage = mainpage;
            DownloadedSongs = mainpage.DownloadedSongs;
            base.OnNavigatedTo(e);
        }


        private async void DownloadListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var song = (LocalSong)((ListViewItemPresenter)e.OriginalSource).Content;
            e.Handled = true;

            await mainPage.OpenLocalSongAsync(song);
        }

        private void DownloadListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListView listview = (ListView)sender;
            Button button;
            //取消上一次点击的效果
            if (_listSelectedIndex >= 0)
            {
                button = (Button)((RelativePanel)(((Grid)((ListViewItem)listview.ItemsPanelRoot.Children[_listSelectedIndex]).ContentTemplateRoot).Children[3])).Children[1];//获取点击的条目的隐藏按钮
                button.Visibility = Visibility.Collapsed;
                button.IsEnabled = false;
            }
            base.OnTapped(e);
            e.Handled = true;
            button = (Button)((RelativePanel)(((Grid)((ListViewItem)listview.ItemsPanelRoot.Children[listview.SelectedIndex]).ContentTemplateRoot).Children[3])).Children[1];//获取点击的条目的隐藏按钮
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
            var song = (LocalSong)((Grid)sender).DataContext;
            e.Handled = true;
            await mainPage.OpenLocalSongAsync(song);
        }

        private void TextBlock_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            LocalSong song = (LocalSong)((TextBlock)sender).DataContext;
            e.Handled = true;
            if (song.IsFavorite)
                mainPage.UnFavorite(song);
            else
                mainPage.Favorite(song);
        }

        private async void TextBlock_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var song = (LocalSong)((TextBlock)sender).DataContext;
            e.Handled = true;
            await mainPage.OpenLocalSongAsync(song);
        }

        private async void PlayMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            LocalSong song = (LocalSong)item.DataContext;
            await mainPage.OpenLocalSongAsync(song);
        }

        private void FavMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            LocalSong song = (LocalSong)item.DataContext;
            mainPage.Favorite(song);
        }

        private void AddPlayListMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            LocalSong song = (LocalSong)item.DataContext;
            mainPage.AddToPlayingList(song);
        }
    }
}
