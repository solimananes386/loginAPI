using WebApplication4.Models;

namespace WebApplication4.Interface
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel register);
        Task<AuthModel> LoginAsync(LoginModel register);
    }
}
