using Bachelor_Server.DataAccessLayer.Repositories.Logging;
using Bachelor_Server.OldModels.LogHandling;
using Newtonsoft.Json;

namespace Bachelor_Server.BusinessLayer.Services.Logging;

public class LogHandling : ILogHandling
{
    private ILogRepo _logRepo;

    public LogHandling()
    {
        _logRepo = new LogRepo();
    }

    public async Task<string> LogError(Exception e)
    {
        JsonMessage json = new JsonMessage
        {
            StatusCode = 400,
            Description = e.Message,
            Exception = e.StackTrace,
            Date = DateTime.Now
        };
        await _logRepo.LogError(json);
        return JsonConvert.SerializeObject(json);
    }

    public async Task<string> Log(string content)
    {
        JsonMessage json = new JsonMessage
        {
            StatusCode = 200,
            Description = content,
            Exception = "",
            Date = DateTime.Now
        };
        await _logRepo.Log(json);
        return JsonConvert.SerializeObject(json);
    }
}