// using Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig;
// using Bachelor_Server.Models;
// using Microsoft.Extensions.Configuration;
//
// namespace ServerTest
// {
//     public class WorkerConfigurationRepoTest
//     {
//         IWorkerConfigurationRepo _repo;
//         BachelorDBContext _dbContext;
//         [SetUp]
//         public void Setup()
//         {
//             _repo = new WorkerConfigurationRepo(_dbContext);
//         }
//         [Test]
//         public async Task GetWorkerConfigurations()
//         {
//             //arrange
//             List<WorkerConfiguration> workerConfigurations = new List<WorkerConfiguration>();
//             //act
//             workerConfigurations = await _repo.GetWorkerConfigurations();
//             //assert
//             Assert.IsNotNull(workerConfigurations);
//             Assert.That(workerConfigurations.Count, Is.EqualTo(1));
//             Assert.That(workerConfigurations[0].Url, Is.EqualTo("mamaie"));
//             Assert.That(workerConfigurations[0].RequestType, Is.EqualTo("get"));
//             Assert.That(workerConfigurations[0].LastSavedBody, Is.EqualTo("none"));
//             Assert.That(workerConfigurations[0].LastSavedAuth, Is.EqualTo("none"));
//         }
//     }
// }
