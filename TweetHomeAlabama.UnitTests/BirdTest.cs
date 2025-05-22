using TweetHomeAlabama.Data.Entities;

namespace TweetHomeAlabama.Tests;

[TestClass]
public class BirdTest
{
    private TweetHomeAlabamaDbContext _context;
    private IBirdService _birdService;

    [TestInitialize]
    public async Task Setup()
    {
        DbContextOptions<TweetHomeAlabamaDbContext> options = new DbContextOptionsBuilder<TweetHomeAlabamaDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new TweetHomeAlabamaDbContext(options);
        _birdService = new BirdService(_context);

        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }

    [TestMethod]
    public async Task AddBird_ShouldReturnTrue()
    {
        BirdDto birdDto = new BirdDto
        {
            Name = "Flamingo",
            Colors = new List<string> { "pink", "white" },
            Habitats = new List<string> { "water" },
            Shape = "tall",
            Size = "large",
            ImageUrl = "http://flamingoooooooos",
            ShortDescription = "You would love to find one in your pool"
        };

        bool success = await _birdService.AddBird(birdDto);

        Assert.IsTrue(success);

        BirdEntity? birdInDb = await _context.Birds
            .Include(b => b.Shape)
            .Include(b => b.Size)
            .Include(b => b.Colors)
            .Include(b => b.Habitats)
            .FirstOrDefaultAsync();

        Assert.IsNotNull(birdInDb);
        Assert.AreEqual("Flamingo", birdInDb.Name);
    }

    [TestMethod]
    public async Task SearchBird_ShouldReturnCorrectBird()
    {
        // Use the service to add a bird (avoids duplication)
        await AddBird_ShouldReturnTrue();

        List<string> colors = new List<string> { "pink", "white" };
        List<string> habitats = new List<string> { "water" };

        IList<BirdDto> result = await _birdService.GetBirds(colors, "large", "tall", habitats);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Flamingo", result.First().Name);
    }
}
