using Fina.Shared.Models;
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

        Task<Response<Category?>> CreateAsync(Requests.Categories.CreateCategoryRequest request);
        Task<Response<Category?>> UpdateAsync(Requests.Categories.UpdateCategoryRequest request);
        Task<Response<Category?>> DeleteAsync(Requests.Categories.DeleteCategoryRequest request);
        Task<Response<Category?>> GetbyIdAsync(Requests.Categories.GetCategoryByIdRequest request);
        Task<PagedResponse<List<Category>>> GetAllAsync(Requests.Categories.GetAllCategoriesRequest request);
    
    }
}
