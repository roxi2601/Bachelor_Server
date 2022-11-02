using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.Models;
using Moq;


namespace ServerTest;

public class Tests
{
    private readonly string _expected =
        "{\"ip\":\"161.185.160.93\",\"city\":\"NewYorkCity\",\"region\":\"NewYork\",\"country\":\"US\",\"loc\":\"40.7143,-74.0060\",\"org\":\"AS22252TheCityofNewYork\",\"postal\":\"10004\",\"timezone\":\"America/New_York\",\"readme\":\"https://ipinfo.io/missingauth\"}";
    private Mock<ILogHandling> log = new();
    private Mock<IWorkerConfigService> config = new();

    [Test]
    public async Task GenerateGetRequest()
    {
        
        WorkerConfiguration workerConfigurationModel = new WorkerConfiguration()
        {
            PkWorkerConfigurationId = 1,
            Url = "https://ipinfo.io/161.185.160.93/geo",
            Headers = new List<Header>(),
            Parameters = new List<Parameter>(),
            LastSavedAuth = "NoAuth",
            LastSavedBody = "none",
            RequestType = "get"
        };
        // config.Setup(_ => _.CreateWorkerConfiguration(It.IsAny<WorkerConfigurationModel>())) 
        //     .ReturnsAsync();

        var service = new RestService(log.Object, config.Object);

        string actual = await service.GenerateGetRequest(workerConfigurationModel);
        string formatted = actual.Replace("\n  ", "");
        string formatted1 = formatted.Replace("\n", "");
        Assert.That(formatted1.Replace(" ", ""), Is.EqualTo(_expected));
    }
}