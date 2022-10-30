using System.ComponentModel.DataAnnotations;
using Bachelor_Server.OldModels.Authorization;
using Bachelor_Server.OldModels.Body;
using Bachelor_Server.OldModels.General;

namespace Bachelor_Server.OldModels.WorkerConfiguration
{
    public class WorkerConfigurationModel
    {
        [Key] public int ID { get; set; }
        public string url { get; set; }
        public string requestType { get; set; }

        public List<ParametersHeaderModel> parameters { get; set; }
        public string authorizationType { get; set; }

        public List<ParametersHeaderModel> headers { get; set; }
        public string bodyType { get; set; }

        public APIKeyModel ApiKeyModel { get; set; }

        public BasicAuthModel BasicAuthModel { get; set; }

        public BearerTokenModel BearerTokenModel { get; set; }

        public OAuth1Model OAuth1Model { get; set; }

        public OAuth2Model OAuth2Model { get; set; }

        public List<FormDataModel> FormDataModel { get; set; }

        public RawModel RawModel { get; set; }

        public WorkerConfigData Data { get; set; }

        public WorkerConfigurationModel()
        {
            parameters = new List<ParametersHeaderModel>();
            headers = new List<ParametersHeaderModel>();
            ApiKeyModel = new APIKeyModel();
            BasicAuthModel = new BasicAuthModel();
            BearerTokenModel = new BearerTokenModel();
            OAuth1Model = new OAuth1Model();
            OAuth2Model = new OAuth2Model();
            FormDataModel = new List<FormDataModel>();
            RawModel = new RawModel();
        }
    }
}