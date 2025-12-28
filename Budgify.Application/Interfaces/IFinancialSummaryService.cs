using Budgify.Application.Dtos;

namespace Budgify.Application.Interfaces
{
    public interface IFinancialSummaryService
    {
        FinancialSummaryDto GetSummary();
        MonthlySummaryDto GetMonthlySummary(int month, int year);
        YearlySummaryDto GetYearlySummary(int year);

        List<FutureInvoiceDto> GetFutureInvoices();
    }
}
