using Bachelor_Server.Models;

namespace Bachelor_Server.BusinessLayer.Services.Email;

public interface IEmailSerivce
{
    void SendEmailToAccount(Models.Account account);

    void SendEmailAboutError(string log);
}