﻿using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Member.Movies
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

        public List<int> YearLists = new List<int>();
        public List<int> MovieOnlyYears = new List<int>();
        public List<string> MovieSoundList  = new List<string>() { "พากย์ไทย", "ซับไทย", "พากย์ไทย-ซับไทย", "อังกฤษ" };
        [Parameter]
        public int PostNumber { get; set; } = 20;
        public List<MovieEntity> MovieWithYear { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MovieList = await MovieService.GetMovieListLimitPostNumber(PostNumber);
            MovieOnlyYears = await MovieService.MovieYears();
            PopulateYear();
        }

        protected override async Task OnParametersSetAsync()
        {
            MovieList = await MovieService.GetMovieListLimitPostNumber(PostNumber);
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
