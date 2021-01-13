using System.IO;
using Android.OS;
using ProjektDownloadMp3mp4.Downloading;
using ProjektDownloadMp3mp4.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileDownloader))]

namespace ProjektDownloadMp3mp4.Droid
{
    public class FileDownloader : IDownloader
    {
        public void WriteDownloadedFile(byte[] bytes, string filename)
        {
            var dir = Environment.GetExternalStoragePublicDirectory(Environment
                .DirectoryDownloads)?.AbsolutePath;
            var path = Path.Combine(dir ?? string.Empty, filename);
            WriteLowLevel(bytes, path);
        }

        private static void WriteLowLevel(byte[] bytes, string path)
        {
            File.WriteAllBytes(path, bytes);
        }
    }
}
