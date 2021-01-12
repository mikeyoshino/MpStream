using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.TvShow
{
    public partial class Edit : ComponentBase
    {
        [Inject]
        public TvShowService TvShowService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public TvShowEntity TvShowEntity { get; set; } = new TvShowEntity();
        public List<Season> SeasonList { get; private set; } = new List<Season>();
        public List<Episode> EpisodeList { get; set; } = new List<Episode>();
        public List<TvShowWithGenre> TvShowWithGenreList { get; set; } = new List<TvShowWithGenre>();
        public List<TvShowGenre> TvShowGenreList { get; set; } = new List<TvShowGenre>();
        public List<string> SelectedGenreIds { get; set; } = new List<string>();
        public List<string> GenreNameList { get; set; }
        public string PreviewImage { get; set; }
        public string OperationMessage { get; set; }
        public string showStatusMessageApiRequest { get; set; }
        public List<string> soundChoices = new List<string>() { "พากย์ไทย", "ซับไทย", "พากย์ไทย-ซับไทย", "อังกฤษ" };
        public string ImdbId { get; set; }
        [Parameter]
        public int tvShowId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await FetchAllNecessaryData();
            foreach (var eachTvshowWithGenre in TvShowWithGenreList)
            {
                SelectedGenreIds.Add(eachTvshowWithGenre.TvShowGenreId.ToString());
            }
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
            List<TvShowGenre> newSelectedList = new List<TvShowGenre>();
            foreach (var genre in TvShowGenreList)
            {
                if (SelectedGenreIds.Contains(genre.Id.ToString()))
                {
                    newSelectedList.Add(new TvShowGenre { Id = genre.Id, Name = genre.Name });
                }
            }
            var result = await TvShowService.UpdateTvShow(TvShowEntity, newSelectedList);
            if (result) { 
                NavigationManager.NavigateTo("admin/movie"); 
            } else { 
                NavigationManager.NavigateTo($"admin/movie/edit/{TvShowEntity.Id}"); 
                OperationMessage = "Fail to update"; 
            }

        }
        public async Task AddSeason(int tvshowId)
        {
            int recentSeasonNumber = await TvShowService.GetRecentSeasonNumberBySeasonIdAndTvShowId(tvshowId);
            //check if season is exist in database, if exsit pick last season number and plus by one when user click add season.
            //if season doesnt have any record mean user delete all season. season start counting from one.
            if(recentSeasonNumber != 0)
            {
                recentSeasonNumber += 1;
                SeasonList.Add(new Season { Name = recentSeasonNumber.ToString() });
            }
            recentSeasonNumber += 1;
            SeasonList.Add(new Season { Name = recentSeasonNumber.ToString() });
        }
        public async Task AddEpisode(string seasonId)
        {
            EpisodeList.Add(new Episode { SeasonNumber = seasonId });
        }
        public async Task RemoveSeason(int seasonId, int tvShowId)
        {
            var result = await TvShowService.RemoveSeasonByIdAndTvShowId(seasonId, tvShowId);
            if (result)
            {
                OperationMessage = "ทำการลบสำเร็จ";
            }
            else
            {
                OperationMessage = "เกิดข้อผิดพลาด";
            }
            StateHasChanged();
        }
        public async Task RemoveEpisode(int seasonNumber, string episodeName)
        {
            var result = await TvShowService.RemovieEpisodeBySeasonIdAndSeasonNumber(seasonNumber, episodeName);
            if (result)
            {
                OperationMessage = "ทำการลบสำเร็จ";
            } else
            {
                OperationMessage = "เกิดข้อผิดพลาด";
            }
            StateHasChanged();
        }

        public async Task HandleFileSelected()
        {

        }
        public async Task FetchImdbApi(string Id)
        {
            var tvShowApiRequestTH = await TvShowService.FetchTmdbTvShowApi(Id, true);
            var tvShaowTrailerRequest = await TvShowService.FetchTmdbTrailerApi(Id);
            var tvShowApiRequestEnglish = await TvShowService.FetchTmdbTvShowApi(Id, false);
            if (tvShowApiRequestTH.isRequestSucceed && tvShowApiRequestEnglish.isRequestSucceed)
            {
                TvShowEntity.Title = tvShowApiRequestEnglish.Name;
                TvShowEntity.Backdrop_Path = tvShowApiRequestEnglish.Backdrop_path;
                if (tvShowApiRequestTH.Overview != null)
                {
                    TvShowEntity.Description = tvShowApiRequestTH.Overview;
                }
                else
                {
                    TvShowEntity.Description = tvShowApiRequestEnglish.Overview;
                }
                TvShowEntity.Description = tvShowApiRequestEnglish.Overview;
                TvShowEntity.FirstAirDate = tvShowApiRequestEnglish.First_air_date;
                TvShowEntity.NumberOfEpisode = tvShowApiRequestEnglish.Number_of_episodes;
                TvShowEntity.NumberOfSeason = tvShowApiRequestEnglish.Number_of_seasons;
                TvShowEntity.Score = tvShowApiRequestEnglish.Vote_average;
                TvShowEntity.VoteCount = tvShowApiRequestEnglish.Vote_count;
                TvShowEntity.Status = tvShowApiRequestEnglish.Status;
                PreviewImage = tvShowApiRequestEnglish.Poster_path;
                if (tvShaowTrailerRequest.Results.Length != 0)
                {
                    TvShowEntity.Trailer = tvShaowTrailerRequest.Results[0].Key;
                }
                var genresApiList = tvShowApiRequestEnglish.Genres;
                foreach (var eachGenre in TvShowGenreList)
                {
                    GenreNameList.Add(eachGenre.Name);
                }
                foreach (var eachGenre in genresApiList)
                {
                    if (!GenreNameList.Contains(eachGenre.Name))
                    {
                        //if category not exist create.
                        await TvShowService.CreateTvShowGenre(new TvShowGenre { Name = eachGenre.Name });
                        TvShowGenreList = await TvShowService.TvShowGenreList();
                        var getGenreBaseOnName = TvShowGenreList.Where(s => s.Name.Contains(eachGenre.Name)).FirstOrDefault();
                        SelectedGenreIds.Add(getGenreBaseOnName.Id.ToString());
                    }
                    else
                    {
                        //if category exist add to selectGenreIds List.
                        var getGenreBaseOnName = TvShowGenreList.Where(s => s.Name.Contains(eachGenre.Name)).FirstOrDefault();
                        SelectedGenreIds.Add(getGenreBaseOnName.Id.ToString());
                    }
                }
                showStatusMessageApiRequest = "เรียกข่้อมูลสำเร็จ";

            }
            else
            {
                showStatusMessageApiRequest = "เกิดข้อผิดพลาด เช็คไอดีให้ถูกต้อง";
            }
        }
    }
}
