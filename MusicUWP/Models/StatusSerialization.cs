using MusicUWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicUWP.Models
{
    public class StatusSerialization
    {
        public PlayerStatus playerStatus { get; set; } = new PlayerStatus();
        public FavoriteSongsStatus favoriteSongStatus { get; set; } = new FavoriteSongsStatus();
        public PlayingSongsStatus playingSongsStatus { get; set; } = new PlayingSongsStatus();
        public DownloadedSongsStatus downloadedSongsStatus { get; set; } = new DownloadedSongsStatus();
    }

    public class DownloadedSongsStatus
    {
        public int count { get; set; }
        public IList<Song> downloadedSongsList { get; set; } = new ObservableCollection<Song>();
    }

    public class PlayingSongsStatus
    {
        public int count { get; set; }
        public IList<Song> playingSongsList { get; set; } = new ObservableCollection<Song>();
    }

    public class FavoriteSongsStatus
    {
        public int count { get; set; }
        public IList<Song> favoriteSongsList { get; set; } = new ObservableCollection<Song>();
    }

    public class PlayerStatus
    {
        public double volume { get; set; }
        public PlayMode palyMode { get; set; }
        public Song currentSong { get; set; } = new Song();
    }
}
