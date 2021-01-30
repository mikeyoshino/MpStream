using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MpStream.Pages.Members
{
    public partial class Detail : ComponentBase
    {
        [Inject]
        public MovieService MovieService { get; set; }
        [Inject]
        public LikeService LikeService { get; set; }
        public bool ShowDialog { get; set; } = false;
        public MovieEntity MovieEntity { get; set; } = new MovieEntity();
        public List<MovieEntity> RelatedMovies { get; set; } = new List<MovieEntity>();
        public int LikeCount { get; set; } = 0;


        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }
        public string ValueKey { get; set; }
        public bool IsActiveBookmark { get; set; } = false;
        public bool IsLiked { get; set; } = false;
        [Parameter]
        public int MovieId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            MovieEntity = MovieService.GetMovieById(MovieId);
            RelatedMovies = await MovieService.RelatedMovies(MovieId, MovieEntity.MovieWithGenres);
            LikeCount = await LikeService.GetLikes(MovieEntity.Id);
            if (await LocalStorageService.ContainKeyAsync(MovieEntity.Id.ToString()))
            {
                IsActiveBookmark = true;
            }
            ValueKey = await LocalStorageService.GetItemAsStringAsync("Liked");
            if (await LocalStorageService.ContainKeyAsync("Liked") && ValueKey.Contains(MovieId.ToString()))
            {
                IsLiked = true;
            }

        }

        void ShowUpDialog()
        {
            ShowDialog = true;
        }
        void CloseDialog()
        {
            ShowDialog = false;
        }

        async Task AddMovieToBookMarkList()
        {
            IsActiveBookmark = true;
            await LocalStorageService.SetItemAsync(MovieId.ToString(), 1);
        }
        async Task AddLike()
        {
            if(LikeCount == 0)
            {
                LikeCount = await LikeService.AddMovieLike(MovieEntity.Id);
                await LocalStorageService.SetItemAsync("Liked", MovieId);
                IsLiked = true;
            } else if(await LocalStorageService.ContainKeyAsync("Liked") && ValueKey.Contains(MovieId.ToString()))
            {
                LikeCount = await LikeService.UpdateLikes(MovieEntity.Id);
                IsLiked = true;
            }
        }
    }
}