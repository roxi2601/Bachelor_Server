using System.Net;
using System.Net.Mail;

namespace ServerTest;

public class EmailServiceTest
{
    [Test]
    public async Task SendEmail()
    {

        SmtpFailedRecipientException error = null;
        try
        {

            var smtpClient = new SmtpClient("smtp-relay.sendinblue.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("icalexandru1700@gmail.com", "nDKzMfyQcrJ7hAbv"),
                EnableSsl = true,
            };

            smtpClient.Send("icalexandru1700@gmail.com", "roxi260111@gmail.com", "Information about your account",
                "Email: " + "roxi260111@gmail.comTEST" + "\n" +
                "Password: " + "roxi260111@gmail.comTEST" + "\n" +
                "Display Name: " + "roxi260111@gmail.comTEST" + "\n" +
                "Type: " + "roxi260111@gmail.comTEST"
            );

        }
        catch (SmtpFailedRecipientException ex)
        {
            error = ex;
        }
        finally
        {
            Assert.Null(error);
        }
    }
    
    [Test]
    public async Task SendEmailBad()
    {

        Exception error = null;
        try
        {

            var smtpClient = new SmtpClient("smtp-relay.sendinblue.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("icalexandru1700gmail.com", "nDKzMfyQcrJ7hAbv"),
                EnableSsl = true,
            };

            smtpClient.Send("icalexandru1700", "roxi260111", "Information about your account",
                "Email: " + "roxi260111@gmail.comTEST" + "\n" +
                "Password: " + "roxi260111@gmail.comTEST" + "\n" +
                "Display Name: " + "roxi260111@gmail.comTEST" + "\n" +
                "Type: " + "roxi260111@gmail.comTEST"
            );

        }
        catch (Exception ex)
        {
            error = ex;
        }
        finally
        {
            Assert.NotNull(error);
        }
    }
}