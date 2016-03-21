using MusicUWP.Models;
using MusicUWP.ViewPage;
using MusicUWP.ViewModels;
using MusicUWP.Converter;
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
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Core;
using Windows.Storage;
using Windows.ApplicationModel.VoiceCommands;
using System.Net.Http;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace MusicUWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // WebSong 继承于Song
        public PlayerBarViewModel PlayerBarState { get; set; } = new PlayerBarViewModel() { IsPlaying = false};
        public ObservableCollection<Song> FavoriteSongsList { get; set; } = new ObservableCollection<Song>();
        public ObservableCollection<Song> PlayingSongsList { get; set; } = new ObservableCollection<Song>();
        public ObservableCollection<LocalSong> LocalSongsList { get; set; } = new ObservableCollection<LocalSong>();
        public ObservableCollection<LocalSong> DownloadedSongs { get; set; } = new ObservableCollection<LocalSong>();
        public StorageFolder DownloadFolder { get; set; } = KnownFolders.MusicLibrary;
        private List<StorageFile> songFiles = new List<StorageFile>();
         
        private DispatcherTimer _timer; //用于进度条更新的计时器
        public MainPage()
        {
            this.InitializeComponent();
            //初始化标题栏
            CustomizeTitleBar();
            EnableBackButtonOnTitleBar((sender, e) =>
            {
                if (ContentFrame.CanGoBack)
                {
                    ContentFrame.GoBack();
                    LeftDockList.SelectionChanged -= LeftDockList_SelectionChanged;
                    Type tempType = ContentFrame.CurrentSourcePageType;
                    if (tempType == typeof(BandCoverPage))
                        BandList.IsSelected = true;
                    else if (tempType == typeof(BandListPage))
                        BandList.IsSelected = true;
                    else if (tempType == typeof(DownloadPage))
                        Download.IsSelected = true;
                    else if (tempType == typeof(LocalMusicPage))
                        LocalMusic.IsSelected = true;
                    else if (tempType == typeof(FavoriteListPage))
                        FavoriteList.IsSelected = true;
                    else if (tempType == typeof(SearchSongPage))
                        SearchMusic.IsSelected = true;

                    LeftDockList.SelectionChanged += LeftDockList_SelectionChanged;
                }
            });
            _timer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            _timer.Tick += (s, e) =>
            {
                var position = MusicPlayer.Position;
                PlayerBarState.PlayedPosition = position;
            };
            //DataContext = PlayerBarState; 

        }

        #region 功能处理方法
        private void CustomizeTitleBar()
        {
            var view = ApplicationView.GetForCurrentView();
            view.TitleBar.ForegroundColor = Colors.White;
            view.TitleBar.BackgroundColor = Colors.IndianRed;
            view.TitleBar.ButtonBackgroundColor = Colors.IndianRed;
            view.TitleBar.ButtonForegroundColor = Colors.White;
            view.TitleBar.ButtonHoverBackgroundColor = Colors.Red;
            view.TitleBar.ButtonHoverForegroundColor = Colors.White;
            view.TitleBar.ButtonPressedForegroundColor = Colors.Black;

        } //自定义标题栏
        private void EnableBackButtonOnTitleBar(EventHandler<BackRequestedEventArgs> onBackRequested)
        {
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested += onBackRequested;
        }
        private async Task AddDownloadedSong(StorageFile file)
        {
            songFiles.Add(file);
            await SongFileManager.PushFrontSong(DownloadedSongs, file);
        }
        public async Task OpenLocalSongAsync(LocalSong song)
        {
            // 清理上一首歌的状态
            PlayerBarState.CurrentSong.IsPlaying = false;
            PlayerBarState.IsPlaying = false;

            //加载歌曲
            PlayerBarState.CurrentSong = song;
            PlayerBarState.IsPlaying = true;
            //再打开歌曲
            MusicPlayer.SetSource(
                await song.SongFile.OpenAsync(Windows.Storage.FileAccessMode.Read),
                song.SongFile.ContentType);
        }
        public void OpenWebSong(WebSong song)
        {
            // 清理上一首歌的状态
            PlayerBarState.CurrentSong.IsPlaying = false;
            PlayerBarState.IsPlaying = false;

            //加载歌曲
            PlayerBarState.CurrentSong = song;
            PlayerBarState.IsPlaying = true;
            //再打开歌曲
            MusicPlayer.Source = new Uri(song.DownUrl,UriKind.Absolute);
            MusicPlayer.Play();
            //MusicPlayer.SetSource(
            //    await song.SongFile.OpenAsync(Windows.Storage.FileAccessMode.Read),
            //    song.SongFile.ContentType);
        }
        public void AddToPlayingList(Song song)
        {
            if (!PlayingSongsList.Any(s => s.Title == song.Title && s.Artist == song.Artist && s.IsLoaclSong == song.IsLoaclSong))
            {
                PlayingSongsList.Add(song);
            }
        }

        public void Favorite(Song song)
        {
            if (FavoriteSongsList.Any(s => s.Title == song.Title && s.Artist == song.Artist && s.IsLoaclSong == song.IsLoaclSong))
                return;
            song.IsFavorite = true;
            FavoriteSongsList.Add(song);
        }
        public void Favorite(IEnumerable<Song>songs)
        {
            foreach(Song song in songs)
            {
                if (FavoriteSongsList.Any(s => s.Title == song.Title && s.Artist == song.Artist && s.IsLoaclSong == song.IsLoaclSong))
                    continue;
                else
                {
                    song.IsFavorite = true;
                    FavoriteSongsList.Add(song);
                }
            }
        }
        public void UnFavorite(Song song)
        {
            song.IsFavorite = false;
            FavoriteSongsList.Remove(song);
        }

        public async Task HandleDownload(string title, string url)
        {
            string filename;
            if (string.IsNullOrEmpty(DownloadFolder.Path))
                filename = "C:\\Users\\BILL\\Music\\" + title + ".mp3";
            else
                filename = DownloadFolder.Path + "\\"+ title + ".mp3";
            try
            {
                await Task.Run(() => WebSongProxy.DownloadSong(url, filename));
                StorageFile file = await StorageFile.GetFileFromPathAsync(filename);
                await AddDownloadedSong(file);
            }
            catch (HttpRequestException ex)
            {
                FileStream fs = new FileStream(".//log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string content = ex.Message;
                    sw.Write(content + DateTime.Now.ToString());
                }
                throw;
            }
            //catch (Exception ex)
            //{
            //    FileStream fs = new FileStream(".//log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //    using (StreamWriter sw = new StreamWriter(fs))
            //    {
            //        string content = ex.Message;
            //        sw.Write(content + DateTime.Now.ToString());
            //    }
            //}
        }
        //语音 Todo
        public void OnVoiceCmdSearch(string tokens)
        {
            //TO DO
        }
        #endregion

        //private void BackButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ContentFrame.CanGoBack)
        //    {
        //        ContentFrame.GoBack();
        //        LeftDockList.SelectionChanged -= LeftDockList_SelectionChanged;
        //        Type tempType = ContentFrame.CurrentSourcePageType;
        //        if (tempType == typeof(BandCoverPage))
        //            BandList.IsSelected = true;
        //        else if (tempType == typeof(BandListPage))
        //            BandList.IsSelected = true;
        //        else if (tempType == typeof(DownloadPage))
        //            Download.IsSelected = true;
        //        else if (tempType == typeof(LocalMusicPage))
        //            LocalMusic.IsSelected = true;
        //        else if (tempType == typeof(FavoriteListPage))
        //            FavoriteList.IsSelected = true;
        //        else if (tempType == typeof(SearchSongPage))
        //            SearchMusic.IsSelected = true;

        //        LeftDockList.SelectionChanged += LeftDockList_SelectionChanged;
        //    }
        //}


        private void LeftDockList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BandList.IsSelected)
                ContentFrame.Navigate(typeof(BandCoverPage), this);
            else if (Download.IsSelected)
                ContentFrame.Navigate(typeof(DownloadPage), this);
            else if (LocalMusic.IsSelected)
                ContentFrame.Navigate(typeof(LocalMusicPage), this);
            else if (FavoriteList.IsSelected)
                ContentFrame.Navigate(typeof(FavoriteListPage), this);
            else if (SearchMusic.IsSelected)
                ContentFrame.Navigate(typeof(SearchSongPage), this);
            else if (HamburgerButton.IsSelected)
            {
                MainPageSplitView.IsPaneOpen = !MainPageSplitView.IsPaneOpen;
                HamburgerButton.IsSelected = false;
            }
            else
                return;
        }

        private void PlayStopButton_Click(object sender, RoutedEventArgs e)
        {
            switch(MusicPlayer.CurrentState)
            {
                case MediaElementState.Playing:
                    {
                        MusicPlayer.Pause();
                        PlayerBarState.IsPlaying = false;
                        break;
                    }
                case MediaElementState.Paused:
                    {
                        MusicPlayer.Play();
                        PlayerBarState.IsPlaying = true;
                        break;
                    }
                default:
                    break;
            }
        }

        private async void MainPageName_Loaded(object sender, RoutedEventArgs e)
        {
            // 播放条初始化
            MusicPlayer.Volume = 0.5;
            VolumeSilder.Value = 50;
            _timer.Start();

            PlayerBarState.CurrentSong = new Song() { IsFavorite = false , Duration=new TimeSpan(0,1,40)};

            //加载语音模块
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Cortana.xml"));
            await VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(storageFile);
        }

        private void PlayBarSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            var slider = (Slider)sender;
            if(Math.Abs(slider.Value - PlayerBarState.PlayedPosition.TotalSeconds)>2)
            {
                int seconds = (int)slider.Value;
                TimeSpan timespan = new TimeSpan(0, seconds / 60, seconds % 60);
                PlayerBarState.PlayedPosition = timespan;
                MusicPlayer.Position = timespan;
            }
        }

        private void FavoriteSongButton_Click(object sender, RoutedEventArgs e)
        {
            if (!PlayerBarState.CurrentSong.IsFavorite)
            {
                PlayerBarState.CurrentSong.IsFavorite = true;
                Favorite(PlayerBarState.CurrentSong);
            }
            else
            {
                PlayerBarState.CurrentSong.IsFavorite = false;
                UnFavorite(PlayerBarState.CurrentSong);
            }
        }

        private void ModeSelcetButton_Click(object sender, RoutedEventArgs e)
        {
            switch (PlayerBarState.PlayMode)
            {
                case PlayMode.ListCycle:
                    {
                        PlayerBarState.PlayMode = PlayMode.Order;
                        return;
                    }
                case PlayMode.Order:
                    {
                        PlayerBarState.PlayMode = PlayMode.Random;
                        return;
                    }
                case PlayMode.Random:
                    {
                        PlayerBarState.PlayMode = PlayMode.SingleCycle;
                        return;
                    }
                case PlayMode.SingleCycle:
                    {
                        PlayerBarState.PlayMode = PlayMode.ListCycle;
                        return;
                    }
                default:
                    return;
            }
        }

        private void MusicPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            PlayerBarState.CurrentSong.IsPlaying = true;
            MusicPlayer.Position = new TimeSpan(0, 0, 0);
            //将播放器的封面设成歌曲的图片， 
            // ！！！不应该这么做 ， UI的事应该在xaml中完成
            //PlayerBarCover.Source = PlayerBarState.CurrentSong.AlbumCover;

            AddToPlayingList(PlayerBarState.CurrentSong);
        }

        private async void MusicPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            PlayerBarState.CurrentSong.IsPlaying = false;

            switch(PlayerBarState.PlayMode)
            {
                case PlayMode.ListCycle:
                    {
                        // 获取列表索引
                        var index = PlayingSongsList.IndexOf(PlayerBarState.CurrentSong);
                        if (index < PlayingSongsList.Count - 1)
                            index++;
                        else
                            index = 0;
                        Song nextSong = PlayingSongsList[index];


                        //打开歌曲
                        if (nextSong.IsLoaclSong)
                        {
                            var song = (LocalSong)nextSong;
                            await OpenLocalSongAsync(song);
                        }
                        else
                        {
                            var song = (WebSong)nextSong;
                            OpenWebSong(song);
                        }
                        return;
                    }
                case PlayMode.Order:
                    {
                        // 获取列表索引
                        var index = PlayingSongsList.IndexOf(PlayerBarState.CurrentSong);
                        if (index < PlayingSongsList.Count - 1)
                        {
                            index++;
                            Song nextSong = PlayingSongsList[index];
                            //打开歌曲
                            if (nextSong.IsLoaclSong)
                            {
                                var song = (LocalSong)nextSong;
                                await OpenLocalSongAsync(song);
                            }
                            else
                            {
                                var song = (WebSong)nextSong;
                                OpenWebSong(song);
                            }
                            return;
                        }
                        else
                        {
                            MusicPlayer.Stop();
                            // 改变PlayerBarState的状态
                            PlayerBarState.CurrentSong = null;
                            PlayerBarState.IsPlaying = false;
                            return;
                        }

                    }
                case PlayMode.Random:
                    {
                        //获取随机索引
                        Random random = new Random(DateTime.Now.GetHashCode());
                        int index = random.Next(PlayingSongsList.Count);

                        Song nextSong = PlayingSongsList[index];

                        //打开歌曲
                        if (nextSong.IsLoaclSong)
                        {
                            var song = (LocalSong)nextSong;
                            await OpenLocalSongAsync(song);
                        }
                        else
                        {
                            var song = (WebSong)nextSong;
                            OpenWebSong(song);
                        }
                        return;
                    }
                case PlayMode.SingleCycle:
                    {
                        Song nextSong = PlayerBarState.CurrentSong;
                        //打开歌曲
                        if (nextSong.IsLoaclSong)
                        {
                            var song = (LocalSong)nextSong;
                            await OpenLocalSongAsync(song);
                        }
                        else
                        {
                            var song = (WebSong)nextSong;
                            OpenWebSong(song);
                        }
                        return;
                    }
            }
        }

        private void FavorAllBtn_Click(object sender, RoutedEventArgs e)
        {
            Favorite(PlayingSongsList);
        }

    }
}
