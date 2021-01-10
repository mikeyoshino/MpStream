using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MpStream.Models;
using System.Threading.Tasks;

namespace MpStream.Pages.Shared
{
    public partial class LoginDashboard : ComponentBase
    {
        public ApplicationUser user { get; set; } = new ApplicationUser();
        [Inject]
        private SignInManager<IdentityUser> signInManager { get; set; }
        [Inject]
        private NavigationManager navigation { get; set; }
        private string AdminPage { get; set; } = "/Admin";
        public string LockOut { get; set; } = "/Lockout";
        public string ErrorMessage { get; set; }

        private async Task ValidateUser()
        {
            if(user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    navigation.NavigateTo(AdminPage);
                }
                if (result.IsLockedOut)
                {
                    navigation.NavigateTo(LockOut);
                }
                else
                {
                    ErrorMessage = "Invalid login attempt.";
                    navigation.NavigateTo(AdminPage);
                }

            } 
        }

    }

}
