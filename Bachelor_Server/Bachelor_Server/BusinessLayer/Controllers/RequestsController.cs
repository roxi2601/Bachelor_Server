using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.Models.WorkerConfiguration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bachelor_Server.BusinessLayer.Controllers;

[ApiController]
public class RequestsController : ControllerBase
{
    private IRestService _restService;
    
    public RequestsController(IRestService restService)
    {
        _restService = restService;
    }
    
    
    [HttpPost("requests/{requestType}/{id}")]
    public async Task<string> PerformRequest(string requestType, int id)  
    {
        var reader = new StreamReader(Request.Body);
        reader.BaseStream.Seek(0, SeekOrigin.Begin);
        var rawMessage = await reader.ReadToEndAsync();
        switch (requestType)
                {
                    case "getnone": //get with no body
                        return await _restService.GenerateGetRequest(JsonConvert.DeserializeObject<WorkerConfigurationModel>(rawMessage));
                    case "postform-data": return await _restService.GeneratePostRequestFormData(JsonConvert.DeserializeObject<WorkerConfigurationModel>(rawMessage));
                    case "postraw": return await _restService.GeneratePostRequestRaw(JsonConvert.DeserializeObject<WorkerConfigurationModel>(rawMessage));
                        // switch (workerConfigurationModel.bodyType)
                        // {
                        //     case "raw":
                        //         GenerateAPICallConfirmation.Content = await RestService.GeneratePostRequestRaw(workerConfigurationModel,
                        //             workerConfigurationModel.RawModel.Text);
                        //         GenerateAPICall_Click(confirmed);
                        //         break;
                        //     case "form-data":
                        //         GenerateAPICallConfirmation.Content = await RestService.GeneratePostRequestFormData(workerConfigurationModel,
                        //             workerConfigurationModel.FormDataModel);
                        //         GenerateAPICall_Click(confirmed);
                        //         break;
                        // }
                    // case "put":
                    //     switch (workerConfigurationModel.bodyType)
                    //     {
                    //         case "raw":
                    //             GenerateAPICallConfirmation.Content = await RestService.GeneratePutRequestRaw(workerConfigurationModel,
                    //                 workerConfigurationModel.RawModel.Text);
                    //             GenerateAPICall_Click(confirmed);
                    //             break;
                    //         case "form-data":
                    //             GenerateAPICallConfirmation.Content = await RestService.GeneratePutRequestFormdata(workerConfigurationModel,
                    //                 workerConfigurationModel.FormDataModel);
                    //             GenerateAPICall_Click(confirmed);
                    //             break;
                    //     }
                    //     
                    //     break;
                    case "putform-data": return await _restService.GeneratePutRequestFormdata(JsonConvert.DeserializeObject<WorkerConfigurationModel>(rawMessage));
                    case "putraw": return await _restService.GeneratePutRequestRaw(JsonConvert.DeserializeObject<WorkerConfigurationModel>(rawMessage));
                    // case "patch":
                    //     switch (workerConfigurationModel.bodyType)
                    //     {
                    //         case "raw":
                    //             GenerateAPICallConfirmation.Content = await RestService.GeneratePatchRequestRaw(workerConfigurationModel,
                    //                 workerConfigurationModel.RawModel.Text);
                    //             GenerateAPICall_Click(confirmed);
                    //             break;
                    //         case "form-data":
                    //             GenerateAPICallConfirmation.Content = await RestService.GeneratePatchRequestFormdata(
                    //                 workerConfigurationModel,
                    //                 workerConfigurationModel.FormDataModel);
                    //             GenerateAPICall_Click(confirmed);
                    //             break;
                    //     }
                    //     
                    //     break;
                    case "patchform-data": return await _restService.GeneratePatchRequestFormdata(JsonConvert.DeserializeObject<WorkerConfigurationModel>(rawMessage));
                    case "patchraw": return await _restService.GeneratePatchRequestRaw(JsonConvert.DeserializeObject<WorkerConfigurationModel>(rawMessage));
                    case "deletenone": //delete no body
                        return await _restService.GenerateDeleteRequest(JsonConvert.DeserializeObject<WorkerConfigurationModel>(rawMessage));  
                }
        return "";
        
    }
}