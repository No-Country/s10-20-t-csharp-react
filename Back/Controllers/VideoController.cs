using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using s10.Back.Core.Common;
using s10.Back.Services;
using s10.Back.Services.Media;
using System.Security.Claims;

namespace s10.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private UploadVideoService _uploadVideoService;
        private BackgroundWorkerQueue _backgroundWorkerQueue;

        public VideoController(UploadVideoService uploadVideoService, BackgroundWorkerQueue backgroundWorkerQueue)
        {
            _uploadVideoService = uploadVideoService;
            _backgroundWorkerQueue = backgroundWorkerQueue;
        }

        [HttpPost, DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = Int32.MaxValue, ValueLengthLimit = Int32.MaxValue)]
        public async Task<IActionResult> PostVideoAsync(IFormFile file)
        {
            //steps
            //receive file  
            //save file on temp folder
            //create videoId
            //send to youtube on background
            //return videoId

            //on background upload to youtbe
            //once uploades delete temp file
            //update 

            var host = $"{Request.Scheme}://{Request.Host}";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "User";
            var fileName = userId + "." + file.FileName.Split(".").Last();

            try
            {
                var path =
                      await _uploadVideoService.UploadVideoAsync(file.OpenReadStream(), new VideoMetaData(), fileName);


                return Created($"{host}/{userId}", null);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet]
        public async Task SlowUpload()
        {
            Console.Write("Tsarting");
            _backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
            {
                await Task.Delay(10000);
                Console.Write("DONE");
            });
        }
    }
}
