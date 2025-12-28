namespace Budgify.Application.Dtos
{
    public class YearlySummaryDto
    {
        public int Year { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal YearlyBalance { get; set; }
    }
}
