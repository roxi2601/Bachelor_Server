using Bachelor_Server.OldModels.Account;

namespace Bachelor_Server.DataAccessLayer.Repositories.Account;

public interface IAccountRepo
{
    Task<AccountModel> GetAccount(AccountModel accountModel);
}