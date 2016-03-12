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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace MusicUWP.ViewPage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LocalMusicPage : Page
    {
        private ObservableCollection<LocalSong> localSongs;
        private MainPage mainPage;
        public LocalMusicPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            localSongs = new ObservableCollection<LocalSong>();
        }

        private async void LocalMusicListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (LocalSong)e.ClickedItem;

            await mainPage.OpenLocalSongAsync(song);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LocalMusicLoadingRing.IsActive = true;

            if (localSongs.Count <100)
                await SongFileManager.SetMusicList(localSongs);

            LocalMusicLoadingRing.IsActive = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var mainpage = (MainPage)e.Parameter;
            mainPage = mainpage;
            localSongs = mainpage.LocalSongsList;
            base.OnNavigatedTo(e);
        }
    }
}
