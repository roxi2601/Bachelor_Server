using System.Text;
using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.Models;

using Newtonsoft.Json;

namespace Bachelor_Server.BusinessLayer.Services.Requests
{
    public class RestService : IRestService
    {
        private ILogService _log;
        private IWorkerConfigService _workerConfigService;


        public RestService(ILogService log, IWorkerConfigService workerConfigService)
        {
            _log = log;
            _workerConfigService = workerConfigService;
        }

        private string auth(WorkerConfiguration workerConfigurationModel)
        {
            switch (workerConfigurationModel.LastSavedAuth)
            {
                case "APIKey":
                    return workerConfigurationModel.FkApikey.Key + " " + workerConfigurationModel.FkApikey.Value;
                case "BearerToken":
                    return workerConfigurationModel.FkBearerToken.Token;
                case "BasicAuth":
                    return workerConfigurationModel.FkBasicAuth.Username + ":" +
                           workerConfigurationModel.FkBasicAuth.Password;
                    break;
                // case "OAuth1": 
                case "OAuth2":
                    return workerConfigurationModel.FkOauth20.AccessToken + ":" +
                           workerConfigurationModel.FkOauth20.HeaderPrefix;
                default: return "NoAuth";
            }
        }

        public async Task<string> GenerateGetRequest(WorkerConfiguration workerConfiguration)
        {
         //   WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //uri
                    try
                    {
                        httpClient.BaseAddress = new Uri(workerConfiguration.Url);
                    }
                    catch (Exception e)
                    {
                        await _log.LogError(e);
                        return e.Message;
                    }

                    //Headers
                    foreach (var item in workerConfiguration.Headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.Parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    string auth = this.auth(workerConfiguration);
                    //auth
                    if (!auth.Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.LastSavedAuth + " " + auth);

                    if (!auth.Equals("NoAuth") && workerConfiguration.FkApikey.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.FkApikey.Key + "=" +
                                           workerConfiguration.FkApikey.Value;
                    }


                    HttpResponseMessage responseMessage = await httpClient.GetAsync(fullParamString);
                    await _log.Log(JsonConvert.SerializeObject(responseMessage.Content.ReadAsStringAsync()));
                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.LogError(e);
                return response;
            }
        }


        public async Task<string> GenerateDeleteRequest(WorkerConfiguration workerConfiguration)
        {
  //          WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(workerConfiguration.Url);

                    //Headers
                    foreach (var item in workerConfiguration.Headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.Parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.FkApikey.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.FkApikey.Key + "=" +
                                           workerConfiguration.FkApikey.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.LastSavedAuth + " " + auth);
                    HttpResponseMessage responseMessage = await httpClient.DeleteAsync(workerConfiguration.Url);
                    await _log.Log(JsonConvert.SerializeObject(responseMessage.Content.ReadAsStringAsync()));
                    // Parse the response body.
                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.LogError(e);
                return response;
            }
        }


        public async Task<string> GeneratePatchRequestFormdata(WorkerConfiguration workerConfiguration)
        {
  //          WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (var VARIABLE in workerConfiguration.FormData)
                    {
                        dictionary.Add(VARIABLE.Key, VARIABLE.Value);
                    }

                    var content = new FormUrlEncodedContent(dictionary);
                    httpClient.BaseAddress = new Uri(workerConfiguration.Url);

                    //Headers
                    foreach (var item in workerConfiguration.Headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.Parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.FkApikey.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.FkApikey.Key + "=" +
                                           workerConfiguration.FkApikey.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.LastSavedAuth + " " + auth);

                    HttpResponseMessage responseMessage =
                        await httpClient.PatchAsync(workerConfiguration.Url, content);
                    await _log.Log(JsonConvert.SerializeObject(responseMessage.Content.ReadAsStringAsync()));

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.LogError(e);
                return response;
            }
        }


        public async Task<string> GeneratePatchRequestRaw(WorkerConfiguration workerConfiguration)
        {
//            WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(workerConfiguration.Url);

                    //Headers
                    foreach (var item in workerConfiguration.Headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.Parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.FkApikey.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.FkApikey.Key + "=" +
                                           workerConfiguration.FkApikey.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.LastSavedAuth + " " + auth);
                    HttpResponseMessage responseMessage = await httpClient.PatchAsync(workerConfiguration.Url,
                        new StringContent(workerConfiguration.FkRaw.Text, Encoding.UTF8, "application/json"));
                    await _log.Log(JsonConvert.SerializeObject(responseMessage.Content.ReadAsStringAsync()));

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.LogError(e);
                return response;
            }
        }

        public async Task<string> GeneratePostRequestFormData(WorkerConfiguration workerConfiguration)
        {
   //         WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (var VARIABLE in workerConfiguration.FormData)
                    {
                        dictionary.Add(VARIABLE.Key, VARIABLE.Value);
                    }

                    var content = new FormUrlEncodedContent(dictionary);
                    httpClient.BaseAddress = new Uri(workerConfiguration.Url);

                    //Headers
                    foreach (var item in workerConfiguration.Headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.Parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.FkApikey.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.FkApikey.Key + "=" +
                                           workerConfiguration.FkApikey.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.LastSavedAuth + " " + auth);

                    HttpResponseMessage responseMessage =
                        await httpClient.PostAsync(workerConfiguration.Url, content);
                    await _log.Log(JsonConvert.SerializeObject(responseMessage.Content.ReadAsStringAsync()));

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.LogError(e);
                return response;
            }
        }

        public async Task<string> GeneratePostRequestRaw(WorkerConfiguration workerConfiguration)
        {
 //           WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(workerConfiguration.Url);

                    //Headers
                    foreach (var item in workerConfiguration.Headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.Parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.FkApikey.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.FkApikey.Key + "=" +
                                           workerConfiguration.FkApikey.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.LastSavedAuth + " " + auth);

                    HttpResponseMessage responseMessage = await httpClient.PostAsync(workerConfiguration.Url,
                        new StringContent(workerConfiguration.FkRaw.Text, Encoding.UTF8, "application/json"));
                    await _log.Log(JsonConvert.SerializeObject(responseMessage.Content.ReadAsStringAsync()));

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.LogError(e);
                return response;
            }
        }

        public async Task<string> GeneratePutRequestRaw(WorkerConfiguration workerConfiguration)
        {
 //           WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(workerConfiguration.Url);

                    //Headers
                    foreach (var item in workerConfiguration.Headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.Parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.FkApikey.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.FkApikey.Key + "=" +
                                           workerConfiguration.FkApikey.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.LastSavedAuth + " " + auth);

                    HttpResponseMessage responseMessage = await httpClient.PutAsync(workerConfiguration.Url,
                        new StringContent(workerConfiguration.FkRaw.Text, Encoding.UTF8, "application/json"));
                    await _log.Log(JsonConvert.SerializeObject(responseMessage.Content.ReadAsStringAsync()));

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.LogError(e);
                return response;
            }
        }

        public async Task<string> GeneratePutRequestFormdata(WorkerConfiguration workerConfiguration)
        {
   //         WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (var VARIABLE in workerConfiguration.FormData)
                    {
                        dictionary.Add(VARIABLE.Key, VARIABLE.Value);
                    }

                    var content = new FormUrlEncodedContent(dictionary);
                    httpClient.BaseAddress = new Uri(workerConfiguration.Url);

                    //Headers
                    foreach (var item in workerConfiguration.Headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.Parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.FkApikey.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.FkApikey.Key + "=" +
                                           workerConfiguration.FkApikey.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.LastSavedAuth + " " + auth);

                    HttpResponseMessage responseMessage =
                        await httpClient.PutAsync(workerConfiguration.Url, content);
                    await _log.Log(JsonConvert.SerializeObject(responseMessage.Content.ReadAsStringAsync()));

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.LogError(e);
                return response;
            }
        }
    }
}