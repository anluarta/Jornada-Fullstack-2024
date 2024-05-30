using Fina.Api.Common.Api;
using Fina.Shared.Handler;
using Fina.Shared.Models;
using Fina.Shared.Requests.Categories;
using Fina.Shared.Requests.Transactions;
using Fina.Shared.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Excluir uma transação")
            .WithDescription("Exclui uma Transação existente")
            .WithOrder(3)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(
            //ClaimsPrincipal user,
            ITransactionHandler handler,
            long id)
        {
            var request = new DeleteTransactionRequest
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
