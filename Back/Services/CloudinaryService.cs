using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using Microsoft.Extensions.Options;
using s10.Back.Data;
using System.Net.WebSockets;

namespace s10.Back.Services
{
    public class CloudinaryService : ICloudinaryService
    {

        private readonly Cloudinary _cloudinary;

        public CloudinaryService()
        {
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            _cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            _cloudinary.Api.Secure = true;
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if(file.Length > 0)
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

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}
