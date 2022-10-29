using System.Text;
using Bachelor_Server.BusinessLayer.Services.Logging;
using Bachelor_Server.BusinessLayer.Services.WorkerConfig;
using Bachelor_Server.Models.WorkerConfiguration;

namespace Bachelor_Server.BusinessLayer.Services.Requests
{
    public class RestService : IRestService
    {
        private ILogHandling _log;
        private IWorkerConfigService _workerConfigService;
        

        public RestService(ILogHandling log, IWorkerConfigService workerConfigService)
        {
            _log = log;
            _workerConfigService = workerConfigService;
        }

        private string auth(WorkerConfigurationModel workerConfigurationModel)
        {
            switch (workerConfigurationModel.authorizationType)
            {
                case "APIKey":
                    return workerConfigurationModel.ApiKeyModel.Key + " " + workerConfigurationModel.ApiKeyModel.Value;
                case "BearerToken":
                    return workerConfigurationModel.BearerTokenModel.Token;
                case "BasicAuth":
                    return workerConfigurationModel.BasicAuthModel.Username + ":" +
                           workerConfigurationModel.BasicAuthModel.Password;
                    break;
                // case "OAuth1": 
                case "OAuth2":
                    return workerConfigurationModel.OAuth2Model.AccessToken + ":" +
                           workerConfigurationModel.OAuth2Model.HeaderPrefix;
                default: return "NoAuth";
            }
        }

        public async Task<string> GenerateGetRequest(int id)
        {
            WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //uri
                    try
                    {
                        httpClient.BaseAddress = new Uri(workerConfiguration.url);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("The URL is not valid");
                        /*string response = await _log.Log(e);
                        return response;*/
                    }

                    //headers
                    foreach (var item in workerConfiguration.headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    string auth = this.auth(workerConfiguration);
                    //auth
                    if (!auth.Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.authorizationType + " " + auth);

                    if (!auth.Equals("NoAuth") && workerConfiguration.ApiKeyModel.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.ApiKeyModel.Key + "=" +
                                           workerConfiguration.ApiKeyModel.Value;
                    }



                    HttpResponseMessage responseMessage = await httpClient.GetAsync(fullParamString);
                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.Log(e);
                return response;
            }
        }


        public async Task<string> GenerateDeleteRequest(int id)
        {
            WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(workerConfiguration.url);

                    //headers
                    foreach (var item in workerConfiguration.headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.ApiKeyModel.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.ApiKeyModel.Key + "=" +
                                           workerConfiguration.ApiKeyModel.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.authorizationType + " " + auth);
                    HttpResponseMessage responseMessage = await httpClient.DeleteAsync(workerConfiguration.url);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        // Parse the response body.
                        return responseMessage.Content.ReadAsStringAsync().Result;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                string response = await _log.Log(e);
                return response;
            }
        }


        public async Task<string> GeneratePatchRequestFormdata(int id)
        {
            WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (var VARIABLE in workerConfiguration.FormDataModel)
                    {
                        dictionary.Add(VARIABLE.Key, VARIABLE.Value);
                    }

                    var content = new FormUrlEncodedContent(dictionary);
                    httpClient.BaseAddress = new Uri(workerConfiguration.url);

                    //headers
                    foreach (var item in workerConfiguration.headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.ApiKeyModel.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.ApiKeyModel.Key + "=" +
                                           workerConfiguration.ApiKeyModel.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.authorizationType + " " + auth);

                    HttpResponseMessage responseMessage =
                        await httpClient.PatchAsync(workerConfiguration.url, content);

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.Log(e);
                return response;
            }
        }


        public async Task<string> GeneratePatchRequestRaw(int id)
        {
            WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(workerConfiguration.url);

                    //headers
                    foreach (var item in workerConfiguration.headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.ApiKeyModel.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.ApiKeyModel.Key + "=" +
                                           workerConfiguration.ApiKeyModel.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.authorizationType + " " + auth);
                    HttpResponseMessage responseMessage = await httpClient.PatchAsync(workerConfiguration.url,
                        new StringContent(workerConfiguration.RawModel.Text, Encoding.UTF8, "application/json"));

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.Log(e);
                return response;
            }
        }

        public async Task<string> GeneratePostRequestFormData(int id)
        {
            WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (var VARIABLE in workerConfiguration.FormDataModel)
                    {
                        dictionary.Add(VARIABLE.Key, VARIABLE.Value);
                    }

                    var content = new FormUrlEncodedContent(dictionary);
                    httpClient.BaseAddress = new Uri(workerConfiguration.url);

                    //headers
                    foreach (var item in workerConfiguration.headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.ApiKeyModel.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.ApiKeyModel.Key + "=" +
                                           workerConfiguration.ApiKeyModel.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.authorizationType + " " + auth);

                    HttpResponseMessage responseMessage =
                        await httpClient.PostAsync(workerConfiguration.url, content);

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.Log(e);
                return response;
            }
        }

        public async Task<string> GeneratePostRequestRaw(int id)
        {
            WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(workerConfiguration.url);

                    //headers
                    foreach (var item in workerConfiguration.headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.ApiKeyModel.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.ApiKeyModel.Key + "=" +
                                           workerConfiguration.ApiKeyModel.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.authorizationType + " " + auth);

                    HttpResponseMessage responseMessage = await httpClient.PostAsync(workerConfiguration.url,
                        new StringContent(workerConfiguration.RawModel.Text, Encoding.UTF8, "application/json"));

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.Log(e);
                return response;
            }
        }

        public async Task<string> GeneratePutRequestRaw(int id)
        {
            WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(workerConfiguration.url);

                    //headers
                    foreach (var item in workerConfiguration.headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.ApiKeyModel.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.ApiKeyModel.Key + "=" +
                                           workerConfiguration.ApiKeyModel.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.authorizationType + " " + auth);

                    HttpResponseMessage responseMessage = await httpClient.PutAsync(workerConfiguration.url,
                        new StringContent(workerConfiguration.RawModel.Text, Encoding.UTF8, "application/json"));

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.Log(e);
                return response;
            }
        }

        public async Task<string> GeneratePutRequestFormdata(int id)
        {
            WorkerConfigurationModel workerConfiguration = _workerConfigService.GetWorkerConfigurationById(id);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (var VARIABLE in workerConfiguration.FormDataModel)
                    {
                        dictionary.Add(VARIABLE.Key, VARIABLE.Value);
                    }

                    var content = new FormUrlEncodedContent(dictionary);
                    httpClient.BaseAddress = new Uri(workerConfiguration.url);

                    //headers
                    foreach (var item in workerConfiguration.headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    //params
                    string fullParamString = "?";
                    foreach (var item in workerConfiguration.parameters)
                    {
                        fullParamString += item.Key + "=" + item.Value + "&";
                    }

                    if (workerConfiguration.ApiKeyModel.AddTo.Equals("QueryParam"))
                    {
                        fullParamString += workerConfiguration.ApiKeyModel.Key + "=" +
                                           workerConfiguration.ApiKeyModel.Value;
                    }

                    //auth
                    if (!auth(workerConfiguration).Equals("NoAuth"))
                        httpClient.DefaultRequestHeaders.Add("Authorization",
                            workerConfiguration.authorizationType + " " + auth);

                    HttpResponseMessage responseMessage =
                        await httpClient.PutAsync(workerConfiguration.url, content);

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                string response = await _log.Log(e);
                return response;
            }
        }
    }
}