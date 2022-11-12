using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.DataAccessLayer.Repositories.Account;
using Bachelor_Server.Models;

namespace Bachelor_Server.BusinessLayer.Services.Account;

public class AccountService : IAccountService
{
    private ILogHandling _log;
    private IAccountRepo _accountRepo;


    public AccountService(ILogHandling log, IAccountRepo accountRepo)
    {
        _log = log;
        _accountRepo = accountRepo;
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

    public async Task<string> CreateUser(Models.Account account)
    {
        string res = "";
        try
        {
            res = await _accountRepo.CreateAccount(account);
            if (res.Equals("Account created successfully"))
            {
                await _log.Log(account.DisplayName + "created");
                SendEmail(account);
            }
        }
        catch (Exception e)
        {
            await _log.LogError(e);
        }

        return res;
    }

    public async Task<List<Models.Account>> GetAllUsers()
    {
        return await _accountRepo.GetAllUsers();
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
                SendEmail(account);
            }
        }
        catch (Exception e)
        {
            await _log.LogError(e);
        }

        return res;
    }


    private void SendEmail(Models.Account account)
    {
        var smtpClient = new SmtpClient("smtp-relay.sendinblue.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("icalexandru1700@gmail.com", "nDKzMfyQcrJ7hAbv"),
            EnableSsl = true,
        };

        smtpClient.Send("icalexandru1700@gmail.com", "alex_catalin1700@yahoo.com", "Your account has been created",
            "Email: " + account.Email + "\n" +
            "Password: " + account.Password + "\n" +
            "Display Name: " + account.DisplayName + "\n" +
            "Type: " + account.Type
        );
    }
}