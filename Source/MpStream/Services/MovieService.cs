using Microsoft.EntityFrameworkCore;
using MpStream.Data;
using MpStream.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
        public TmdbMovieModel TmdbMovieModel = new TmdbMovieModel();
        public Video VideoModel = new Video();
        public HttpClient httpClient { get; set; }
        public List<MovieWithGenre> GenreRemoveList = new List<MovieWithGenre>();
        public Dictionary<int, int> movieWithGenreMapById = new Dictionary<int, int>();
        public Dictionary<int, int> movieGenreInDbMapById = new Dictionary<int, int>();
        public List<MovieWithGenre> ToRemoveMovieWithGenre = new List<MovieWithGenre>();
        public List<MovieWithGenre> ToAddMovieWithGenre = new List<MovieWithGenre>();
        public Dictionary<int, string> genreNameMapById = new Dictionary<int, string>();
        public List<MovieEntity> MovieList = new List<MovieEntity>();
        public List<MovieEntity> BrowseList = new List<MovieEntity>();
        public MovieService(){}
        public MovieService(ApplicationDbContext database){aDatabase = database;}

        public Task<bool> AddMovie(MovieEntity movieEntity)
        {
            aDatabase.MovieEntity.Add(movieEntity);
            var result = aDatabase.SaveChanges();
            return Task.FromResult(result > 0);
        }

        public List<MovieEntity> GetMovieList()
        {
            MovieList = aDatabase.MovieEntity.Include("MovieWithGenres").ToList();
            return MovieList;
        }
        public Task<List<int>> MovieYears()
        {
            var years = aDatabase.MovieEntity.Select(s => s.ReleaseYear).ToListAsync();
            return years;
        }

        public Task<List<MovieEntity>> GetMovieListLimitPostNumber(int postDisplayNumber)
        {
            MovieList = aDatabase.MovieEntity.Include("MovieWithGenres").Take(postDisplayNumber).ToList();
            return Task.FromResult(MovieList);
        }

        public Task<int> CountMovies()
        {
            return Task.FromResult(aDatabase.MovieEntity.Count());
        }
        public async Task DownloadImageAsync(string directoryPath, string fileName, Uri uri)
        {
            using var httpClient = new HttpClient();

            // Get the file extension
            var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
            var fileExtension = Path.GetExtension(uriWithoutQuery);

            // Create file path and ensure directory exists
            var path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
            Directory.CreateDirectory(directoryPath);

            // Download the image and write to the file
            var imageBytes = await httpClient.GetByteArrayAsync(uri);
            await File.WriteAllBytesAsync(path, imageBytes);
        }
        public async Task<List<MovieEntity>> GetMovieListIndexPage(int pageSize)
        {
            List<MovieEntity> movieList;
            movieList = await aDatabase.MovieEntity.Include("MovieWithGenres").OrderByDescending(c => c.Id).Take(pageSize).ToListAsync();
            return movieList;
        }
        public async Task<List<MovieEntity>> GetMovieListByPage(int pageSize, int pageIndex)
        {
            List<MovieEntity> movieList;
            if(pageIndex == 1) //mean click on page 1.
            {
                movieList = await aDatabase.MovieEntity.Include("MovieWithGenres").OrderBy(c => c.Id).Take(pageSize).ToListAsync();
            }
            else
            {
                movieList = await aDatabase.MovieEntity.Include("MovieWithGenres").OrderBy(c => c.Id).Skip(pageSize * (pageIndex-1)).Take(pageSize).ToListAsync();
            }
            return movieList;
        }

        public async Task<List<MovieEntity>> SearchByWords (string keywords)
        {
            List<MovieEntity> movieList;
            movieList = await aDatabase.MovieEntity.Where(q => (q.Title).ToLower().Contains(keywords.ToLower())).ToListAsync();
            return movieList;
        }

        public MovieEntity GetMovieById(int Id)
        {
            MovieEntity = aDatabase.MovieEntity.Include(s => s.MovieWithGenres).ThenInclude(s => s.MovieGenreEntity).SingleOrDefault(s => s.Id == Id);
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
            MovieWithGenres.Clear();
            genreNameMapById = new Dictionary<int, string>();
            var genreList = aDatabase.MovieGenreEntities.ToList();
            foreach (var eachGenre in genreList)
            {
                genreNameMapById.Add(eachGenre.Id, eachGenre.Name);
            }
            foreach (var eachId in Ids)
            {
                if (genreNameMapById.ContainsKey(Convert.ToInt32(eachId)))
                {
                    MovieWithGenres.Add(new MovieWithGenre { MovieEntityId = movie.Id, MovieGenreEntityId = Convert.ToInt32(eachId), GenreName = genreNameMapById[Convert.ToInt32(eachId)]});
                }
            }
            aDatabase.MovieWithGenres.AddRange(MovieWithGenres);
            var reuslt = aDatabase.SaveChanges();
            //List wont be clear when genres are saved, what? need to clear it everytime after we save genre so new post wont have this list.
            MovieWithGenres.Clear();
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
            
            var movieDb = aDatabase.MovieEntity.SingleOrDefault(s => s.Id == Id);
            var genreDb = aDatabase.MovieWithGenres.Where(s => s.MovieEntityId == Id).ToList();
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

        public async Task<TmdbMovieModel> FetchTmdbApi(string movieId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format("https://api.themoviedb.org/3/movie/{0}?api_key=09e414c534d47f74def83dd9fa03909c&language=th", movieId)),
            };
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    TmdbMovieModel = JsonConvert.DeserializeObject<TmdbMovieModel>(body);
                }
                else
                {
                    TmdbMovieModel = new TmdbMovieModel { isRequestSucceed = false };
                }
                return await Task.FromResult(TmdbMovieModel);
            }
        }
        public async Task<TmdbMovieModel> FetchTmdbApiEnglish(string movieId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format("https://api.themoviedb.org/3/movie/{0}?api_key=09e414c534d47f74def83dd9fa03909c", movieId)),
            };
            using (var response = await client.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();
                TmdbMovieModel = JsonConvert.DeserializeObject<TmdbMovieModel>(body);
                Console.WriteLine(body);
                return await Task.FromResult(TmdbMovieModel);
            }
        }
        public async Task<Video> FetchTmdbTrailerApi(string movieId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format("http://api.themoviedb.org/3/movie/{0}/videos?api_key=09e414c534d47f74def83dd9fa03909c", movieId)),
            };
            using (var response = await client.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();
                VideoModel = JsonConvert.DeserializeObject<Video>(body);
                Console.WriteLine(body);
                return await Task.FromResult(VideoModel);
            }
        }

        public Task<List<MovieEntity>> BrowseMovieByCategory(string genreName)
        {
            BrowseList.Clear();
            var searchWord = genreName.Replace("-", " ");
            MovieList = aDatabase.MovieEntity.ToList();
            var movieIds = aDatabase.MovieWithGenres.Where(s => s.GenreName.ToLower().StartsWith(searchWord)).Select(s => s.MovieEntityId).ToList();
            foreach (var eachMovieId in movieIds)
            {
                var movieByCategory = MovieList.Where(s => s.Id == eachMovieId).Single();
                BrowseList.Add(movieByCategory);
            }
            return Task.FromResult(BrowseList);
        }
    }
}
