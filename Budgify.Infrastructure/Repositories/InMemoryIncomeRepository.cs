using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;

namespace Budgify.Infrastructure.Repositories
{
    public class InMemoryIncomeRepository: IIncomeRepository
    {
        public static readonly List<Income> _incomes = new List<Income>();
        public void Add(Income income)
        {
            _incomes.Add(income);
        }

        public List<Income> GetAll()
        {
            return _incomes; 
        }
    }
}
