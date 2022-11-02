using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
using Bachelor_Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ServerTest
{
    public class WorkerConfigurationRepoTest
    {
        readonly IConfiguration _configuration;
        IWorkerConfigurationRepo _repo;
        BachelorDBContext _dbContext;
        [SetUp]
        public void Setup()
        {
            
            _dbContext = new BachelorDBContext(options);
            _repo = new WorkerConfigurationRepo(_dbContext);
        }
        [Test]
        public async Task GetWorkerConfigurations()
        {
            //arrange
            List<WorkerConfiguration> workerConfigurations = new List<WorkerConfiguration>();
            //act
            workerConfigurations = await _repo.NewGetWorkerConfigurations();
            //assert
            Assert.IsNotNull(workerConfigurations);
            Assert.That(workerConfigurations.Count, Is.EqualTo(1));
            Assert.That(workerConfigurations[0].Url, Is.EqualTo("mamaie"));
            Assert.That(workerConfigurations[0].RequestType, Is.EqualTo("get"));
            Assert.That(workerConfigurations[0].LastSavedBody, Is.EqualTo("none"));
            Assert.That(workerConfigurations[0].LastSavedAuth, Is.EqualTo("none"));
        }
    }
}
