namespace Bachelor_Server.BusinessLayer.Services.Requests
{
    public interface IRestService
    {
        Task<string> GenerateGetRequest(int id);
        Task<string> GeneratePostRequestRaw(int id);
        Task<string> GeneratePostRequestFormData(int id);
        Task<string> GenerateDeleteRequest(int id);
        Task<string> GeneratePatchRequestRaw(int id);
        Task<string> GeneratePatchRequestFormdata(int id);
        Task<string> GeneratePutRequestRaw(int id);
        Task<string> GeneratePutRequestFormdata(int id);
        
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
