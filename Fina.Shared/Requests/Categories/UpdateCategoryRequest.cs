using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fina.Shared.Requests.Categories
{
    public class UpdateCategoryRequest :Request
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Titulo Invalido")]
        [MaxLength(80, ErrorMessage = "O titulo deve conter ate 80 caracterses")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Descricao Invalida")]
        public string Description { get; set; }
    }
}
