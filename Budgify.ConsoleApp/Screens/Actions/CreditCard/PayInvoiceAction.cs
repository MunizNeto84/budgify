using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;

namespace Budgify.ConsoleApp.Screens.Actions.CreditCard
{
    public class PayInvoiceAction: BaseScreen, IScreenAction
    {
        private readonly IAccountService _accountService;
        private readonly ICreditCardService _creditCardService;
        public PayInvoiceAction(IAccountService accountService, ICreditCardService creditCardService)
        {
            _accountService = accountService;
            _creditCardService = creditCardService;
        }

        public void Execute()
        {
            var accounts = _accountService.GetAllAccounts();
            if (accounts.Count == 0)
            {
                Console.WriteLine("⚠️ Nenhuma conta criada no momento.");
                return;
            }

            ShowHeader("💳 Pagar fatura do cartão");
            Console.WriteLine("🏦 Para qual conta pertence o cartão?");
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i} - {accounts[i].Bank}");
            }
            int index = ReadInt("🔢 Digite a opção");
            if (index < 0 || index >= accounts.Count)
            {
                Console.WriteLine("❌ Índice inválido.");
                WaitUser();
                return;
            }

            var selectAccount = accounts[index];
            var cards = _creditCardService.GetByAccountId(selectAccount.Id);
            if (cards.Count > 0)
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    Console.WriteLine($"{i} {cards[i].Name} | Limite disponivel: {cards[i].AvailableLimit}");
                }
            }
            else
            {
                Console.WriteLine("⚠️ Nenhum cartão cadastrado no momento.");
            }

            index = ReadInt("🔢 Digite a opção");
            if (index < 0 || index >= accounts.Count)
            {
                Console.WriteLine("❌ Índice inválido.");
                WaitUser();
                return;
            }
            var selectCard = cards[index];


            try
            {
                Console.WriteLine("⌛ Processamento pagamento das faturas ...");
                _creditCardService.PayInvoice(selectCard.Id);
                Console.WriteLine("\n✅ Fatura paga com sucesso.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Não foi possivel realizar o pagamento da fatura: {ex.Message}");
            }

            
            WaitUser();
        }
    }
}
