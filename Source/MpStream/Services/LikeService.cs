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

        public Task<int> GetLikes(int movieId)
        {
            var movieLike = Database.MovieLikes.Where(s => s.MovieEntityId == movieId).SingleOrDefault();
            if(movieLike is null)
            {
                return Task.FromResult(0);
            } else
            {
                return Task.FromResult(movieLike.LikeCount);
            }
        }

        public Task<int> AddMovieLike(int movieId)
        {
            var movieLike = Database.MovieLikes.Where(s => s.MovieEntityId == movieId).SingleOrDefault();
            if(movieLike is null)
            {
                Database.MovieLikes.Add(new MovieLike { MovieEntityId = movieId, LikeCount = 1 });
                Database.SaveChanges();
            }
            var movieLikeInDb = Database.MovieLikes.Where(s => s.MovieEntityId == movieId).SingleOrDefault();
            return Task.FromResult(movieLikeInDb.LikeCount);
        }

        public Task<int> UpdateLikes(int movieId)
        {
            var movieLike = Database.MovieLikes.Where(s => s.MovieEntityId == movieId).SingleOrDefault();
            if(movieLike is not null)
            {
                movieLike.LikeCount += 1;
                movieLike.MovieEntityId = movieId;
                Database.SaveChanges();
            }
            var movieLikeInDb = Database.MovieLikes.Where(s => s.MovieEntityId == movieId).SingleOrDefault();
            return Task.FromResult(movieLikeInDb.LikeCount);
        }
    }

}
