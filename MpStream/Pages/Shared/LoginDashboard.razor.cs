using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace MpStream.Pages.Shared
{
    public partial class LoginDashboard : ComponentBase
    {
        [Inject]
        public NavigationManager NavBar { get; set; }
        [CascadingParameter] protected Task<AuthenticationState> AuthStat { get; set; }

        protected async override Task OnInitializedAsync()
        {
            base.OnInitialized();
            var user = (await AuthStat).User;
            if (!user.Identity.IsAuthenticated)
            {
                NavBar.NavigateTo("/identity/account/login");
            }
        }

    }

}
