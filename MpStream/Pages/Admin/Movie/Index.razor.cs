using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.Movie
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public MovieService MovieService { get; set; }
        [Inject]
        public NavigationManager NavBar { get; set; }
        public List<MovieEntity> MovieList { get; set; }
        public string DeleteMessage { get; set; }
        protected override void OnInitialized()
        {
            MovieList =  MovieService.GetMovieList();
        }

        public async Task Remove(int Id)
        {
            var result = MovieService.DeleteMovie(Id);
            if(result == Task.FromResult(true))
            {
                DeleteMessage = "Sucessfully Deleted";
            } else
            {
                DeleteMessage = "Fail to Delete.";
            }
        }
        public void Edit(int Id)
        {
            NavBar.NavigateTo("/admin/movie/edit/" + Id);
        }
    }
}
