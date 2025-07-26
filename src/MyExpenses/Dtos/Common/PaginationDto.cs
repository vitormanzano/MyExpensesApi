using System.ComponentModel.DataAnnotations;

namespace MyExpenses.Dtos.Common
{
    public class PaginationDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page deve ser maior que 0")]
        public int Page { get; set; } = 1;

        [Range(1, 20, ErrorMessage = "PageSize deve estar entre 1 e 20")]
        public int PageSize { get; set; } = 10;
    }
}
