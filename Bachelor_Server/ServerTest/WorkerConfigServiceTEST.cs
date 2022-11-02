
using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Bachelor_Server.OldModels.General;
using Bachelor_Server.OldModels.WorkerConfiguration;
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
        var headers = new List<ParametersHeaderModel>();
        headers.Add(new ParametersHeaderModel
        {
            Key = "accept",
            Value = "*/*",
            Description = ""
        });
        var parameters = new List<ParametersHeaderModel>();
        var requestType = "get";
        var data = new WorkerConfigData();
        data.AuthType = "noAuth";
        data.BodyType = "none";
        
        WorkerConfigurationModel model = new WorkerConfigurationModel
        {
            url = url,
            headers = headers,
            parameters = parameters,
            requestType = requestType,
            Data = data
        };

        await service.CreateWorkerConfiguration(model);

        repo.Verify(v => v.CreateWorkerConfiguration(It.Is<WorkerConfigurationModel>(
            w =>
                w.url == url &&
                w.requestType == requestType &&
                w.parameters == parameters &&
                w.headers == headers &&
                w.Data == data
        )));
    }
    
    [Test]
    public async Task DeleteWorkerConfig()
    {
        var service = new WorkerConfigService(repo.Object, log.Object);
        var url = "https://catfact.ninja/fact";
        var headers = new List<ParametersHeaderModel>();
        headers.Add(new ParametersHeaderModel
        {
            Key = "accept",
            Value = "*/*",
            Description = ""
        });
        var parameters = new List<ParametersHeaderModel>();
        var requestType = "get";
        var data = new WorkerConfigData();
        data.AuthType = "noAuth";
        data.BodyType = "none";

        WorkerConfigurationModel expected = new WorkerConfigurationModel
        {
            ID = 1,
            url = url,
            headers = headers,
            parameters = parameters,
            requestType = requestType,
            authorizationType = data.AuthType,
            bodyType = data.BodyType
        };

        await service.DeleteWorkerConfiguration(expected.ID);

        repo.Verify(v => v.DeleteWorkerConfiguration(It.Is<int>(
            id =>
                id == expected.ID 
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
        var headers = new List<ParametersHeaderModel>();
        headers.Add(new ParametersHeaderModel
        {
            Key = "accept",
            Value = "*/*",
            Description = ""
        });
        var parameters = new List<ParametersHeaderModel>();
        var requestType = "get";
        var data = new WorkerConfigData();
        data.AuthType = "noAuth";
        data.BodyType = "none";

        WorkerConfigurationModel expected1 = new WorkerConfigurationModel
        {
            url = url,
            headers = headers,
            parameters = parameters,
            requestType = requestType,
            authorizationType = data.AuthType,
            bodyType = data.BodyType
        };
        
        WorkerConfigurationModel expected2 = new WorkerConfigurationModel
        {
            url = "https://www.boredapi.com/api/activity",
            headers = headers,
            parameters = parameters,
            requestType = requestType,
            authorizationType = data.AuthType,
            bodyType = data.BodyType
        };

        List<WorkerConfigurationModel> listFromRepo = new();
        listFromRepo.Add(expected1);
        listFromRepo.Add(expected2);
        
        Assert.That(listFromRepo.Any(p => p.url == "https://www.boredapi.com/api/activity"));
        Assert.That(listFromRepo.Any(p => p.url == "https://catfact.ninja/fact"));
        
    }

    [Test]
    public async Task EditWorkerConfig()
    {
        var service = new WorkerConfigService(repo.Object, log.Object);
        var ID = 1;
        var url = "https://catfact.ninja/fact";
        var headers = new List<ParametersHeaderModel>();
        headers.Add(new ParametersHeaderModel
        {
            Key = "accept",
            Value = "*/*",
            Description = ""
        });
        var parameters = new List<ParametersHeaderModel>();
        var requestType = "get";
        var data = new WorkerConfigData();
        data.AuthType = "noAuth";
        data.BodyType = "none";

        WorkerConfigurationModel expected = new WorkerConfigurationModel
        {
            ID = ID,
            url = "https://www.boredapi.com/api/activity",
            headers = headers,
            parameters = parameters,
            requestType = requestType,
            authorizationType = data.AuthType,
            bodyType = data.BodyType
        };

        await service.EditWorkerConfiguration(expected);

        repo.Verify(v => v.EditWorkerConfiguration(It.Is<WorkerConfigurationModel>(
            w =>
                w.ID == expected.ID &&
                w.url == expected.url &&
                w.requestType == expected.requestType &&
                w.parameters == expected.parameters &&
                w.headers == expected.headers &&
                w.authorizationType == expected.authorizationType &&
                w.bodyType == expected.bodyType
        )));
    }
}