using Dourfor.Api.Common.Api;
using Dourfor.Api.Endpoints.Categories;
using Dourfor.Api.Endpoints.Identity;
using Dourfor.Api.Endpoints.Orders;
using Dourfor.Api.Endpoints.Profiles;
using Dourfor.Api.Endpoints.Reports;
using Dourfor.Api.Endpoints.Stripe;
using Dourfor.Api.Endpoints.Transactions;
using Dourfor.Api.Models;

namespace Dourfor.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "OK" });

        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .RequireAuthorization()
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>();

        endpoints.MapGroup("v1/transactions")
            .WithTags("Transactions")
            .RequireAuthorization()
            .MapEndpoint<CreateTransactionEndpoint>()
            .MapEndpoint<UpdateTransactionEndpoint>()
            .MapEndpoint<DeleteTransactionEndpoint>()
            .MapEndpoint<GetTransactionByIdEndpoint>()
            .MapEndpoint<GetTransactionsByPeriodEndpoint>();

        endpoints.MapGroup("v1/products")
            .WithTags("Products")
            .RequireAuthorization()
            .MapEndpoint<GetAllProductsEndpoint>()
            .MapEndpoint<GetProductBySlugEndpoint>();

        endpoints.MapGroup("v1/vouchers")
            .WithTags("Vouchers")
            .RequireAuthorization()
            .MapEndpoint<GetVoucherByNumberEndpoint>();

        endpoints.MapGroup("v1/orders")
            .WithTags("Orders")
            .RequireAuthorization()
            .MapEndpoint<GetAllOrdersEndpoint>()
            .MapEndpoint<GetOrderByNumberEndpoint>()
            .MapEndpoint<CreateOrderEndpoint>()
            .MapEndpoint<CancelOrderEndpoint>()
            .MapEndpoint<PayOrderEndpoint>()
            .MapEndpoint<RefundOrderEndpoint>();

        endpoints.MapGroup("v1/payments/stripe")
            .WithTags("Payments - Stripe")
            .RequireAuthorization()
            .MapEndpoint<CreateSessionEndpoint>()
            .MapEndpoint<GetTransactionsByOrderNumberEndpoint>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>();

        endpoints.MapGroup("/v1/reports")
            .WithTags("Reports")
            .RequireAuthorization()
            .MapEndpoint<GetIncomesAndExpensesEndpoint>()
            .MapEndpoint<GetIncomesByCategoryEndpoint>()
            .MapEndpoint<GetExpensesByCategoryEndpoint>()
            .MapEndpoint<GetFinancialSummaryEndpoint>();

        endpoints.MapGroup("v1/Profiles")
            .WithTags("Profiles")
            .RequireAuthorization()
            .MapEndpoint<UpdateProfileEndpoint>()
            .MapEndpoint<DeleteProfileEndpoint>()
            .MapEndpoint<GetProfileByIdEndpoint>()
            .MapEndpoint<GetAllProfilesEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}