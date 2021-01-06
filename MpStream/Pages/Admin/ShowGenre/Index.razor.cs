using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.ShowGenre
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public TvShowService TvShowService { get; set; }
        public List<TvShowGenre> TvShowGenres { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TvShowGenres = await TvShowService.TvShowGenreList();
        }

        public async Task Remove(int Id)
        {

        }

        public async Task Edit(int Id)
        {

        }
    }
}
