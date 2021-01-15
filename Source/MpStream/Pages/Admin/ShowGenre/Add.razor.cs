using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.ShowGenre
{
    public partial class Add : ComponentBase
    {
        public TvShowGenre TvShowGenre { get; set; } = new TvShowGenre();
        [Inject]
        public TvShowService TvShowService { get; set; }
        [Inject]
        public NavigationManager NavBar { get; set; }

        public async Task SaveShowGenre()
        {
           var result = await TvShowService.CreateTvShowGenre(TvShowGenre);
           if(result)
            {
                NavBar.NavigateTo("/admin/tvshowgenre");
            }
        }
    }
}
