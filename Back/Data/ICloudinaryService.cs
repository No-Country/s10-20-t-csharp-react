using CloudinaryDotNet.Actions;

namespace s10.Back.Data
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
