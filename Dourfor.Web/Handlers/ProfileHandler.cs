using System.Net.Http.Json;
using Dourfor.Core.Handlers;
using Dourfor.Core.Models;
using Dourfor.Core.Requests.Profiles;
using Dourfor.Core.Responses;

namespace Dourfor.Web.Handlers;

public class ProfileHandler(IHttpClientFactory httpClientFactory) : IProfileHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Response<Profile?>> UpdateAsync(UpdateProfileRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/profiles/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Profile?>>()
               ?? new Response<Profile?>(null, 400, "Falha ao atualizar a profile");
    }

    public async Task<Response<Profile?>> DeleteAsync(DeleteProfileRequest request)
    {
        var result = await _client.DeleteAsync($"v1/profiles/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Profile?>>()
               ?? new Response<Profile?>(null, 400, "Falha ao excluir a profile");
    }

    public async Task<Response<Profile?>> GetByIdAsync(GetProfileByIdRequest request)
        => await _client.GetFromJsonAsync<Response<Profile?>>($"v1/profiles/{request.Id}")
           ?? new Response<Profile?>(null, 400, "Não foi possível obter a profile");

    public async Task<PagedResponse<List<Profile>>> GetAllAsync(GetAllProfilesRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<Profile>>>("v1/profiles")
           ?? new PagedResponse<List<Profile>>(null, 400, "Não foi possível obter as profiles");
}