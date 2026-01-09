using Dourfor.Core.Models;
using Dourfor.Core.Requests.Orders;
using Dourfor.Core.Responses;

namespace Dourfor.Core.Handlers;

public interface IProductHandler
{
    Task<PagedResponse<List<Product>>> GetAllAsync(GetAllProductsRequest request);
    Task<Response<Product?>> GetBySlugAsync(GetProductBySlugRequest request);
}