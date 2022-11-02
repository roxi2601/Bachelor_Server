
using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Bachelor_Server.Models;
using Moq;

namespace BusinessLogicLayerTest;

public class WorkerConfigServiceTEST
{
    private Mock<ILogHandling> log = new();
    private Mock<IWorkerConfigurationRepo> repo = new();

    [Test]
    public async Task CreateWorkerConfig()
    {
        var service = new WorkerConfigService(repo.Object, log.Object);
        var url = "https://catfact.ninja/fact";
        var headers = new List<Header>();
        headers.Add(new Header
        {
            Key = "accept",
            Value = "*/*",
            Description = ""
        });
        var parameters = new List<Parameter>();
        var requestType = "get";
      //  var data = new WorkerConfigData();
        var LastSavedAuth = "noAuth";
        var LastSavedBody = "none";
        
        WorkerConfiguration model = new WorkerConfiguration
        {
            Url = url,
            Headers = headers,
            Parameters = parameters,
            RequestType = requestType,
        };

        await service.CreateWorkerConfiguration(model);

        repo.Verify(v => v.CreateWorkerConfiguration(It.Is<WorkerConfiguration>(
            w =>
                w.Url == url &&
                w.RequestType == requestType &&
                w.Parameters == parameters &&
                w.Headers == headers 
        )));
    }
    
    [Test]
    public async Task DeleteWorkerConfig()
    {
        var service = new WorkerConfigService(repo.Object, log.Object);
        var url = "https://catfact.ninja/fact";
        var headers = new List<Header>();
        headers.Add(new Header()
        {
            Key = "accept",
            Value = "*/*",
            Description = ""
        });
        var parameters = new List<Parameter>();
        var requestType = "get";
        var LastSavedAuth = "noAuth";
        var LastSavedBody = "none";

        WorkerConfiguration expected = new WorkerConfiguration
        {
            PkWorkerConfigurationId = 1,
            Url = url,
            Headers = headers,
            Parameters = parameters,
            RequestType = requestType,
            LastSavedAuth = LastSavedAuth,
            LastSavedBody = LastSavedBody
        };

        await service.DeleteWorkerConfiguration(expected.PkWorkerConfigurationId);

        repo.Verify(v => v.DeleteWorkerConfiguration(It.Is<int>(
            id =>
                id == expected.PkWorkerConfigurationId 
                //&&
                // w.url == expected.url &&
                // w.requestType == expected.requestType &&
                // w.parameters == expected.parameters &&
                // w.headers == expected.headers &&
                // w.authorizationType == expected.authorizationType &&
                // w.bodyType == expected.bodyType
        )));
    }
    
    [Test]
    public async Task ReadAllWorkerConfigurations()
    {
        var service = new WorkerConfigService(repo.Object, log.Object);
        var url = "https://catfact.ninja/fact";
        var headers = new List<Header>();
        headers.Add(new Header
        {
            Key = "accept",
            Value = "*/*",
            Description = ""
        });
        var parameters = new List<Parameter>();
        var requestType = "get";
        var LastSavedAuth = "noAuth";
        var LastSavedBody = "none";
        
        WorkerConfiguration expected1 = new WorkerConfiguration
        {
            Url = url,
            Headers = headers,
            Parameters = parameters,
            RequestType = requestType,
            LastSavedAuth = LastSavedAuth,
            LastSavedBody = LastSavedBody
        };
        
        WorkerConfiguration expected2 = new WorkerConfiguration
        {
            Url = "https://www.boredapi.com/api/activity",
            Headers = headers,
            Parameters = parameters,
            RequestType = requestType,
            LastSavedAuth = LastSavedAuth,
            LastSavedBody = LastSavedBody
        };
       
        List<WorkerConfiguration> listFromRepo = new();
        listFromRepo.Add(expected1);
        listFromRepo.Add(expected2);
        
        Assert.That(listFromRepo.Any(p => p.Url == "https://www.boredapi.com/api/activity"));
        Assert.That(listFromRepo.Any(p => p.Url == "https://catfact.ninja/fact"));
        
    }

    [Test]
    public async Task EditWorkerConfig()
    {
        var service = new WorkerConfigService(repo.Object, log.Object);
        var ID = 1;
        var url = "https://catfact.ninja/fact";
        var headers = new List<Header>();
        headers.Add(new Header
        {
            Key = "accept",
            Value = "*/*",
            Description = ""
        });
        var parameters = new List<Parameter>();
        var requestType = "get";
        var LastSavedAuth = "noAuth";
        var LastSavedBody = "none";
        
        WorkerConfiguration expected = new WorkerConfiguration()
        {
            PkWorkerConfigurationId = ID,
            Url = "https://www.boredapi.com/api/activity",
            Headers = headers,
            Parameters = parameters,
            RequestType = requestType,
            LastSavedAuth = LastSavedAuth,
            LastSavedBody = LastSavedBody
        };

        await service.EditWorkerConfiguration(expected);

        repo.Verify(v => v.EditWorkerConfiguration(It.Is<WorkerConfiguration>(
            w =>
                w.PkWorkerConfigurationId == expected.PkWorkerConfigurationId &&
                w.Url == expected.Url &&
                w.RequestType == expected.RequestType &&
                w.Parameters == expected.Parameters &&
                w.Headers == expected.Headers &&
                w.LastSavedAuth == expected.LastSavedAuth &&
                w.LastSavedBody == expected.LastSavedBody
        )));
    }
}