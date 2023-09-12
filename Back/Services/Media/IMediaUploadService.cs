namespace s10.Back.Services.Media
{
    public interface IVideoUploadService
    {
        public Task<String> UploadVideoAsync(Stream stream, VideoMetaData metadata);
    }
}
