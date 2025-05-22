using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TweetHomeAlabama.Application.Interfaces;
using TweetHomeAlabama.Application.Models;
using TweetHomeAlabama.Data.DataContext;
using TweetHomeAlabama.Data.Entities;

namespace TweetHomeAlabama.Application.Services
{
    public class BirdService : IBirdService
    {
        private readonly TweetHomeAlabamaDbContext _context;

        public BirdService(TweetHomeAlabamaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddBird(BirdDto bird)
        {
            if (bird is null)
                throw new ArgumentNullException(nameof(bird));

            if (bird.Colors is not null && bird.Colors.Any(b => !IsValidString(b)))
                throw new ArgumentException("Invalid color string format.");

            if (bird.Habitats is not null && bird.Habitats.Any(b => !IsValidString(b)))
                throw new ArgumentException("Invalid color string format.");

            BirdEntity? existingBird = await _context.Birds
                .FirstOrDefaultAsync(b => b.Name == bird.Name);

            if (existingBird is null)
            {
                ShapeEntity shape = new ShapeEntity()
                {
                    Name = bird.Shape
                };

                SizeEntity size = new SizeEntity()
                {
                    Name = bird.Size
                };

                List<ColorEntity> colors = new List<ColorEntity>();
                if (bird.Colors is not null && bird.Colors.Any())
                {
                    foreach (string color in bird.Colors)
                    {
                        colors.Add(new ColorEntity
                        {
                            Name = color
                        });
                    }
                }
                List<HabitatEntity> habitats = new List<HabitatEntity>();
                if (bird.Habitats is not null && bird.Habitats.Any())
                {
                    foreach (string habitat in bird.Habitats)
                    {
                        habitats.Add(new HabitatEntity
                        {
                            Name = habitat
                        });
                    }
                }

                await _context.Birds
                    .AddAsync(new BirdEntity(
                        bird.Name,
                        bird.ImageUrl,
                        bird.ShortDescription,
                        shape,
                        size,
                        colors,
                        habitats
                    ));

                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<IList<BirdDto>> GetAll()
        {
            List<BirdDto> birdDtos = new List<BirdDto>();

            List<BirdEntity> birdEntities = await _context.Birds
                .Include(b => b.Shape)
                .Include(b => b.Size)
                .Include(b => b.Colors)
                .Include(b => b.Habitats)
                .ToListAsync();

            foreach (BirdEntity bird in birdEntities)
            {
                birdDtos.Add(new BirdDto
                {
                    Name = bird.Name,
                    ImageUrl = bird.ImageUrl,
                    ShortDescription = bird.ShortDescription,
                    Shape = bird.Shape.Name,
                    Size = bird.Size.Name,
                    Colors = bird.Colors.Select(c => c.Name).ToList(),
                    Habitats = bird.Habitats.Select(h => h.Name).ToList()
                });
            }

            return birdDtos;
        }

        public async Task<IList<BirdDto>> GetBirds(IEnumerable<string> colors, string size, string shape, IEnumerable<string> habitat)
        {
            IList<BirdDto> birdDtos = await GetAll();
            List<BirdDto> filteredBirds = new List<BirdDto>();

            foreach (BirdDto bird in birdDtos)
            {
                int matches = 0;
                if (colors != null && colors.Count() > 0)
                {
                    if (bird.Colors.Where(c => colors.Contains(c)).Count() > 1)
                    {
                        filteredBirds.Add(bird);
                        continue;
                    }
                    else if (bird.Colors.Any(c => colors.Contains(c)))
                    {
                        matches++;
                    }
                }
                else if (size != null)
                {
                    if (bird.Size == size)
                    {
                        matches++;
                    }
                }
                else if (shape != null)
                {
                    if (bird.Shape == shape)
                    {
                        matches++;
                    }
                }
                else if (habitat != null && habitat.Count() > 0)
                {
                    if (bird.Habitats.Any(h => habitat.Contains(h)))
                    {
                        matches++;
                    }
                }

                if (matches > 2)
                {
                    filteredBirds.Add(bird);
                }
            }

            return filteredBirds;
        }

        private bool IsValidString(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            Regex regex = new Regex(@"^([a-zA-Z]+)(,\s*[a-zA-Z]+)*$");

            return regex.IsMatch(input);
        }

    }
}
