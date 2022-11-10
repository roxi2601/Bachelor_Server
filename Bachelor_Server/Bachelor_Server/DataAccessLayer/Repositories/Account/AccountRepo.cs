using Bachelor_Server.Models;
using Microsoft.EntityFrameworkCore;


namespace Bachelor_Server.DataAccessLayer.Repositories.Account;

public class AccountRepo : IAccountRepo
{
    private IDbContextFactory<BachelorDBContext> _dbContext;

    public AccountRepo(IDbContextFactory<BachelorDBContext> dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Models.Account> GetAccount(Models.Account accountModel)
    {
        await using (var context = await _dbContext.CreateDbContextAsync())
        {
            var account =
                context.Accounts.First(a => a.Email == accountModel.Email && a.Password == accountModel.Password);
            if (account == null)
            {
                throw new Exception();
            }

            return account;
        }
    }

    public Task CreateAccount(Models.Account accountModel)
    {
        throw new NotImplementedException();
    }

    public Task<List<Models.Account>> GetAllUsers()
    {
        throw new NotImplementedException();
    }
}