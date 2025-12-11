using Budgify.Domain.Entities;

namespace Budgify.Application.Interfaces
{
    public interface IIncomeRepository
    {
        public void Add(Income income);
        List<Income> GetAll();
    }
}
