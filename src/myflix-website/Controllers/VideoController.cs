using Microsoft.AspNetCore.Mvc;
using myflix_website.Services;

namespace myflix_website.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoService _videoService;
        private readonly IThumbnailService  _thumbnailService;

        public VideoController(IVideoService videoService, IThumbnailService thumbnailService) { 
            _videoService = videoService;
            _thumbnailService = thumbnailService;
        }

        public async Task<IActionResult> Index()
        {
            var storedAccount = HttpContext.Session.GetString("Account");

            if (string.IsNullOrEmpty(storedAccount))
                throw new Exception("Not Logged In");

            var videos = await _videoService.GetVideoCatalogueAsync();

            var videoIds = videos.Select(video => video.Id.ToString()).ToList();

            ViewBag.ImageUrls = await _thumbnailService.GetVideoThumbnailsAsync(videoIds);

            return View(videos);

        }

        [HttpGet("video/{filename}")]
        public async Task<IActionResult> Watch(string filename)
        {
            var storedAccount = HttpContext.Session.GetString("Account");

            if (string.IsNullOrEmpty(storedAccount))
                throw new Exception("Not Logged In");


            byte[] videoData = await _videoService.GetVideoFromUrlAsync(filename);
           
            string videoBase64 = Convert.ToBase64String(videoData);
            ViewBag.VideoBase64 = videoBase64;

            return View();
        }
    }
}
