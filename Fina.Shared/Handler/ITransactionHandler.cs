using Fina.Shared.Models;
using Fina.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fina.Shared.Handler
{
    public interface ITransactionHandler
    {
        Task<Response<Transaction?>> CreateAsync(Requests.Transactions.CreateTransactionRequest request);
        Task<Response<Transaction?>> UpdateAsync(Requests.Transactions.UpdateTransactionRequest request);
        Task<Response<Transaction?>> DeleteAsync(Requests.Transactions.DeleteTransactionRequest request);
        Task<Response<Transaction?>> GetbyIdAsync(Requests.Transactions.GetTransactionByIdRequest request);
        Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(Requests.Transactions.GetTransactionsByPeriodRequest request);
    }
}
