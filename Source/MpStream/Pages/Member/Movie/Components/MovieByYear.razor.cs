using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Member.Movies
{
    public partial class MovieByYear : ComponentBase
    {
        [Parameter]
        public List<int> YearLists { get; set; }
    }
}
