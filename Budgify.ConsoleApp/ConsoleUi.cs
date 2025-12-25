using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Screens;
using Budgify.Domain.Entities;
using Budgify.Domain.Enums;


namespace Budgify.ConsoleApp
{
    public class ConsoleUi
    {
        private readonly IAccountService _accountService;
        private readonly IIncomeService _incomeService;
        private readonly IExpenseService _expenseService;
        private readonly IFinancialSummaryService _financialSummaryService;
        private readonly ICreditCardService _creditCardService;


        public ConsoleUi(IAccountService accountService, IIncomeService incomeService, IExpenseService expenseService, IFinancialSummaryService financialSummaryService, ICreditCardService creditCardService)
        {
            _accountService = accountService;
            _incomeService = incomeService;
            _expenseService = expenseService;
            _financialSummaryService = financialSummaryService;
            _creditCardService = creditCardService;
        }

        public void Run()
        {
            int option = -1;

            do
            {
                ShowMenu();
                Console.Write("\nDigite uma opção: ");
                var input = Console.ReadLine()!;
                if(!int.TryParse(input, out option))
                {
                    option = -1;
                }

                Console.Clear();

                switch (option)
                {
                    case 1:
                        new AccountMenuScreen(_accountService, _creditCardService).Show();
                        break;
                    case 2:
                        new IncomeMenuScreen(_accountService, _incomeService).Show();
                        break;
                    case 3:
                        HandleExpenseMenu();
                        WaitUser();
                        break;
                    case 4:
                        var summary = _financialSummaryService.GetSummary();
                        Console.WriteLine("Resumo:");
                        Console.WriteLine($"Total Receitas: {summary.TotalIncome:C}");
                        Console.WriteLine($"Total Despesas: {summary.TotalExpense:C}");
                        Console.WriteLine();

                        if(summary.Balance >= 0)
                        {
                            Console.WriteLine($"Saldo final: {summary.Balance:C} ✅");
                        } else
                        {
                            Console.WriteLine($"Saldo final: {summary.Balance:C} 🚨");
                        }

                        WaitUser();
                        break;
                    case 0:
                        Console.WriteLine("Saindo [...]");
                        WaitUser();
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        WaitUser();
                        break;
                }

            } while (option != 0);

        }

        private void HandleExpenseMenu()
        {
            int option = -1;
            do
            {
                ShowExpenseMenu();
                Console.Write("\nDigite uma opção: ");
                var input = Console.ReadLine()!;
                if (!int.TryParse(input, out option))
                {
                    option = -1;
                }

                Console.Clear();

                switch (option)
                {
                    case 1:
                        var accounts = _accountService.GetAllAccounts();
                        if (accounts.Count == 0)
                        {
                            Console.WriteLine("Você precisa criar uma conta antes");
                            WaitUser();
                            break;
                        }

                        Console.WriteLine("Lançar Despesa:");
                        Console.WriteLine("1 - Debito");
                        Console.WriteLine("2 - Credito");

                        option = int.Parse(Console.ReadLine()!);

                        if (option == 1)
                        {
                            Console.WriteLine("Para qual conta é essa despesa? \n");
                            for (int i = 0; i < accounts.Count; i++)
                            {
                                Console.Write($"{i} {accounts[i].Bank}");
                            }
                            Console.WriteLine();
                            Console.Write("\nDigite o número da conta: ");
                            int index = int.Parse(Console.ReadLine()!);
                            var selectedAccount = accounts[index];

                            Console.Write("Descrição: ");
                            string description = Console.ReadLine()!;

                            Console.Write("Valor: ");
                            decimal amount = decimal.Parse(Console.ReadLine()!);

                            Console.WriteLine("\nSelecione a categoria:");
                            foreach (var cat in Enum.GetValues<ExpenseCategory>())
                            {
                                Console.WriteLine($"{(int)cat} - {cat}");
                            }
                            Console.Write("Opção: ");
                            int catOption = int.Parse(Console.ReadLine()!);

                            ExpenseCategory category = (ExpenseCategory)catOption;

                            _expenseService.CreateExpense(selectedAccount.Id, amount, DateTime.Now, category, description);

                            Console.WriteLine("Despesa lançada no sistema!");
                            WaitUser();
                            break;
                        } else if (option == 2)
                        {
                            Console.WriteLine("Para qual conta é essa despesa? \n");
                            for (int i = 0; i < accounts.Count; i++)
                            {
                                Console.Write($"{i} {accounts[i].Bank}");
                            }
                            Console.WriteLine();
                            Console.Write("\nDigite o número da conta: ");
                            int indexAccount = int.Parse(Console.ReadLine()!);
                            var selectedCardAccount = accounts[indexAccount];
                            var cardAccountId = accounts[indexAccount].Id;

                            var cards = _creditCardService.GetByAccountId(cardAccountId);
                            Console.WriteLine("Para qual cartão é essa despesa? \n");
                            if (cards.Count > 0)
                            {
                                for (int i = 0; i < cards.Count; i++)
                                {
                                    Console.Write($"{i} {cards[i].Name} | Limite disponivél: {cards[i].AvailableLimit}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Não há cartões vinculados");
                            }
                            Console.Write("\nDigite o número do cartão: ");
                            int indexCard = int.Parse(Console.ReadLine()!);
                            var selectedCard = cards[indexCard];


                            Console.Write("Descrição: ");
                            string descriptionCard = Console.ReadLine()!;

                            Console.Write("Valor: ");
                            decimal amountCard = decimal.Parse(Console.ReadLine()!);

                            Console.Write("Quantas parcelas: ");
                            int installments = int.Parse(Console.ReadLine()!);


                            Console.WriteLine("\nSelecione a categoria:");
                            foreach (var cat in Enum.GetValues<ExpenseCategory>())
                            {
                                Console.WriteLine($"{(int)cat} - {cat}");
                            }
                            Console.Write("Opção: ");
                            int catCardOption = int.Parse(Console.ReadLine()!);

                            ExpenseCategory categoryCard = (ExpenseCategory)catCardOption;

                            _expenseService.CreateCardExpense(selectedCard.Id, amountCard, installments, categoryCard, descriptionCard);

                            Console.WriteLine("Despesa do cartão de credito lançada no sistema!");
                            WaitUser();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida.");
                        }
                        WaitUser();
                        break;



                    case 2:
                                var accountsCard = _accountService.GetAllAccounts();
                                if (accountsCard.Count == 0)
                                {
                                    Console.WriteLine("Você precisa criar uma conta antes");
                                    WaitUser();
                                    break;
                                }
                        Console.WriteLine("Pagar fatura do cartão:");
                                Console.WriteLine("Para qual conta é essa despesa? \n");
                                for (int i = 0; i < accountsCard.Count; i++)
                                {
                                    Console.Write($"{i} {accountsCard[i].Bank}");
                                }
                                Console.WriteLine();
                                Console.Write("\nDigite o número da conta: ");
                                int indexAccount2 = int.Parse(Console.ReadLine()!);
                                var selectedCardAccount2 = accountsCard[indexAccount2];
                                var cardAccountId2 = accountsCard[indexAccount2].Id;

                                var cards2 = _creditCardService.GetByAccountId(cardAccountId2);
                                Console.WriteLine("Para qual cartão é vai pagar a fatura? \n");
                                if (cards2.Count > 0)
                                {
                                    for (int i = 0; i < cards2.Count; i++)
                                    {
                                        Console.Write($"{i} {cards2[i].Name} | Limite disponivél: {cards2[i].AvailableLimit}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Não há cartões vinculados");
                                }
                                Console.Write("\nDigite o número do cartão: ");
                                int indexCard2 = int.Parse(Console.ReadLine()!);
                                var selectedCard2 = cards2[indexCard2];
                        _creditCardService.PayInvoice(selectedCard2.Id);

                        

                        Console.WriteLine("Fatura paga");


                        WaitUser();
                        break;
                                
                           
                    case 3:
                        Console.WriteLine("Listar despesas lançadas:");
                        var expenses = _expenseService.GetAllExpenses();
                        if (expenses.Count == 0)
                        {
                            Console.WriteLine("Não existe despesas lançadas");
                        }
                        else
                        {
                            foreach (var expense in expenses)
                            {
                                Console.WriteLine($"{expense.Date.ToShortDateString()} | {expense.Description} | {expense.Amount:C} | ({expense.Category})");
                            }
                        }

                        WaitUser();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        WaitUser();
                        break;




                }


            } while (option != 0);
        }

        private void ShowMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("=================== BUDGIFY ===================");
            Console.WriteLine("===============================================");
            Console.WriteLine("1 - Conta");
            Console.WriteLine("2 - Receita");
            Console.WriteLine("3 - Despesa");
            Console.WriteLine("4 - Resumo\n");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("===============================================");
        }

         

       

        private void ShowExpenseMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("==================== EXPENSE ==================");
            Console.WriteLine("===============================================");
            Console.WriteLine("1 - Lançar despesa");
            Console.WriteLine("2 - Pagar fatura do cartão");
            Console.WriteLine("3 - Listar despesas(s)\n");
            Console.WriteLine("0 - Voltar");
            Console.WriteLine("===============================================");
        }
        private void WaitUser()
        {
            Console.WriteLine("\nPressione ENTER para voltar ao menu...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
