using MpStream.Data;
using MpStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Services
{
    public class TvShowService
    {
        private readonly ApplicationDbContext aDatabase;
        private TvShowEntity TvShowModel = new TvShowEntity();
        private List<TvShowWithGenre> TvShowWithGenreList = new List<TvShowWithGenre>();
        public List<Episode> EpisodeList = new List<Episode>();
        private int Result;

        public TvShowService(ApplicationDbContext database)
        {
            aDatabase = database;
        }
        #region TvShow Service
        public Task<List<TvShowEntity>> GetTvShowList()
        {
            return Task.FromResult(aDatabase.TvShowEntities.ToList());
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
        public Task<bool> UpdateTvShow(int tvShowId, TvShowEntity tvShowModel, List<TvShowGenre> newSelectedGenreList)
        {
            var tvShowInDb = aDatabase.TvShowEntities.SingleOrDefault(s => s.Id == tvShowId);
            tvShowInDb.NumberOfSeason = tvShowModel.NumberOfSeason;
            tvShowInDb.Sound = tvShowModel.Sound;
            tvShowInDb.Status = tvShowModel.Status;
            tvShowInDb.Title = tvShowModel.Title;
            tvShowInDb.Trailer = tvShowModel.Trailer;
            return Task.FromResult(true);

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
        #endregion
    }
}
