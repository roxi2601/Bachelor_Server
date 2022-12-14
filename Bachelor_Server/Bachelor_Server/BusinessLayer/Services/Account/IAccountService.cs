namespace Bachelor_Server.BusinessLayer.Services.Account;

public interface IAccountService
{
    Task<Models.Account> GetLoggedAccount(Models.Account accountModel);
    Task<string> CreateAccount(Models.Account deserializeObject);
    Task<List<Models.Account>> GetAccounts();
    Task DeleteAccount(int id);
    Task<string> EditAccount(Models.Account account);
}