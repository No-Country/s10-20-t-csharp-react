using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using s10.Back.Services.Media;

namespace s10.Back.Services
{
    public class UploadVideoService
    {
        private readonly IVideoUploadService _youtubeService;

        public UploadVideoService(IVideoUploadService youtubeService)
        {
            _youtubeService = youtubeService;
        }



        public async Task<string> UploadVideoAsync(Stream stream, VideoMetaData metaData, string fileName, string basePath = "wwwroot/tempVideos")
        {
            //save file on temp folder in the case it fails, or for a retry
            var path = await SaveToTempFolderAsync(stream, fileName);
            //create videoId
            var videoId = await  _youtubeService.UploadVideoAsync(stream, metaData);
            //send to youtube on background

            return path;
        }



        /// <summary>Saves a stream to a temp path</summary>
        /// <returns>The Path</returns>
        private async Task<string> SaveToTempFolderAsync(Stream stream, string fileName, string basePath = "wwwroot/tempVideos")
        {
            var fileNameWithPath = $"{basePath}/{fileName}";
            var newName = $"{Guid.NewGuid()}-{fileName}";

            if (stream.Length == 0)
            { throw new InvalidDataException("stream is empty"); }

            if (fileName.Length == 0) { throw new Exception("Invalid fileName"); }

            try
            {
                using (var fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    await stream.CopyToAsync(fileStream);
                }
            }
            catch (Exception e)
            {
                //log
                throw;
            }

            return fileNameWithPath;
        }
    }
}
