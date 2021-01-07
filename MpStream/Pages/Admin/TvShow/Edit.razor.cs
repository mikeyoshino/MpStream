using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.TvShow
{
    public partial class Edit : ComponentBase
    {
        [Inject]
        public TvShowService TvShowService { get; set; }
        public TvShowEntity TvShowEntity { get; set; } = new TvShowEntity();
        public List<Season> SeasonList { get; private set; } = new List<Season>();
        public List<Episode> EpisodeList { get; set; } = new List<Episode>();
        public List<TvShowWithGenre> TvShowWithGenreList { get; set; } = new List<TvShowWithGenre>();
        public List<TvShowGenre> TvShowGenreList { get; set; } = new List<TvShowGenre>();
        public List<string> SelectedGenreIds { get; set; } = new List<string>();
        public string ImdbId { get; set; }
        [Parameter]
        public int tvShowId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await FetchAllNecessaryData();
        }
        public async Task FetchAllNecessaryData()
        {
            TvShowEntity = await TvShowService.GetTvShowById(tvShowId);
            TvShowWithGenreList = await TvShowService.GetTvShowGenreListByShowId(tvShowId);
            TvShowGenreList = await TvShowService.TvShowGenreList();
            SeasonList = await TvShowService.GetSeasonListByTvShowId(tvShowId);
            EpisodeList = await TvShowService.GetEpisodeListBySeasonList(SeasonList);
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
        public async Task UpdateShow()
        {

        }
        public async Task AddSeason()
        {

        }
        public async Task AddEpisode(string seasonId)
        {

        }
        public async Task RemoveSeason(string Id)
        {

        }
        public async Task RemoveEpisode(string seasonNumber, string episodeSeasonNumber)
        {

        }
        public async Task FetchImdbApi(string ImdbId)
        {

        }

    }
}
