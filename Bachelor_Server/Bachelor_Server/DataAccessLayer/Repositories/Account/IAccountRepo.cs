

using Bachelor_Server.Models;

namespace Bachelor_Server.DataAccessLayer.Repositories.Account;

public interface IAccountRepo
{
    Task<Models.Account> GetAccount(Models.Account accountModel);
    Task<string> CreateAccount(Models.Account accountModel);
    Task<List<Models.Account>> GetAllUsers();
    Task EditAccount(Models.Account accountModel);
    Task DeleteAccount(int id);
}