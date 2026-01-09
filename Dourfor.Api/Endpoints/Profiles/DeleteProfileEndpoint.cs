using System.Security.Claims;
using Dourfor.Api.Common.Api;
using Dourfor.Core.Handlers;
using Dourfor.Core.Models;
using Dourfor.Core.Requests.Profiles;
using Dourfor.Core.Responses;

namespace Dourfor.Api.Endpoints.Profiles;

public class DeleteProfileEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Profiles: Delete")
            .WithSummary("Exclui Profile")
            .WithDescription("Exclui Profile")
            .WithOrder(3)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProfileHandler handler,
        long id)
    {
        var request = new DeleteProfileRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}