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

    public async Task CreateUser(Models.Account account)
    {
        try
        {
            await _accountRepo.CreateAccount(account);
            await _log.Log(account.DisplayName + "created");
            SendEmail();
        }
        catch (Exception e)
        {
            await _log.LogError(e);
        }

    }

    public async Task<List<Models.Account>> GetAllUsers()
    {
        return await _accountRepo.GetAllUsers();
    }

    private void SendEmail()
    {
        // try
        // {
        //
        //     System.Web.Mail.MailMessage Msg = new System.Web.Mail.MailMessage();
        //     // Sender e-mail address.
        //     Msg.From = txtemail.Text;
        //     // Recipient e-mail address.
        //     Msg.To = "info@user.com";
        //     Msg.Subject = "Enquiry";
        //     Msg.Body = "Name : " + txtname.Text + "\n" + "Mobile : " + txtsubject.Text + "\n" + "Query : " + txtmsg.Text;
        //     // your remote SMTP server IP.
        //     SmtpMail.SmtpServer = "67.225.221.112";//your ip address
        //     SmtpMail.Send(Msg);
        //     Msg = null;
        //     Page.RegisterStartupScript("UserMsg", "<script>alert('Mail sent thank you...');if(alert){ window.location='contactus.aspx';}</script>");
        //
        //     txtemail.Text = "";
        //     txtmsg.Text = "";
        //     txtname.Text = "";
        //     txtsubject.Text = "";
        // }
        // catch (Exception ex)
        // {
        //     Page.RegisterStartupScript("UserMsg", "<script>alert('Mail not sent ');if(alert){ window.location='page.aspx';}</script>");
        // }
    }
}