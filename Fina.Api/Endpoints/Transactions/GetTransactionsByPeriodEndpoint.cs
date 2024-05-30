using Fina.Api.Common.Api;
using Fina.Shared;
using Fina.Shared.Handler;
using Fina.Shared.Models;
using Fina.Shared.Requests.Transactions;
using Fina.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
            .WithName("Transactions: Get By Period")
            .WithSummary("Listar transações por período")
            .WithDescription("Lista todas as transações existentes dentro de um período")
            .WithOrder(5)
            .Produces<PagedResponse<List<Transaction>?>>();
        private static async Task<IResult> HandleAsync(
            //ClaimsPrincipal user,
            ITransactionHandler handler,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
            
        {
            var request = new GetTransactionsByPeriodRequest
            {
                // UserId = user.Identity?.Name ?? string.Empty,
                UserId = ApiConfiguration.UserId,
                StartDate = startDate,
                EndDate = endDate,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await handler.GetByPeriodAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
