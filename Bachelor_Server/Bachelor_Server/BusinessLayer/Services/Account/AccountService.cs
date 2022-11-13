using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Bachelor_Server.BusinessLayer.Services.Email;
using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.DataAccessLayer.Repositories.Account;
using Bachelor_Server.Models;

namespace Bachelor_Server.BusinessLayer.Services.Account;

public class AccountService : IAccountService
{
    private ILogService _log;
    private IAccountRepo _accountRepo;
    private IEmailSerivce _email;


    public AccountService(ILogService log, IAccountRepo accountRepo, IEmailSerivce email)
    {
        _log = log;
        _accountRepo = accountRepo;
        _email = email;
    }

    public async Task<Models.Account> GetLoggedAccount(Models.Account accountModel)
    {
        try
        {
            Models.Account account = await _accountRepo.GetAccount(accountModel);
            await _log.Log(account.DisplayName + "logged in");
            return account;
        }
        catch (Exception e)
        {
            await _log.LogError(e);
        }

        return new Models.Account();
    }

    public async Task<string> CreateAccount(Models.Account account)
    {
        string res = "";
        try
        {
            res = await _accountRepo.CreateAccount(account);
            if (res.Equals("Account created successfully"))
            {
                await _log.Log(account.DisplayName + "created");
                _email.SendEmailToAccount(account);
            }
        }
        catch (Exception e)
        {
            await _log.LogError(e);
        }

        return res;
    }

    public async Task<List<Models.Account>> GetAccounts()
    {
        return await _accountRepo.GetAccounts();
    }

    public async Task DeleteAccount(int id)
    {
        try
        {
            await _accountRepo.DeleteAccount(id);
            await _log.Log("Object deleted with id: " + id);
        }
        catch (Exception e)
        {
            await _log.LogError(e);
        }
    }

    public async Task<string> EditAccount(Models.Account account)
    {
        string res = "";
        try
        {
            res = await _accountRepo.EditAccount(account);
            if (res.Equals("Account edited successfully"))
            {
                await _log.Log("Object edited with id: " + account.PkAccountId);
                _email.SendEmailToAccount(account);
            }
        }
        catch (Exception e)
        {
            await _log.LogError(e);
        }

        return res;
    }
    
}