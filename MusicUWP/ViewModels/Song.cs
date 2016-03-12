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
        private BitmapImage _alblmCover;
        private bool _isFavorite;
        private bool _isPlaying;

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

        public BitmapImage AlbumCover
        {
            get { return _alblmCover; }
            set
            {
                _alblmCover = value;
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
