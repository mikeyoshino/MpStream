using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Member.Movies
{
    public partial class Browse : ComponentBase
    {
        [Inject]
        public MovieService MovieService { get; set; }
        [Parameter]
        public string CategoryName { get; set; }
        public List<MovieEntity> Movies = new List<MovieEntity>();
        public bool IsMouseOver { get; set; } = false;
        public Dictionary<int, bool> MouseEventMapbyMovieId;
        public string Message { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Message = string.Empty;
            await MovieList();
            MouseEvent();
        }

        async Task MovieList()
        {
            Movies = await MovieService.BrowseMovieByCategory(CategoryName);
            if (Movies.Count == 0)
            {
                Message = "ไม่พบรายการที่ค้นหา";
            }
        }

        void MouseEvent()
        {
            MouseEventMapbyMovieId = new Dictionary<int, bool>();
            foreach (var eachMovie in Movies)
            {
                MouseEventMapbyMovieId.Add(eachMovie.Id, false);
            }
        }
        void MouseOver(int Id)
        {
            MouseEventMapbyMovieId[Id] = true;
        }
        void MouseOut(int Id)
        {
            MouseEventMapbyMovieId[Id] = false;
        }
    }

}
