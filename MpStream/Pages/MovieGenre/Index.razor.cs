using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.MovieGenre
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public MovieGenreService MovieGenreService { get; set; }
        List<MovieGenreEntity> GenreList;
        protected override void OnInitialized()
        {
            GenreList = MovieGenreService.GetGenreList();
        }
    }
}
