using Budgify.Application.Interfaces;
using Budgify.Domain.Enums;

namespace Budgify.ConsoleApp
{
    public class ConsoleUi
    {
        private readonly IAccountService _accountService;
        private readonly IIncomeService _incomeService;


        public ConsoleUi(IAccountService accountService, IIncomeService incomeService)
        {
            _accountService = accountService;
            _incomeService = incomeService;
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
                        Console.WriteLine("Despesa");
                        WaitUser();
                        break;
                    case 4:
                        Console.WriteLine("Resumo");
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
                        Console.Write("Banco: ");
                        string bank = Console.ReadLine()!;
                        Console.Write("Saldo Inicial: ");
                        decimal initialAmount = decimal.Parse(Console.ReadLine()!);
                        _accountService.CreateAccount(bank, initialAmount);
                        Console.WriteLine("Conta criada!");
                        WaitUser();
                        break;
                    case 2:
                        Console.WriteLine("Listar conta:");
                        var accounts = _accountService.GetAllAccounts();
                        if(accounts.Count == 0)
                        {
                            Console.WriteLine("Não existe conta");
                        } else
                        {
                            foreach (var account in accounts)
                            {
                                Console.WriteLine($"Conta: {account.Name} | Saldo: {account.Balance:C}");
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
                            Console.Write($"{i} {accounts[i].Name}");
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
            Console.WriteLine("2 - Listar Conta(s)\n");
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
        private void WaitUser()
        {
            Console.WriteLine("\nPressione ENTER para voltar ao menu...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
