namespace TweetHomeAlabama.Domain.Models
{
    public class Bird
    {
        #region Properties
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ShortDescription { get; set; }

        public Shape Shape { get; set; } = null!;
        public Size Size { get; set; }

        public ICollection<Color> Colors { get; set; } = new List<Color>();
        public ICollection<Habitat> Habitats { get; set; } = new List<Habitat>();
        #endregion

        #region Constructors
        public Bird(string name, string imageUrl, string shortDescription,
            Shape shape, Size size, IList<Color> colors, IList<Habitat> habitats)
        {
            Name = name;
            ImageUrl = imageUrl;
            ShortDescription = shortDescription;
            Shape = shape;
            Size = size;
            Colors = colors;
            Habitats = habitats;
        }
        #endregion
    }
}
