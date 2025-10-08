﻿using Budgify.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Budgify.ConsoleApp
{
    public class ConsoleUi
    {
        private readonly IServiceProvider _services;

        public ConsoleUi(IServiceProvider services)
        {
            _services = services;
        }

        public void Run()
        {
            int option;

            do
            {
                ShowMenu();
                option = int.Parse(Console.ReadLine()!);
                Console.Clear();

                if (option == 1)
                {
                    HandleCreateAccount();
                }
                else if (option == 2)
                {
                    HandleCreateIncome();
                }
                else if (option == 3)
                {
                    HandleCreateExpense();
                }
                else if (option == 0)
                {
                    Console.WriteLine("\nSaindo do Budgify...");
                    Console.Clear();
                }

            } while (option != 0);
        }

        public void ShowMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("=================== BUGDIFY ===================");
            Console.WriteLine("===============================================");

            Console.WriteLine("1 - Cadastro de Nova Conta.");
            Console.WriteLine("2 - Cadastro de Nova Receita.");
            Console.WriteLine("3 - Cadastro de Nova Despesa.");
            Console.WriteLine("0 - Sair.");

            Console.Write("\nDigite uma opção: ");
        }

        public void HandleCreateAccount()
        {
            var accountService = _services.GetRequiredService<IAccountService>();

            Console.WriteLine("--- Cadastro de Nova Conta. ---");
            Console.Write("Nome do Branco: ");
            var bankName = Console.ReadLine()!;
            Console.Write("Saldo Inicial: ");
            var initialBalance = decimal.Parse(Console.ReadLine()!);
            Console.Write("Possui Cartão de Crédito (s/n): ");
            var hasCreditCard = Console.ReadLine()!.ToLower() == "s";

            accountService.CreateAccount(bankName, initialBalance, hasCreditCard);
            Console.WriteLine("\nConta criada com sucesso!");
            Console.WriteLine("Precione Enter para contunuar...");
            Console.ReadLine();
            Console.Clear();
        }

        public void HandleCreateIncome()
        {
            var incomeService = _services.GetRequiredService<IIncomeService>();

            Console.WriteLine("--- Cadastro de Nova Receita. ---");
            var date = DateTime.UtcNow;
            Console.Write("Categoria: ");
            var category = Console.ReadLine()!;
            Console.Write("Tipo: ");
            var incomeType = Console.ReadLine()!;
            Console.Write("Valor: ");
            var amount = decimal.Parse(Console.ReadLine()!);

            incomeService.CreateIncome(date, category, incomeType, amount);
            Console.WriteLine("\nReceita criada com sucesso!");
            Console.WriteLine("Precione Enter para contunuar...");
            Console.ReadLine();
            Console.Clear();
        }

        public void HandleCreateExpense()
        {
            var expenseService = _services.GetRequiredService<IExpenseService>();

            Console.WriteLine("--- Cadastro de Nova Despesa. ---");
            var date = DateTime.UtcNow;
            Console.Write("Categoria: ");
            var category = Console.ReadLine()!;
            Console.Write("Tipo: ");
            var expenseType = Console.ReadLine()!;
            Console.Write("Valor: ");
            var amount = decimal.Parse(Console.ReadLine()!);

            expenseService.CreateExpense(date, category, expenseType, amount);
            Console.WriteLine("\nDespesa criada com sucesso!");
            Console.WriteLine("Precione Enter para contunuar...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
