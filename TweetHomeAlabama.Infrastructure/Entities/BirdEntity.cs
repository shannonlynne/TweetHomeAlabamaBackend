namespace TweetHomeAlabama.Data.Entities;

public class BirdEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;

    public int ShapeId { get; set; }
    public ShapeEntity Shape { get; set; } = null!;
    public int SizeId { get; set; }
    public SizeEntity Size { get; set; } = null!;

    public ICollection<ColorEntity> Colors { get; set; } = new List<ColorEntity>();
    public ICollection<HabitatEntity> Habitats { get; set; } = new List<HabitatEntity>();

    public BirdEntity(string name, string imageUrl, string shortDescription)
    {
        Name = name;
        ImageUrl = imageUrl;
        ShortDescription = shortDescription;
    }

    public BirdEntity(string name, string imageUrl, string shortDescription, ShapeEntity shape, SizeEntity size,
        ICollection<ColorEntity> colors, ICollection<HabitatEntity> habitats)
    {
        Name = name;
        ImageUrl = imageUrl;
        ShortDescription = shortDescription;
        Shape = shape;
        Size = size;
        Colors = colors;
        Habitats = habitats;
    }

    protected BirdEntity()
    { }
}
