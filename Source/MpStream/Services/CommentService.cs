using MpStream.Data;
using MpStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Services
{
    public class CommentService
    {
        private readonly ApplicationDbContext Database;
        public CommentService(ApplicationDbContext aDatabase)
        {
            Database = aDatabase;
        }
        public Task<List<MovieComment>> DisplayComments(int movidId)
        {
            var movieComments = Database.MovieComments.Where(s=>s.MovieEntityId == movidId).Take(5).ToList();
            return Task.FromResult(movieComments);
        }

        public Task<bool> AddMovieComment(MovieComment movieCommentModel)
        {
            Database.MovieComments.Add(movieCommentModel);
            var result = Database.SaveChanges();
            if(result > 0)
            {
                return Task.FromResult(true);
            } else
            {
                return Task.FromResult(false);
            }
        }
    }
}
