

namespace Bachelor_Server.BusinessLayer.Services.Account;

public interface IAccountService
{
    Task<Models.Account> GetLoggedAccount(Models.Account accountModel);
}