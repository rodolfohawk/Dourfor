using Dourfor.Core.Requests.Account;
using Dourfor.Core.Responses;

namespace Dourfor.UI.Services;

public interface IAuthService
{
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task<Response<string>> RegisterAsync(RegisterRequest request);
    Task LogoutAsync();
    bool IsAuthenticated { get; }
    string? CurrentUserEmail { get; }
}
