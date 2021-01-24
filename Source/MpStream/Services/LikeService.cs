using MpStream.Data;
using MpStream.Models;
using System.Threading.Tasks;
using System.Linq;

namespace MpStream.Services
{
    public class LikeService
    {
        private readonly ApplicationDbContext Database;
        public LikeService(ApplicationDbContext aDatabase)
        {
            Database = aDatabase;
        }

        public Task<bool> AddMovieLike(MovieEntity movieEntity)
        {
            var movieLike = Database.MovieLikes.Where(s => s.MovieEntityId == movieEntity.Id).SingleOrDefault();
            if(movieLike.LikeCount >= 1 && movieLike != null)
            {
                movieLike.LikeCount += 1;
            }else
            {
                Database.MovieLikes.Add(new MovieLike { MovieEntityId = movieEntity.Id, LikeCount = 1 });
            }
            
            return Task.FromResult(true);
        }
    }

}
