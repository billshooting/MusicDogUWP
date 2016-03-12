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

        private  async void FavMusicListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Song song = (Song)e.ClickedItem;

            // 将选中的Song传给MainPage
            if (song.IsLoaclSong)
            {
                var nextSong = (LocalSong)song;
                await mainPage.OpenLocalSongAsync(nextSong);
            }
            else
            {
                //从网络中打开歌曲
            }
        }
    }
}
