using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Shared
{
    public partial class NavMenu 
    {
        private bool categoryShow = false;
        [Inject]
        public MovieService MovieService { get; set; }
        [Inject]
        public TvShowService TvShowService { get; set; }
        public List<MovieGenreEntity> MovieGenreEntities { get; set; }
        public List<TvShowGenre> TvShowGenreEntities { get; set; }

        private async Task ShowCategory(bool reuslt)
        {
            categoryShow = true;
            if (reuslt == true)
            {
                categoryShow = false;
            }

            MovieGenreEntities = MovieService.MovieGenreList();
            TvShowGenreEntities = await TvShowService.TvShowGenreList();
        }

        private async Task ChangeMenu()
        {
            categoryShow = false;
        }
    }
}
