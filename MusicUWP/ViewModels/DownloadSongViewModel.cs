using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MusicUWP.ViewModels
{
    public  class DownloadSongViewModel : LocalSong , INotifyPropertyChanged
    {
        private double _processPercent;
        public double ProcessPercent
        {
            get { return _processPercent; }
            set
            {
                _processPercent = value;
                OnPropertyChanged();
            }
        }



        public new event PropertyChangedEventHandler PropertyChanged;
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
