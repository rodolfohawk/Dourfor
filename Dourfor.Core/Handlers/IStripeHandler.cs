using Dourfor.Core.Requests.Stripe;
using Dourfor.Core.Responses;
using Dourfor.Core.Responses.Stripe;

namespace Dourfor.Core.Handlers;

public interface IStripeHandler
{
    Task<Response<string?>> CreateSessionAsync(CreateSessionRequest request);
    Task<Response<List<StripeTransactionResponse>>> GetTransactionsByOrderNumberAsync(GetTransactionByOrderNumberRequest request);
}