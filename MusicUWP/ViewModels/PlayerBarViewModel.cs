using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicUWP.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MusicUWP.ViewModels
{
    public enum PlayMode { SingleCycle, ListCycle, Random, Order}
    public class PlayerBarViewModel : INotifyPropertyChanged
    {
        private PlayMode _playMode;
        private Song _currentSong;
        private TimeSpan _playedPosition;
        private bool _isPlaying;


        public PlayMode PlayMode
        {
            get { return _playMode; }
            set
            {
                _playMode = value;
                OnPropertyChanged();
            }
        }
        public Song CurrentSong
        {
            get { return _currentSong; }
            set
            {
                if (value != null)
                    _currentSong = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan PlayedPosition
        {
            get { return _playedPosition; }
            set
            {
                if (_playedPosition == value)
                    return;
                _playedPosition = value;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
