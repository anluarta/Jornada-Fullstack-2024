using Fina.Api.Common.Api;
using Fina.Shared.Handler;
using Fina.Shared.Models;
using Fina.Shared.Requests.Transactions;
using Fina.Shared.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Transactions: Create")
            .WithSummary("Criar uma transação")
            .WithDescription("Cria uma nova transação")
            .WithOrder(1)
            .Produces<Response<Transaction?>>();
        private static async Task<IResult> HandleAsync(
            //ClaimsPrincipal user,
            ITransactionHandler handler,
            CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;

            var result = await handler.CreateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
