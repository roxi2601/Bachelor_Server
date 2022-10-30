using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.Models.WorkerConfiguration;
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
        
        WorkerConfigurationModel workerConfigurationModel = new WorkerConfigurationModel
        {
            ID = 1,
            url = "https://ipinfo.io/161.185.160.93/geo",
            headers = new List<ParametersHeaderModel>(),
            parameters = new List<ParametersHeaderModel>(),
            authorizationType = "NoAuth",
            bodyType = "none",
            requestType = "get"
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