using Fina.Api.Common.Api;
using Fina.Shared.Handler;
using Fina.Shared.Models;
using Fina.Shared.Requests.Categories;
using Fina.Shared.Responses;
using System.Security.Claims;

namespace Fina.Api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Excluir uma categoria")
            .WithDescription("Exclui uma categoria existente")
            .WithOrder(3)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(
            //ClaimsPrincipal user,
            ICategoryHandler handler,
            long id)
        {
            var request = new DeleteCategoryRequest
            {
                // UserId = user.Identity?.Name ?? string.Empty,
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handler.DeleteAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
