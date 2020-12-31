using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    public class WebSiteService
    {
        private List<string> _webSiteUrls = new List<string>
        {
            "https://www.yahoo.com",
            "https://www.google.com",
            "https://www.microsoft.com",
            "https://www.cnn.com",
            "https://www.codeproject.com",
            "https://www.stackoverflow.com",
        };


        public string DownloadWebSiteStats()
        {
            var sb = new StringBuilder();

            foreach (var url in _webSiteUrls)
            {
                var webSiteRawData = DownloadWebSite(url);
                var webSiteStats = GetWebSiteStats(webSiteRawData);
                sb.Append($"{url}: {webSiteStats}\n");
            }

            return sb.ToString();
        }

        public string DownloadWebSiteStatsParallel()
        {
            var sb = new StringBuilder();

            Parallel.ForEach(_webSiteUrls, url =>
            {
                var webSiteRawData = DownloadWebSite(url);
                var webSiteStats = GetWebSiteStats(webSiteRawData);

                lock (sb) sb.Append($"{url}: {webSiteStats}\n");
            });

            return sb.ToString();
        }

        public Task<string> DownloadWebSiteStatsAsyncNonParallel()
        {
            return Task.Run(() => DownloadWebSiteStats());
        }

        public Task<string> DownloadWebSiteStatsParallelAsync()
        {
            return Task.Run(() => DownloadWebSiteStatsParallel());
        }

        private string DownloadWebSite(string url)
        {
            return new WebClient().DownloadString(url);
        }

        private string GetWebSiteStats(string webSiteRawData)
        {
            return $"Web site has {webSiteRawData.Length} characters.";
        }
    }
}
