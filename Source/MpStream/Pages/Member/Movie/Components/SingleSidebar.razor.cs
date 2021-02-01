using Microsoft.AspNetCore.Components;
using MpStream.Models;
using System.Collections.Generic;


namespace MpStream.Pages.Member
{
    public partial class SingleSidebar : ComponentBase
    {
        [Parameter]
        public List<MovieEntity> RelatedMovies { get; set; }
    }
}
