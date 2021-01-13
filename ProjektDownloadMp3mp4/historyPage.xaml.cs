using System;
using ProjektDownloadMp3mp4.Classess;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjektDownloadMp3mp4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new MainPage();
            return base.OnBackButtonPressed();
        }

        private void GoBack(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<HistoryEntry>();
                var history = con.Table<HistoryEntry>().ToList();
                HistoryListView.ItemsSource = history;
            }
        }
    }
}
