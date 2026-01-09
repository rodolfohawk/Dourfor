using Dourfor.Api.Common.Api;
using Dourfor.Core.Handlers;
using Dourfor.Core.Models;
using Dourfor.Core.Requests.Profiles;
using Dourfor.Core.Responses;
using System.Security.Claims;

namespace Dourfor.Api.Endpoints.Profiles;

public class GetProfileByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Profiles: Get By Id")
            .WithSummary("Recupera Profile")
            .WithDescription("Recupera Profile")
            .WithOrder(4)
            .Produces<Response<Profile?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProfileHandler handler,
        long id)
    {
        var request = new GetProfileByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}