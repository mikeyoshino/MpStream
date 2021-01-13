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
        public List<string> GenreNameList { get; set; } = new List<string>();
        public int NumberOfSeason { get; set; }
        public string ImdbId { get; set; }
        public string OperationMessage { get; set; }
        public string showStatusMessageApiRequest { get; set; }
        public string PreviewImage { get; set; }
        public List<string> soundChoices = new List<string>() { "พากย์ไทย", "ซับไทย", "พากย์ไทย-ซับไทย", "อังกฤษ" };
        protected override async Task OnInitializedAsync()
        {
            TvShowGenreList = await TvShowService.TvShowGenreList();
        }

        public void SaveShow()
        {
            TvShowEntity.Sound = "พากย์ไทย";
            TvShowEntity.PublishedDate = DateTime.Now;
            var saveTvShow = TvShowService.Create(TvShowEntity);
            var saveTvShowGenreList = TvShowService.SaveBulkTvShowGenre(TvShowEntity, SelectedGenreIds);
            var saveBulkSeason = TvShowService.InsertBulkSeason(TvShowEntity, SeasonList);
            var saveBulkEpisode = TvShowService.InsertBulkEpisode(TvShowEntity, EpisodeList);
            if (saveTvShow && saveTvShowGenreList) {
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
        public async Task FetchImdbApi(string Id)
        {
            var tvShowApiRequestTH = await TvShowService.FetchTmdbTvShowApi(Id, true);
            var tvShaowTrailerRequest = await TvShowService.FetchTmdbTrailerApi(Id);
            var tvShowApiRequestEnglish = await TvShowService.FetchTmdbTvShowApi(Id, false);
            if(tvShowApiRequestTH.isRequestSucceed && tvShowApiRequestEnglish.isRequestSucceed) {
                TvShowEntity.Title = tvShowApiRequestEnglish.Name;
                TvShowEntity.Backdrop_Path = tvShowApiRequestEnglish.Backdrop_path;
                if(tvShowApiRequestTH.Overview != null){
                    TvShowEntity.Description = tvShowApiRequestTH.Overview;
                } else {
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
                if (tvShaowTrailerRequest.Results.Length != 0) {
                    TvShowEntity.Trailer = tvShaowTrailerRequest.Results[0].Key;
                }
                var genresApiList = tvShowApiRequestEnglish.Genres;
                foreach (var eachGenre in TvShowGenreList) {
                    GenreNameList.Add(eachGenre.Name);
                }
                foreach (var eachGenre in genresApiList) {
                    if (!GenreNameList.Contains(eachGenre.Name)) {
                        //if category not exist create.
                        await TvShowService.CreateTvShowGenre(new TvShowGenre { Name = eachGenre.Name });
                        TvShowGenreList = await TvShowService.TvShowGenreList();
                        var getGenreBaseOnName = TvShowGenreList.Where(s => s.Name.Contains(eachGenre.Name)).FirstOrDefault();
                        SelectedGenreIds.Add(getGenreBaseOnName.Id.ToString());
                    } else {
                        //if category exist add to selectGenreIds List.
                        var getGenreBaseOnName = TvShowGenreList.Where(s => s.Name.Contains(eachGenre.Name)).FirstOrDefault();
                        SelectedGenreIds.Add(getGenreBaseOnName.Id.ToString());
                    }
                }
                showStatusMessageApiRequest = "เรียกข่้อมูลสำเร็จ";

            } else {
                showStatusMessageApiRequest = "เกิดข้อผิดพลาด เช็คไอดีให้ถูกต้อง";
            }
        }

        void HandleFileSelected()
        {

        }

        void AddSeason()
        {
            NumberOfSeason += 1;
            SeasonList.Add( new Season { SeasonNumber = NumberOfSeason.ToString()});
        }
        void AddEpisode(string seasonNumber)
        {
            EpisodeList.Add(new Episode { SeasonNumber = seasonNumber});
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
            Season selectedSeason = SeasonList.Where(s => s.SeasonNumber == Id).First();
            List<Episode> selectedEpisodes = EpisodeList.Where(s => s.SeasonNumber == Id).ToList();
            SeasonList.Remove(selectedSeason);
            foreach (var eachEpisode in selectedEpisodes)
            {
                EpisodeList.Remove(eachEpisode);
            }
        }
    }
}
