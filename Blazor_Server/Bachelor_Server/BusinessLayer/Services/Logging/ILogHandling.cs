namespace Bachelor_Server.BusinessLayer.Services.Logging;

public interface ILogHandling
{
    Task<string> Log(Exception e);
}