using Bachelor_Server.Models;

namespace Bachelor_Server.DataAccessLayer.Repositories.Schedule
{
    public interface IScheduleRepo
    {
        Task CreateWorker(Worker worker);
        Task<List<Worker>> GetWorkers();
        Task<Worker> GetWorkerById(int id);
        Task EditWorker(Worker worker);
        Task DeleteWorker(int id);
    }
}
