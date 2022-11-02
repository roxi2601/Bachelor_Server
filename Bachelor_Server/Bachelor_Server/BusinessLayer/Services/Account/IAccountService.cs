using Bachelor_Server.Models.Account;

namespace Bachelor_Server.BusinessLayer.Services.Account;

public interface IAccountService
{
    Task<AccountModel> GetLoggedAccount(AccountModel accountModel);
}