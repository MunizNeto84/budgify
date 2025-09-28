using Budgify.Application;
using Budgify.Domain;
using Moq;

namespace Budgify.Tests;

public class AccountServiceTests
{
    [Fact]
    public void CreateAccount_ShouldCallAddOnRepository_WhenCalled()
    {
        var mockRepository = new Mock<IAccountRepository>();
        var accountService = new AccountService(mockRepository.Object);


        //Act (Agir)
        accountService.CreateAccount("Test Bank", 100, false);

        //Assert (Verificar)
        mockRepository.Verify(repo => repo.Add(It.IsAny<Account>()), Times.Once);
    }
}