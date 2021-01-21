using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MpStream.Pages.Members
{
    public partial class Detail : ComponentBase
    {
        [Inject]
        public MovieService MovieService { get; set; }
        public bool ShowDialog { get; set; } = false;
        public MovieEntity MovieEntity { get; set; } = new MovieEntity();
        public List<MovieEntity> RelatedMovies { get; set; } = new List<MovieEntity>();

        [Parameter]
        public int MovieId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            MovieEntity = MovieService.GetMovieById(MovieId);
            RelatedMovies = await MovieService.RelatedMovies(MovieId, MovieEntity.MovieWithGenres);
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