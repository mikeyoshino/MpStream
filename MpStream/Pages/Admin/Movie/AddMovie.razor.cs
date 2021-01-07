using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.Movie
{
    public partial class AddMovie : ComponentBase
    {
        public string ImdbId { get; set; }
        public string TitleTH {private get; set; }
        public string PreviewImage { get; set; }
        public MovieEntity MovieEntity { get; set; } = new MovieEntity();
        [Inject]
        public MovieService MovieService { get; set; }
        [Inject]
        public MovieGenreService MovieGenreService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<MovieGenreEntity> MovieGenres { get; private set; } = new List<MovieGenreEntity>();
        public List<string> GenreStringList { get; set; } = new List<string>();
        public List<string> SelectedGenreIds { get; set; } = new List<string>();
        public List<string> soundChoices = new List<string>() { "พากย์ไทย", "ซับไทย", "พากย์ไทย-ซับไทย", "อังกฤษ" };
        public byte[] PosterImage { get; set; }
        protected override void OnInitialized()
        {
            MovieGenres = MovieService.MovieGenreList();
        }

        void SaveMovie()
        {
            MovieEntity.PublishedDate = DateTime.Now;
            MovieEntity.IsFeatured = false;
            var resuleSaveMovie = MovieService.AddMovie(MovieEntity);
            var resultSaveGenre = MovieService.SaveMovieWithGenre(MovieEntity, SelectedGenreIds);
            if (resuleSaveMovie.IsCompleted && resultSaveGenre.IsCompleted)
            {
                NavigationManager.NavigateTo("/admin/movie");
            }
        }

        async Task HandleFileSelected(IFileListEntry[] files)
        {
            var file = files.FirstOrDefault();
            if(file != null)
            {
                var ms = new MemoryStream();
                await file.Data.CopyToAsync(ms);
                PosterImage = ms.ToArray();
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
            var movieDataTH = await MovieService.FetchTmdbApi(Id);
            var videoTrailer = await MovieService.FetchTmdbTrailerApi(Id);
            MovieEntity.Title = movieDataTH.Original_title;
            MovieEntity.Description = movieDataTH.Overview;
            MovieEntity.Score = movieDataTH.Vote_average;
            MovieEntity.Runtime = movieDataTH.Runtime;
            MovieEntity.Revenue = movieDataTH.Revenue;
            MovieEntity.Vote_count = movieDataTH.Vote_count;
            MovieEntity.ReleaseYear = movieDataTH.Release_date.Year;
            PreviewImage = movieDataTH.Poster_path;
            TitleTH = movieDataTH.Title;
            var movieDataEnglish = await MovieService.FetchTmdbApiEnglish(Id);
            if (videoTrailer.Results != null)
            {
                MovieEntity.TrailerId = videoTrailer.Results[0].Key;
            }
            var genreInEnglish = movieDataEnglish.Genres;
            foreach (var eachGenre in MovieGenres)
            {
                GenreStringList.Add(eachGenre.Name);
            }
            foreach (var genre in genreInEnglish)
            {
                if (!GenreStringList.Contains(genre.Name))
                {
                   await MovieGenreService.AddGenre(new MovieGenreEntity { Name = genre.Name });
                    MovieGenres = MovieGenreService.GetGenreList();
                    var genreModel = MovieGenres.Where(s => s.Name.Contains(genre.Name)).FirstOrDefault();
                    SelectedGenreIds.Add(genreModel.Id.ToString());
                } else if (GenreStringList.Contains(genre.Name))
                {
                    var genreModel = MovieGenres.Where(s => s.Name.Contains(genre.Name)).FirstOrDefault();
                    SelectedGenreIds.Add(genreModel.Id.ToString());
                }
            }
            //var client = new HttpClient();
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri($"https://movie-database-imdb-alternative.p.rapidapi.com/?i=tt10539608&r=json"),
            //    Headers =
            //{
            //    { "x-rapidapi-key", "095c60bedemsh1e527c4f4aadf50p17e5c8jsn4ac63cfa2017" },
            //    { "x-rapidapi-host", "movie-database-imdb-alternative.p.rapidapi.com" },
            //},
            //};
            //using (var response = await client.SendAsync(request))
            //{
            //    response.EnsureSuccessStatusCode();
            //    var body = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(body);
            //}
        }
    }
}

