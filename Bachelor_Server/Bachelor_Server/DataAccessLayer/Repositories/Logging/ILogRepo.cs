
using Bachelor_Server.DataAccessLayer.Repositories.Logging.Helper;

namespace Bachelor_Server.DataAccessLayer.Repositories.Logging;

public interface ILogRepo
{
    // Task AddErrorLog(string description, string exception, DateTime date);
    // Task AddLog(string content, DateTime date);
    
  //  Task AddErrorLog(JsonMessage message);
    Task AddLog(JsonMessage jsonMessage);
}