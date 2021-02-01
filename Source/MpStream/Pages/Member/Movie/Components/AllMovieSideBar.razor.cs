using Microsoft.AspNetCore.Components;
using MpStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Member.Movies
{
    public partial class AllMovieSideBar : ComponentBase
    {
        [Parameter]
        public List<MovieEntity> MovieList { get; set; }
        [Parameter]
        public List<int> YearLists { get; set; }
        [Parameter]
        public List<string> SoundLists { get; set; }
    }
}
