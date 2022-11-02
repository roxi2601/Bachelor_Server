

namespace Bachelor_Server.DataAccessLayer.Repositories.Account;

public interface IAccountRepo
{
    Task<Models.Account> GetAccount(Models.Account accountModel);
}