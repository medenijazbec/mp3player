using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ProjektDownloadMp3mp4.Model;
using ProjektDownloadMp3mp4.View;
using Xamarin.Forms;

namespace ProjektDownloadMp3mp4.ViewModel
{
    public class LandingViewModel : BaseViewModel
    {
        public LandingViewModel()
        {
            /*_musicList = GetMusic();//have to find mp3 files on the device and pass them into here
            _recentMusic = _musicList.Where(x => x.IsRecent == true).FirstOrDefault();*/
        }

        private ObservableCollection<Music> _musicList;

        public ObservableCollection<Music> MusicList
        {
            get => _musicList;
            set
            {
                _musicList = value;
                OnPropertyChanged();
            }
        }

        private Music _recentMusic;

        public Music RecentMusic
        {
            get => _recentMusic;
            set
            {
                _recentMusic = value;
                OnPropertyChanged();
            }
        }

        private Music _selectedMusic;

        public Music SelectedMusic
        {
            get => _selectedMusic;
            set
            {
                _selectedMusic = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectionCommand => new Command(PlayMusic);

        private void PlayMusic()
        {
            if (_selectedMusic == null)
                return;
            var viewModel = new PlayerViewModel(_selectedMusic, _musicList);
            var playerPage = new PlayerPage {BindingContext = viewModel};

            var navigation = Application.Current.MainPage as NavigationPage;
            navigation?.PushAsync(playerPage, true);
        }

        /*  private ObservableCollection<Music> GetMusic()
          {
              return new ObservableCollection<Music>;
  
              new Music { Title = "Test", Artist = "Artest", Url = "urltosong", CoverImage = "image" };
          
          }*/

    }
}
