using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MpStream.Pages.Member.TvShow
{
    public partial class Detail : ComponentBase
    {
        [Parameter]
        public int TvshowId { get; set; }
        [Inject]
        public TvShowService TvShowService { get; set; }
        public TvShowEntity TvshowModel { get; set; } = new TvShowEntity();
        public List<Season> SeasonList { get; set; } = new List<Season>();
        public List<Episode> EpisodeList { get; set; } = new List<Episode>();
        public List<TvShowGenre> TvShowGenreList { get; set; } = new List<TvShowGenre>();
        public bool ShowDialog { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            TvshowModel = await TvShowService.GetTvShowById(TvshowId);
            SeasonList = await TvShowService.GetSeasonListByTvShowId(TvshowId);
            EpisodeList = await TvShowService.GetEpisodeListBySeasonList(SeasonList);
            TvShowGenreList = await TvShowService.GetGenreListByTvshowWithGenres(TvshowModel.TvShowWithGenres);

        }

        void ShowUpDialog()
        {
            ShowDialog = true;
        }
        void CloseDialog()
        {
            ShowDialog = false;
        }
    }
}
