namespace Budgify.Application.Dtos
{
    public class FutureInvoiceDto
    {
        public string MonthYear { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
