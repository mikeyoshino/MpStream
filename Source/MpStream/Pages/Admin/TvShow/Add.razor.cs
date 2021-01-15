using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.TvShow
{
    public partial class Add : ComponentBase
    {
        [Inject]
        public TvShowService TvShowService { get; set; }
        [Inject]
        public MovieService MovieService { get; set; }
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
        public string ImageUrl { get; set; }
        public bool ApiRequestSpinner { get; set; } = false;
        public bool TvshowSaveSpinner { get; set; } = false;
        [Inject]
        public IWebHostEnvironment environment { get; set; }
        IBrowserFile SelectedImage;
        protected override async Task OnInitializedAsync()
        {
            TvShowGenreList = await TvShowService.TvShowGenreList();
        }

        async Task SaveShow()
        {
            TvshowSaveSpinner = true;
            if (ImageUrl != null)
            {
                Stream stream = SelectedImage.OpenReadStream();
                var extension = Path.GetExtension(SelectedImage.Name);
                var fileNameBasedOnDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                var path = $"{environment.WebRootPath}\\posters\\{fileNameBasedOnDate + extension}";
                FileStream fileStream = File.Create(path);
                await stream.CopyToAsync(fileStream);
                fileStream.Close();
                TvShowEntity.PosterImage = $"/posters/{fileNameBasedOnDate + extension}";

            }
            else if (PreviewImage != null)
            {
                var path = $"{environment.WebRootPath}" + "\\" + "Posters";
                var fileNameBasedOnDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                var apiImageUrl = $"https://image.tmdb.org/t/p/original/{PreviewImage}";
                await MovieService.DownloadImageAsync(path, fileNameBasedOnDate, new Uri(apiImageUrl));
                TvShowEntity.PosterImage = $"/Posters/{fileNameBasedOnDate + ".jpg"}";
            }
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
            TvshowSaveSpinner = false;
        }
        async Task UploadImageOnchange(InputFileChangeEventArgs e)
        {
            string format = "image/jpg";
            var imageFile = e.File;
            var resizeFile = await imageFile.RequestImageFileAsync(format, 660, 420);
            var buffer = new byte[resizeFile.Size];
            await resizeFile.OpenReadStream().ReadAsync(buffer);
            SelectedImage = imageFile;
            ImageUrl = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
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
            ApiRequestSpinner = true;
            
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
            ApiRequestSpinner = false;
        }


        async Task AddSeason()
        {
            NumberOfSeason += 1;
            SeasonList.Add( new Season { SeasonNumber = NumberOfSeason.ToString()});
        }
        async Task AddEpisode(string seasonNumber)
        {
            EpisodeList.Add(new Episode { SeasonNumber = seasonNumber});
        }
        async Task RemoveEpisode(string seasonNumber, string episodeSeasonNumber)
        {
            if(seasonNumber == episodeSeasonNumber)
            {
                var selectedEpsode = EpisodeList.Where(s => s.SeasonNumber == seasonNumber).Last();
                EpisodeList.Remove(selectedEpsode);
            }
        }
        async Task RemoveSeason(string Id)
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
