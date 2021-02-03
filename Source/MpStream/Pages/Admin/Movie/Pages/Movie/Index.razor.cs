using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.Movies
{
    public partial class Index : ComponentBase
    {
        #region Pagination
        public int pageSize { get; set; } = 10;
        public int pageIndex { get; set; }
        public List<int> PageNumber { get; set; } = new List<int>();
        public int CountMovie { get; set; }
        #endregion


        [Inject]
        public MovieService MovieService { get; set; }
        [Inject]
        public NavigationManager NavBar { get; set; }
        public List<MovieEntity> MovieListInDb { get; set; } = new List<MovieEntity>();
        public List<MovieEntity> MovieList { get; set; } = new List<MovieEntity>();
        public List<MovieEntity> MovieEntity { get; set; }
        public List<int> SelectedMovieIds { get; set; } = new List<int>();
        public string DeleteMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MovieList = await MovieService.GetMovieListIndexPage(pageSize);
            CountMovie = await MovieService.CountMovies();

            //what is it you divide two int and cant round the number up? need to use this algorihm to fix it.
            var number = (CountMovie + (pageSize - 1)) / pageSize;
            for (int i = 1; i <= number; i++)
            {
                PageNumber.Add(i);
            }
        }

        public async Task ChangePage(int pageIndex)
        {
            MovieList = await MovieService.GetMovieListByPage(pageSize, pageIndex);
        }
        public void CheckboxClicked(int Id, object checkedValue)
        {
            if ((bool)checkedValue)
            {
                if (!SelectedMovieIds.Contains(Id))
                {
                    SelectedMovieIds.Add(Id);
                }
            }
            else
            {
                if (SelectedMovieIds.Contains(Id))
                {
                    SelectedMovieIds.Remove(Id);
                }
            }
        }
        public async Task Search(object searchKeywords)
        {
            MovieList = await MovieService.SearchByWords((string)searchKeywords);
            StateHasChanged();
        }

        public async Task Remove(int Id)
        {
            var result = await MovieService.DeleteMovie(Id);
            if(result == true)
            {
                DeleteMessage = "Succeeded";
                MovieList.Clear();
                MovieList = MovieService.GetMovieList();
                StateHasChanged();
                DeleteMessage = "Restart";
            } else
            {
                DeleteMessage = "Failed";
            }
        }
        public void Edit(int Id)
        {
            NavBar.NavigateTo("/admin/movie/edit/" + Id);
        }

        protected void PagerPageChanged(int page)
        {
            NavBar.NavigateTo("/page/" + page);
        }
    }
}
