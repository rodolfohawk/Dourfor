using Dourfor.Core.Models;
using Dourfor.Core.Requests.Categories;
using Dourfor.Core.Responses;

namespace Dourfor.Core.Handlers;

public interface ICategoryHandler
{
    Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
    Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
    Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
    Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
    Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
}