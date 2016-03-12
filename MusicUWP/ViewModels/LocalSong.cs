using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MusicUWP.ViewModels
{
    public class LocalSong : Song, INotifyPropertyChanged
    {
        private StorageFile _songFile;

        public StorageFile SongFile
        {
            get { return _songFile; }
            set
            {
                _songFile = value;
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
