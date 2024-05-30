using Fina.Shared.Models;
using Fina.Shared.Requests.Categories;
using Fina.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fina.Shared.Handler
{
    public interface ICategoryHandler
    {
            
        Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
        Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
        Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
        Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
        Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
    }
}
