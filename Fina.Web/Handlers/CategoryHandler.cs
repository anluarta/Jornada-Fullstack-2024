using Fina.Shared.Handler;
using Fina.Shared.Models;
using Fina.Shared.Requests.Categories;
using Fina.Shared.Responses;
using System.Net.Http.Json;

namespace Fina.Web.Handlers
{
    public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);

        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("v1/categories", request);
            return await response.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha cria categoria");
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            var response = await _httpClient.DeleteAsync($"v1/categories/{request.Id}");
            return await response.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha deletar categoria");
        }

        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
        => await _httpClient.GetFromJsonAsync<PagedResponse<List<Category>?>>("v1/categories")
            ?? new PagedResponse<List<Category>?>(null, 400, "Não foi possível obter as categorias");
        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        => await _httpClient.GetFromJsonAsync<Response<Category?>>($"v1/categories/{request.Id}")
            ?? new Response<Category?>(null, 400, "Falha buscar categoria");

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"v1/categories/{request.Id}", request);
            return await response.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha atualizar categoria");
        }
    }
}
