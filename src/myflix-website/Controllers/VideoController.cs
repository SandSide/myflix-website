using Microsoft.AspNetCore.Mvc;
using myflix_website.Services;

namespace myflix_website.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService) { 
            _videoService = videoService;
        }

        public async Task<IActionResult> Index()
        {
            var videos = await _videoService.GetVideoAsync();
            return View(videos);
        }
    }
}
