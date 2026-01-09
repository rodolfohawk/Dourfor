using Dourfor.Api.Common.Api;
using Dourfor.Core;
using Dourfor.Core.Handlers;
using Dourfor.Core.Models;
using Dourfor.Core.Requests.Orders;
using Dourfor.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dourfor.Api.Endpoints.Orders;

public class GetAllProductsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Products: Get All")
            .WithSummary("Recupera todos os produtos")
            .WithDescription("Recupera todos os produtos")
            .WithOrder(1)
            .Produces<PagedResponse<List<Product>?>>();

    private static async Task<IResult> HandleAsync(
        IProductHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllProductsRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result.Data);
    }
}