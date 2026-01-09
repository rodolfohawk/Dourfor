using System.Security.Claims;
using Dourfor.Api.Common.Api;
using Dourfor.Core.Handlers;
using Dourfor.Core.Models;
using Dourfor.Core.Requests.Orders;
using Dourfor.Core.Responses;

namespace Dourfor.Api.Endpoints.Orders;

public class CancelOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{id}/cancel", HandleAsync)
            .WithName("Orders: Cancel a new order")
            .WithSummary("Cancela um pedido")
            .WithDescription("Cancela um pedido")
            .WithOrder(2)
            .Produces<Response<Order?>>();

    private static async Task<IResult> HandleAsync(
        IOrderHandler handler,
        long id,
        ClaimsPrincipal user)
    {
        var request = new CancelOrderRequest
        {
            Id = id,
            UserId = user.Identity!.Name ?? string.Empty
        };

        var result = await handler.CancelAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}