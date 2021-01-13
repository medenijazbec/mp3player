namespace ProjektDownloadMp3mp4.Downloading
{
    public interface IDownloader
    {
        void WriteDownloadedFile(byte[] bytes, string filename);
    }
}
