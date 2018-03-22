using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.YouTube.v3;

using Google.GData.Client;
using Google.GData.YouTube;
using Google.GData.Extensions;

namespace EverythingFootballDemo.DAL
{
    public class VideoAPI
    {
        private static Google.Apis.YouTube.v3.YouTubeService ytService = AuthorizeService();
        private static Google.Apis.YouTube.v3.YouTubeService AuthorizeService() {
            UserCredential credentials;
            using (var stream = new FileStream("YoutubeAPI.json", FileMode.Open, FileAccess.Read)) {
                credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { Google.Apis.YouTube.v3.YouTubeService.Scope.YoutubeReadonly },
                    "user",
                    CancellationToken.None,
                    new FileDataStore("VideoAPI")
                    ).Result;
            }

            var ytService = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = "The Offside Club"
            });

            return ytService;
        }
        public static void getVideoInfo(YoutubeVideo video) {
            var videoRequest = ytService.Videos.List("snippet");
            //videoRequest.Id = video.id;

        }

        public class YouTube
        {
            public string YouTubeMovieID { get; set; }
            public string YouTubeMovieTitle { get; set; }
        }

        public class YouTubeHelper
        {
            const string YT_CHANNEL_NAME = "blendercookie";
            const string YT_DEVELOPER_ID = "SOMETHING LIKE THAT : AI39si4MmBNkAvglhJrirLO19NpGcehhfdhdfDyT1qHRigohHdYDw8Lj2l_G_6djdfjdFQPGnGFTfxpwewuhJMukVxUZjmXA";

            //public static List<YouTube> GetVideos()
            //{
            //    YouTubeRequestSettings ytSettings = new YouTubeRequestSettings("YouTubeTest", YT_DEVELOPER_ID);
            //    YouTubeRequest ytRequest = new YouTubeRequest(ytSettings);
            //    string feedURL = String.Format("http://gdata.youtube.com/feeds/api/users/{0}/uploads?orderby=published", YT_CHANNEL_NAME);
            //    Feed<Video> videoFeed = ytRequest.Get<Video>(new Uri(feedURL));
            //    return (from video in videoFeed.Entries
            //            select new YouTube() { YouTubeMovieID = video.VideoId, YouTubeMovieTitle = video.Title }).ToList();
            //}

        }
    }
}
