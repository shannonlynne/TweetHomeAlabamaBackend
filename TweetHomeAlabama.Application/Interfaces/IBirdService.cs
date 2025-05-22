using TweetHomeAlabama.Application.Models;

namespace TweetHomeAlabama.Application.Interfaces
{
    public interface IBirdService
    {
        Task<bool> AddBird(BirdDto bird);
        Task<IList<BirdDto>> GetAll();
        Task<IList<BirdDto>> GetBirds(
            IEnumerable<string> colors,
            string size,
            string shape,
            IEnumerable<string> habitats
            );
    }
}
