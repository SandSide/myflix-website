namespace myflix_website.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Length { get; set; }
        public string VideoUrl { get; set; }
    }
}
