using Fina.Api.Common.Api;
using Fina.Api.Endpoints.Categories;
using Fina.Api.Endpoints.Transactions;

namespace Fina.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoint = app.MapGroup("");

            endpoint.MapGroup("/")
                .WithTags("Health check")
                .MapGet("/", () => new { Message = "OK" });

            endpoint.MapGroup("v1/categories")
                .WithTags("Categories")
                //.RequireAuthorization()
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<GetCategoryByIdEndpoint>()
                .MapEndpoint<GetAllCategoriesEndpoint>();

            endpoint.MapGroup("v1/transactions")
                .WithTags("Transactions")
                //.RequireAuthorization()
                .MapEndpoint<CreateTransactionEndpoint>()
                .MapEndpoint<UpdateTransactionEndpoint>()
                .MapEndpoint<DeleteTransactionEndpoint>()
                .MapEndpoint<GetTransactionByIdEndpoint>()
                .MapEndpoint<GetTransactionsByPeriodEndpoint>();


        }
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
