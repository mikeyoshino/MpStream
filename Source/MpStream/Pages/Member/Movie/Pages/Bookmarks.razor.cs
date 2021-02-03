using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Member.Movies
{
    public partial class Bookmarks : ComponentBase
    {
        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }
        [Inject]
        public MovieService MovieService { get; set; }
        public Dictionary<string, int> MovieIdList { get; set; } = new Dictionary<string, int>();
        public List<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
        public Dictionary<int, bool> MouseEventMapbyMovieId { get; set; } = new Dictionary<int, bool>();
        public int number { get; set; }
        public string What { get; set; }
        public bool IsSpinner { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {
            IsSpinner = true;
            number = await LocalStorageService.LengthAsync();
            for (int i = 0; i < number; i++)
            {
                var sessionKey = await LocalStorageService.KeyAsync(i);
                var sessionVaule = await LocalStorageService.GetItemAsync<string>(sessionKey);
                if (!sessionKey.Contains("Liked"))
                {
                    MovieIdList.Add(sessionKey, Convert.ToInt32(sessionVaule));
                }
            }
            Movies = await MovieService.BookmarkMovies(MovieIdList);
            IsSpinner = false;

        }

        async Task RemoveFromBookmarks(int movieId)
        {
            await LocalStorageService.RemoveItemAsync(movieId.ToString());
            Movies.RemoveAll(x => x.Id == movieId);
        }
        async Task RemoveAllBookmarks()
        {
            foreach (var movieId in MovieIdList.Keys)
            {
                await LocalStorageService.RemoveItemAsync(movieId.ToString());
                Movies.RemoveAll(x => x.Id == Convert.ToInt32(movieId));
            }
        }
    }
}
