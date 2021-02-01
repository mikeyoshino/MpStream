using Microsoft.AspNetCore.Components;
using MpStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Member.Movies
{
    public partial class Comments : ComponentBase
    {
        [Parameter]
        public MovieEntity MovieEntity { get; set; }
    }
}
