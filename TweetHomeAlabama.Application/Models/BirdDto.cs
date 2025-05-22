namespace TweetHomeAlabama.Application.Models
{
    public class BirdDto
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Shape { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public List<string> Colors { get; set; } = new List<string>();
        public List<string> Habitats { get; set; } = new List<string>();
    }

}
