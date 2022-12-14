using Bachelor_Server.BusinessLayer.Services.Account;
using Bachelor_Server.BusinessLayer.Services.Email;
using Bachelor_Server.DataAccessLayer.Repositories.Account;
using Bachelor_Server.Models;

namespace ServerTest;
using Bachelor_Server.BusinessLayer.Services.Logging;
using Moq;

public class AccountServiceTest
{
    private Mock<ILogService> log = new();
    private Mock<IAccountRepo> repo = new();
    private Mock<IEmailSerivce> emailMock = new();

    [Test]
    public async Task LogIn()
    {
        var service = new AccountService(log.Object, repo.Object, emailMock.Object);
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
    

    [Test]
    public async Task CreateAccount()
    {
        var service = new AccountService(log.Object, repo.Object, emailMock.Object);
        var email = "test@email.dk";
        var password = "test";
        var displayName = "testname";
        var type = "admin";

        Account account = new Account
        {
            
            Email = email,
            Password = password,
            DisplayName = displayName,
            Type = type
           
        };

        await service.CreateAccount(account);

        repo.Verify(v => v.CreateAccount(It.Is<Account>(
            a => a.Email == email && a.Password == password && a.DisplayName == displayName && a.Type == type)));
    }
    [Test]
    public async Task CreateAccountWithExistingMail()
    {
        var service = new AccountService(log.Object, repo.Object, emailMock.Object);
        var email = "test@email.dk";
        var password = "test";
        var displayName = "testname";
        var type = "admin";

        Account account = new Account
        {

            Email = email,
            Password = password,
            DisplayName = displayName,
            Type = type

        };

        await service.CreateAccount(account);

        repo.Verify(v => v.CreateAccount(It.Is<Account>(
            a => a.Email == email && a.Password == password && a.DisplayName == displayName && a.Type == type)));
    }
    [Test]
    public async Task CreateAccountWithExistingDisplayName()
    {
        var service = new AccountService(log.Object, repo.Object, emailMock.Object);
        var email = "test@email.dk";
        var password = "test";
        var displayName = "testname";
        var type = "admin";

        Account account = new Account
        {

            Email = email,
            Password = password,
            DisplayName = displayName,
            Type = type

        };

        await service.CreateAccount(account);

        repo.Verify(v => v.CreateAccount(It.Is<Account>(
            a => a.Email == email && a.Password == password && a.DisplayName == displayName && a.Type == type)));
    }

    [Test]
    public async Task DeleteAccount()
    {
        var service = new AccountService(log.Object, repo.Object, emailMock.Object);
        var email = "test@email.dk";
        var password = "test";
        var displayName = "testname";
        var type = "admin";
        
        Account account = new Account
        {
            PkAccountId = 1,
            Email = email,
            Password = password,
            DisplayName = displayName,
            Type = type
           
        };

        await service.DeleteAccount(account.PkAccountId);

        repo.Verify(v => v.DeleteAccount(It.Is<int>(
            a => a == account.PkAccountId)));
    }
    
    [Test]
    public async Task EditAccount()
    {
        var service = new AccountService(log.Object, repo.Object, emailMock.Object);
        var email = "test@email.dk";
        var password = "test";
        var displayName = "testname";
        var type = "admin";

        Account account = new Account
        {
            PkAccountId = 1,
            Email = email,
            Password = password,
            DisplayName = displayName,
            Type = type
           
        };

        account.Email = "test1@email.dk";
        account.Password = "test1";

        await service.EditAccount(account);

        repo.Verify(v => v.EditAccount(It.Is<Account>(
            a => a.Email == "test1@email.dk" && a.Password == "test1")));
    }
    
    [Test]
    public async Task GetAllAccounts()
    {
        var service = new AccountService(log.Object, repo.Object, emailMock.Object);
        var email = "test@email.dk";
        var password = "test";
        var displayName = "testname";
        var type = "admin";
        var email1 = "test1@email.dk";
        var email2 = "test2@email.dk";
        
        Account account1 = new Account
        {
            Email = email,
            Password = password,
            DisplayName = displayName,
            Type = type
           
        };
        Account account2 = new Account
        {
            Email = email1,
            Password = password,
            DisplayName = displayName,
            Type = type
           
        };
        Account account3 = new Account
        {
            Email = email2,
            Password = password,
            DisplayName = displayName,
            Type = type
           
        };

        List<Account> accounts = new();
        accounts.Add(account1);
        accounts.Add(account2);
        accounts.Add(account3);

       

        Assert.That(accounts.Any(p => p.Email == "test@email.dk"));
        Assert.That(accounts.Any(p => p.Email == "test1@email.dk"));
        Assert.That(accounts.Any(p => p.Email == "test2@email.dk"));
        Assert.That(accounts.All(p => p.Email != "test3@email.dk"));
    }
}