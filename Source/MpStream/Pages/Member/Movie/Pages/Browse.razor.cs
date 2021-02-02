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
        [Parameter]
        public string SearchKeyword { get; set; }
        [Parameter]
        public string SoundType { get; set; }
        [Parameter]
        public string SearchByYear { get; set; }
        public List<MovieEntity> Movies = new List<MovieEntity>();
        public string Message { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Message = string.Empty;
            await MovieList();
        }

        async Task MovieList()
        {
            if (!string.IsNullOrEmpty(CategoryName))
            {
                Movies = await MovieService.BrowseMovieByCategory(CategoryName);
                if (Movies.Count == 0)
                {
                    Message = "ไม่พบรายการที่ค้นหา";
                }
            } else if (!string.IsNullOrEmpty(SearchKeyword))
            {
                Movies = await MovieService.BrowseMovieBySearchKeyword(SearchKeyword);
                if (Movies.Count == 0)
                {
                    Message = "ไม่พบรายการที่ค้นหา";
                }
            }
            else if (!string.IsNullOrEmpty(SoundType))
            {
                Movies = await MovieService.BrowseMovieBySoundType(SoundType);
                if (Movies.Count == 0)
                {
                    Message = "ไม่พบรายการที่ค้นหา";
                }
            } else if (!string.IsNullOrEmpty(SearchByYear))
            {
                Movies = await MovieService.BrowseMovieByYear(SearchByYear);
                if (Movies.Count == 0)
                {
                    Message = "ไม่พบรายการที่ค้นหา";
                }
            }
        }

    }

}
