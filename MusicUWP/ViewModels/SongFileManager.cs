using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicUWP.Models;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;
using System.Text.RegularExpressions;

namespace MusicUWP.ViewModels
{
    public class SongFileManager
    {
        private const string uriMatchKey = @"\w{1,}\.jpg";
        /// <summary>
        /// 该方法用于扫描某个文件夹下的歌曲文件
        /// </summary>
        /// <param name="songs">歌曲集合</param>
        /// <param name="parent">要扫描的文件夹</param>
        private static async Task GetLocalSongsAysnc(List<StorageFile> songFiles, StorageFolder parent)
        {
            foreach(var item in await parent.GetFilesAsync())
            {
                if (item.FileType == ".mp3")
                    songFiles.Add(item);
            }
            foreach(var folder in await parent.GetFoldersAsync())
            {
                await GetLocalSongsAysnc(songFiles, folder);
            }
        }

        public static async Task PushFrontSong(ObservableCollection<LocalSong> localSongs, StorageFile songFiles)
        {
            // 1. 获取文件信息
            MusicProperties musicProperty = await songFiles.Properties.GetMusicPropertiesAsync();
            if (string.IsNullOrEmpty(musicProperty.Title))
                return;
            else
            {
                StorageItemThumbnail currentThumb = await songFiles.GetThumbnailAsync(ThumbnailMode.MusicView, 60, ThumbnailOptions.UseCurrentScale);

                // 2.将文件信息转换为数据模型
                LocalSong song = new LocalSong();

                var albumCover = new BitmapImage();
                if (currentThumb != null)
                {
                    await albumCover.SetSourceAsync(currentThumb);
                }
                else
                {
                    albumCover = new BitmapImage(new Uri("/Assets/Default/Default.jpg"));
                }

                song.Id = localSongs.Count + 1;
                song.SongFile = songFiles;
                song.Title = musicProperty.Title;
                if (!string.IsNullOrEmpty(musicProperty.Artist))
                    song.Artist = musicProperty.Artist;
                else
                    song.Artist = "未知歌手";
                if (!string.IsNullOrEmpty(musicProperty.Album))
                    song.Album = musicProperty.Album;
                else
                    song.Album = "未知唱片";
                song.Duration = musicProperty.Duration;
                song.AlbumCover = albumCover;

                song.IsPlaying = false;
                song.IsLoaclSong = true;
                //3. 添加至UI集合中
                localSongs.Add(song);
            }
        }

        /// <summary>
        /// 该方法用于将songFiles里的文件转变为songs里的ViewModel
        /// </summary>
        /// <param name="localSongs">可显示的歌曲</param>
        /// <param name="songFiles">歌曲文件列表</param>
        /// <returns></returns>
        private static async Task PopulateSongList(ObservableCollection<LocalSong> localSongs, List<StorageFile> songFiles, IList<Song> FavSongsList = null)
        {
            int Id = 1;
            
            foreach(var file in songFiles)
            {
                // 1. 获取文件信息
                MusicProperties musicProperty = await file.Properties.GetMusicPropertiesAsync();
                if (string.IsNullOrEmpty(musicProperty.Title))
                    continue;
                //验证是否已经在内存中
                Song localsong = FavSongsList.FirstOrDefault(s => s.Title == musicProperty.Title && s.Artist == musicProperty.Artist && s.IsLoaclSong == true);
                if (localsong != null)
                {
                    localsong.Id = Id;
                    localSongs.Add((LocalSong)localsong);
                    Id++;
                }
                else
                {
                    StorageItemThumbnail currentThumb = await file.GetThumbnailAsync(ThumbnailMode.MusicView, 60, ThumbnailOptions.UseCurrentScale);

                    // 2.将文件信息转换为数据模型
                    LocalSong song = new LocalSong();

                    var albumCover = new BitmapImage();
                    if (currentThumb != null)
                    {
                        await albumCover.SetSourceAsync(currentThumb);
                    }
                    else
                    {
                        albumCover = new BitmapImage(new Uri("/Assets/Default/Default.jpg"));
                    }

                    song.Id = Id;
                    song.SongFile = file;
                    song.Title = musicProperty.Title;
                    if (!string.IsNullOrEmpty(musicProperty.Artist))
                        song.Artist = musicProperty.Artist;
                    else
                        song.Artist = "未知歌手";
                    if (!string.IsNullOrEmpty(musicProperty.Album))
                        song.Album = musicProperty.Album;
                    else
                        song.Album = "未知唱片";
                    song.Duration = musicProperty.Duration;
                    song.AlbumCover = albumCover;

                    song.IsPlaying = false;
                    song.IsLoaclSong = true;
                    //3. 添加至UI集合中
                    localSongs.Add(song);
                    Id++;
                }
            }
        }


        /// <summary>
        /// 将指定文件夹下的歌曲全部添加到传入参数songs中
        /// </summary>
        /// <param name="localSongs">保存歌曲的列表</param>
        /// <param name="musicFolder">要扫描的文件夹</param>
        /// <returns></returns>
        public static async Task SetMusicList(ObservableCollection<LocalSong> localSongs,StorageFolder musicFolder = null)
        {
            List<StorageFile> songfiles = new List<StorageFile>();
            if (musicFolder == null)
            {
                musicFolder = KnownFolders.MusicLibrary;
            }

            await GetLocalSongsAysnc(songfiles, musicFolder);
            await PopulateSongList(localSongs, songfiles);

        }

        public static async Task SetMusicList(ObservableCollection<LocalSong> localSongs, List<StorageFolder> musicFolders, IList<Song> FavSongsList)
        {
            List<StorageFile> songfiles = new List<StorageFile>();
            if (musicFolders == null || musicFolders.Count == 0)
            {
                return;
            }
            foreach (var folder in musicFolders)
            {
                await GetLocalSongsAysnc(songfiles, folder);
            }
            await PopulateSongList(localSongs, songfiles, FavSongsList);
        }

        public static void SetWebSongsByBandList(ObservableCollection<WebSong> displaySongs, List<WebRequestSong>webSongs)
        {
            int id = 1;
            foreach(WebRequestSong websong in webSongs)
            {
                if (string.IsNullOrEmpty(websong.songname))
                    continue;
                BitmapImage albumCover = new BitmapImage();
                Uri uri;
                if (!string.IsNullOrEmpty(websong.albumpic_small) && Regex.IsMatch(websong.albumpic_small, uriMatchKey))
                    uri = new Uri(websong.albumpic_small, UriKind.Absolute);
                else
                    uri = new Uri("ms-appx:///Assets/Default/Default.jpg", UriKind.Absolute);
                albumCover.UriSource = uri;

                WebSong displaySong = new WebSong()
                {
                    IsLoaclSong = false,
                    IsFavorite = false,
                    IsPlaying = false,
                    Id = id,
                    Title = websong.songname,
                    Artist = websong.singername,
                    Duration = new TimeSpan(0, websong.seconds / 60, websong.seconds % 60),
                    AlbumCover = albumCover,
                    Songid = websong.songid,
                    Singerid = websong.singerid,
                    Albumid = websong.albumid,
                    Albummid = websong.albummid,
                    Albumpic_big = websong.albumpic_big,
                    Albumpic_small = websong.albumpic_small,
                    DownUrl = websong.downUrl,
                    Url = websong.url
                };
                id++;
                displaySongs.Add(displaySong);
            }
        }

        public static void SetWebSongsByBandList(ObservableCollection<WebSong> displaySongs, List<WebRequestSong> webSongs, IList<Song> FavSongsList)
        {
            int id = 1;
            foreach (WebRequestSong websong in webSongs)
            {
                Song song = FavSongsList.FirstOrDefault(s => s.Title == websong.songname && s.Artist == websong.singername && s.IsLoaclSong == false);
                if (song != null)
                {
                    song.Id = id;
                    displaySongs.Add((WebSong)song);
                    id++;
                }
                else
                {
                    if (string.IsNullOrEmpty(websong.songname))
                        continue;
                    BitmapImage albumCover = new BitmapImage();
                    Uri uri;
                    if (!string.IsNullOrEmpty(websong.albumpic_small) && Regex.IsMatch(websong.albumpic_small, uriMatchKey))
                        uri = new Uri(websong.albumpic_small, UriKind.Absolute);
                    else
                        uri = new Uri("ms-appx:///Assets/Default/Default.jpg", UriKind.Absolute);
                    albumCover.UriSource = uri;

                    WebSong displaySong = new WebSong()
                    {
                        IsLoaclSong = false,
                        IsFavorite = false,
                        IsPlaying = false,
                        Id = id,
                        Title = websong.songname,
                        Artist = websong.singername,
                        Duration = new TimeSpan(0, websong.seconds / 60, websong.seconds % 60),
                        AlbumCover = albumCover,
                        Songid = websong.songid,
                        Singerid = websong.singerid,
                        Albumid = websong.albumid,
                        Albummid = websong.albummid,
                        Albumpic_big = websong.albumpic_big,
                        Albumpic_small = websong.albumpic_small,
                        DownUrl = websong.downUrl,
                        Url = websong.url
                    };
                    id++;
                    displaySongs.Add(displaySong);
                }
            }
        }

        public static void SetWebSongByNameList(ObservableCollection<WebSong> displaySongs, List<Contentlist> webSongs)
        {
            int id = 1;
            foreach (var websong in webSongs)
            {
                if (string.IsNullOrEmpty(websong.songname))
                    continue;
                BitmapImage albumCover = new BitmapImage();
                Uri uri;
                if ( !string.IsNullOrEmpty(websong.albumpic_small)&&Regex.IsMatch(websong.albumpic_small, uriMatchKey))
                {
                    uri = new Uri(websong.albumpic_small, UriKind.Absolute);
                    albumCover.UriSource = uri;
                }
                else
                {
                    uri = new Uri("ms-appx:///Assets/Default/Default.jpg",UriKind.Absolute);
                    albumCover.UriSource = uri;
                }
                

                WebSong displaySong = new WebSong()
                {
                    IsLoaclSong = false,
                    IsFavorite = false,
                    IsPlaying = false,
                    Id = id,
                    Title = websong.songname,
                    Artist = websong.singername,
                    Album = websong.albumname,
                    AlbumCover = albumCover,
                    //Duration = new TimeSpan(0, websong.seconds / 60, websong.seconds % 60),
                    Songid = websong.songid,
                    Singerid = websong.singerid,
                    Albumid = websong.albumid,
                    Albummid = websong.albummid,
                    Albumpic_big = websong.albumpic_big,
                    Albumpic_small = websong.albumpic_small,
                    DownUrl = websong.downUrl,
                    //Url = websong.url
                };
                id++;
                displaySongs.Add(displaySong);
            }
        }

        public static void SetWebSongByNameList(ObservableCollection<WebSong> displaySongs, List<Contentlist> webSongs , IList<Song> FavSongsList)
        {
            int id = 1;
            foreach (var websong in webSongs)
            {
                Song song = FavSongsList.FirstOrDefault(s => s.Title == websong.songname && s.Artist == websong.singername && s.IsLoaclSong == false);
                if (song != null)
                {
                    song.Id = id;
                    displaySongs.Add((WebSong)song);
                    id++;
                }
                else
                {
                    if (string.IsNullOrEmpty(websong.songname))
                        continue;
                    BitmapImage albumCover = new BitmapImage();
                    Uri uri;
                    if (!string.IsNullOrEmpty(websong.albumpic_small) && Regex.IsMatch(websong.albumpic_small, uriMatchKey))
                    {
                        uri = new Uri(websong.albumpic_small, UriKind.Absolute);
                        albumCover.UriSource = uri;
                    }
                    else
                    {
                        uri = new Uri("ms-appx:///Assets/Default/Default.jpg", UriKind.Absolute);
                        albumCover.UriSource = uri;
                    }


                    WebSong displaySong = new WebSong()
                    {
                        IsLoaclSong = false,
                        IsFavorite = false,
                        IsPlaying = false,
                        Id = id,
                        Title = websong.songname,
                        Artist = websong.singername,
                        Album = websong.albumname,
                        AlbumCover = albumCover,
                        //Duration = new TimeSpan(0, websong.seconds / 60, websong.seconds % 60),
                        Songid = websong.songid,
                        Singerid = websong.singerid,
                        Albumid = websong.albumid,
                        Albummid = websong.albummid,
                        Albumpic_big = websong.albumpic_big,
                        Albumpic_small = websong.albumpic_small,
                        DownUrl = websong.downUrl,
                        //Url = websong.url
                    };
                    id++;
                    displaySongs.Add(displaySong);
                }
            }
        }

    }
}
