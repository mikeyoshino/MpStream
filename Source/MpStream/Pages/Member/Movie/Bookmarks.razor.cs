using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Member
{
    public partial class Bookmarks : ComponentBase
    {
        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }
        [Inject]
        public MovieService MovieService { get; set; }
        public List<string> MovieIdList { get; set; }
        public int number { get; set; }

        protected override void OnInitialized()
        {
            number = LocalStorageService.LengthAsync().Result;
        }

        void PopulateMovieIdToList()
        {
            for (int i = 0; i < LocalStorageService.LengthAsync().Result; i++)
            {
                var sessionKey = LocalStorageService.KeyAsync(i);
                MovieIdList.Add(sessionKey.Result);
            }
        }

    }
}
