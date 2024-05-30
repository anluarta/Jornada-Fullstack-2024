using Fina.Api.Common.Api;
using Fina.Shared;
using Fina.Shared.Handler;
using Fina.Shared.Models;
using Fina.Shared.Requests.Categories;
using Fina.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Categories: Get All")
            .WithSummary("Listar todas as categorias")
            .WithDescription("Lista todas as categorias existentes")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();

        private static async Task<IResult> HandleAsync(
            //ClaimsPrincipal user,
            ICategoryHandler handler,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllCategoriesRequest
            {
                // UserId = user.Identity?.Name ?? string.Empty,
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await handler.GetAllAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
