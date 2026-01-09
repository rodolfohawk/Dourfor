using System.Security.Claims;
using Dourfor.Api.Common.Api;
using Dourfor.Core.Handlers;
using Dourfor.Core.Requests.Stripe;
using Dourfor.Core.Responses;

namespace Dourfor.Api.Endpoints.Stripe;

public class CreateSessionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/session", HandleAsync)
            .Produces<Response<string?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IStripeHandler handler,
        CreateSessionRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;

        var result = await handler.CreateSessionAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}