using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.Movies
{
    public partial class EditMovie : ComponentBase
    {
        [Inject]
        public MovieService MovieService { get; set; }
        [Inject]
        public MovieGenreService MovieGenreService { get; set; }
        [Inject]
        public NavigationManager NavBar { get; set; }
        public string Message { get; set; }

        public MovieEntity MovieEntity { get; set; } = new MovieEntity();
        public List<MovieWithGenre> MovieWithGenreList { get; private set; } = new List<MovieWithGenre>();
        public List<MovieGenreEntity> MovieGenres { get; private set; } = new List<MovieGenreEntity>();
        public List<string> SelectedGenreIds { get; set; } = new List<string>();
        public List<int> existGenreIdList { get; set; } = new List<int>();
        public List<int> ExistGenreId { get; set; } = new List<int>();
        [Parameter]
        public int MovieId { get; set; }
        public string ImdbId { get; set; }
        public string PreviewImage { get; set; }
        public List<string> soundChoices = new List<string>() { "พากย์ไทย", "ซับไทย", "พากย์ไทย-ซับไทย", "อังกฤษ" };
        public string TitleTH { get; set; }

        protected override void OnInitialized()
        {
            MovieEntity = MovieService.EditMovie(MovieId);
            MovieGenres = MovieService.MovieGenreList();
            MovieWithGenreList = MovieService.GetGenreListByMovieId(MovieId);
            PopulateExistGenre(MovieWithGenreList);

            foreach (var genre in MovieWithGenreList)
            {
               SelectedGenreIds.Add(genre.MovieGenreEntityId.ToString());
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

        public void PopulateExistGenre(List<MovieWithGenre> genreList)
        {
            foreach (var eachGenre in genreList)
            {
                ExistGenreId.Add(eachGenre.MovieGenreEntityId);
            }
        }

        public void UpdateMovie()
        {
            List<MovieGenreEntity> newSelectedList = new List<MovieGenreEntity>();
            foreach (var genre in MovieGenres)
            {
                if (SelectedGenreIds.Contains(genre.Id.ToString()))
                {
                    newSelectedList.Add(new MovieGenreEntity { Id = genre.Id, Name = genre.Name, Tag = genre.Tag });
                }
            }
            var result = MovieService.UpdateMovie(MovieId, MovieEntity, newSelectedList);
            if (result) { NavBar.NavigateTo("admin/movie"); } else { NavBar.NavigateTo($"admin/movie/edit/{MovieId}"); Message = "Fail to update"; }
        }
        public void FetchImdbApi(string Id)
        {

        }

        public async Task HandleFileSelected() { }
    }

}
