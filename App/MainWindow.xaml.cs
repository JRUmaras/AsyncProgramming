using System.Windows;

namespace AsyncProgramming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnExecSyncButtonClicked(object sender, RoutedEventArgs e)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var stats = new WebSiteService().DownloadWebSiteStats();

            var execTimeInSec = sw.ElapsedMilliseconds;

            TbxResult.Text = $"{stats}\nExec SYNC took {execTimeInSec} s\n";
        }

        private async void OnExecAsyncButtonClicked(object sender, RoutedEventArgs e)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var stats = await new WebSiteService().DownloadWebSiteStatsAsyncNonParallel();

            var execTimeInSec = sw.ElapsedMilliseconds;

            TbxResult.Text = $"{stats}\nExec ASYNC took {execTimeInSec} s\n";
        }

        private async void OnExecParallelAsyncButtonClicked(object sender, RoutedEventArgs e)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var stats = await new WebSiteService().DownloadWebSiteStatsParallelAsync();

            var execTimeInSec = sw.ElapsedMilliseconds;

            TbxResult.Text = $"{stats}\nExec ASYNC took {execTimeInSec} s\n";
        }
    }
}
