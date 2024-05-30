using Fina.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fina.Shared.Requests.Transactions
{
    public class CreateTransactionRequest : Request
    {
        [Required(ErrorMessage = "Titulo invalido")]
        public string Title { get; set; }=string.Empty;

        [Required(ErrorMessage = "Tipo invalida")]
        public ETransactionType Type { get; set; } 

        [Required(ErrorMessage = "Valor invalido")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Categoria invalida")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data invalida")]
        public DateTime? PaidOrReceivedAt { get; set; }
    }
}
