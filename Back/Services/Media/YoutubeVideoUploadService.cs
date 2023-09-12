using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using System.Reflection;
using System.IO;
using Google.Apis.Auth.OAuth2.Responses;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net;
using System.Runtime;
using Google.Apis.Auth.OAuth2.Flows;

namespace s10.Back.Services.Media
{
    public class YoutubeVideoUploadService : IVideoUploadService
    {
        private readonly ClientSecrets _clientSecrets;
        private readonly IConfiguration _config;
        private UserCredential _credential;
        private YouTubeService _youtubeService;

        public YoutubeVideoUploadService(IConfiguration config)
        {
            _config = config;
            _clientSecrets = _config.GetSection("Authentication:Google").Get<ClientSecrets>();
        }
        private async Task Initialize()
        {

            var refreshToken =
                "1//05nRNIZcORQlYCgYIARAAGAUSNwF-L9Irp-UnngsgMPEvRVWrqofVp5Mm0luyuUmzEaLn4hsjm6qokbQSlHYyg045lP3b4zkJPKY";
            var token = new TokenResponse()
            {
                RefreshToken = refreshToken,
                TokenType = "refresh_token"
            };


            _credential = new UserCredential(
                new GoogleAuthorizationCodeFlow(
                      new GoogleAuthorizationCodeFlow.Initializer
                      {
                          ClientSecrets = _clientSecrets,
                          //Scopes = new[] { YouTubeService.Scope.YoutubeUpload },
                      }
              ), "dnto", token);


            // new YoutubeResource
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = this.GetType().ToString()
            });
        }

        public async Task<string> UploadVideoAsync(Stream stream, VideoMetaData metadata)
        {
            await Initialize();

            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = "Default Video Title";
            video.Snippet.Description = "Default Video Description";
            video.Snippet.Tags = new string[] { "tag1", "tag2" };
            video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
            video.Status = new VideoStatus();
            video.Status.PrivacyStatus = "unlisted"; // or "private" or "public"
            var filePath = @"REPLACE_ME.mp4"; // Replace with path to actual movie file.


            // var fileStream
            var videosInsertRequest = _youtubeService.Videos.Insert(video, "snippet,status", stream, "video/*");
            videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
            videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

            var t = videosInsertRequest.UploadAsync();
            Task.WaitAll(new[] { t });
            stream.Close();
            return videosInsertRequest.ResponseBody.Id;


        }


        void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    Console.WriteLine("{0} bytes sent.", progress.BytesSent);
                    break;

                case UploadStatus.Failed:
                    Console.WriteLine("An error prevented the upload from completing.\n{0}", progress.Exception);
                    break;
            }
        }

        void videosInsertRequest_ResponseReceived(Video video)
        {
            Console.WriteLine("Video id '{0}' was successfully uploaded.", video.Id);
        }

    }
}
