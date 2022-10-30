using Bachelor_Server.Models.Account;

namespace Bachelor_Server.DataAccessLayer.Repositories.Account;

public class AccountRepo : IAccountRepo
{
    public Task<AccountModel> GetAccount(AccountModel accountModel)
    {
        throw new NotImplementedException();
    }
}