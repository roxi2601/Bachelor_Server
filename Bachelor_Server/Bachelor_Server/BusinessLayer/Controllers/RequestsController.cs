using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Bachelor_Server.BusinessLayer.Controllers;

[ApiController]
public class RequestsController : ControllerBase
{
    private IRestService _restService;
    
    public RequestsController(IRestService restService)
    {
        _restService = restService;
    }
    
    
    [HttpGet("requests/{requestType}/{id}")]
    public async Task<string> PerformRequest(string requestType, int id)  
    {
        switch (requestType)
                {
                    case "getnone": //get with no body
                        return await _restService.GenerateGetRequest(id);
                    case "postform-data": return await _restService.GeneratePostRequestFormData(id);
                    case "postraw": return await _restService.GeneratePostRequestRaw(id);
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
                    case "putform-data": return await _restService.GeneratePutRequestFormdata(id);
                    case "putraw": return await _restService.GeneratePutRequestRaw(id);
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
                    case "patchform-data": return await _restService.GeneratePatchRequestFormdata(id);
                    case "patchraw": return await _restService.GeneratePatchRequestRaw(id);
                    case "deletenone": //delete no body
                        return await _restService.GenerateDeleteRequest(id);  
                }
        return "";
        
    }
}