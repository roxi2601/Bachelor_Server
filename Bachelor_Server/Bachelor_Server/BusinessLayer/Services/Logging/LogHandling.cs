using Bachelor_Server.DataAccessLayer.Repositories.Logging;

namespace Bachelor_Server.BusinessLayer.Services.Logging;

public class LogHandling : ILogHandling
{
    private ILogRepo _logRepo;

    public LogHandling()
    {
        _logRepo = new LogRepo();
    }

    public async Task<string> Log(Exception e)
    {


        // JsonMessage json = new JsonMessage
        // {
        //     StatusCode = e.
        //     Description = e.Message,
        //     Exception = "",
        //     Date = DateTime.Now
        // };
        // _logRepo.Log(json);
        // return JsonConvert.SerializeObject(json);
        return e.Message;
    }
}