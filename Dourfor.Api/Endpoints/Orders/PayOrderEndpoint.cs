using System.Security.Claims;
using Dourfor.Api.Common.Api;
using Dourfor.Core.Handlers;
using Dourfor.Core.Models;
using Dourfor.Core.Requests.Orders;
using Dourfor.Core.Responses;

namespace Dourfor.Api.Endpoints.Orders;

public class PayOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{number}/pay", HandleAsync)
            .WithName("Orders: Pay an order")
            .WithSummary("Marca um pedido como pago")
            .WithDescription("Marca um pedido como pago")
            .WithOrder(3)
            .Produces<Response<Order?>>();

    private static async Task<IResult> HandleAsync(
        IOrderHandler handler,
        string number,
        PayOrderRequest request,
        ClaimsPrincipal user)
    {
        request.OrderNumber = number;
        request.UserId = user.Identity!.Name ?? string.Empty;

        var result = await handler.PayAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}