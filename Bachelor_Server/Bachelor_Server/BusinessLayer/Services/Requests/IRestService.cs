using Bachelor_Server.Models.WorkerConfiguration;

namespace Bachelor_Server.BusinessLayer.Services.Requests
{
    public interface IRestService
    {
        Task<string> GenerateGetRequest(WorkerConfigurationModel configurationModel);
        Task<string> GeneratePostRequestRaw(WorkerConfigurationModel configurationModel);
        Task<string> GeneratePostRequestFormData(WorkerConfigurationModel configurationModel);
        Task<string> GenerateDeleteRequest(WorkerConfigurationModel configurationModel);
        Task<string> GeneratePatchRequestRaw(WorkerConfigurationModel configurationModel);
        Task<string> GeneratePatchRequestFormdata(WorkerConfigurationModel configurationModel);
        Task<string> GeneratePutRequestRaw(WorkerConfigurationModel configurationModel);
        Task<string> GeneratePutRequestFormdata(WorkerConfigurationModel configurationModel);
        
        // Task<string> GenerateGetRequest(WorkerConfigurationModel workerConfigurationModel);
        // Task<string> GeneratePostRequestRaw(WorkerConfigurationModel workerConfiguration, string body);
        // Task<string> GeneratePostRequestFormData(WorkerConfigurationModel workerConfiguration, List<FormDataModel> formdata);
        // Task<string> GenerateDeleteRequest(WorkerConfigurationModel workerConfiguration);
        // Task<string> GeneratePatchRequestRaw(WorkerConfigurationModel workerConfiguration, string body);
        // Task<string> GeneratePatchRequestFormdata(WorkerConfigurationModel workerConfiguration, List<FormDataModel> formdata);
        // Task<string> GeneratePutRequestRaw(WorkerConfigurationModel workerConfiguration, string body);
        // Task<string> GeneratePutRequestFormdata(WorkerConfigurationModel workerConfiguration, List<FormDataModel> formdata);
    }
}
