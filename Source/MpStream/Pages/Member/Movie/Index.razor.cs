using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Members
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public MovieService MovieService { get; set; }
        public List<MovieEntity> MovieList { get; set; } = new List<MovieEntity>();
        Dictionary<int, string> GenreNameMappedById = new Dictionary<int, string>();
        public bool IsMouseOver { get; set; } = false;
        public bool IsClickFilter { get; set; } = false;
        public Dictionary<int, bool> MouseEventMapbyMovieId { get; set; } = new Dictionary<int, bool>();
        public List<int> YearLists { get; set; } = new List<int>();
        public List<int> MovieOnlyYears { get; set; }
        [Parameter]
        public int PostNumber { get; set; } = 20;
        public List<MovieEntity> MovieWithYear { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MovieList = await MovieService.GetMovieListLimitPostNumber(PostNumber);
            MouseEvent();
            MovieOnlyYears = await MovieService.MovieYears();
            PopulateYear();
        }

        protected override async Task OnParametersSetAsync()
        {
            MovieList = await MovieService.GetMovieListLimitPostNumber(PostNumber);
        }

        void MouseEvent()
        {
            foreach (var eachMovie in MovieList)
            {
                if (!MouseEventMapbyMovieId.ContainsKey(eachMovie.Id))
                {
                    MouseEventMapbyMovieId.Add(eachMovie.Id, false);
                }
            }
        }
        void PopulateYear()
        {
            foreach (var eachyear in MovieOnlyYears)
            {
                if (!YearLists.Contains(eachyear))
                {
                    YearLists.Add(eachyear);
                }
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
        void IsFilterClick(bool isClick)
        {
            if (isClick == true)
            {
                IsClickFilter = false;
            } else
            {
                IsClickFilter = true;
            }
        }
        async Task LoadMorePost()
        {
            PostNumber += PostNumber;
            await OnInitializedAsync();
        }
    }
}
