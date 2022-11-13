
using Bachelor_Server.DataAccessLayer.Repositories.Logging.Helper;

namespace Bachelor_Server.DataAccessLayer.Repositories.Logging;

public interface ILogRepo
{
    Task AddLog(JsonMessage jsonMessage);
}