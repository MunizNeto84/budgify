using Budgify.Application.Interfaces;

namespace Budgify.ConsoleApp
{
    public class ConsoleUi
    {
        private readonly IAccountService _accountService;

        public ConsoleUi(IAccountService accountService)
        {
            _accountService = accountService;
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
                        Console.WriteLine("Receita");
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
        private void WaitUser()
        {
            Console.WriteLine("\nPressione ENTER para voltar ao menu...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
