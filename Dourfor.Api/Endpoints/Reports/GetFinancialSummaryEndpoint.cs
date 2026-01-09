using System.Reflection.Metadata;
using System.Security.Claims;
using Dourfor.Api.Common.Api;
using Dourfor.Core.Handlers;
using Dourfor.Core.Models.Reports;
using Dourfor.Core.Requests.Reports;
using Dourfor.Core.Responses;

namespace Dourfor.Api.Endpoints.Reports;

public class GetFinancialSummaryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/summary", HandleAsync)
            .Produces<Response<FinancialSummary?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IReportHandler handler)
    {
        var request = new GetFinancialSummaryRequest
        {
            UserId = user.Identity?.Name ?? string.Empty
        };
        var result = await handler.GetFinancialSummaryReportAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}