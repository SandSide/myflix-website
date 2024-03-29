﻿using myflix_website.Models;

namespace myflix_website.Services
{
    public interface IVideoService
    {
        Task<List<Video>> GetVideoCatalogueAsync();
        Task<byte[]> GetVideoFromUrlAsync(string filename);
    }
}
