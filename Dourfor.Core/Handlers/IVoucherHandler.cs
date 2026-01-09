using Dourfor.Core.Models;
using Dourfor.Core.Requests.Orders;
using Dourfor.Core.Responses;

namespace Dourfor.Core.Handlers;

public interface IVoucherHandler
{
    Task<Response<Voucher?>> GetByNumberAsync(GetVoucherByNumberRequest request);
}