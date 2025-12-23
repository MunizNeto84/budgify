using Budgify.Domain.Enums;

namespace Budgify.Domain.Entities
{
    public abstract class  Account
    {
        public Guid Id { get; set; }
        public BankName Bank { get; set; }
        public decimal Balance { get; set; }

        public List<CreditCard> Cards { get; set; } = new();

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new Exception ("Valor deve ser positivo!");
            } 
                Balance += amount;
            
            
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Valor deve ser positivo!");
            }
            if (Balance < amount)
            {
                throw new Exception("Saldo insuficiente para o saque!");
            }
            Balance -= amount;
        }
    }
}
