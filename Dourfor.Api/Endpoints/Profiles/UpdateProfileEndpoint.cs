using System.Security.Claims;
using Dourfor.Api.Common.Api;
using Dourfor.Core.Handlers;
using Dourfor.Core.Models;
using Dourfor.Core.Requests.Profiles;
using Dourfor.Core.Responses;

namespace Dourfor.Api.Endpoints.Profiles;

public class UpdateProfileEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Profiles: Update")
            .WithSummary("Atualiza Profile")
            .WithDescription("Atualiza Profile")
            .WithOrder(2)
            .Produces<Response<Profile?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProfileHandler handler,
        UpdateProfileRequest request,
        long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}