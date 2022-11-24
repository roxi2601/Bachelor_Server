// using Bachelor_Server.BusinessLayer.Services.Requests;
// using Bachelor_Server.Models;
// using Quartz;
//
// namespace Bachelor_Server.BusinessLayer.Services.ScheduleService;
//
// public class Job : IJob
// {
//
//     private IRestService _restService;
//     public Job(IRestService)
//     {
//     }
//
//     public Task Execute(IJobExecutionContext context)
//     {
//         switch (_workerConfiguration.RequestType + _workerConfiguration.LastSavedBody)
//         {
//             case "getnone": //get with no body
//                 return await _restService.GenerateGetRequest(workerConfiguration);
//             case "postform-data": return await _restService.GeneratePostRequestFormData(workerConfiguration);
//             case "postraw": return await _restService.GeneratePostRequestRaw(workerConfiguration);
//             case "putform-data": return await _restService.GeneratePutRequestFormdata(workerConfiguration);
//             case "putraw": return await _restService.GeneratePutRequestRaw(workerConfiguration);
//             case "patchform-data": return await _restService.GeneratePatchRequestFormdata(workerConfiguration);
//             case "patchraw": return await _restService.GeneratePatchRequestRaw(workerConfiguration);
//             case "deletenone": //delete no body
//                 return await _restService.GenerateDeleteRequest(workerConfiguration);
//         }
//
//         return "";
//     }
// }