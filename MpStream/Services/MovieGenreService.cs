using MpStream.Data;
using MpStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Services
{
    public class MovieGenreService
    {
        private readonly ApplicationDbContext aDatabase;
        List<MovieGenreEntity> GenreList = new List<MovieGenreEntity>();
        MovieGenreEntity GenreEntity = new MovieGenreEntity();
        public MovieGenreService()
        {

        }
        public MovieGenreService(ApplicationDbContext database)
        {
            database = aDatabase;
        }
        public List<MovieGenreEntity> GetGenreList()
        {
            GenreList = aDatabase.MovieGenreEntities.ToList();
            return GenreList;
        }
        public async Task<bool> DeleteGenre(int Id)
        {
            GenreEntity = aDatabase.MovieGenreEntities.SingleOrDefault(s => s.Id == Id);
            aDatabase.Remove(GenreEntity);
            var changes = await aDatabase.SaveChangesAsync();
            if(changes >= 0) { return true; } else { return false;}
        }
        public Task<bool> AddGenre(MovieGenreEntity movieGenreEntity)
        {
            aDatabase.MovieGenreEntities.Add(movieGenreEntity);
            var result = aDatabase.SaveChanges();
            return Task.FromResult(result > 0);
        }

    }
}
