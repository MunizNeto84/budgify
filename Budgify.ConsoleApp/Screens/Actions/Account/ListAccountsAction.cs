
using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;

namespace Budgify.ConsoleApp.Screens.Actions.Account
{
    public class ListAccountsAction: BaseScreen, IScreenAction
    {
        private readonly IAccountService _accountService;
        private readonly ICreditCardService _creditCardService;

        public ListAccountsAction(IAccountService accountService, ICreditCardService creditCardService)
        {
            _accountService = accountService;
            _creditCardService = creditCardService;
        }

        public void Execute()
        {
            ShowHeader("📋 Minhas contas e cartões");

            var accounts = _accountService.GetAllAccounts();

            if (accounts.Count == 0)
            {
                Console.WriteLine("⚠️ Nenhuma conta criada no momento.");
                return;
            } else
            {
                foreach (var account in accounts)
                {
                    Console.WriteLine($"🏦 {account.Bank} | Saldo: {account.Balance:C}");

                    var cards = _creditCardService.GetByAccountId(account.Id);
                    if(cards.Count > 0)
                    {
                        foreach(var card in cards)
                        {
                            Console.WriteLine($"💳 {card.Name} | Limite: {card.Limit:C} | Vencde dia {card.DueDay}");
                        }
                    }
                    Console.WriteLine("--------------------------------");
                }
            }
            WaitUser();
        }
    }
}
