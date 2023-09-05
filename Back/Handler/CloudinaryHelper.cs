using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using s10.Back.Data.IRepositories;

namespace s10.Back.Handler
{
    public class CloudinaryHelper : ICloudinaryService
    {

        private readonly Cloudinary _cloudinary;

        public CloudinaryHelper()
        {
            _cloudinary = new Cloudinary("cloudinary://427996914125567:qQe4v1MexM4mNmrluGG2K41exUQ@dumwds5gm");
            _cloudinary.Api.Secure = true;
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParam = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),

                    // se necesitan transformations
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParam);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string wholeUrlPath)
        {
            var publicId = wholeUrlPath.Split('/')[6].Split(".")[0];
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}
