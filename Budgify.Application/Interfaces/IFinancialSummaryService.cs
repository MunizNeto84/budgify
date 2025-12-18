using Budgify.Application.Dtos;

namespace Budgify.Application.Interfaces
{
    public interface IFinancialSummaryService
    {
       FinancialSummaryDto GetSummary();
    }
}
