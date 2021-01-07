using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MpStream.Pages.Admins
{
    public partial class Index : ComponentBase
    {
        #region Pagination
        public int pageSize { get; set; } = 3;
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
        public string SearchKeyword { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MovieList = await MovieService.GetMovieListIndexPage(pageSize);
            CountMovie = await MovieService.CountMovies();
            var number = CountMovie / pageSize;
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
        public async Task Search(string searchKeywords)
        {
            MovieList = await MovieService.SearchByWords(searchKeywords);
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

        protected void PagerPageChanged(int page)
        {
            NavBar.NavigateTo("/page/" + page);
        }
    }
}
