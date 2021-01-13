using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MediaManager;
using ProjektDownloadMp3mp4.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjektDownloadMp3mp4.ViewModel
{
    public class PlayerViewModel : BaseViewModel
    {
        //private Music selectedMusic;
        //private ObservableCollection<Music> musicList;

        public PlayerViewModel(Music selectedMusic, ObservableCollection<Music> musicList)
        {
            _selectedMusic = selectedMusic;
            _musicList = musicList;

            //SelectedMusic = musicList[currentIndex- 1];
            PlayMusic(_selectedMusic);
        }

        public ICommand PlayCommand => new Command(Play);
        public ICommand ChangeCommand => new Command(ChangeMusic);
        public ICommand BackCommand => new Command(() => Application.Current.MainPage.Navigation.PopAsync());
        public ICommand ShareCommand => new Command(() => Share.RequestAsync(_selectedMusic.Url, _selectedMusic.Title));

        private async void Play()
        {
            if (_isPlaying)
            {
                await CrossMediaManager.Current.Pause();
                IsPlaying = false;
            }

            else
            {
                await CrossMediaManager.Current.Pause();
                IsPlaying = true;
            }
        }

        private void ChangeMusic(object obj)
        {
            if ((string) obj == "p")
                PreviousMusic();
            else if ((string) obj == "N")
                NextMusic();
        }

        private void NextMusic()
        {
            var currentIndex = _musicList.IndexOf(_selectedMusic);

            if (currentIndex < _musicList.Count - 1)
            {
                SelectedMusic = _musicList[currentIndex + 1];
                PlayMusic(_selectedMusic);
            }
        }

        private void PreviousMusic()
        {
            var currentIndex = _musicList.IndexOf(_selectedMusic);

            if (currentIndex > 0)
            {
                SelectedMusic = _musicList[currentIndex - 1];
                PlayMusic(_selectedMusic);
            }
        }

        private async void PlayMusic(Music music)
        {
            var mediaInfo = CrossMediaManager.Current;
            await mediaInfo.Play(music?.Url);
            IsPlaying = true;

            mediaInfo.MediaItemFinished += (sender, args) =>
            {
                IsPlaying = false;
                NextMusic();
            };

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                Duration = mediaInfo.Duration;
                Maximum = _duration.TotalSeconds;
                Position = mediaInfo.Position;
                return true;
            });

        }

        #region Properties
        //46:35
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

        private TimeSpan _duration;

        public TimeSpan Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _position;

        public TimeSpan Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }

        private double _maximum = 100f;

        public double Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                OnPropertyChanged();
            }
        }

        private bool _isPlaying;
        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                _isPlaying = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PlayIcon));
            }
        }
        public string PlayIcon => IsPlaying ? "pause.png" : "play.png";
        #endregion

    }
}
