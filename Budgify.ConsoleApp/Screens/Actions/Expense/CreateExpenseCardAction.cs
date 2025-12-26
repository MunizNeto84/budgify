using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;
using Budgify.Domain.Enums;

namespace Budgify.ConsoleApp.Screens.Actions.Expense
{
    public class CreateExpenseCardAction: BaseScreen, IScreenAction
    {
        private readonly IAccountService _accountService;
        private readonly IExpenseService _expenseService;
        private readonly ICreditCardService _creditCardService;

        public CreateExpenseCardAction(IAccountService accountService, IExpenseService expenseService, ICreditCardService creditCardService)
        {
            _accountService = accountService;
            _expenseService = expenseService;
            _creditCardService = creditCardService;
        }

        public void Execute()
        {
            var accounts = _accountService.GetAllAccounts();
            if(accounts.Count == 0)
            {
                Console.WriteLine("⚠️ Nenhuma conta criada no momento.");
                return;
            }

            Console.WriteLine("🏦 Vincular a qual conta?");
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i} - {accounts[i].Bank}");
            }
            int index = ReadInt("🔢 Digite a opção");
            if (index < 0 || index >= accounts.Count)
            {
                Console.WriteLine("❌ Índice inválido.");
                WaitUser();
                return;
            }

            var selectAccount = accounts[index];

            var cards = _creditCardService.GetByAccountId(selectAccount.Id);
            Console.WriteLine("💳 Para qual cartão é essa despesa?");
            if (cards.Count > 0)
            {
                for (int i = 0;i < cards.Count;i++)
                {
                    Console.WriteLine($"{i} {cards[i].Name} | Limite disponivel: {cards[i].AvailableLimit}");
                }
            }
            else
            {
                Console.WriteLine("⚠️ Nenhum cartão cadastrado no momento.");
            }

            index = ReadInt("🔢 Digite a opção");
            if (index < 0 || index >= accounts.Count)
            {
                Console.WriteLine("❌ Índice inválido.");
                WaitUser();
                return;
            }

            var selectCard = cards[index];

            string description = ReadString("Descrição");
            decimal amount = ReadDecimal("Valor");
            int installments = ReadInt("Quantas parcelas?");
            var category = ReadEnum<ExpenseCategory>("Selecione a categoria");
            _expenseService.CreateCardExpense(selectCard.Id, amount, installments, category, description);
            
            Console.WriteLine("\n✅ Despesa lançada com sucesso.");
            WaitUser();
        }
    }
}
