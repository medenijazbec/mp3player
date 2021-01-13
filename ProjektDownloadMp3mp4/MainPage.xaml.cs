using System;
using System.ComponentModel;
using ProjektDownloadMp3mp4.Downloading;
using Xamarin.Forms;

namespace ProjektDownloadMp3mp4
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage
    {
        private readonly VideoDownload _downloader = new VideoDownload();

        public MainPage()
        {
            InitializeComponent();
            //VideoClass video = new VideoClass();
        }

        private void DownloadMp4(object sender, EventArgs e)
        {
            CompletedLabel.IsVisible = false;
            _downloader.DownloadVideo(Link.Text, VideoDownload.DownloadType.Mp4);
            CompletedLabel.IsVisible = true;
        }

        private void DownloadMp3(object sender, EventArgs e)
        {
            CompletedLabel.IsVisible = false;
            _downloader.DownloadVideo(Link.Text, VideoDownload.DownloadType.Mp3);
            CompletedLabel.IsVisible = true;
        }

        private void EntryCompleted(object sender, EventArgs e)
        {
        }

        private void ShowHistory(object sender, EventArgs e)
        {
            Application.Current.MainPage = new HistoryPage();
        }
    }
}
