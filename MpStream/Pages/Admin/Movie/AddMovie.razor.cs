using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.Movie
{
    public partial class AddMovie : ComponentBase
    {
        public string ImdbId { get; set; }
        public MovieEntity MovieEntity { get; set; } = new MovieEntity();
        [Inject]
        public MovieService MovieService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<MovieGenreEntity> MovieGenres { get; private set; } = new List<MovieGenreEntity>();
        public List<string> SelectedGenreIds { get; set; } = new List<string>();
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
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://movie-database-imdb-alternative.p.rapidapi.com/?i=tt10539608&r=json"),
                Headers =
            {
                { "x-rapidapi-key", "095c60bedemsh1e527c4f4aadf50p17e5c8jsn4ac63cfa2017" },
                { "x-rapidapi-host", "movie-database-imdb-alternative.p.rapidapi.com" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }
    }
}

