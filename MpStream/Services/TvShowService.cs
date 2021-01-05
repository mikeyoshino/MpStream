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

        public TvShowService(ApplicationDbContext database)
        {
            aDatabase = database;
        }
        #region TvShow Service
        public Task<List<TvShowEntity>> GetTvShowList()
        {
            return Task.FromResult(aDatabase.TvShowEntities.ToList());
        }
        public Task Create(TvShowEntity tvShowModel)
        {
            if(tvShowModel != null)
            {
                aDatabase.TvShowEntities.Add(tvShowModel);
                var result = aDatabase.SaveChanges();
                if(result >= 0)
                {
                    return Task.FromResult(true);
                }
            } else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(tvShowModel);
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

        public Task<bool> SaveBulkTvShowGenre(int tvShowId, List<int> GenreIdList)
        {
            foreach (var eachGenreId in GenreIdList)
            {
                TvShowWithGenreList.Add(new TvShowWithGenre { TvShowEntityId = tvShowId, TvShowGenreId = eachGenreId});
            }
            if(TvShowWithGenreList != null)
            {
                aDatabase.TvShowWithGenres.AddRange(TvShowWithGenreList);
                aDatabase.SaveChanges();
                return Task.FromResult(true);
            } else
            {
                return Task.FromResult(false);
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

        public Task<bool> CreateTvShow(TvShowGenre tvShowGenreModel)
        {
            if(tvShowGenreModel != null)
            {
                aDatabase.TvShowGenres.Add(tvShowGenreModel);
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
        #endregion
    }
}
