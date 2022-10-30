namespace Bachelor_Server.BusinessLayer.Services.Logging;

public interface ILogHandling
{
    Task<string> LogError(Exception e);
    
    Task<string> Log(string content);
}