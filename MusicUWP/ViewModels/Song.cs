using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;


namespace MusicUWP.ViewModels
{
    public class Song : INotifyPropertyChanged
    {
        private int _id;
        private string _title;
        private string _artist;
        private string _album;
        
        private TimeSpan _duration;
        private string _alblmCoverUrl;
        private bool _isFavorite;
        private bool _isPlaying;

        private string _songFile;


        public int Albumid { get; set; }
        public string Albummid { get; set; }
        public string Albumpic_big { get; set; }
        public string Albumpic_small { get; set; }
        public string DownUrl { get; set; }
        public int Singerid { get; set; }
        public int Songid { get; set; }
        public string Url { get; set; }
        public string SongFile
        {
            get { return _songFile; }
            set
            {
                _songFile = value;
                OnPropertyChanged();
            }
        }

        //   需要展示的属性
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                if(PropertyChanged!=null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }
        public string Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                if(PropertyChanged!=null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Artist"));
                }
            }
        }
        public string Album
        {
            get { return _album; }
            set
            {
                _album = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Album"));
            }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Duration"));
            }
        }
        public bool IsFavorite
        {
            get { return _isFavorite; }
            set
            {
                _isFavorite = value;
                OnPropertyChanged();
            }
        }
        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                _isPlaying = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoaclSong { get; set; }

        public string AlbumCoverUrl
        {
            get { return _alblmCoverUrl; }
            set
            {
                _alblmCoverUrl = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("AlbumCover"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
