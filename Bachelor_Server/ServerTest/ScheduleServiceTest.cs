using Bachelor_Server.BusinessLayer.Services.ScheduleService;
using Bachelor_Server.Models;
using Moq;
using System.Security.Policy;

namespace ServerTest;

public class ScheduleServiceTest
{
    private Mock<IScheduleService> _scheduleServiceMock = new();

    [Test]
    public void BuildTriggerTest()
    {
        WorkerConfiguration modelMin = new WorkerConfiguration
        {
            ScheduleRate = "1/min"
        };
        WorkerConfiguration modelHour = new WorkerConfiguration
        {
            ScheduleRate = "1/h"
        };

    }
}