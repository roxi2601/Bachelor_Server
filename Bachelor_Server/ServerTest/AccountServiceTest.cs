using Bachelor_Server.BusinessLayer.Services.Account;
using Bachelor_Server.DataAccessLayer.Repositories.Account;
using Bachelor_Server.Models;

namespace ServerTest;
using Bachelor_Server.BusinessLayer.Services.Logging;
using Moq;

public class AccountServiceTest
{
    private Mock<ILogHandling> log = new();
    private Mock<IAccountRepo> repo = new();

    [Test]
    public async Task LogIn()
    {
        var service = new AccountService(log.Object, repo.Object);
        var email = "test@email.dk";
        var password = "test";

        Account account = new Account
        {
            
            Email = email,
            Password = password,
            
           
        };

        await service.GetLoggedAccount(account);

        repo.Verify(v => v.GetAccount(It.Is<Account>(
            a => a.Email == email && a.Password == password)));
    }
}