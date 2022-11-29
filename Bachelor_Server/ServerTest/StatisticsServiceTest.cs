using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Statistics;
using Bachelor_Server.DataAccessLayer.Repositories.Schedule;
using Bachelor_Server.Models;
using Moq;

namespace ServerTest;

public class StatisticsServiceTest
{
    private Mock<IStatisticsRepo> repo = new();
    private Mock<ILogService> log = new();

    [Test]
    public async Task GetAllStatistics()
    {
        List<WorkerStatistic> list = new List<WorkerStatistic>();
        var service = new StatisticsService(repo.Object, log.Object);

        list.Add(new WorkerStatistic
        {
            EndTime = DateTime.MaxValue,
            StartTime = DateTime.Now,
            Status = "Running"
        });

        list.Add(new WorkerStatistic
        {
            EndTime = DateTime.MaxValue,
            StartTime = DateTime.Now,
            Status = "Paused"
        });

        Assert.That(list.Any(p => p.Status == "Running"));
        Assert.That(list.Any(p => p.Status == "Paused"));
        Assert.That(list.All(p => p.Status != "TEST"));
    }

    [Test]
    public async Task GetStatisticsForWorkerConfiguration()
    {
        
        //TODO
        // int id = 1;
        // var service = new StatisticsService(repo.Object, log.Object);
        // WorkerStatistic _1 = new WorkerStatistic
        // {
        //     StartTime = DateTime.Now,
        //     EndTime = DateTime.MaxValue,
        //     Status = "Running",
        //     FkWorkerConfigurationId = id
        // };
        //
        //
        // List<WorkerStatistic> list = await service.GetStatisticsForWorkerConfiguration(id);
        //
        // repo.Verify(v => v.GetStatisticsForWorkerConfiguration(It.Is<List<WorkerStatistic>>(
        //     a => a.Email == email && a.Password == password)));
    }
}