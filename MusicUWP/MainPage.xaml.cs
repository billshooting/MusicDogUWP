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
using System.ComponentModel;

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
        public ObservableCollection<Song> FavoriteSongsList {
            get { return _favoriteSongsList; }
            set {
                _favoriteSongsList = value;
            } } 
        public ObservableCollection<Song> PlayingSongsList {
            get { return _playingSongsList; }
            set
            {
                _playingSongsList = value;
            }}
        public ObservableCollection<Song> DownloadedSongs {
            get { return _downloadedSongs; }
            set
            {
                _downloadedSongs = value;
            } }
        public ObservableCollection<Song> LocalSongsList { get; set; } = new ObservableCollection<Song>();
        public StorageFolder DownloadFolder { get; set; } = KnownFolders.MusicLibrary;
        private List<StorageFile> songFiles = new List<StorageFile>();

        private ObservableCollection<Song> _favoriteSongsList = new ObservableCollection<Song>();
        private ObservableCollection<Song> _playingSongsList = new ObservableCollection<Song>();
        private ObservableCollection<Song> _downloadedSongs = new ObservableCollection<Song>();
        private DispatcherTimer _timer; //用于进度条更新的计时器
        private int _listSelectedIndex = -1;


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
        }


        #region 功能处理方法
        //
        //自定义标题栏 方法
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

        } 
        //
        //使后退键出现在标题栏上
        private void EnableBackButtonOnTitleBar(EventHandler<BackRequestedEventArgs> onBackRequested)
        {
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested += onBackRequested;
        }
        //
        //将下载的歌曲的信息加到记录表中
        //songFiles : 记录下载歌曲的路径等信息
        //DownloadedSongs ： 以LocalSong形式记录的下载表
        private async Task AddDownloadedSong(StorageFile file)
        {
            songFiles.Add(file);
            await SongFileManager.PushFrontSongAsync(DownloadedSongs, file);
            //await SaveSettings();
        }
        //
        //将状态 设置序列化到本地
        private async Task SaveSettings()
        {
            StatusSerialization status = new StatusSerialization();
            status.playerStatus.currentSong = PlayerBarState.CurrentSong;
            status.playerStatus.palyMode = PlayerBarState.PlayMode;
            status.playerStatus.volume = VolumeSilder.Value;
            status.favoriteSongStatus.count = FavoriteSongsList.Count;
            status.favoriteSongStatus.favoriteSongsList = FavoriteSongsList;
            status.playingSongsStatus.count = PlayingSongsList.Count;
            status.playingSongsStatus.playingSongsList = PlayingSongsList;
            status.downloadedSongsStatus.count = DownloadedSongs.Count;
            status.downloadedSongsStatus.downloadedSongsList = DownloadedSongs;

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync("Settings.txt", CreationCollisionOption.ReplaceExisting);
            await SongFileManager.SerializeSettingsAsync(status, file);
        }

        public void SaveSettings(int i)
        {
            StatusSerialization status = new StatusSerialization();
            status.playerStatus.currentSong = PlayerBarState.CurrentSong;
            status.playerStatus.palyMode = PlayerBarState.PlayMode;
            status.playerStatus.volume = VolumeSilder.Value;
            status.favoriteSongStatus.count = FavoriteSongsList.Count;
            status.favoriteSongStatus.favoriteSongsList = FavoriteSongsList;
            status.playingSongsStatus.count = PlayingSongsList.Count;
            status.playingSongsStatus.playingSongsList = PlayingSongsList;
            status.downloadedSongsStatus.count = DownloadedSongs.Count;
            status.downloadedSongsStatus.downloadedSongsList = DownloadedSongs;

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            string path = folder.Path + "\\Settings.txt";

            SongFileManager.SerializeSettings(status, path);
        }
        //
        //加载配置
        private async Task LoadSettings()
        {

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            if (!File.Exists(folder.Path + "\\" + "Settings.txt"))
                return;
            else
            {
                StorageFile file = await folder.GetFileAsync("Settings.txt");
                StatusSerialization status = await SongFileManager.DeserializeSettingsAsync(file);
                if (status == null) return;
                if (status.favoriteSongStatus != null)
                    FavoriteSongsList = (ObservableCollection<Song>)status.favoriteSongStatus.favoriteSongsList;
                if (status.playingSongsStatus != null)
                {
                    foreach(var song in status.playingSongsStatus.playingSongsList)
                    {
                        PlayingSongsList.Add(song);
                    }
                }
                if (status.downloadedSongsStatus != null)
                    DownloadedSongs = (ObservableCollection<Song>)status.downloadedSongsStatus.downloadedSongsList;
                if (status.playerStatus != null)
                {
                    PlayerBarState.CurrentSong = status.playerStatus.currentSong;
                    PlayerBarState.PlayMode = status.playerStatus.palyMode;
                    VolumeSilder.Value = status.playerStatus.volume;
                }
            }
        }
        
        //
        //播放器打开本地歌曲的方法
        public async Task OpenLocalSongAsync(Song song)
        {
            // 清理上一首歌的状态
            PlayerBarState.CurrentSong.IsPlaying = false;
            PlayerBarState.IsPlaying = false;

            //加载歌曲
            PlayerBarState.CurrentSong = song;
            PlayerBarState.IsPlaying = true;
            //再打开歌曲
            StorageFile file = await StorageFile.GetFileFromPathAsync(song.SongFile);
            MusicPlayer.SetSource(
                await file.OpenAsync(FileAccessMode.Read),
                file.ContentType);
        }
        //
        //播放器打开网络歌曲的方法
        public void OpenWebSong(Song song)
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
        //
        //将歌曲的信息添加到播放列表中
        public  void AddToPlayingList(Song song)
        {
            if (!PlayingSongsList.Any(s => s.Title == song.Title && s.Artist == song.Artist && s.IsLoaclSong == song.IsLoaclSong))
            {
                PlayingSongsList.Add(song);
            }
            //await SaveSettings();
        }
        //
        // 从播放列表移除的方法
        public  void RemoveFromPlayingList(Song song)
        {
            PlayingSongsList.Remove(song);
            //await SaveSettings();
        }
        //
        //收藏歌曲
        public  void Favorite(Song song)
        {
            if (FavoriteSongsList.Any(s => s.Title == song.Title && s.Artist == song.Artist && s.IsLoaclSong == song.IsLoaclSong))
                return;
            song.IsFavorite = true;
            FavoriteSongsList.Add(song);
            //await SaveSettings();
        }
        //
        //收藏歌曲 如果是列表的话
        public  void Favorite(IEnumerable<Song> songs)
        {
            foreach (Song song in songs)
            {
                if (FavoriteSongsList.Any(s => s.Title == song.Title && s.Artist == song.Artist && s.IsLoaclSong == song.IsLoaclSong))
                    continue;
                else
                {
                    song.IsFavorite = true;
                    FavoriteSongsList.Add(song);
                }
            }
            //await SaveSettings();
        }
        //
        //取消收藏歌曲
        public  void UnFavorite(Song song)
        {
            song.IsFavorite = false;
            FavoriteSongsList.Remove(song);
            //await SaveSettings();
        }
        //
        //处理下载歌曲事务
        public async Task HandleDownload(string title, string url)
        {
            StorageFile file;
            if (string.IsNullOrEmpty(DownloadFolder.Path))
                file = await KnownFolders.MusicLibrary.CreateFileAsync(title + ".mp3");
            else
                file = await DownloadFolder.CreateFileAsync(title + ".mp3");
            try
            {
                await Task.Run(() => WebSongProxy.DownloadSong(url, file));
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
        }

        //语音 Todo
        public void OnVoiceCmdSearch(string tokens)
        {
            //TO DO
        }
        #endregion


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

            //加载配置文件
            await LoadSettings();
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
                            var song = (Song)nextSong;
                            await OpenLocalSongAsync(song);
                        }
                        else
                        {
                            var song = (Song)nextSong;
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
                                var song = nextSong;
                                await OpenLocalSongAsync(song);
                            }
                            else
                            {
                                var song = (Song)nextSong;
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
                            var song = (Song)nextSong;
                            await OpenLocalSongAsync(song);
                        }
                        else
                        {
                            var song = (Song)nextSong;
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
                            var song = (Song)nextSong;
                            await OpenLocalSongAsync(song);
                        }
                        else
                        {
                            var song = (Song)nextSong;
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

        private async void PlayingListPlayBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button item = (Button)(sender);
            Song song = (Song)item.DataContext;

            // 将选中的Song传给MainPage
            if (song.IsLoaclSong)
            {
                var nextSong = (Song)song;
                await OpenLocalSongAsync(nextSong);
            }
            else
            {
                //从网络中打开歌曲
                var nextSong = (Song)song;
                OpenWebSong(nextSong);
            }
        }


        private async void ListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Song song = (Song)((ListViewItemPresenter)e.OriginalSource).Content;
            e.Handled = true;

            // 将选中的Song传给MainPage
            if (song.IsLoaclSong)
            {
                var nextSong = (Song)song;
                await OpenLocalSongAsync(nextSong);
            }
            else
            {
                //从网络中打开歌曲
                var nextSong = (Song)song;
                OpenWebSong(nextSong);
            }
        }

        private void ListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListView listview = (ListView)sender;
            Button button1;
            //取消上一次点击的效果
            if (_listSelectedIndex >= 0)
            {
                button1 = (Button)((RelativePanel)(((Grid)((ListViewItem)listview.ItemsPanelRoot.Children[_listSelectedIndex]).ContentTemplateRoot).Children[1])).Children[1];//获取点击的条目的隐藏按钮 播放按钮
                button1.Visibility = Visibility.Collapsed;
                button1.IsEnabled = false;
            }
            base.OnTapped(e);
            e.Handled = true;
            button1 = (Button)((RelativePanel)(((Grid)((ListViewItem)listview.ItemsPanelRoot.Children[listview.SelectedIndex]).ContentTemplateRoot).Children[1])).Children[1];//获取点击的条目的隐藏按钮 播放按钮
            if (button1.Visibility == Visibility.Collapsed)
            {
                button1.Visibility = Visibility.Visible;
                button1.IsEnabled = true;
            }
            else
            {
                button1.Visibility = Visibility.Collapsed;
                button1.IsEnabled = false;
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
                await OpenLocalSongAsync(nextSong);
            }
            else
            {
                //从网络中打开歌曲
                var nextSong = (Song)song;
                OpenWebSong(nextSong);
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
                await OpenLocalSongAsync(nextSong);
            }
            else
            {
                //从网络中打开歌曲
                var nextSong = (Song)song;
                OpenWebSong(nextSong);
            }
        }

        private void RemoveFromPlayingListBtn_Click(object sender, RoutedEventArgs e)
        {
            Song song = PlayingListView.SelectedItem as Song;
            if (song == PlayerBarState.CurrentSong)
                return;
            else
            {
                RemoveFromPlayingList(song);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App app = (App)e.Parameter;
            app.mainPage = this;
            base.OnNavigatedTo(e);
        }
    }
}
