using Dourfor.Core.Requests.Account;
using Dourfor.Core.Responses;
using System.Net.Http.Json;

namespace Dourfor.UI.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private string? _authToken;
    private string? _currentUserEmail;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public bool IsAuthenticated => !string.IsNullOrEmpty(_authToken);
    public string? CurrentUserEmail => _currentUserEmail;

    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("v1/identity/login?useCookies=true&useSessionCookies=false", request);
            
            if (response.IsSuccessStatusCode)
            {
                _currentUserEmail = request.Email;
                _authToken = "authenticated"; // Para ASP.NET Identity, pode não retornar token JWT
                
                return new Response<string>("Login realizado com sucesso!", 200, "Login realizado com sucesso!");
            }
            
            var errorContent = await response.Content.ReadAsStringAsync();
            return new Response<string>(null, (int)response.StatusCode, "Email ou senha inválidos");
        }
        catch (HttpRequestException ex)
        {
            return new Response<string>(null, 500, $"Erro de conexão: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new Response<string>(null, 500, $"Erro: {ex.Message}");
        }
    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("v1/identity/register", request);
            
            if (response.IsSuccessStatusCode)
            {
                return new Response<string>("Cadastro realizado com sucesso!", 201, "Cadastro realizado com sucesso!");
            }
            
            var errorContent = await response.Content.ReadAsStringAsync();
            return new Response<string>(null, (int)response.StatusCode, "Não foi possível realizar o cadastro. Verifique os dados informados.");
        }
        catch (HttpRequestException ex)
        {
            return new Response<string>(null, 500, $"Erro de conexão: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new Response<string>(null, 500, $"Erro: {ex.Message}");
        }
    }

    public Task LogoutAsync()
    {
        _authToken = null;
        _currentUserEmail = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
        return Task.CompletedTask;
    }
}
