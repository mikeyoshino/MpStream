using Microsoft.AspNetCore.Components;
using MpStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Member.Movies
{
    public partial class Video : ComponentBase
    {
        [Parameter]
        public MovieEntity MovieEntity { get; set; }
        public string EmbedUrl { get; set; }

        //protected override void OnInitialized()
        //{
        //    EmbedUrl = MovieEntity.PlayerOne;
        //}

        //protected override async Task OnParametersSetAsync()
        //{
        //    EmbedUrl = MovieEntity.PlayerOne;
        //}

        //private void PlayerOneClick()
        //{
        //    EmbedUrl = string.Empty;
        //    EmbedUrl = MovieEntity.PlayerOne;
        //}
        //private void PlayerTwoClick()
        //{
        //    EmbedUrl = string.Empty;
        //    EmbedUrl = MovieEntity.PlayerTwo;
        //}
        //private void PlayerThreeClick()
        //{
        //    EmbedUrl = string.Empty;
        //    EmbedUrl = MovieEntity.PlayerThree;
        //}
        //private void PlayerFourClick()
        //{
        //    EmbedUrl = string.Empty;
        //    EmbedUrl = MovieEntity.PlayerFour;
        //}
    }
}
