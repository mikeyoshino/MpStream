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
        [Inject]
        public NavigationManager NavBar { get; set; }
        public TvShowEntity TvShowEntity { get; set; } = new TvShowEntity();
        public List<TvShowGenre> TvShowGenreList { get; set; } = new List<TvShowGenre>();
        public List<string> SelectedGenreIds { get; set; } = new List<string>();
        public List<Season> SeasonList { get; set; } = new List<Season>();
        public List<Episode> EpisodeList { get; set; } = new List<Episode>();
        public int NumberOfSeason { get; set; }
        public string ImdbId { get; set; }
        public string OperationMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TvShowGenreList = await TvShowService.TvShowGenreList();
        }

        public void SaveShow()
        {
            var saveTvShow = TvShowService.Create(TvShowEntity);
            var saveTvShowGenreList = TvShowService.SaveBulkTvShowGenre(TvShowEntity, SelectedGenreIds);
            var saveBulkSeason = TvShowService.InsertBulkSeason(TvShowEntity, SeasonList);
            var saveBulkEpisode = TvShowService.InsertBulkEpisode(TvShowEntity, EpisodeList);
            if (saveTvShow) {
                OperationMessage = "Inserted TvShow Succesfully!";
                NavBar.NavigateTo("/admin/tvshow");
            } else
            {
                OperationMessage = "Failed to create!";
            }
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
            SeasonList.Add( new Season { Name = NumberOfSeason.ToString()});
        }
        void AddEpisode(string seasonId)
        {
            EpisodeList.Add(new Episode { SeasonNumber = seasonId });
        }
        void RemoveEpisode(string seasonNumber, string episodeSeasonNumber)
        {
            if(seasonNumber == episodeSeasonNumber)
            {
                var selectedEpsode = EpisodeList.Where(s => s.SeasonNumber == seasonNumber).Last();
                EpisodeList.Remove(selectedEpsode);
            }
        }
        void RemoveSeason(string Id)
        {
            NumberOfSeason -= 1;
            Season selectedSeason = SeasonList.Where(s => s.Name == Id).First();
            List<Episode> selectedEpisodes = EpisodeList.Where(s => s.SeasonNumber == Id).ToList();
            SeasonList.Remove(selectedSeason);
            foreach (var eachEpisode in selectedEpisodes)
            {
                EpisodeList.Remove(eachEpisode);
            }
        }
    }
}
