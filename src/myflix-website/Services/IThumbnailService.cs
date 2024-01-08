namespace myflix_website.Services
{
    public interface IThumbnailService
    {
        Task<string> GetVideoThumbnailAsync(string videoId);
    }
}
