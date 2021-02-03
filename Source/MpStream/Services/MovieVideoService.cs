using MpStream.Data;
using MpStream.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Services
{
    public class MovieVideoService
    {
        private readonly ApplicationDbContext Database;
        public MovieVideoService(ApplicationDbContext aDatabase)
        {
            Database = aDatabase;
        }
        public Task<List<MovieLanguage>> GetMovieLanguages()
        {
            var movieLangs = Database.MovieLanguages.ToList();
            return Task.FromResult(movieLangs);
        }

        public Task<MovieLanguage> GetMovieLanguage(int Id)
        {
            var movieLangs = Database.MovieLanguages.Where(x => x.Id == Id).SingleOrDefault();
            return Task.FromResult(movieLangs);
        }

        public Task<bool> SaveLanguage(MovieLanguage movieLang)
        {
            if(movieLang is not null)
            {
                Database.MovieLanguages.Add(movieLang);
                Database.SaveChanges();
                return Task.FromResult(true);
            } else
            {
                return Task.FromResult(false);
            }
        }
        public Task<bool> RemoveLanguage(int langId)
        {
            var lang = Database.MovieLanguages.Where(x => x.Id == langId).SingleOrDefault();
            Database.Remove(lang);
            var reuslt = Database.SaveChanges();
            if(reuslt > 0)
            {
                return Task.FromResult(true);
            }else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> UpdateLanguage(MovieLanguage movieLang)
        {
            if (movieLang is not null)
            {
                var langInDb = Database.MovieLanguages.Where(x => x.Id == movieLang.Id).SingleOrDefault();
                langInDb.LanguageName = movieLang.LanguageName;
                Database.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
