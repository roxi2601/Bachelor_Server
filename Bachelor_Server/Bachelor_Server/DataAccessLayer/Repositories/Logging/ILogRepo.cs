using Bachelor_Server.Models.LogHandling;

namespace Bachelor_Server.DataAccessLayer.Repositories.Logging;

public interface ILogRepo
{
    Task LogError(JsonMessage json);
    Task Log(JsonMessage json);
}