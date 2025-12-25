namespace Budgify.ConsoleApp.Entities
{
    public abstract class BaseScreen
    {
        public void WaitUser()
        {
            Console.WriteLine("\nPressione ENTER para voltar ao menu anterior...");
            Console.ReadLine();
            Console.Clear();
        }

        public void ShowHeader(string title)
        {
            Console.Clear();
            int letterCount = title.Length;
            string hashtags = string.Empty.PadLeft(letterCount, '#');
            Console.WriteLine(hashtags);
            Console.WriteLine(title.ToUpper());
            Console.WriteLine(hashtags + "\n");
        }

        public String ReadString(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine() ?? "";
        }

        public int ReadInt(string prompt)
        {
            Console.Write($"{prompt}: ");
            int.TryParse(Console.ReadLine(), out int result);
            return result;
        }
        public decimal ReadDecimal(string prompt)
        {
            Console.Write($"{prompt}: ");
            decimal.TryParse(Console.ReadLine(), out decimal result);
            return result;
        }

        public T ReadEnum<T>(string prompt) where T : struct, Enum
        {
            Console.WriteLine($"\n{prompt}");
            foreach (var value in Enum.GetValues<T>())
            {
                Console.WriteLine($"{(int)(object)value} - {value}");
            }
            Console.Write("Escolha Opção: ");

            if (int.TryParse(Console.ReadLine(), out int id) && Enum.IsDefined(typeof(T), id))
            {
                return (T)(object)id;
            }
            return default(T);
        }
    }
}
