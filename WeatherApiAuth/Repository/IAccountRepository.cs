using Microsoft.AspNetCore.Identity;
using WeatherApiAuth.Models;

namespace WeatherApiAuth.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string> LoginAsync(SignInModel signInModel);
    }
}
