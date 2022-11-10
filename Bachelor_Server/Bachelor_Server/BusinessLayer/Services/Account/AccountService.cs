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
            SendEmail(); //TODO
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
            try
            {
                MailMessage newMail = new MailMessage();
                // use the Gmail SMTP Host
                SmtpClient client = new SmtpClient("smtp.gmail.com"); 

                // Follow the RFS 5321 Email Standard
                newMail.From = new MailAddress("alex_catalin1700@yahoo.com", "caca");

                newMail.To.Add("ajurj12@yahoo.com");// declare the email subject

                newMail.Subject = "My First Email"; // use HTML for the email body

                newMail.IsBodyHtml = true;newMail.Body = "<h1> This is my first Templated Email in C# </h1>";

                // enable SSL for encryption across channels
                client.EnableSsl = true; 
                // Port 465 for SSL communication
                client.Port = 465; 
                // Provide authentication information with Gmail SMTP server to authenticate your sender account
                client.Credentials = new System.Net.NetworkCredential("<<SENDER-EMAIL>>", "<<SENDER-GMAIL-PASSWORD>>");

                client.Send(newMail); // Send the constructed mail
                Console.WriteLine("Email Sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -" +ex);
            }
    }
}