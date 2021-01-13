using System.Collections.Generic;
using System.IO;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using MediaManager;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Environment = System.Environment;
using Platform = Xamarin.Essentials.Platform;

namespace ProjektDownloadMp3mp4.Droid
{
    [Activity(Label = "ProjektDownloadMp3mp4", Icon = "@mipmap/icon", Theme = "@style/MainTheme",
        MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);

            var fileName = "Baza.db";
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var completePath = Path.Combine(folderPath, fileName);

            // attempt to get the storage permissions at runtime
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int) Permission.Granted
                || ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int) Permission.Granted)
                ActivityCompat.RequestPermissions(this,
                    new[] {Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage}, 0);

            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App(completePath));

            //init for media manager
            CrossMediaManager.Current.Init(this);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
