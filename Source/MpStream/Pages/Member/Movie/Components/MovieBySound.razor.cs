using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Member.Movies
{
    public partial class MovieBySound : ComponentBase
    {
        [Parameter]
        public List<string> MovieSoundList { get; set; }
    }
}
