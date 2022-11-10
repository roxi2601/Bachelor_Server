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

    public async Task CreateAccount(Models.Account accountModel)
    {
        await using (var context = await _dbContext.CreateDbContextAsync())
        {
            await context.Accounts.AddAsync(accountModel);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<Models.Account>> GetAllUsers()
    {
        await using (var context = await _dbContext.CreateDbContextAsync())
        {
            return await context.Accounts.ToListAsync();
        }
    }

    public async Task EditAccount(Models.Account accountModel)
    {
        await using (var context = await _dbContext.CreateDbContextAsync())
        {
            context.Accounts.Update(accountModel);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteAccount(int id)
    {
        await using (var context = await _dbContext.CreateDbContextAsync())
        {
            var delete = await context.Accounts.FirstAsync(x => x.PkAccountId == id);
            context.Accounts.Remove(delete);
            await context.SaveChangesAsync();
        }
    }
}