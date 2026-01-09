using Dourfor.Core.Requests.Account;
using Dourfor.Core.Responses;

namespace Dourfor.Core.Handlers;

public interface IAccountHandler
{
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task<Response<string>> RegisterAsync(RegisterRequest request);
    Task LogoutAsync();
}