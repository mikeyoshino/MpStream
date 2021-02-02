using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Shared
{
    public partial class HeaderSearch : ComponentBase
    {
        [Inject]
        public NavigationManager NavManager { get; set; }
        public string SearchKeyword { get; set; }

        void Search()
        {
            NavManager.NavigateTo(String.Format("/search/{0}", SearchKeyword));
        }
        void Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                NavManager.NavigateTo(String.Format("/search/{0}", SearchKeyword));
            }
        }
    }
}
