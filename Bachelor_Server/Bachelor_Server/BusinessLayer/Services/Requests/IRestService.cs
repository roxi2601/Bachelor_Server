using Bachelor_Server.Models;


namespace Bachelor_Server.BusinessLayer.Services.Requests
{
    public interface IRestService
    {
        Task<string> GenerateGetRequest(WorkerConfiguration configurationModel);
        Task<string> GeneratePostRequestRaw(WorkerConfiguration configurationModel);
        Task<string> GeneratePostRequestFormData(WorkerConfiguration configurationModel);
        Task<string> GenerateDeleteRequest(WorkerConfiguration configurationModel);
        Task<string> GeneratePatchRequestRaw(WorkerConfiguration configurationModel);
        Task<string> GeneratePatchRequestFormdata(WorkerConfiguration configurationModel);
        Task<string> GeneratePutRequestRaw(WorkerConfiguration configurationModel);
        Task<string> GeneratePutRequestFormdata(WorkerConfiguration configurationModel);
    }
}
