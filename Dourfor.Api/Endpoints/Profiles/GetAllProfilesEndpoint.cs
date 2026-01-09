using Dourfor.Api.Common.Api;
using Dourfor.Core;
using Dourfor.Core.Handlers;
using Dourfor.Core.Models;
using Dourfor.Core.Requests.Profiles;
using Dourfor.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dourfor.Api.Endpoints.Profiles;

public class GetAllProfilesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Profile: Get All")
            .WithSummary("Recupera todas Profiles")
            .WithDescription("Recupera todos Profile")
            .WithOrder(5)
            .Produces<PagedResponse<List<Profile>?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProfileHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllProfilesRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}