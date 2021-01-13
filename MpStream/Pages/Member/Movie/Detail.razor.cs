﻿using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System.Threading.Tasks;

namespace MpStream.Pages.Members
{
    public partial class Detail : ComponentBase
    {
        [Inject]
        public MovieService MovieService { get; set; }
        public bool ShowDialog { get; set; } = false;
        public MovieEntity MovieEntity { get; set; } = new MovieEntity();
        [Parameter]
        public int MovieId { get; set; }

        protected override void OnInitialized()
        {
            MovieEntity = MovieService.GetMovieById(MovieId);
        }
        void ShowUpDialog()
        {
            ShowDialog = true;
            StateHasChanged();
        }
        void CloseDialog()
        {
            ShowDialog = false;
            StateHasChanged();
        }
    }
}