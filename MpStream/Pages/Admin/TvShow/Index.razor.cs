using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.TvShow
{
    public partial class Index : ComponentBase
    {
        #region Pagination
        public int pageSize { get; set; } = 5;
        public int pageIndex { get; set; }
        public List<int> PageNumber { get; set; } = new List<int>();
        public int CountTvShow { get; set; }
        #endregion

        [Inject]
        public TvShowService TvShowService { get; set; }
        public List<TvShowEntity> TvShowEntities { get; set; } = new List<TvShowEntity>();
        [Inject]
        public NavigationManager NavBar { get; set; }
        protected override async Task OnInitializedAsync()
        {
            TvShowEntities = await TvShowService.GetTvShowListIndexPage(pageSize);
            CountTvShow = await TvShowService.CountTvShow();

            //what is it you divide two int and cant round the number up? need to use this algorihm to fix it.
            var number = (CountTvShow + (pageSize - 1)) / pageSize;
            for (int i = 1; i <= number; i++)
            {
                PageNumber.Add(i);
            }
            TvShowEntities = await TvShowService.GetTvShowList();
        }

        public async Task Remove(int Id)
        {

        }
        async void Search(object searchKeywords)
        {
            TvShowEntities = await TvShowService.SearchByWords((string)searchKeywords);
            StateHasChanged();
        }
        public async Task ChangePage(int pageIndex)
        {
            TvShowEntities = await TvShowService.GetMovieListByPage(pageSize, pageIndex);
        }
        void CheckboxClicked(int Id, object obj)
        {

        }
        public async Task Edit(int Id)
        {
            NavBar.NavigateTo($"/admin/tvshow/edit/{Id}");
        }
    }
}
