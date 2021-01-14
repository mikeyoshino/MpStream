using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages
{
    public partial class Index : ComponentBase
    {
        public List<MovieEntity> MovieList { get; set; }
        [Inject]
        public MovieService MovieService { get; set; }
        public bool IsMouseOver { get; set; } = false;
        public Dictionary<int, bool> MouseEventMapbyMovieId { get; set; } = new Dictionary<int, bool>();

        protected override async Task OnInitializedAsync()
        {
            MovieList = MovieService.GetMovieList();
            foreach (var eachMovie in MovieList)
            {
                MouseEventMapbyMovieId.Add(eachMovie.Id, false);
            }
        }

        async Task MouseOver(int Id)
        {
            MouseEventMapbyMovieId[Id] = true;
        }
        async Task MouseOut(int Id)
        {
            MouseEventMapbyMovieId[Id] = false;
        }

    }
}
