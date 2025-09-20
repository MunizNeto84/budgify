namespace Budgify.Domain;

public class Account
{
    public string BankName { get; set; }
    public decimal InitialBalance { get; set; }
    public bool HasCreditCard { get; set; }
}
