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
using Newtonsoft.Json;
using System.IO;

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
        //
        // 这个方法用于将下载的歌曲的信息添加到DownloadedSongs中
        public static async Task PushFrontSongAsync(ObservableCollection<Song> localSongs, StorageFile songFiles)
        {
            // 1. 获取文件信息
            MusicProperties musicProperty = await songFiles.Properties.GetMusicPropertiesAsync();
            if (string.IsNullOrEmpty(musicProperty.Title))
                return;
            else
            {
                StorageItemThumbnail currentThumb = await songFiles.GetThumbnailAsync(ThumbnailMode.MusicView, 60, ThumbnailOptions.UseCurrentScale);

                // 2.将文件信息转换为数据模型
                Song song = new Song();

                string coverUri = "ms-appx:///Assets/Default/Default.jpg";

                song.Id = localSongs.Count + 1;
                song.SongFile = songFiles.Path;
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
                song.AlbumCoverUrl = coverUri;

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
        private static async Task PopulateSongListAsync(ObservableCollection<Song> localSongs, List<StorageFile> songFiles, IList<Song> FavSongsList = null)
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
                    localSongs.Add(localsong);
                    Id++;
                }
                else
                {
                    StorageItemThumbnail currentThumb = await file.GetThumbnailAsync(ThumbnailMode.MusicView, 60, ThumbnailOptions.UseCurrentScale);

                    // 2.将文件信息转换为数据模型
                    Song song = new Song();

                    string coverUri = "ms-appx:///Assets/Default/Default.jpg";

                    song.Id = Id;
                    song.SongFile = file.Path;
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
                    song.AlbumCoverUrl = coverUri;

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
        public static async Task SetMusicListAsync(ObservableCollection<Song> localSongs,StorageFolder musicFolder = null)
        {
            List<StorageFile> songfiles = new List<StorageFile>();
            if (musicFolder == null)
            {
                musicFolder = KnownFolders.MusicLibrary;
            }

            await GetLocalSongsAysnc(songfiles, musicFolder);
            await PopulateSongListAsync(localSongs, songfiles);

        }

        /// <summary>
        /// 将指定文件夹下的歌曲全部添加到传入参数songs中，如果这个歌曲已经在FavSongList中，则直接引用其中的信息
        /// </summary>
        /// <param name="localSongs">保存歌曲的列表</param>
        /// <param name="musicFolders">要扫描的文件夹</param>
        /// <param name="FavSongsList">收藏的歌曲</param>
        /// <returns></returns>
        public static async Task SetMusicListAsync(ObservableCollection<Song> localSongs, List<StorageFolder> musicFolders, IList<Song> FavSongsList)
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
            await PopulateSongListAsync(localSongs, songfiles, FavSongsList);
        }

        /// <summary>
        /// 将Json对应的数据转换成ViewModel需要的数据形式 
        /// 榜单的形式
        /// </summary>
        /// <param name="displaySongs">ViewModel需要的数据列表</param>
        /// <param name="webSongs">Json对应的C#数据格式</param>
        public static void SetWebSongsByBandList(ObservableCollection<Song> displaySongs, List<WebRequestSong>webSongs)
        {
            int id = 1;
            foreach(WebRequestSong websong in webSongs)
            {
                if (string.IsNullOrEmpty(websong.songname))
                    continue;
                string coverUri;
                if (!string.IsNullOrEmpty(websong.albumpic_small) && Regex.IsMatch(websong.albumpic_small, uriMatchKey))
                    coverUri = websong.albumpic_small;
                else
                    coverUri = "ms-appx:///Assets/Default/Default.jpg";

                Song displaySong = new Song()
                {
                    IsLoaclSong = false,
                    IsFavorite = false,
                    IsPlaying = false,
                    Id = id,
                    Title = websong.songname,
                    Artist = websong.singername,
                    Duration = new TimeSpan(0, websong.seconds / 60, websong.seconds % 60),
                    AlbumCoverUrl = coverUri,
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

        /// <summary>
        /// 将Json对应的数据转换成ViewModel需要的数据形式，但如果收藏列表已经有了，则使用收藏列表中的数据
        /// 榜单的形式
        /// </summary>
        /// <param name="displaySongs">ViewModel对应的数据列表</param>
        /// <param name="webSongs">Json对应的C#格式列表</param>
        /// <param name="FavSongsList">收藏的歌曲的列表</param>
        public static void SetWebSongsByBandList(ObservableCollection<Song> displaySongs, List<WebRequestSong> webSongs, IList<Song> FavSongsList)
        {
            int id = 1;
            foreach (WebRequestSong websong in webSongs)
            {
                Song song = FavSongsList.FirstOrDefault(s => s.Title == websong.songname && s.Artist == websong.singername && s.IsLoaclSong == false);
                if (song != null)
                {
                    song.Id = id;
                    displaySongs.Add(song);
                    id++;
                }
                else
                {
                    if (string.IsNullOrEmpty(websong.songname))
                        continue;
                    //BitmapImage albumCover = new BitmapImage();
                    string coverUri;
                    if (!string.IsNullOrEmpty(websong.albumpic_small) && Regex.IsMatch(websong.albumpic_small, uriMatchKey))
                        coverUri = websong.albumpic_small;
                    else
                        coverUri = "ms-appx:///Assets/Default/Default.jpg";

                    Song displaySong = new Song()
                    {
                        IsLoaclSong = false,
                        IsFavorite = false,
                        IsPlaying = false,
                        Id = id,
                        Title = websong.songname,
                        Artist = websong.singername,
                        Duration = new TimeSpan(0, websong.seconds / 60, websong.seconds % 60),
                        AlbumCoverUrl = coverUri,
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

        /// <summary>
        /// 将Json对应的数据转换成ViewModel需要的数据形式 
        /// 搜索的形式
        /// </summary>
        /// <param name="displaySongs">ViewModel需要的数据列表</param>
        /// <param name="webSongs">Json对应的C#数据格式列表</param>
        public static void SetWebSongByNameList(ObservableCollection<Song> displaySongs, List<Contentlist> webSongs)
        {
            int id = 1;
            foreach (var websong in webSongs)
            {
                if (string.IsNullOrEmpty(websong.songname))
                    continue;
                string coverUri;
                if (!string.IsNullOrEmpty(websong.albumpic_small) && Regex.IsMatch(websong.albumpic_small, uriMatchKey))
                    coverUri = websong.albumpic_small;
                else
                    coverUri = "ms-appx:///Assets/Default/Default.jpg";


                Song displaySong = new Song()
                {
                    IsLoaclSong = false,
                    IsFavorite = false,
                    IsPlaying = false,
                    Id = id,
                    Title = websong.songname,
                    Artist = websong.singername,
                    Album = websong.albumname,
                    AlbumCoverUrl = coverUri,
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

        /// <summary>
        /// 将Json对应的数据转换成ViewModel需要的数据形式 
        /// 搜索的形式
        /// </summary>
        /// <param name="displaySongs"></param>
        /// <param name="webSongs"></param>
        /// <param name="FavSongsList"></param>
        public static void SetWebSongByNameList(ObservableCollection<Song> displaySongs, List<Contentlist> webSongs , IList<Song> FavSongsList)
        {
            int id = 1;
            foreach (var websong in webSongs)
            {
                Song song = FavSongsList.FirstOrDefault(s => s.Title == websong.songname && s.Artist == websong.singername && s.IsLoaclSong == false);
                if (song != null)
                {
                    song.Id = id;
                    displaySongs.Add(song);
                    id++;
                }
                else
                {
                    if (string.IsNullOrEmpty(websong.songname))
                        continue;
                    string coverUri;
                    if (!string.IsNullOrEmpty(websong.albumpic_small) && Regex.IsMatch(websong.albumpic_small, uriMatchKey))
                        coverUri = websong.albumpic_small;
                    else
                        coverUri = "ms-appx:///Assets/Default/Default.jpg";


                    Song displaySong = new Song()
                    {
                        IsLoaclSong = false,
                        IsFavorite = false,
                        IsPlaying = false,
                        Id = id,
                        Title = websong.songname,
                        Artist = websong.singername,
                        Album = websong.albumname,
                        AlbumCoverUrl = coverUri,
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

        private static async Task<string> SerializeStatusAsync<T>(T status)
        {
            string str;
            str = await Task.Run(()=> { return JsonConvert.SerializeObject(status); });
            return str;
        }

        private static async Task<T> DeserializeStatusAsync<T>(string str)
        {
            T status = await Task.Run(() => { return JsonConvert.DeserializeObject<T>(str); });
            return status;
        }

        private static async Task SaveToLocalAsync(string str, StorageFile file)
        {
            await FileIO.WriteTextAsync(file, str);
        }

        private static async Task<string> LoadFromLocalAsync(StorageFile file)
        {
            return await FileIO.ReadTextAsync(file);
        }

        public static async Task SerializeSettingsAsync(StatusSerialization status, StorageFile file)
        {
            string str = await SerializeStatusAsync(status);
            await SaveToLocalAsync(str, file);
        }

        public static void SerializeSettings(StatusSerialization status, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            string str = JsonConvert.SerializeObject(status); 
            try { }
            finally
            {
                sw.Write(str);

                sw.Dispose();
                fs.Dispose();
            }
        }

        public static async Task<StatusSerialization> DeserializeSettingsAsync(StorageFile file)
        {
            string str = await LoadFromLocalAsync(file);
            StatusSerialization status = await DeserializeStatusAsync<StatusSerialization>(str);
            return status;
        }



    }
}
