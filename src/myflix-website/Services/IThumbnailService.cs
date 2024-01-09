using myflix_website.Models;

namespace myflix_website.Services
{
    public interface IThumbnailService
    {
        Task<List<string>> GetVideoThumbnailsAsync(List<string> videoIds);
    }
}
