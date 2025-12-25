using Budgify.Application.Interfaces;
using Budgify.Application.Services;
using Budgify.Domain.Entities;
using Budgify.Domain.Enums;
using Moq; 
using Xunit; 

namespace Budgify.UnitTests;

public class AccountServiceTests
{
    
    [Fact]
    public void CreateAccount_CreateChecking()
    {
        var mockRepo = new Mock<IAccountRepository>();
        var service = new AccountService(mockRepo.Object);

        var bank = BankName.Nubank;
        var balance = 1000m;
        var type = AccountType.Checking;

        service.CreateAccount(bank, balance, type);
        mockRepo.Verify(repo => repo.Add(It.IsAny<CheckingAccount>()), Times.Once);
    }

    [Fact]
    public void CreateAccountCreateInvestment()
    {
    
        var mockRepo = new Mock<IAccountRepository>();
        var service = new AccountService(mockRepo.Object);

        service.CreateAccount(BankName.Inter, 5000m, AccountType.Investiment);
        mockRepo.Verify(repo => repo.Add(It.IsAny<InventimentAccount>()), Times.Once);
    }
}