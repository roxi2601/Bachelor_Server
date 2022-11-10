

namespace Bachelor_Server.BusinessLayer.Services.Account;

public interface IAccountService
{
    Task<Models.Account> GetLoggedAccount(Models.Account accountModel);
    Task CreateUser(Models.Account deserializeObject);
    Task<List<Models.Account>> GetAllUsers();
}