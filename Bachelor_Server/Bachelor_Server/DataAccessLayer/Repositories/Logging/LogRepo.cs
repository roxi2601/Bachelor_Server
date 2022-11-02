using Bachelor_Server.OldModels.LogHandling;

namespace Bachelor_Server.DataAccessLayer.Repositories.Logging;

public class LogRepo : ILogRepo
{
    public Task LogError(JsonMessage json)
    {
        throw new NotImplementedException();
    }

    public Task Log(JsonMessage json)
    {
        throw new NotImplementedException();
    }
}