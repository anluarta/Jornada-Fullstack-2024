using Fina.Api.Common.Api;
using Fina.Shared.Handler;
using Fina.Shared.Models;
using Fina.Shared.Requests.Categories;
using Fina.Shared.Responses;

namespace Fina.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Obter uma categoria")
            .WithDescription("Obtém uma categoria existente")
            .WithOrder(4)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(
            //ClaimsPrincipal user,
            ICategoryHandler handler,
            long id)
        {
            var request = new GetCategoryByIdRequest
            {
                // UserId = user.Identity?.Name ?? string.Empty,
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
