using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;
using Budgify.Domain.Enums;
using System.Threading.Channels;

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
                        HandleAccountMenu();
                        break;
                    case 2:
                        HandleIncomeMenu();
                        WaitUser();
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

        private void HandleAccountMenu()
        {

            int option = -1;
            do
            {
                ShowAccountMenu();
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
                        Console.WriteLine("Criar conta:");
                        Console.Write("Selecione o Banco: ");
                        foreach (var bank in Enum.GetValues<BankName>())
                        {
                            Console.WriteLine($"{(int)bank} - {bank}");
                        }
                        Console.Write("Opção: ");
                        int bankOption = int.Parse(Console.ReadLine()!);

                        BankName bankName = (BankName)bankOption;

                        Console.Write("Saldo Inicial: ");
                        decimal initialAmount = decimal.Parse(Console.ReadLine()!);

                        Console.Write("Selecione o tipo da conta: ");
                        foreach (var type in Enum.GetValues<AccountType>())
                        {
                            Console.WriteLine($"{(int)type} - {type}");
                        }
                        Console.Write("Opção: ");
                        int typeOption = int.Parse(Console.ReadLine()!);

                        AccountType accountType = (AccountType)typeOption;

                        _accountService.CreateAccount(bankName, initialAmount, accountType);
                        Console.WriteLine("Conta criada!");
                        WaitUser();
                        break;
                    case 2:
                        Console.WriteLine("Criar cartão:");
                        var accountsCard = _accountService.GetAllAccounts();
                        if(accountsCard.Count == 0)
                        {
                            Console.WriteLine("Crie uma conta antes!");
                            WaitUser();
                            break;
                        }
                        
                        Console.WriteLine("Vincular cartão a qual conta?");
                        for (int i = 0; i < accountsCard.Count; i++)
                        {
                            Console.Write($"{i} {accountsCard[i].Bank}");
                        }
                        Console.Write("\nDigite o indice da conta: ");
                        int index = int.Parse(Console.ReadLine()!);
                        var acc = accountsCard[index];

                        Console.Write("\nApelido do cartão: ");
                        string cardName = Console.ReadLine()!;

                        Console.Write("Limite do cartão: ");
                        int limit = int.Parse(Console.ReadLine()!);

                        Console.Write("Dia do fechamento: ");
                        int closingDay = int.Parse(Console.ReadLine()!);

                        Console.Write("Dia do vencimento: ");
                        int dueDay = int.Parse(Console.ReadLine()!);

                        _creditCardService.CreateCreditCard(acc.Id, cardName, limit, closingDay, dueDay);

                        Console.WriteLine("Cartão criado, com sucesso!");

                        WaitUser();
                        break;
                    case 3:
                        Console.WriteLine("Listar conta:");
                        var accountsList = _accountService.GetAllAccounts();
                        if (accountsList.Count == 0)
                        {
                            Console.WriteLine("Não existe conta");
                        }
                        else
                        {
                            foreach (var account in accountsList)
                            {
                                Console.WriteLine($"Conta: {account.Bank} | Saldo: {account.Balance:C}");
                                var cards = _creditCardService.GetByAccountId(account.Id);
                                if(cards.Count > 0)
                                {
                                    foreach (var card in cards)
                                    {
                                        Console.WriteLine($"Cartão: {card.Name} | Limite: {card.Limit:C} | Vence dia: {card.DueDay}");
                                    }
                                } else
                                {
                                    Console.WriteLine("Não há cartões vinculados");
                                }
                            }
                        }
                        WaitUser();
                        break;
                    case 0:
                        break;
                    default :
                        Console.WriteLine("Opção inválida.");
                        WaitUser();
                        break;
                }


            } while (option != 0);
         }

        private void HandleIncomeMenu()
        {
            int option = -1;
            do
            {
                ShowIncomeMenu();
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

                        Console.WriteLine("Lançar Receita:");
                        Console.WriteLine("Para qual conta é essa receita? \n");
                        for (int i = 0;  i < accounts.Count; i++)
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
                        foreach (var cat in Enum.GetValues<IncomeCategory>())
                        {
                            Console.WriteLine($"{(int)cat} - {cat}");
                        }
                        Console.Write("Opção: ");
                        int catOption = int.Parse(Console.ReadLine()!);

                        IncomeCategory category = (IncomeCategory)catOption;

                        _incomeService.CreateIncome(selectedAccount.Id, amount, DateTime.Now, category, description);
                        
                        Console.WriteLine("Receita lançada no sistema!");
                        WaitUser();
                        break;
                    case 2:
                        Console.WriteLine("Listar receitas lançadas:");
                        var incomes = _incomeService.GetAllIncomes();
                        if (incomes.Count == 0)
                        {
                            Console.WriteLine("Não existe receitas lançadas");
                        }
                        else
                        {
                            foreach (var income in incomes)
                            {
                                Console.WriteLine($"{income.Date.ToShortDateString()} | {income.Description} | {income.Amount:C} | ({income.Category})");
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
                    case 2:
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

        private void ShowAccountMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("==================== CONTA ====================");
            Console.WriteLine("===============================================");
            Console.WriteLine("1 - Criar Conta");
            Console.WriteLine("2 - Criar Cartão");
            Console.WriteLine("3 - Listar Conta(s)\n");
            Console.WriteLine("0 - Voltar");
            Console.WriteLine("===============================================");
        }

        private void ShowIncomeMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("==================== INCOME ===================");
            Console.WriteLine("===============================================");
            Console.WriteLine("1 - Lançar Receita");
            Console.WriteLine("2 - Listar receitas(s)\n");
            Console.WriteLine("0 - Voltar");
            Console.WriteLine("===============================================");
        }

        private void ShowExpenseMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("==================== EXPENSE ==================");
            Console.WriteLine("===============================================");
            Console.WriteLine("1 - Lançar Despesa");
            Console.WriteLine("2 - Listar despesas(s)\n");
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
