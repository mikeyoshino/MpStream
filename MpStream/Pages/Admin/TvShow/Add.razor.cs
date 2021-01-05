using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.TvShow
{
    public partial class Add : ComponentBase
    {
        [Inject]
        public TvShowService TvShowService { get; set; }
        public TvShowEntity TvShowEntity { get; set; } = new TvShowEntity();
        public List<TvShowGenre> TvShowGenreList { get; set; } = new List<TvShowGenre>();
        public List<string> SelectedGenreIds { get; set; }
        public string ImdbId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TvShowGenreList = await TvShowService.TvShowGenreList();
        }

        void SaveShow()
        {

        }
        void CheckboxClicked(string Id, object checkedValue)
        {
            if ((bool)checkedValue)
            {
                if (!SelectedGenreIds.Contains(Id))
                {
                    SelectedGenreIds.Add(Id);
                }
            }
            else
            {
                if (SelectedGenreIds.Contains(Id))
                {
                    SelectedGenreIds.Remove(Id);
                }
            }
        }
        void FetchImdbApi(string Id)
        {

        }
    }
}
