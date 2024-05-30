using Fina.Api.Common.Api;
using Fina.Shared.Handler;
using Fina.Shared.Models;
using Fina.Shared.Requests.Categories;
using Fina.Shared.Requests.Transactions;
using Fina.Shared.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
      => app.MapPut("/{id}", HandleAsync)
          .WithName("Transactions: Update")
          .WithSummary("Atualizar uma Transactions")
          .WithDescription("Atualiza uma Transactions existente")
          .WithOrder(2)
          .Produces<Response<Transaction?>>();
        private static async Task<IResult> HandleAsync(
            //ClaimsPrincipal user,
            ITransactionHandler handler,
            UpdateTransactionRequest request,
            long id)
        {
            request.UserId = ApiConfiguration.UserId;
            request.Id = id;

            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
