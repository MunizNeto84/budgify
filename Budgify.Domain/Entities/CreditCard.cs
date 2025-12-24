namespace Budgify.Domain.Entities
{
    public class CreditCard
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Limit { get; set; }
        public int ClosingDay { get; set; }
        public int DueDay { get; set; }
        public decimal AvailableLimit { get; set; }

        public void Purchase(decimal amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Valor deve ser positivo!");
            }

            if (amount > AvailableLimit)
            {
                throw new Exception("Limite insuficiente!");
            }
            AvailableLimit -= amount;

        }

    }
}
