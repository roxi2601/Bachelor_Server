using Bachelor_Server.DataAccessLayer.Repositories.Logging;
using Bachelor_Server.DataAccessLayer.Repositories.Logging.Helper;
using Bachelor_Server.Models;
using Newtonsoft.Json;

namespace Bachelor_Server.BusinessLayer.Services.Logging;

public class LogHandling : ILogHandling
{
    private ILogRepo _logRepo;

    public LogHandling(ILogRepo logRepo)
    {
        _logRepo = logRepo;
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
      //  await _logRepo.AddErrorLog(e.Message, e.StackTrace, DateTime.Now);
      await _logRepo.AddLog(json);
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
        await _logRepo.AddLog(json);
        return JsonConvert.SerializeObject(json);
    }
}