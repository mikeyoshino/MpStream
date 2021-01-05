using Microsoft.EntityFrameworkCore;
using MpStream.Data;
using MpStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Services
{
    public class MovieService
    {
        private readonly ApplicationDbContext aDatabase;
        public List<MovieWithGenre> MovieWithGenres { get; set; } = new List<MovieWithGenre>();
        Dictionary<int, string> GenreNameMappedById = new Dictionary<int, string>();
        List<MovieWithGenre> GenresByMovieId = new List<MovieWithGenre>();
        public MovieEntity MovieEntity { get; set; } = new MovieEntity();
        public MovieService(){}
        public MovieService(ApplicationDbContext database, ApplicationDbContext sdatabase){aDatabase = database;}

        public Task<bool> AddMovie(MovieEntity movieEntity)
        {
            aDatabase.MovieEntity.Add(movieEntity);
            var result = aDatabase.SaveChanges();
            return Task.FromResult(result > 0);
        }

        public List<MovieEntity> GetMovieList()
        {
            List<MovieEntity> movieList;
            movieList = aDatabase.MovieEntity.Include("MovieWithGenres").ToList();
            return movieList;
        }

        public MovieEntity GetMovieById(int Id)
        {
            MovieEntity = aDatabase.MovieEntity.SingleOrDefault(s => s.Id == Id);
            return MovieEntity;
        }

        public Task<bool> AddGenre(MovieGenreEntity movieGenreEntity)
        {
            aDatabase.MovieGenreEntities.Add(movieGenreEntity);
            var result = aDatabase.SaveChanges();
            return Task.FromResult(result > 0);
        }

        public List<MovieGenreEntity> MovieGenreList()
        {
            List<MovieGenreEntity> movieGenreLists;
            movieGenreLists = aDatabase.MovieGenreEntities.ToList();
            return movieGenreLists;
        }

        public Task<bool> SaveMovieWithGenre(MovieEntity movie, List<string> Ids)
        {
            foreach (var eachId in Ids)
            {
                MovieWithGenres.Add(new MovieWithGenre { MovieEntityId = movie.Id, MovieGenreEntityId = Convert.ToInt32(eachId)});
            }
            aDatabase.MovieWithGenres.AddRange(MovieWithGenres);
            var reuslt = aDatabase.SaveChanges();
            return Task.FromResult(reuslt > 0);
        }
        public Dictionary<int, string> MappedGenreToDictionary()
        {
            foreach (var eachGenre in aDatabase.MovieGenreEntities.ToList())
            {
                GenreNameMappedById.Add(eachGenre.Id, eachGenre.Name);
            }
            return GenreNameMappedById;
        }

        public async Task<bool> DeleteMovie(int Id)
        {
            MovieEntity = aDatabase.MovieEntity.Find(Id);
            aDatabase.Remove(MovieEntity);
            var result = await aDatabase.SaveChangesAsync();
            if(result >= 0)
            {
                return await Task.FromResult(true);
            } else
            {
                return await Task.FromResult(false);
            }
        }
        public MovieEntity EditMovie(int Id)
        {
            MovieEntity = aDatabase.MovieEntity.Include("MovieWithGenres").SingleOrDefault(s => s.Id == Id);
            return MovieEntity;
        }
        public List<MovieWithGenre> GetGenreListByMovieId(int Id)
        {
            GenresByMovieId = aDatabase.MovieWithGenres.Where(m => m.MovieEntityId == Id).ToList();
            return GenresByMovieId;
        }

        public bool UpdateMovie(int Id, MovieEntity movieEntity, List<MovieGenreEntity> newSelectGenreList)
        {
            List<MovieWithGenre> GenreRemoveList = new List<MovieWithGenre>();
            Dictionary<int, int> movieWithGenreMapById = new Dictionary<int, int>();
            Dictionary<int, int> movieGenreInDbMapById = new Dictionary<int, int>();
            MovieEntity movieDb = aDatabase.MovieEntity.SingleOrDefault(s => s.Id == Id);
            List<MovieWithGenre> genreDb = aDatabase.MovieWithGenres.Where(s => s.MovieEntityId == Id).ToList();
            List<MovieWithGenre> ToRemoveMovieWithGenre = new List<MovieWithGenre>();
            List<MovieWithGenre> ToAddMovieWithGenre = new List<MovieWithGenre>();
            movieDb.Player = movieEntity.Player;
            movieDb.Title = movieEntity.Title;
            movieDb.Tag = movieEntity.Tag;
            movieDb.Sound = movieEntity.Sound;
            movieDb.Score = movieEntity.Score;
            foreach (var eachGenreInDb in genreDb)
            {
                movieWithGenreMapById.Add(eachGenreInDb.MovieGenreEntityId, eachGenreInDb.MovieEntityId);
            }

            //add new select to list before puting to db.
            foreach (var newGenre in newSelectGenreList)
            {
                if (!movieWithGenreMapById.ContainsKey(newGenre.Id))
                {
                    //remove list;
                    ToAddMovieWithGenre.Add(new MovieWithGenre { MovieEntityId = Id, MovieGenreEntityId = newGenre.Id });
                }
                movieGenreInDbMapById.Add(newGenre.Id, Id);
            }
            aDatabase.MovieWithGenres.AddRange(ToAddMovieWithGenre);

            foreach (var oldGenre in genreDb)
            {
                if (!movieGenreInDbMapById.ContainsKey(oldGenre.MovieGenreEntityId))
                {
                    aDatabase.Entry(oldGenre).State = EntityState.Deleted;
                }
                
            }
            var resultFirst = aDatabase.SaveChanges();
            if(resultFirst >= 0) {return true; } else {return false;}
        }
    }
}
