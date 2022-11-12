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

    public async Task<string> CreateAccount(Models.Account accountModel)
    {
        await using (var context = await _dbContext.CreateDbContextAsync())
        {
            if (await context.Accounts.AnyAsync(x => x.Email.Equals(accountModel.Email) && x.DisplayName.Equals(accountModel.DisplayName)))
            {
                return "Email and display name already exist";
            }
            else if (await context.Accounts.AnyAsync(x => x.Email.Equals(accountModel.Email)))
            {
                return "Email already exists";
            }
            else if (await context.Accounts.AnyAsync(x => x.DisplayName.Equals(accountModel.DisplayName)))
            {
                return "Display name already exists";
            }
            else
            {
                await context.Accounts.AddAsync(accountModel);
                await context.SaveChangesAsync();
                return "Account created successfully";
            }
        }
    }

    public async Task<List<Models.Account>> GetAllUsers()
    {
        await using (var context = await _dbContext.CreateDbContextAsync())
        {
            return await context.Accounts.ToListAsync();
        }
    }

    public async Task<string> EditAccount(Models.Account accountModel)
    {
        await using (var context = await _dbContext.CreateDbContextAsync())
        {
            if (await context.Accounts.AnyAsync(x => x.Email.Equals(accountModel.Email) && x.DisplayName.Equals(accountModel.DisplayName)))
            {
                return "Email and display name already exist";
            }
            else if (await context.Accounts.AnyAsync(x => x.Email.Equals(accountModel.Email)))
            {
                return "Email already exists";
            }
            else if (await context.Accounts.AnyAsync(x => x.DisplayName.Equals(accountModel.DisplayName)))
            {
                return "Display name already exists";
            }
            else
            {
                context.Accounts.Update(accountModel);
                await context.SaveChangesAsync();
                return "Account edited successfully";
            }
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