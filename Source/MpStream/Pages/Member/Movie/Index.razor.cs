using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Members
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public MovieService MovieService { get; set; }
        public List<MovieEntity> MovieList { get; set; } = new List<MovieEntity>();
        Dictionary<int, string> GenreNameMappedById = new Dictionary<int, string>();
        public bool IsMouseOver { get; set; } = false;
        public Dictionary<int, bool> MouseEventMapbyMovieId { get; set; } = new Dictionary<int, bool>();
        protected override void OnInitialized()
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
