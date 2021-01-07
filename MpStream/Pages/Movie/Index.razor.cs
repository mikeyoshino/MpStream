using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Movie
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public MovieService MovieService { get; set; }
        public List<MovieEntity> MovieList { get; set; } = new List<MovieEntity>();
        Dictionary<int, string> GenreNameMappedById = new Dictionary<int, string>();
        protected override void OnInitialized()
        {
            MovieList = MovieService.GetMovieList();
        }
    }
}
