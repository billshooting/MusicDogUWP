using MusicUWP.Models;
using MusicUWP.ViewModels;
using MusicUWP.Converter;
using MusicUWP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Pickers;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MusicUWP.ViewPage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LocalMusicPage : Page
    {
        private ObservableCollection<StorageFolder> localFolders; //当前的扫描的文件夹
        private bool isFoldersChanged = false;  //指示选择的本地文件夹时候改变
        private ObservableCollection<Song> localSongs;   //当前加载的本地歌曲
        private MainPage mainPage;  //用于和主页通信的引用
        private int _listSelectedIndex = -1; //ListView前一个选中的索引
        public LocalMusicPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            localSongs = new ObservableCollection<Song>();
            localFolders = new ObservableCollection<StorageFolder>();
            localFolders.Add(KnownFolders.MusicLibrary);
        }


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LocalMusicLoadingRing.IsActive = true;

            if (localSongs.Count < 50)
            {
                localSongs.Clear();
                await SongFileManager.SetMusicListAsync(localSongs, localFolders.ToList(), mainPage.FavoriteSongsList.Where(s=>s.IsLoaclSong==true).ToList());
            }

            LocalMusicLoadingRing.IsActive = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var mainpage = (MainPage)e.Parameter;
            mainPage = mainpage;
            localSongs = mainpage.LocalSongsList;
            base.OnNavigatedTo(e);
        }

        private async void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderPicker fp = new FolderPicker();
            fp.FileTypeFilter.Add(".mp3");
            var storageFolder = await fp.PickSingleFolderAsync();
            localFolders.Add(storageFolder);
            isFoldersChanged = true;
        }

        private void DeleteFolder_Click(object sender, RoutedEventArgs e)
        {
            IList<object> selectedItem = SelectedFoldersList.SelectedItems;
            if (selectedItem.Count == 0)
                return;
            foreach(var item in selectedItem)
            {
                localFolders.Remove(item as StorageFolder);
            }
            isFoldersChanged = true;
        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            LocalMusicLoadingRing.IsActive = true;

            if (isFoldersChanged == false)
                return;
            localSongs.Clear();
            await SongFileManager.SetMusicListAsync(localSongs, localFolders.ToList(), mainPage.FavoriteSongsList);
            isFoldersChanged = false;

            LocalMusicLoadingRing.IsActive = false;
        }




        private void IsFavoriteBtn_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Song song = (Song)((TextBlock)sender).DataContext;
            e.Handled = true;
            if (song.IsFavorite)
                mainPage.UnFavorite(song);
            else
                mainPage.Favorite(song);
        }

        private void LocalMusicListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListView listview = (ListView)sender;
            Button button;
            //取消上一次点击的效果
            if(_listSelectedIndex >= 0)
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

        private async void LocalMusicListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var song = (Song)((ListViewItemPresenter)e.OriginalSource).Content;
            e.Handled = true;

            await mainPage.OpenLocalSongAsync(song);
        }

        private async void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var song = (Song)((Grid)sender).DataContext;
            e.Handled = true;
            await mainPage.OpenLocalSongAsync(song);
        }

        private async void TextBlock_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var song = (Song)((TextBlock)sender).DataContext;
            e.Handled = true;
            await mainPage.OpenLocalSongAsync(song);
        }


        private async void PlayMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            Song song = (Song)item.DataContext;
            await mainPage.OpenLocalSongAsync(song);
        }

        private void FavMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            Song song = (Song)item.DataContext;
            mainPage.Favorite(song);
        }

        private void AddPlayListMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)(sender);
            Song song = (Song)item.DataContext;
            mainPage.AddToPlayingList(song);
        }
    }
}
