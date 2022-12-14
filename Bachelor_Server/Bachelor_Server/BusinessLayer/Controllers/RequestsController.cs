using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.Models;
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

    [HttpPost("requests/{requestType}")]
    public async Task<string> PerformRequest(string requestType)  
    {
        var body = new StreamReader(Request.Body).ReadToEndAsync();
        switch (requestType)
                {
                    case "getnone": 
                        return await _restService.GenerateGetRequest(JsonConvert.DeserializeObject<WorkerConfiguration>(body.Result));
                    case "postform-data": return await _restService.GeneratePostRequestFormData(JsonConvert.DeserializeObject<WorkerConfiguration>(body.Result));
                    case "postraw": return await _restService.GeneratePostRequestRaw(JsonConvert.DeserializeObject<WorkerConfiguration>(body.Result));
                    case "putform-data": return await _restService.GeneratePutRequestFormdata(JsonConvert.DeserializeObject<WorkerConfiguration>(body.Result));
                    case "putraw": return await _restService.GeneratePutRequestRaw(JsonConvert.DeserializeObject<WorkerConfiguration>(body.Result));
                    case "patchform-data": return await _restService.GeneratePatchRequestFormdata(JsonConvert.DeserializeObject<WorkerConfiguration>(body.Result));
                    case "patchraw": return await _restService.GeneratePatchRequestRaw(JsonConvert.DeserializeObject<WorkerConfiguration>(body.Result));
                    case "deletenone": 
                        return await _restService.GenerateDeleteRequest(JsonConvert.DeserializeObject<WorkerConfiguration>(body.Result));  
                }
        return "";
        
    }
}