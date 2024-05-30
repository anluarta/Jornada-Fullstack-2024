using Fina.Api.Data;
using Fina.Shared.Common;
using Fina.Shared.Handler;
using Fina.Shared.Models;
using Fina.Shared.Requests.Transactions;
using Fina.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handler
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { Type: Shared.Enums.ETransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= -1;
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreateAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Type = request.Type
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction?>(transaction, 201, "Transação criado com sucesso");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro ao criar transação");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if (transaction == null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação deletada com sucesso");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro ao deletar transação");
            }

        }

        public async Task<Response<Transaction?>> GetbyIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                return transaction is null
                    ? new Response<Transaction?>(null, 404, "Transação não encontrada")
                    : new Response<Transaction?>(transaction, 200, "Transação encontrada");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro ao buscar transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Nao foi possivel determinar a data de inicio");
            }
            try
            {
                var query = context
                    .Transactions
                    .AsNoTracking()
                    .Where(t =>
                    t.UserId == request.UserId &&
                    t.PaidOrReceivedAt >= request.StartDate &&
                    t.PaidOrReceivedAt <= request.EndDate)
                    .OrderBy(t => t.PaidOrReceivedAt);

                var transactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(data: transactions,
                                                          currentPage: request.PageNumber,
                                                          pageSize: request.PageSize,
                                                          totalCount: count);
            }catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Erro ao buscar transações");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            if (request is { Type: Shared.Enums.ETransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= -1;

            try
            {
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if (transaction == null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
                transaction.Title = request.Title;
                transaction.Type = request.Type;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction?>(transaction, 200, "Transação atualizada com sucesso");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro ao atualizar transação");
            }
        }
    }
}
