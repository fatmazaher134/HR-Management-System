using HRMS.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HRMS.Interfaces.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);

        Task<SignInResult> LoginUserAsync(LoginViewModel model);

        Task LogoutUserAsync();
    }
}
