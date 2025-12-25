using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Screens.Actions.Income;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgify.ConsoleApp.Screens
{
    public class IncomeMenuScreen: BaseScreen
    {
        private readonly IAccountService _accountService;
        private readonly IIncomeService _incomeService;

        public IncomeMenuScreen(IAccountService accountService, IIncomeService incomeService)
        {
            _accountService = accountService;
            _incomeService = incomeService;
        }

        public void Show()
        {
            int option = -1;
            do
            {
                ShowHeader("💰 Receitas");
                Console.WriteLine("1 - 💲 Lançar receita");
                Console.WriteLine("2 - 📋 Listar receitas");
                Console.WriteLine("\n0 - ↪️ Voltar");

                option = ReadInt("Opção");
                switch (option)
                {
                    case 1:
                        new CreateIncomeAction(_accountService, _incomeService).Execute();
                        break;
                    case 2:
                        new ListIncomesAction(_incomeService).Execute();
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
