using Bachelor_Server.OldModels.Body;

namespace Bachelor_Server.OldModels.General;

public class WorkerConfigData
{
    public string OAuth2_HeaderPrefix { get; set; }
    public string OAuth2_AccessToken { get; set; }
    public string OAuth1_ParamSig { get; set; }
    public string OAuth1_BodyHash { get; set; }
    public string OAuth1_Realm { get; set; }
    public string OAuth1_Version { get; set; }
    public string OAuth1_Nonce { get; set; }
    public string OAuth1_Timestamp { get; set; }
    public string OAuth1_CallbackURL { get; set; }
    public string OAuth1_TokenSecret { get; set; }
    public string OAuth1_AccessToken { get; set; }
    public string OAuth1_CusSecret { get; set; }
    public string OAuth1_CusKey { get; set; }
    public string OAuth1_SignatureMethod { get; set; }
    public string BasicAuth_Password { get; set; }
    public string BasicAuth_Username { get; set; }
    public string BearerToken_Token { get; set; }
    public string APIKEY_AddTo { get; set; }
    public string APIKey_KEY { get; set; }
    public string APIKey_Value { get; set; }
    public string Raw { get; set; }
    public List<FormDataModel> Formdata { get; set; }
    public string AuthType { get; set; }
    public string BodyType { get; set; }
}