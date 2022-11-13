 namespace Bachelor_Server.BusinessLayer.Services.Logging;

public interface ILogService
{
    Task<string> LogError(Exception e);
    
    Task<string> Log(string content);
}