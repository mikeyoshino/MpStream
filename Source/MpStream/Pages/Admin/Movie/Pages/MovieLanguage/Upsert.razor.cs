using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.MovieLanguages
{
    public partial class Upsert : ComponentBase
    {
        MovieLanguage MovieLanguageModel = new MovieLanguage();
        [Inject]
        public MovieVideoService MovieVideoService { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }
        public List<MovieLanguage> MovieLanguages { get; set; }
        [Parameter]
        public int LanguageId { get; set; }
        public string ProceedStatus { get; set; } = "";
        public const string DuplicatedLang = "หัวข้อซ้ำ";
        public const string SuccessfullySave = "สำเร็จ";

        protected override async Task OnInitializedAsync()
        {
            MovieLanguages = await MovieVideoService.GetMovieLanguages();

        }

        protected override async Task OnParametersSetAsync()
        {
            if(LanguageId != 0)
            {
                MovieLanguageModel = await MovieVideoService.GetMovieLanguage(LanguageId);
                ProceedStatus = "";
            }
        }

        async Task Save()
        {
            if (LanguageId != 0)
            {
                var update = await MovieVideoService.UpdateLanguage(MovieLanguageModel);
                MovieLanguageModel = new MovieLanguage();
                ProceedStatus = SuccessfullySave;
                NavManager.NavigateTo("/admin/movie/language");
            }
            else
            {
                if (!MovieLanguages.Any(x => x.LanguageName == MovieLanguageModel.LanguageName))
                {
                    var save = await MovieVideoService.SaveLanguage(MovieLanguageModel);
                    MovieLanguageModel = new MovieLanguage();
                    await OnInitializedAsync();
                    ProceedStatus = SuccessfullySave;
                }
                else
                {
                    ProceedStatus = DuplicatedLang;
                    MovieLanguageModel = new MovieLanguage();
                }
            }
            
        }

        void Edit(int Id)
        {
            LanguageId = Id;
            NavManager.NavigateTo(string.Format("/admin/movie/language/{0}", Id));
        }

        async Task Remove(int Id)
        {
            var removeLang = await MovieVideoService.RemoveLanguage(Id);
            if (removeLang)
            {
                MovieLanguages.RemoveAll(x => x.Id == Id);
            }
        }
    }
}
