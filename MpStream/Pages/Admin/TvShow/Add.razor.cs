using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.TvShow
{
    public partial class Add : ComponentBase
    {
        [Inject]
        public TvShowService TvShowService { get; set; }
        public TvShowEntity TvShowEntity { get; set; } = new TvShowEntity();
        public List<TvShowGenre> TvShowGenreList { get; set; } = new List<TvShowGenre>();
        public List<string> SelectedGenreIds { get; set; }
        public List<Season> SeasonList { get; set; } = new List<Season>();
        public List<Episode> EpisodeList { get; set; } = new List<Episode>();
        public int NumberOfSeason { get; set; }

        public int NumberOfEpisode { get; set; }
        public string ImdbId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TvShowGenreList = await TvShowService.TvShowGenreList();
        }

        void SaveShow()
        {

        }
        void CheckboxClicked(string Id, object checkedValue)
        {
            if ((bool)checkedValue)
            {
                if (!SelectedGenreIds.Contains(Id))
                {
                    SelectedGenreIds.Add(Id);
                }
            }
            else
            {
                if (SelectedGenreIds.Contains(Id))
                {
                    SelectedGenreIds.Remove(Id);
                }
            }
        }
        void FetchImdbApi(string Id)
        {

        }

        void AddSeason()
        {
            NumberOfSeason += 1;
            SeasonList.Add( new Season {TvShowEntityId = TvShowEntity.Id, Name = NumberOfSeason.ToString()});
            if(NumberOfSeason > 1)
            {
                NumberOfEpisode -= NumberOfEpisode;
            }
        }
        void AddEpisode(string seasonId)
        {
            NumberOfEpisode += 1;
            EpisodeList.Add(new Episode { Name = NumberOfEpisode.ToString(), SeasonNumber = seasonId });
        }
        void RemoveEpisode(string seasonNumber, string episodeSeasonNumber)
        {
            if(seasonNumber == episodeSeasonNumber)
            {
                NumberOfEpisode -= 1;
                var selectedEpsode = EpisodeList.Where(s => s.SeasonNumber == seasonNumber).Last();
                EpisodeList.Remove(selectedEpsode);
            }
        }
        void RemoveSeason(string Id)
        {
            NumberOfSeason -= 1;
            var selectedSeason = SeasonList.Where(s => s.Name == Id).First();
            SeasonList.Remove(selectedSeason);
        }
    }
}
