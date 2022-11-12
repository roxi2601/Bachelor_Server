

using Bachelor_Server.Models;

namespace Bachelor_Server.BusinessLayer.Services.Account;

public interface IAccountService
{
    Task<Models.Account> GetLoggedAccount(Models.Account accountModel);
    Task<string> CreateUser(Models.Account deserializeObject);
    Task<List<Models.Account>> GetAllUsers();
    Task DeleteAccount(int id);
    Task<string> EditAccount(Models.Account account);
}