namespace Budgify.ConsoleApp
{
    public class ConsoleUi
    {
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
                        Console.WriteLine("Conta");
                        WaitUser();
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

        private void WaitUser()
        {
            Console.WriteLine("\nPressione ENTER para voltar ao menu...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
