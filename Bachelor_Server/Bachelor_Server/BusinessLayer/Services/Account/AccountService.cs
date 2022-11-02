using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.DataAccessLayer.Repositories.Account;
using Bachelor_Server.OldModels.Account;

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
    
    public async Task<AccountModel> GetLoggedAccount(AccountModel accountModel)
    {
        try
        {
            AccountModel account = await _accountRepo.GetAccount(accountModel);
            await _log.Log(account.DisplayName + "logged in");
            return account;
        }
        catch (Exception e)
        {
            await _log.LogError(e);
        }

        return new AccountModel();
    }
}