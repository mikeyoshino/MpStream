using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System.Threading.Tasks;

namespace MpStream.Pages.Member.Movies
{
    public partial class SingleMovie : ComponentBase
    {
        [Inject]
        public LikeService LikeService { get; set; }
        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }
        public string ValueKey { get; set; }
        public bool IsActiveBookmark { get; set; } = false;
        public bool IsLiked { get; set; } = false;
        [Parameter]
        public MovieEntity MovieEntity { get; set; }
        public int LikeCount { get; set; } = 0;
        public bool ShowDialog { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            LikeCount = await LikeService.GetLikes(MovieEntity.Id);
            if (await LocalStorageService.ContainKeyAsync(MovieEntity.Id.ToString()))
            {
                IsActiveBookmark = true;
            }
            ValueKey = await LocalStorageService.GetItemAsStringAsync(MovieEntity.Id + "Liked");
            if (await LocalStorageService.ContainKeyAsync(MovieEntity.Id + "Liked") && ValueKey.Contains(MovieEntity.Id.ToString()))
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
            await LocalStorageService.SetItemAsync(MovieEntity.Id.ToString(), 1);
        }
        async Task AddLike()
        {
            if (await LocalStorageService.ContainKeyAsync(MovieEntity.Id + "Liked") && ValueKey.Contains(MovieEntity.Id.ToString()))
            {
                //Show message that you liked already.
            }
            else
            {
                LikeCount = await LikeService.AddMovieLike(MovieEntity.Id);
                await LocalStorageService.SetItemAsync(MovieEntity.Id + "Liked", MovieEntity.Id);
                IsLiked = true;
            }
        }
    }
}
