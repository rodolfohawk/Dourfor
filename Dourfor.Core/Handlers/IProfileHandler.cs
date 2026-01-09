using Dourfor.Core.Models;
using Dourfor.Core.Requests.Profiles;
using Dourfor.Core.Responses;

namespace Dourfor.Core.Handlers;

public interface IProfileHandler
{
    Task<Response<Profile?>> UpdateAsync(UpdateProfileRequest request);
    Task<Response<Profile?>> DeleteAsync(DeleteProfileRequest request);
    Task<Response<Profile?>> GetByIdAsync(GetProfileByIdRequest request);
    Task<PagedResponse<List<Profile>>> GetAllAsync(GetAllProfilesRequest request);
}