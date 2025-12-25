using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Screens.Actions.Account;
using Budgify.ConsoleApp.Screens.Actions.CreditCard;
using System;

namespace Budgify.ConsoleApp.Screens
{
    public class AccountMenuScreen: BaseScreen
    {
        private readonly IAccountService _accountService;
        private readonly ICreditCardService _creditCardService;

        public AccountMenuScreen(IAccountService accountService, ICreditCardService creditCardService)
        {
            _accountService = accountService;
            _creditCardService = creditCardService;
        }

        public void Show()
        {
            int option = -1;
            do
            {
                ShowHeader("👥 Gestão de contas:");
                Console.WriteLine("1 - 🏦 Criar conta");
                Console.WriteLine("2 - 💳 Adicionar cartão");
                Console.WriteLine("3 - 📋 Listar");
                Console.WriteLine("\n0 - ↪️ Voltar");

                option = ReadInt("Opção");

                switch (option)
                {
                    case 1:
                        new CreateAccountAction(_accountService).Execute();
                        break;
                    case 2:
                        new CreateCardAction(_accountService, _creditCardService).Execute();
                        break;
                    case 3:
                        new ListAccountsAction(_accountService, _creditCardService).Execute();
                        break;
                    case 0:
                        Console.WriteLine("Voltando...");
                        WaitUser();
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        WaitUser();
                        break;

                }

            } while (option != 0);
        }
    }
}
