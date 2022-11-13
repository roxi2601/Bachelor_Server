namespace Bachelor_Server.BusinessLayer.Services.Email;

public interface IEmailSerivce
{
    void SendEmailToAccount(Models.Account account);
}