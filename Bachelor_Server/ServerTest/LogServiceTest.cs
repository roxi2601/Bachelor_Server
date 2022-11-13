using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.DataAccessLayer.Repositories.Logging;
using Bachelor_Server.DataAccessLayer.Repositories.Logging.Helper;
using Moq;

namespace ServerTest;

public class LogServiceTest
{
    private Mock<ILogRepo> repo = new();

    [Test]
    public async Task Log()
    {
        var service = new LogService(repo.Object);

        await service.Log("TESTcontentTEST");

        repo.Verify(r => r.AddLog(It.Is<JsonMessage>(
            l =>
                 l.Description.Equals("TESTcontentTEST") 
               )));
    }
    
    [Test]
    public async Task LogError()
    {
        var service = new LogService(repo.Object);

        Exception e = new Exception("TESTING");

        await service.LogError(e);

        repo.Verify(r => r.AddLog(It.Is<JsonMessage>(
            l =>
                l.Description.Equals("TESTING") 
        )));
    }
}