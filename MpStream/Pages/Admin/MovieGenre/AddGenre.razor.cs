using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;

namespace MpStream.Pages.Admin.MovieGenre
{
    public partial class AddGenre : ComponentBase
    {
        [Inject]
        public MovieGenreService MovieGenreService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public MovieGenreEntity MovieGenreEntity { get; set; } = new MovieGenreEntity();
        void SaveGenre()
        {
            var result = MovieGenreService.AddGenre(MovieGenreEntity);
            if (result.IsCompleted)
            {
                NavigationManager.NavigateTo("/moviegenre");
            }
            else
            {
                NavigationManager.NavigateTo("/moviegenre/add");
            }
        }
    }
}
