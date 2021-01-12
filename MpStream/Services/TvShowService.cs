using Microsoft.EntityFrameworkCore;
using MpStream.Data;
using MpStream.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MpStream.Services
{
    public class TvShowService
    {
        private readonly ApplicationDbContext aDatabase;
        private TvShowEntity TvShowModel = new TvShowEntity();
        private List<TvShowWithGenre> TvShowWithGenreList = new List<TvShowWithGenre>();
        public List<Episode> EpisodeList = new List<Episode>();
        public TmdbTvShowModel TmdbTvShow { get; set; } = new TmdbTvShowModel();
        public Video TmdbVideoModel = new Video();
        private int Result;

        public TvShowService(ApplicationDbContext database)
        {
            aDatabase = database;
        }
        #region TvShow Service
        public Task<List<TvShowEntity>> GetTvShowList()
        {
            return Task.FromResult(aDatabase.TvShowEntities.Include("TvShowWithGenres").ToList());
        }
        public bool Create(TvShowEntity tvShowModel)
        {
            if(tvShowModel != null)
            {
                aDatabase.TvShowEntities.Add(tvShowModel);
                Result = aDatabase.SaveChanges();
            }
            if (Result >= 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public Task<TvShowEntity> GetTvShowById(int Id)
        {
            TvShowModel = aDatabase.TvShowEntities.SingleOrDefault(t => t.Id == Id);
            return Task.FromResult(TvShowModel);
        }
        public Task Delete(int Id)
        {
            TvShowModel = aDatabase.TvShowEntities.Find(Id);
            if(TvShowModel != null)
            {
                aDatabase.TvShowEntities.Remove(TvShowModel);
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public bool SaveBulkTvShowGenre(TvShowEntity tvShowModel, List<string> GenreIdList)
        {
            foreach (var eachGenreId in GenreIdList)
            {
                TvShowWithGenreList.Add(new TvShowWithGenre { TvShowEntityId = tvShowModel.Id, TvShowGenreId = Convert.ToInt32(eachGenreId)});
            }
            if(TvShowWithGenreList != null)
            {
                aDatabase.TvShowWithGenres.AddRange(TvShowWithGenreList);
                aDatabase.SaveChanges();
                return true;
            } else
            {
                return false;
            }
        }
        public Task<bool> UpdateTvShow(TvShowEntity tvShowModel, List<TvShowGenre> newSelectGenreList)
        {
            List<TvShowWithGenre> GenreRemoveList = new List<TvShowWithGenre>();
            Dictionary<int, int> tvshowWithGenreMapById = new Dictionary<int, int>();
            Dictionary<int, int> tvshowGenreInDbMapById = new Dictionary<int, int>();
            TvShowEntity tvshowInDb = aDatabase.TvShowEntities.SingleOrDefault(s => s.Id == tvShowModel.Id);
            List<TvShowWithGenre> genreDb = aDatabase.TvShowWithGenres.Where(s => s.TvShowEntityId == tvShowModel.Id).ToList();
            List<TvShowWithGenre> ToRemoveTvshowWithGenre = new List<TvShowWithGenre>();
            List<TvShowWithGenre> ToAddTvshowWithGenre = new List<TvShowWithGenre>();
            tvshowInDb.NumberOfSeason = tvShowModel.NumberOfSeason;
            tvshowInDb.Sound = tvShowModel.Sound;
            tvshowInDb.Status = tvShowModel.Status;
            tvshowInDb.Title = tvShowModel.Title;
            tvshowInDb.Trailer = tvShowModel.Trailer;
            tvshowInDb.Backdrop_Path = tvShowModel.Backdrop_Path;
            tvshowInDb.Description = tvShowModel.Description;
            tvshowInDb.EpisodeRunTime = tvShowModel.EpisodeRunTime;
            tvshowInDb.FirstAirDate = tvShowModel.FirstAirDate;
            tvshowInDb.NumberOfEpisode = tvShowModel.NumberOfEpisode;
            tvshowInDb.Score = tvShowModel.Score;
            tvshowInDb.VoteCount = tvShowModel.VoteCount;
            tvshowInDb.PublishedDate = tvShowModel.PublishedDate;
            foreach (var eachGenreInDb in genreDb)
            {
                tvshowWithGenreMapById.Add(eachGenreInDb.TvShowGenreId, eachGenreInDb.TvShowEntityId);
            }
            //add new select to list before puting to db.
            foreach (var newGenre in newSelectGenreList)
            {
                if (!tvshowWithGenreMapById.ContainsKey(newGenre.Id))
                {
                    //remove list;
                    ToAddTvshowWithGenre.Add(new TvShowWithGenre { TvShowEntityId = tvShowModel.Id, TvShowGenreId = newGenre.Id });
                }
                tvshowGenreInDbMapById.Add(newGenre.Id, tvShowModel.Id);
            }
            aDatabase.TvShowWithGenres.AddRange(ToAddTvshowWithGenre);

            foreach (var oldGenre in genreDb)
            {
                if (!tvshowGenreInDbMapById.ContainsKey(oldGenre.TvShowGenreId))
                {
                    aDatabase.Entry(oldGenre).State = EntityState.Deleted;
                }

            }
            var resultFirst = aDatabase.SaveChanges();
            if (resultFirst >= 0) 
            { 
                return Task.FromResult(true); 
            } else 
            { 
                return Task.FromResult(false); 
            }
        }

        public async Task<TmdbTvShowModel> FetchTmdbTvShowApi(string movieId, bool isThaiApi)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            if (isThaiApi){
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(string.Format("https://api.themoviedb.org/3/tv/{0}?api_key=09e414c534d47f74def83dd9fa03909c&language=th", movieId));
            } else {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(string.Format("https://api.themoviedb.org/3/tv/{0}?api_key=09e414c534d47f74def83dd9fa03909c", movieId));
            }
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    TmdbTvShow = JsonConvert.DeserializeObject<TmdbTvShowModel>(body);
                } else
                {
                    TmdbTvShow = new TmdbTvShowModel { isRequestSucceed = false };
                }
                
                return await Task.FromResult(TmdbTvShow);
            }
        }
        public async Task<Video> FetchTmdbTrailerApi(string movieId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format("http://api.themoviedb.org/3/tv/{0}/videos?api_key=09e414c534d47f74def83dd9fa03909c", movieId)),
            };
            using (var response = await client.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();
                TmdbVideoModel = JsonConvert.DeserializeObject<Video>(body);
                Console.WriteLine(body);
                return await Task.FromResult(TmdbVideoModel);
            }
        }
        public Task<int> CountTvShow()
        {
            return Task.FromResult(aDatabase.TvShowEntities.Count());
        }
        public async Task<List<TvShowEntity>> GetTvShowListIndexPage(int pageSize)
        {
            List<TvShowEntity> tvShowList;
            tvShowList = await aDatabase.TvShowEntities.OrderByDescending(c => c.Id).Take(pageSize).ToListAsync();
            return tvShowList;
        }
        public async Task<List<TvShowEntity>> GetMovieListByPage(int pageSize, int pageIndex)
        {
            List<TvShowEntity> tvShowList;
            if (pageIndex == 1) //mean click on page 1.
            {
                tvShowList = await aDatabase.TvShowEntities.OrderBy(c => c.Id).Take(pageSize).ToListAsync();
            }
            else
            {
                tvShowList = await aDatabase.TvShowEntities.OrderBy(c => c.Id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            }
            return tvShowList;
        }

        public async Task<List<TvShowEntity>> SearchByWords(string keywords)
        {
            List<TvShowEntity> tvShowList;
            tvShowList = await aDatabase.TvShowEntities.Where(q => (q.Title).ToLower().Contains(keywords.ToLower())).ToListAsync();
            return tvShowList;
        }
        #endregion

        #region TvShpowGenre Service
        public Task<List<TvShowGenre>> TvShowGenreList()
        {
            return Task.FromResult(aDatabase.TvShowGenres.ToList());
        }

        public Task<bool> CreateTvShowGenre(TvShowGenre tvShowGenreModel)
        {
            if(tvShowGenreModel != null)
            {
                aDatabase.TvShowGenres.Add(tvShowGenreModel);
                aDatabase.SaveChanges();
                return Task.FromResult(true);
            } else
            {
                return Task.FromResult(false);
            }
        }
        public Task<bool> EditTvGenreById(int Id)
        {
            var tvShowGenre = aDatabase.TvShowGenres.SingleOrDefault(s => s.Id == Id);
            if(tvShowGenre != null)
            {
                return Task.FromResult(true);
            } else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> DeleteTvGenreById(int Id)
        {
            var tvShowGenre = aDatabase.TvShowGenres.Find(Id);
            if(tvShowGenre != null)
            {
                aDatabase.TvShowGenres.Remove(tvShowGenre);
                aDatabase.SaveChanges();
                return Task.FromResult(true);
            } else
            {
                return Task.FromResult(false);
            }
        }
        public Task<TvShowGenre> GetTvShowGenreById(int Id)
        {
            return Task.FromResult(aDatabase.TvShowGenres.SingleOrDefault(s => s.Id == Id));
        }

        public Task<List<TvShowWithGenre>> GetTvShowGenreListByShowId(int Id)
        {
            var showGenreList = aDatabase.TvShowWithGenres.Where(s => s.TvShowEntityId == Id).ToList();
            return Task.FromResult(showGenreList);
        }


        #endregion

        #region Season Service
        public bool InsertBulkSeason(TvShowEntity tvshowModel, List<Season> seasonList)
        {
            List<Season> addTvIdToSeasonList = new List<Season>();
            foreach (var eachSeason in seasonList)
            {
                addTvIdToSeasonList.Add(new Season
                {
                    Name = eachSeason.Name,
                    NumberOfEpisode = eachSeason.NumberOfEpisode,
                    PublishedDate = eachSeason.PublishedDate,
                    TvShowEntityId = tvshowModel.Id
                });
            }
            aDatabase.Seasons.AddRange(addTvIdToSeasonList);
            var result = aDatabase.SaveChanges();
            if (result >= 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public List<Season> GetSeasonList()
        {
            return aDatabase.Seasons.ToList();
        }
        public void RemoveSeasonById(int Id)
        {
            var season = aDatabase.Seasons.SingleOrDefault(s => s.Id == Id);
            var result = aDatabase.Seasons.Remove(season);
        }

        public Task<List<Season>> GetSeasonListByTvShowId(int Id)
        {
            var seasonList = aDatabase.Seasons.Where(s => s.TvShowEntityId == Id).ToList();
            return Task.FromResult(seasonList);
        }
        public Task<int> GetRecentSeasonNumberBySeasonIdAndTvShowId(int tvshowId)
        {
            var getSeasonByParams = aDatabase.Seasons.Where(i => i.TvShowEntityId == tvshowId).Last();
            if (getSeasonByParams != null)
            {
                return Task.FromResult(Convert.ToInt32(getSeasonByParams.Name));
            }
            else
            {
                //no record return 0.
                return Task.FromResult(0);
            }
        }
        #endregion

        #region Episode Service
        public bool InsertBulkEpisode(TvShowEntity tvshowModel, List<Episode> episodeList)
        {
            List<Episode> finalEpisodeListBeforeInsert = new List<Episode>();
            //Get Season list based on tv show Id.
            var seasonListInDb = aDatabase.Seasons.Where(s => s.TvShowEntityId == tvshowModel.Id).ToList();
            //add to new list before inserting to epsiode db.
            foreach (var eachEpisode in episodeList)
            {
                var seasonId = seasonListInDb.Where(s => s.Name == eachEpisode.SeasonNumber).First();
                finalEpisodeListBeforeInsert.Add(new Episode { 
                    Language = eachEpisode.Language, 
                    Name = eachEpisode.Name, 
                    PublishedDate = eachEpisode.PublishedDate, 
                    SeasonId = seasonId.Id,
                    EmbedLink = eachEpisode.EmbedLink,
                    SeasonNumber = eachEpisode.SeasonNumber
                    
                });
            }
            aDatabase.Episodes.AddRange(finalEpisodeListBeforeInsert);
            var result = aDatabase.SaveChanges();
            if (result >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<List<Episode>> GetEpisodeListBySeasonList(List<Season> seasonList)
        {
            foreach (var eachSeason in seasonList)
            {
                var eachEpisode = aDatabase.Episodes.Where(s => s.SeasonId == eachSeason.Id).ToList();
                EpisodeList.AddRange(eachEpisode);
            }
            return Task.FromResult(EpisodeList);
        }

        public Task<bool> RemovieEpisodeBySeasonIdAndSeasonNumber(int seasonId, string episodeName)
        { 
            var getEpByParameters = aDatabase.Episodes.Where(s => s.SeasonId == seasonId).Where(i => i.Name.Contains(episodeName)).FirstOrDefault();
            if(getEpByParameters != null)
            {
                aDatabase.Episodes.Remove(getEpByParameters);
                aDatabase.SaveChanges();
                return Task.FromResult(true);
            } else
            {
                return Task.FromResult(false);
            }
        }
        public Task<bool> RemoveSeasonByIdAndTvShowId(int seasonId, int tvShowId)
        {
            var getSeasonByParams = aDatabase.Seasons.Where(s => s.Id == seasonId).Where(i => i.TvShowEntityId == tvShowId).FirstOrDefault();
            if(getSeasonByParams != null)
            {
                aDatabase.Seasons.Remove(getSeasonByParams);
                aDatabase.SaveChanges();
                return Task.FromResult(true);
            } else
            {
                return Task.FromResult(false);
            }
        }
        #endregion
    }
}
