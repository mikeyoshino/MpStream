using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.MovieGenre
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public MovieGenreService MovieGenreService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<MovieGenreEntity> GenreList { get; set; } = new List<MovieGenreEntity>();

        protected override void OnInitialized()
        {
            GenreList = MovieGenreService.GetGenreList();
        }

        void Edit(int Id)
        {
            NavigationManager.NavigateTo($"/admin/moviegenre/edit/{Id}");
        }
        void Remove(int Id)
        {

        }
    }
}
