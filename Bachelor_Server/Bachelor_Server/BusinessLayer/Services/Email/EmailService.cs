using System.Net;
using System.Net.Mail;

namespace Bachelor_Server.BusinessLayer.Services.Email;

public class EmailService : IEmailSerivce
{
    public void SendEmailToAccount(Models.Account account)
    {
        var smtpClient = new SmtpClient("smtp-relay.sendinblue.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("icalexandru1700@gmail.com", "nDKzMfyQcrJ7hAbv"),
            EnableSsl = true,
        };

        smtpClient.Send("icalexandru1700@gmail.com", account.Email, "Information about your account",
            "Email: " + account.Email + "\n" +
            "Password: " + account.Password + "\n" +
            "Display Name: " + account.DisplayName + "\n" +
            "Type: " + account.Type
        );
    }

    public void SendEmailAboutError(string log)
    {
        var smtpClient = new SmtpClient("smtp-relay.sendinblue.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("icalexandru1700@gmail.com", "nDKzMfyQcrJ7hAbv"),
            EnableSsl = true,
        };

        smtpClient.Send("icalexandru1700@gmail.com", "roxi260111@gmail.com", "Request error",
            "The following error has happened while trying to process a REST request: " + "\n\n\n\n"
            + log
        );
    }
}