using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.DataAccessLayer.Repositories.Account;
using Bachelor_Server.Models;

namespace Bachelor_Server.BusinessLayer.Services.Account;

public class AccountService : IAccountService
{
    
    private ILogHandling _log;
    private IAccountRepo _accountRepo;


    public AccountService(ILogHandling log, IAccountRepo accountRepo)
    {
        _log = log;
        _accountRepo = accountRepo;
    }
    
    public async Task<Models.Account> GetLoggedAccount(Models.Account accountModel)
    {
        try
        {
            Models.Account account = await _accountRepo.GetAccount(accountModel);
            await _log.Log(account.DisplayName + "logged in");
            return account;
        }
        catch (Exception e)
        {
            await _log.LogError(e);
        }

        return new Models.Account();
    }

    public async Task CreateUser(Models.Account account)
    {
        try
        {
            await _accountRepo.CreateAccount(account);
            await _log.Log(account.DisplayName + "created");
        }
        catch (Exception e)
        {
            await _log.LogError(e);
        }

    }

    public async Task<List<Models.Account>> GetAllUsers()
    {
        return await _accountRepo.GetAllUsers();
    }
}