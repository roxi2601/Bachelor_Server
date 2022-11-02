using Bachelor_Server.Models;
using Microsoft.EntityFrameworkCore;


namespace Bachelor_Server.DataAccessLayer.Repositories.Account;

public class AccountRepo : IAccountRepo
{

    private BachelorDBContext dbContext;
    public AccountRepo(BachelorDBContext bachelorDBContext)
    {
        dbContext = bachelorDBContext;
    }
    public async Task<Models.Account> GetAccount(Models.Account accountModel)
    {
        await using (dbContext)
        {
            var account = await dbContext.Accounts.FirstAsync(a => a.Email == accountModel.Email && a.Password == accountModel.Password);
            if(account == null)
            {
                throw new Exception();
            }
            return account;
        }
    }
}