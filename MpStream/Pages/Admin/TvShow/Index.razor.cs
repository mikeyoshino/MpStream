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
        [Inject]
        public TvShowService TvShowService { get; set; }
        public List<TvShowEntity> TvShowEntities { get; set; } = new List<TvShowEntity>();
        [Inject]
        public NavigationManager NavBar { get; set; }
        protected override async Task OnInitializedAsync()
        {
            TvShowEntities = await TvShowService.GetTvShowList();
        }

        public async Task Remove(int Id)
        {

        }
        public async Task Edit(int Id)
        {
            NavBar.NavigateTo($"/admin/tvshow/edit/{Id}");
        }
    }
}
