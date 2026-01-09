using Dourfor.Api.Data;
using Dourfor.Core.Handlers;
using Dourfor.Core.Models;
using Dourfor.Core.Requests.Orders;
using Dourfor.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dourfor.Api.Handlers;

public class VoucherHandler(AppDbContext context) : IVoucherHandler
{
    public async Task<Response<Voucher?>> GetByNumberAsync(GetVoucherByNumberRequest request)
    {
        try
        {
            var voucher = await context
                .Vouchers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Number == request.Number && x.IsActive == true);

            return voucher is null
                ? new Response<Voucher?>(null, 404, "Voucher não encontrado")
                : new Response<Voucher?>(voucher);
        }
        catch
        {
            return new Response<Voucher?>(null, 500, "Não foi possível recuperar o voucher");
        }
    }
}