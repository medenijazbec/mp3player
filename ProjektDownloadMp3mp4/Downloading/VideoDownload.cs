using ProjektDownloadMp3mp4.Classess;
using SQLite;
using VideoLibrary;
using Xamarin.Forms;

namespace ProjektDownloadMp3mp4.Downloading
{
    public class VideoDownload
    {
        public enum DownloadType
        {
            Mp3,

            Mp4
        }

        private readonly IDownloader _downloader = DependencyService.Get<IDownloader>();

        public void DownloadVideo(string link, DownloadType downloadType)
        {
            var video = YouTube.Default.GetVideo(link);
            var filename = video.Title + (downloadType == DownloadType.Mp3 ? ".mp3" : ".mp4");
            _downloader.WriteDownloadedFile(video.GetBytes(), filename);
            AddHistoryEntry(video.FullName, link);
        }

        private static void AddHistoryEntry(string videoName, string videoLink)
        {
            using (var con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<HistoryEntry>();
                con.Insert(new HistoryEntry(videoName, videoLink));
            }
        }
    }
}
