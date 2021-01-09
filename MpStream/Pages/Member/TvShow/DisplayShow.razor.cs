using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Members
{
    public partial class DisplayShow : ComponentBase
    {
        [Inject]
        public TvShowService TvShowService { get; set; }
        public List<TvShowEntity> TvShowList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TvShowList = await TvShowService.GetTvShowList();
        }
    }
}
