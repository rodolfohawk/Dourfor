using System.Security.Claims;
using Dourfor.Api.Common.Api;
using Dourfor.Core.Handlers;
using Dourfor.Core.Models;
using Dourfor.Core.Requests.Orders;
using Dourfor.Core.Responses;

namespace Dourfor.Api.Endpoints.Orders;

public class GetOrderByNumberEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{number}", HandleAsync)
            .WithName("Orders: Get By Number")
            .WithSummary("Recupera um pedido pelo número")
            .WithDescription("Recupera um pedido pelo número")
            .WithOrder(6)
            .Produces<Response<Order?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IOrderHandler handler,
        string number)
    {
        var request = new GetOrderByNumberRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Number = number
        };

        var result = await handler.GetByNumberAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}