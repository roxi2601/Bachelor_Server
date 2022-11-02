using Bachelor_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Bachelor_Server.DataAccessLayer.Repositories.Account;

public class AccountRepo : IAccountRepo
{

    private BachelorDBContext dbContext;
    public async Task<Models.Account> GetAccount(Models.Account accountModel)
    {
        await using (dbContext)
        {
            var account = dbContext.Accounts.First(a => a.Email == accountModel.Email && a.Password == accountModel.Password);
            if(account == null)
            {
                throw new Exception();
            }
            return account;
        }
    }
}