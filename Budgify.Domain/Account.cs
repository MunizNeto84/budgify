namespace Budgify.Domain;

public class Account
{
    public Guid Id { get; set; }
    public string BankName { get; set; }
    public decimal InitialBalance { get; set; }
    public bool HasCreditCard { get; set; }

    public void TestMethod()
    {
        int unusedVariable = 5; // Esta variável não é usada
    }
}
