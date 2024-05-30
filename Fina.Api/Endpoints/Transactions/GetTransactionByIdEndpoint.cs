using Fina.Api.Common.Api;
using Fina.Api.Handler;
using Fina.Shared.Handler;
using Fina.Shared.Models;
using Fina.Shared.Requests.Categories;
using Fina.Shared.Requests.Transactions;
using Fina.Shared.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
       => app.MapGet("/{id}", HandleAsync)
           .WithName("Transactions: Get By Id")
           .WithSummary("Obter uma Transactions")
           .WithDescription("Obtém uma Transactions existente")
           .WithOrder(4)
           .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(
        //ClaimsPrincipal user,
            ITransactionHandler handler,
            long id)
        {
            var request = new GetTransactionByIdRequest
            {
                // UserId = user.Identity?.Name ?? string.Empty,
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handler.GetbyIdAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
