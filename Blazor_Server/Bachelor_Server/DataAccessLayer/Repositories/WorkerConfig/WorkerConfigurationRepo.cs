using System.Data.SqlClient;
using Bachelor_Server.Models.Body;
using Bachelor_Server.Models.WorkerConfiguration;


namespace Bachelor_Server.DataAccessLayer.Repositories.WorkerConfig
{
    public class WorkerConfigurationRepo : IWorkerConfigurationRepo
    {
        private DatabaseContext _databaseContext = new DatabaseContext();
        private string connectionString = "Data Source=rcasep6.database.windows.net;" +
                                                                      "Initial Catalog=BachelorProject;" +
                                                                      "User ID=rcasep6semesterproject;" +
                                                                      "Password=Pie1cutiepie2kitten3doggy4;" +
                                                                      "Connect Timeout=30;Encrypt=True;" +
                                                                      "TrustServerCertificate=False;" +
                                                                      "ApplicationIntent=ReadWrite;" +
                                                                      "MultiSubnetFailover=False";
        private int BodyID;
        private int AuthID;

        public async Task CreateWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel)
        {
            //   await using (SqlConnection connection = new SqlConnection(_databaseContext.connectionString))
            await using (SqlConnection connection = new SqlConnection(connectionString))

            {
                try
                {
                    await CreateAuthentication(workerConfigurationModel);
                    await CreateBody(workerConfigurationModel);
                    connection.Open();
                    await using (SqlCommand insertWC = new SqlCommand("insert into [dbo].[WorkerConfiguration]" +
                                                                "([URL],[RequestType], [LastSavedBody],[RawID], [LastSavedAuth], [BasicAuthID],[BearerTokenID],[APIKeyID],[OAuth1ID],[OAuth2ID]) " +
                                                                "values ( @URL, @RequestType, @LastSavedBody, @RawID, @LastSavedAuth, @BasicAuthID, @BearerTokenID, @APIKeyID, @OAuth1ID, @OAuth2ID)",
                               connection))
                    {

                        insertWC.Parameters.Clear();
                        insertWC.Parameters.AddWithValue("@URL", workerConfigurationModel.url);
                        insertWC.Parameters.AddWithValue("@RequestType", workerConfigurationModel.requestType);
                        insertWC.Parameters.AddWithValue("@LastSavedBody", workerConfigurationModel.bodyType);
                        switch (workerConfigurationModel.bodyType)
                        {
                            case "raw":
                                await using (SqlCommand getBody =
                                       new SqlCommand("SELECT TOP 1 * FROM [dbo].[Raw] ORDER BY ID DESC", connection))
                                {
                                    var reader = getBody.ExecuteReader();
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            BodyID = (int)reader["ID"];
                                        }
                                    }

                                    reader.Close();
                                }

                                insertWC.Parameters.AddWithValue("@RawID", BodyID);
                                break;
                            default:
                                insertWC.Parameters.AddWithValue("@RawID", 0);
                                break;
                        }
                        insertWC.Parameters.AddWithValue("@LastSavedAuth", workerConfigurationModel.authorizationType);
                        switch (workerConfigurationModel.authorizationType)
                        {
                            case "APIKey":
                                await using (SqlCommand getAuth =
                                       new SqlCommand("SELECT TOP 1 * FROM [dbo].[APIKey] ORDER BY ID DESC",
                                           connection))
                                {
                                    var reader = getAuth.ExecuteReader();
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            AuthID = (int)reader["ID"];
                                        }
                                    }

                                    reader.Close();
                                }

                                insertWC.Parameters.AddWithValue("@APIKeyID", AuthID);
                                insertWC.Parameters.AddWithValue("@BearerTokenID", 0);
                                insertWC.Parameters.AddWithValue("@BasicAuthID", 0);
                                insertWC.Parameters.AddWithValue("@OAuth1ID", 0);
                                insertWC.Parameters.AddWithValue("@OAuth2ID", 0);
                                break;
                            case "BearerToken":
                                await using (SqlCommand getAuth =
                                       new SqlCommand("SELECT TOP 1 * FROM [dbo].[BearerToken] ORDER BY ID DESC",
                                           connection))
                                {
                                    var reader = getAuth.ExecuteReader();
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            AuthID = (int)reader["ID"];
                                        }
                                    }

                                    reader.Close();
                                }

                                insertWC.Parameters.AddWithValue("@APIKeyID", 0);
                                insertWC.Parameters.AddWithValue("@BearerTokenID", AuthID);
                                insertWC.Parameters.AddWithValue("@BasicAuthID", 0);
                                insertWC.Parameters.AddWithValue("@OAuth1ID", 0);
                                insertWC.Parameters.AddWithValue("@OAuth2ID", 0);
                                break;
                            case "BasicAuth":
                                await using (SqlCommand getAuth =
                                       new SqlCommand("SELECT TOP 1 * FROM [dbo].[BasicAuth] ORDER BY ID DESC",
                                           connection))
                                {
                                    var reader = getAuth.ExecuteReader();
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            AuthID = (int)reader["ID"];
                                        }
                                    }

                                    reader.Close();
                                }

                                insertWC.Parameters.AddWithValue("@APIKeyID", 0);
                                insertWC.Parameters.AddWithValue("@BearerTokenID", 0);
                                insertWC.Parameters.AddWithValue("@BasicAuthID", AuthID);
                                insertWC.Parameters.AddWithValue("@OAuth1ID", 0);
                                insertWC.Parameters.AddWithValue("@OAuth2ID", 0);
                                break;
                            case "OAuth1.0":
                                await using (SqlCommand getAuth =
                                       new SqlCommand("SELECT TOP 1 * FROM [dbo].[OAuth1.0] ORDER BY ID DESC",
                                           connection))
                                {
                                    var reader = getAuth.ExecuteReader();
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            AuthID = (int)reader["ID"];
                                        }
                                    }

                                    reader.Close();
                                }

                                insertWC.Parameters.AddWithValue("@APIKeyID", 0);
                                insertWC.Parameters.AddWithValue("@BearerTokenID", 0);
                                insertWC.Parameters.AddWithValue("@BasicAuthID", 0);
                                insertWC.Parameters.AddWithValue("@OAuth1ID", AuthID);
                                insertWC.Parameters.AddWithValue("@OAuth2ID", 0);
                                break;
                            case "OAuth2.0":
                                await using (SqlCommand getAuth =
                                       new SqlCommand("SELECT TOP 1 * FROM [dbo].[OAuth2.0] ORDER BY ID DESC",
                                           connection))
                                {
                                    var reader = getAuth.ExecuteReader();
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            AuthID = (int)reader["ID"];
                                        }
                                    }

                                    reader.Close();
                                }

                                insertWC.Parameters.AddWithValue("@APIKeyID", 0);
                                insertWC.Parameters.AddWithValue("@BearerTokenID", 0);
                                insertWC.Parameters.AddWithValue("@BasicAuthID", 0);
                                insertWC.Parameters.AddWithValue("@OAuth1ID", 0);
                                insertWC.Parameters.AddWithValue("@OAuth2ID", AuthID);
                                break;
                            default:
                                insertWC.Parameters.AddWithValue("@APIKeyID", 0);
                                insertWC.Parameters.AddWithValue("@BearerTokenID", 0);
                                insertWC.Parameters.AddWithValue("@BasicAuthID", 0);
                                insertWC.Parameters.AddWithValue("@OAuth1ID", 0);
                                insertWC.Parameters.AddWithValue("@OAuth2ID", 0);

                                break;
                        }

                        insertWC.ExecuteNonQuery();
                    }

                    int workerID = await GetWorkerConfigurationID();

                    foreach (FormDataModel formDataModel in workerConfigurationModel.FormDataModel)
                    {
                        await using (SqlCommand insertBody = new SqlCommand("insert into [dbo].[FormData]" +
                                                                  "([WorkerConfigurationID],[Key],[Value],[Description]) " +
                                                                  "values (@WorkerConfigurationID, @Key, @Value, @Description)", connection))
                        {
                            insertBody.Parameters.Clear();
                            insertBody.Parameters.AddWithValue("@WorkerConfigurationID", workerID);
                            insertBody.Parameters.AddWithValue("@Key", formDataModel.Key);
                            insertBody.Parameters.AddWithValue("@Value", formDataModel.Value);
                            insertBody.Parameters.AddWithValue("@Description", formDataModel.Description);
                            insertBody.ExecuteNonQuery();
                        }
                    }
                    foreach (ParametersHeaderModel header in workerConfigurationModel.headers)
                    {
                        await using (SqlCommand insertHeader = new SqlCommand("INSERT INTO [dbo].[Headers] " +
                                                                "([WorkerConfigurationID],[Key],[Value],[Description]) " +
                                                                "values (@WorkerConfigurationID, @Key, @Value, @Description)", connection))
                        {
                            insertHeader.Parameters.Clear();
                            insertHeader.Parameters.AddWithValue("@WorkerConfigurationID", workerID);
                            insertHeader.Parameters.AddWithValue("@Key", header.Key);
                            insertHeader.Parameters.AddWithValue("@Value", header.Value);
                            insertHeader.Parameters.AddWithValue("@Description", header.Description);
                            insertHeader.ExecuteNonQuery();
                        }
                    }
                    foreach (ParametersHeaderModel parameter in workerConfigurationModel.parameters)
                    {
                        await using (SqlCommand insertParameter = new SqlCommand("INSERT INTO [dbo].[Parameters] " +
                                                                "([WorkerConfigurationID],[Key],[Value],[Description]) " +
                                                                "values (@WorkerConfigurationID, @Key, @Value, @Description)", connection))
                        {
                            insertParameter.Parameters.Clear();
                            insertParameter.Parameters.AddWithValue("@WorkerConfigurationID", workerID);
                            insertParameter.Parameters.AddWithValue("@Key", parameter.Key);
                            insertParameter.Parameters.AddWithValue("@Value", parameter.Value);
                            insertParameter.Parameters.AddWithValue("@Description", parameter.Description);
                            insertParameter.ExecuteNonQuery();
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    // We should log the error somewhere, 
                    // for this example let's just show a message
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task CreateBody(WorkerConfigurationModel workerConfigurationModel)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    switch (workerConfigurationModel.bodyType)
                    {
                        case "raw":
                            await using (SqlCommand insertBody = new SqlCommand("insert into [dbo].[Raw]" +
                                                                          "([Text]) " +
                                                                          "values (@Text)", connection))
                            {
                                insertBody.Parameters.Clear();
                                insertBody.Parameters.AddWithValue("@Text", workerConfigurationModel.RawModel.Text);
                                insertBody.ExecuteNonQuery();
                            }

                            break;
                        default:
                            break;
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task CreateAuthentication(WorkerConfigurationModel workerConfigurationModel)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    switch (workerConfigurationModel.authorizationType)
                    {
                        case "APIKey":
                            await using (SqlCommand insertAuth = new SqlCommand("insert into [dbo].[APIKey]" +
                                                                          "([Key], [Value], [AddTo]) " +
                                                                          "values ( @Key, @Value, @AddTo)", connection))
                            {
                                insertAuth.Parameters.Clear();
                                insertAuth.Parameters.AddWithValue("@Key", workerConfigurationModel.ApiKeyModel.Key);
                                insertAuth.Parameters.AddWithValue("@Value", workerConfigurationModel.ApiKeyModel.Value);
                                insertAuth.Parameters.AddWithValue("@AddTo", workerConfigurationModel.ApiKeyModel.AddTo);
                                insertAuth.ExecuteNonQuery();
                            }

                            break;
                        case "BearerToken":
                            await using (SqlCommand insertAuth = new SqlCommand("insert into [dbo].[BearerToken]" +
                                                                          "([Token]) " +
                                                                          "values ( @Token)", connection))
                            {
                                insertAuth.Parameters.Clear();
                                insertAuth.Parameters.AddWithValue("@Key", workerConfigurationModel.BearerTokenModel.Token);
                                insertAuth.ExecuteNonQuery();
                            }

                            break;
                        case "BasicAuth":
                            await using (SqlCommand insertAuth = new SqlCommand("insert into [dbo].[BasicAuth]" +
                                                                          "([Username], [Password]) " +
                                                                          "values ( @Username, @Password)", connection))
                            {
                                insertAuth.Parameters.Clear();
                                insertAuth.Parameters.AddWithValue("@Username", workerConfigurationModel.BasicAuthModel.Username);
                                insertAuth.Parameters.AddWithValue("@Password", workerConfigurationModel.BasicAuthModel.Password);
                                insertAuth.ExecuteNonQuery();
                            }

                            break;
                        case "OAuth1.0":
                            await using (SqlCommand insertAuth = new SqlCommand("insert into [dbo].[OAuth1.0]" +
                                                                           "([SignatureMethod], [ConsumerKey], [ConsumerSecret], [AccessToken], [CallbackURL], [Timestamp], [Nonce], [Version], [Realm], [IncludeBodyHash], [EmptyParamToSig]) " +
                                                                           "values ( @SignatureMethod, @ConsumerKey, @ConsumerSecret, @AccessToken, @CallbackURL, @Timestamp, @Nonce, @Version, @Realm, @IncludeBodyHash, @EmptyParamToSig)",
                                        connection))
                            {
                                insertAuth.Parameters.Clear();
                                insertAuth.Parameters.AddWithValue("@SignatureMethod", workerConfigurationModel.OAuth1Model.SignatureMethod);
                                insertAuth.Parameters.AddWithValue("@ConsumerKey", workerConfigurationModel.OAuth1Model.ConsumerKey);
                                insertAuth.Parameters.AddWithValue("@ConsumerSecret", workerConfigurationModel.OAuth1Model.ConsumerSecret);
                                insertAuth.Parameters.AddWithValue("@AccessToken", workerConfigurationModel.OAuth1Model.AccessToken);
                                insertAuth.Parameters.AddWithValue("@CallbackURL", workerConfigurationModel.OAuth1Model.CallbackURL);
                                insertAuth.Parameters.AddWithValue("@Timestamp", workerConfigurationModel.OAuth1Model.Timestamp);
                                insertAuth.Parameters.AddWithValue("@Nonce", workerConfigurationModel.OAuth1Model.Nonce);
                                insertAuth.Parameters.AddWithValue("@Version", workerConfigurationModel.OAuth1Model.Version);
                                insertAuth.Parameters.AddWithValue("@Realm", workerConfigurationModel.OAuth1Model.Realm);
                                insertAuth.Parameters.AddWithValue("@IncludeBodyHash", workerConfigurationModel.OAuth1Model.IncludeBodyHash);
                                insertAuth.Parameters.AddWithValue("@EmptyParamToSig", workerConfigurationModel.OAuth1Model.EmptyParamToSig);
                                insertAuth.ExecuteNonQuery();
                            }

                            break;
                        case "OAuth2.0":
                            await using (SqlCommand insertAuth = new SqlCommand("insert into [dbo].[OAuth2.0]" +
                                                                          "([AccessToken], [HeaderPrefix]) " +
                                                                          "values ( @AccessToken, @HeaderPrefix)",
                                       connection))
                            {
                                insertAuth.Parameters.Clear();
                                insertAuth.Parameters.AddWithValue("@AccessToken", workerConfigurationModel.OAuth2Model.AccessToken);
                                insertAuth.Parameters.AddWithValue("@HeaderPrefix", workerConfigurationModel.OAuth2Model.HeaderPrefix);
                                insertAuth.ExecuteNonQuery();
                            }

                            break;
                        default:
                            break;
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task<int> GetWorkerConfigurationID()
        {
            int workerID = 0;
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    await using (SqlCommand getWorkerConfig = new SqlCommand("SELECT TOP 1 * FROM [dbo].[WorkerConfiguration] ORDER BY WorkerConfigurationID DESC", connection))
                    {
                        var reader = getWorkerConfig.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                workerID = (int)reader["WorkerConfigurationID"];
                            }
                        }
                        reader.Close();

                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            return workerID;
        }

        public async Task<List<WorkerConfigurationModel>> GetWorkerConfigurations()
        {
            List<WorkerConfigurationModel> workerConfigurations = new List<WorkerConfigurationModel>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    await using (SqlCommand getWorkerConfig = new SqlCommand("select * from [dbo].[WorkerConfiguration] "
                        + "LEFT JOIN [dbo].[APIKey] api ON [dbo].[WorkerConfiguration].[APIKeyID] = api.ID "
                        + "LEFT JOIN [dbo].[BasicAuth] basic ON [dbo].[WorkerConfiguration].[BasicAuthID] = basic.ID "
                        + "LEFT JOIN [dbo].[BearerToken] bearer ON [dbo].[WorkerConfiguration].[BearerTokenID] = bearer.ID "
                        + "LEFT JOIN [dbo].[OAuth1.0] a1 ON [dbo].[WorkerConfiguration].[OAuth1ID] = a1.ID "
                        + "LEFT JOIN [dbo].[OAuth2.0] a2 ON [dbo].[WorkerConfiguration].[OAuth2ID] = a2.ID "
                        + "LEFT JOIN [dbo].[Raw] raw ON [dbo].[WorkerConfiguration].[RawID] = raw.ID ",
                        connection))
                    {
                        var reader = getWorkerConfig.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                WorkerConfigurationModel workerConfiguration = new WorkerConfigurationModel();
                                workerConfiguration.ID = (int)reader["WorkerConfigurationID"];
                                workerConfiguration.url = reader["URL"].ToString();
                                workerConfiguration.requestType = reader["RequestType"].ToString();
                                workerConfiguration.bodyType = reader["LastSavedBody"].ToString();
                                workerConfiguration.RawModel.Id = (int)reader["RawID"];
                                workerConfiguration.RawModel.Text = reader["Text"].ToString();
                                workerConfiguration.authorizationType = reader["LastSavedAuth"].ToString();
                                workerConfiguration.BasicAuthModel.Id = (int)reader["BasicAuthID"];
                                workerConfiguration.BasicAuthModel.Username = reader["Username"].ToString();
                                workerConfiguration.BasicAuthModel.Password = reader["Password"].ToString();
                                workerConfiguration.BearerTokenModel.Id = (int)reader["BearerTokenID"];
                                workerConfiguration.BearerTokenModel.Token = reader["Token"].ToString();
                                workerConfiguration.ApiKeyModel.Id = (int)reader["APIKeyID"];
                                workerConfiguration.ApiKeyModel.Key = reader["Key"].ToString();
                                workerConfiguration.ApiKeyModel.Value = reader["Value"].ToString();
                                workerConfiguration.ApiKeyModel.AddTo = reader["AddTo"].ToString();
                                workerConfiguration.OAuth2Model.Id = (int)reader["OAuth2ID"];
                                workerConfiguration.OAuth2Model.AccessToken = reader["AccessToken"].ToString();
                                workerConfiguration.OAuth2Model.HeaderPrefix = reader["HeaderPrefix"].ToString();

                                Console.WriteLine(workerConfiguration.ToString());
                                workerConfigurations.Add(workerConfiguration);
                            }
                        }
                        reader.Close();
                    }

                    foreach (WorkerConfigurationModel workerConfiguration in workerConfigurations)
                    {
                        await using (SqlCommand getFormData = new SqlCommand("SELECT * FROM [dbo].[FormData] "
                                    + " WHERE WorkerConfigurationID = @WorkerConfigID ORDER BY ID ASC", connection))
                        {
                            getFormData.Parameters.Clear();
                            getFormData.Parameters.AddWithValue("@WorkerConfigID", workerConfiguration.ID);

                            var formReader = getFormData.ExecuteReader();
                            if (formReader.HasRows)
                            {
                                while (formReader.Read())
                                {
                                    FormDataModel formData = new FormDataModel();
                                    formData.Id = (int)formReader["ID"];
                                    formData.Key = formReader["Key"].ToString();
                                    formData.Value = formReader["Value"].ToString();
                                    formData.Description = formReader["Description"].ToString();
                                    workerConfiguration.FormDataModel.Add(formData);
                                }
                            }
                            formReader.Close();
                        }
                        await using (SqlCommand getHeaders = new SqlCommand("SELECT * FROM [dbo].[Headers] "
                            + " WHERE WorkerConfigurationID = @WorkerConfigID ORDER BY ID ASC", connection))
                        {
                            getHeaders.Parameters.Clear();
                            getHeaders.Parameters.AddWithValue("@WorkerConfigID", workerConfiguration.ID);

                            var headersReader = getHeaders.ExecuteReader();
                            if (headersReader.HasRows)
                            {
                                while (headersReader.Read())
                                {
                                    ParametersHeaderModel header = new ParametersHeaderModel();
                                    header.ID = (int)headersReader["ID"];
                                    header.Key = headersReader["Key"].ToString();
                                    header.Value = headersReader["Value"].ToString();
                                    header.Description = headersReader["Description"].ToString();
                                    workerConfiguration.headers.Add(header);
                                }
                            }
                            headersReader.Close();
                        }
                        await using (SqlCommand getParameters = new SqlCommand("SELECT * FROM [dbo].[Parameters] "
                            + " WHERE WorkerConfigurationID = @WorkerConfigID ORDER BY ID ASC", connection))
                        {
                            getParameters.Parameters.Clear();
                            getParameters.Parameters.AddWithValue("@WorkerConfigID", workerConfiguration.ID);

                            var parametersReader = getParameters.ExecuteReader();
                            if (parametersReader.HasRows)
                            {
                                while (parametersReader.Read())
                                {
                                    ParametersHeaderModel parameter = new ParametersHeaderModel();
                                    parameter.ID = (int)parametersReader["ID"];
                                    parameter.Key = parametersReader["Key"].ToString();
                                    parameter.Value = parametersReader["Value"].ToString();
                                    parameter.Description = parametersReader["Description"].ToString();
                                    workerConfiguration.parameters.Add(parameter);
                                }
                            }
                            parametersReader.Close();
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return workerConfigurations;
            }
        }

        public async Task EditWorkerConfiguration(WorkerConfigurationModel workerConfigurationModel)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))

            {
                try
                {
                    await EditAuthentication(workerConfigurationModel);
                    await EditBody(workerConfigurationModel);
                    connection.Open();
                    await using (SqlCommand deleteHeaders = new SqlCommand("DELETE FROM [dbo].[Headers] " +
                            "WHERE WorkerConfigurationID = @WorkerConfigurationID", connection))
                    {
                        deleteHeaders.Parameters.Clear();
                        deleteHeaders.Parameters.AddWithValue("@WorkerConfigurationID", workerConfigurationModel.ID);
                        deleteHeaders.ExecuteNonQuery();
                    }
                    foreach (ParametersHeaderModel header in workerConfigurationModel.headers)
                    {
                        await using (SqlCommand editHeader = new SqlCommand("INSERT INTO [dbo].[Headers] " +
                            "([WorkerConfigurationID],[Key],[Value],[Description]) " +
                            "VALUES (@WorkerConfigurationID, @Key, @Value, @Description)", connection))


                        {
                            editHeader.Parameters.Clear();
                            editHeader.Parameters.AddWithValue("@WorkerConfigurationID", workerConfigurationModel.ID);
                            editHeader.Parameters.AddWithValue("@Key", header.Key);
                            editHeader.Parameters.AddWithValue("@Value", header.Value);
                            editHeader.Parameters.AddWithValue("@Description", header.Description);
                            editHeader.ExecuteNonQuery();
                        }
                    }
                    await using (SqlCommand deleteParameters = new SqlCommand("DELETE FROM [dbo].[Parameters] " +
                            "WHERE WorkerConfigurationID = @WorkerConfigurationID", connection))
                    {
                        deleteParameters.Parameters.Clear();
                        deleteParameters.Parameters.AddWithValue("@WorkerConfigurationID", workerConfigurationModel.ID);
                        deleteParameters.ExecuteNonQuery();
                    }
                    foreach (ParametersHeaderModel parameter in workerConfigurationModel.parameters)
                    {
                        await using (SqlCommand editParameter = new SqlCommand("INSERT INTO [dbo].[Parameters] " +
                            "([WorkerConfigurationID],[Key],[Value],[Description]) " +
                            "VALUES (@WorkerConfigurationID, @Key, @Value, @Description)", connection))
                        {
                            editParameter.Parameters.Clear();
                            editParameter.Parameters.AddWithValue("@WorkerConfigurationID", workerConfigurationModel.ID);
                            editParameter.Parameters.AddWithValue("@Key", parameter.Key);
                            editParameter.Parameters.AddWithValue("@Value", parameter.Value);
                            editParameter.Parameters.AddWithValue("@Description", parameter.Description);
                            editParameter.ExecuteNonQuery();
                        }
                    }
                    await using (SqlCommand updateWC = new SqlCommand("UPDATE [dbo].[WorkerConfiguration] " +
                                                                "SET [URL] = @URL, [RequestType] = @RequestType, [LastSavedBody] = @LastSavedBody, [RawID] = @RawID, " +
                                                                "[LastSavedAuth] = @LastSavedAuth, [BasicAuthID] = @BasicAuthID, [BearerTokenID] = @BearerTokenID, [APIKeyID] = @APIKeyID, [OAuth2ID] = @OAuth2ID " +
                                                                "WHERE WorkerConfigurationID = @WorkerConfigurationID",
                               connection))
                    {

                        updateWC.Parameters.Clear();
                        updateWC.Parameters.AddWithValue("@WorkerConfigurationID", workerConfigurationModel.ID);
                        updateWC.Parameters.AddWithValue("@URL", workerConfigurationModel.url);
                        updateWC.Parameters.AddWithValue("@RequestType", workerConfigurationModel.requestType);
                        updateWC.Parameters.AddWithValue("@LastSavedBody", workerConfigurationModel.bodyType);
                        updateWC.Parameters.AddWithValue("@RawID", workerConfigurationModel.RawModel.Id);
                        updateWC.Parameters.AddWithValue("@LastSavedAuth", workerConfigurationModel.authorizationType);
                        updateWC.Parameters.AddWithValue("@BasicAuthID", workerConfigurationModel.BasicAuthModel.Id);
                        updateWC.Parameters.AddWithValue("@BearerTokenID", workerConfigurationModel.BearerTokenModel.Id);
                        updateWC.Parameters.AddWithValue("@APIKeyID", workerConfigurationModel.ApiKeyModel.Id);
                        //OAuth1 missing
                        updateWC.Parameters.AddWithValue("@OAuth2ID", workerConfigurationModel.OAuth2Model.Id);
                        updateWC.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    // We should log the error somewhere, 
                    // for this example let's just show a message
                    Console.WriteLine(ex.Message);
                }

            }
        }

        private async Task EditAuthentication(WorkerConfigurationModel workerConfigurationModel)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    await using (SqlCommand editAuth = new SqlCommand("IF EXISTS(SELECT * FROM [dbo].[APIKey] WHERE ID = @APIKeyID) " +
                                                        "UPDATE [dbo].[APIKey] " +
                                                        "SET [Key] = @Key, [Value] = @Value, [AddTo] = @AddTo " +
                                                        "WHERE ID = @APIKeyID " +
                                                        "ELSE INSERT INTO [dbo].[APIKey] ([Key],[Value],[AddTo]) " +
                                                        "VALUES (@Key, @Value, @AddTo)", connection))
                    {
                        editAuth.Parameters.Clear();
                        editAuth.Parameters.AddWithValue("@APIKeyID", workerConfigurationModel.ApiKeyModel.Id);
                        editAuth.Parameters.AddWithValue("@Key", workerConfigurationModel.ApiKeyModel.Key);
                        editAuth.Parameters.AddWithValue("@Value", workerConfigurationModel.ApiKeyModel.Value);
                        editAuth.Parameters.AddWithValue("@AddTo", workerConfigurationModel.ApiKeyModel.AddTo);
                        editAuth.ExecuteNonQuery();
                    }

                    await using (SqlCommand editAuth = new SqlCommand("IF EXISTS(SELECT * FROM [dbo].[BearerToken] WHERE ID = @BearerTokenID) " +
                                                        "UPDATE [dbo].[BearerToken] " +
                                                        "SET [Token] = @Token " +
                                                        "WHERE ID = @BearerTokenID " +
                                                        "ELSE INSERT INTO [dbo].[BearerToken] ([Token]) " +
                                                        "VALUES (@Token)", connection))
                    {
                        editAuth.Parameters.Clear();
                        editAuth.Parameters.AddWithValue("@BearerTokenID", workerConfigurationModel.BearerTokenModel.Id);
                        editAuth.Parameters.AddWithValue("@Token", workerConfigurationModel.BearerTokenModel.Token);
                        editAuth.ExecuteNonQuery();
                    }

                    await using (SqlCommand editAuth = new SqlCommand("IF EXISTS(SELECT * FROM [dbo].[BasicAuth] WHERE ID = @BasicAuthID) " +
                                                        "UPDATE [dbo].[BasicAuth] " +
                                                        "SET [Username] = @Username, [Password] = @Password " +
                                                        "WHERE ID = @BasicAuthID " +
                                                        "ELSE INSERT INTO [dbo].[BasicAuth] ([Username],[Password]) " +
                                                        "VALUES (@Username, @Password)", connection))
                    {
                        editAuth.Parameters.Clear();
                        editAuth.Parameters.AddWithValue("@BasicAuthID", workerConfigurationModel.BasicAuthModel.Id);
                        editAuth.Parameters.AddWithValue("@Username", workerConfigurationModel.BasicAuthModel.Username);
                        editAuth.Parameters.AddWithValue("@Password", workerConfigurationModel.BasicAuthModel.Password);
                        editAuth.ExecuteNonQuery();
                    }

                    /*await using (SqlCommand insertAuth = new SqlCommand("insert into [dbo].[OAuth1.0]" +
                                                                    "([SignatureMethod], [ConsumerKey], [ConsumerSecret], [AccessToken], [CallbackURL], [Timestamp], [Nonce], [Version], [Realm], [IncludeBodyHash], [EmptyParamToSig]) " +
                                                                    "values ( @SignatureMethod, @ConsumerKey, @ConsumerSecret, @AccessToken, @CallbackURL, @Timestamp, @Nonce, @Version, @Realm, @IncludeBodyHash, @EmptyParamToSig)",
                                connection))
                    {
                        insertAuth.Parameters.Clear();
                        insertAuth.Parameters.AddWithValue("@SignatureMethod", workerConfigurationModel.OAuth1Model.SignatureMethod);
                        insertAuth.Parameters.AddWithValue("@ConsumerKey", workerConfigurationModel.OAuth1Model.ConsumerKey);
                        insertAuth.Parameters.AddWithValue("@ConsumerSecret", workerConfigurationModel.OAuth1Model.ConsumerSecret);
                        insertAuth.Parameters.AddWithValue("@AccessToken", workerConfigurationModel.OAuth1Model.AccessToken);
                        insertAuth.Parameters.AddWithValue("@CallbackURL", workerConfigurationModel.OAuth1Model.CallbackURL);
                        insertAuth.Parameters.AddWithValue("@Timestamp", workerConfigurationModel.OAuth1Model.Timestamp);
                        insertAuth.Parameters.AddWithValue("@Nonce", workerConfigurationModel.OAuth1Model.Nonce);
                        insertAuth.Parameters.AddWithValue("@Version", workerConfigurationModel.OAuth1Model.Version);
                        insertAuth.Parameters.AddWithValue("@Realm", workerConfigurationModel.OAuth1Model.Realm);
                        insertAuth.Parameters.AddWithValue("@IncludeBodyHash", workerConfigurationModel.OAuth1Model.IncludeBodyHash);
                        insertAuth.Parameters.AddWithValue("@EmptyParamToSig", workerConfigurationModel.OAuth1Model.EmptyParamToSig);
                        insertAuth.ExecuteNonQuery();
                    }*/

                    await using (SqlCommand editAuth = new SqlCommand("IF EXISTS(SELECT * FROM [dbo].[OAuth2.0] WHERE ID = @OAuth2ID) " +
                                                        "UPDATE [dbo].[OAuth2.0] " +
                                                        "SET [AccessToken] = @AccessToken, [HeaderPrefix] = @HeaderPrefix " +
                                                        "WHERE ID = @OAuth2ID " +
                                                        "ELSE INSERT INTO [dbo].[OAuth2.0] ([AccessToken],[HeaderPrefix]) " +
                                                        "VALUES (@AccessToken, @HeaderPrefix)", connection))
                    {
                        editAuth.Parameters.Clear();
                        editAuth.Parameters.AddWithValue("@OAuth2ID", workerConfigurationModel.OAuth2Model.Id);
                        editAuth.Parameters.AddWithValue("@AccessToken", workerConfigurationModel.OAuth2Model.AccessToken);
                        editAuth.Parameters.AddWithValue("@HeaderPrefix", workerConfigurationModel.OAuth2Model.HeaderPrefix);
                        editAuth.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task EditBody(WorkerConfigurationModel workerConfigurationModel)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    await using (SqlCommand editBody = new SqlCommand("IF EXISTS(SELECT * FROM [dbo].[Raw] WHERE ID = @RawID) " +
                            "UPDATE [dbo].[Raw] " +
                            "SET [Text] = @Text " +
                            "WHERE ID = @RawID " +
                            "ELSE INSERT INTO [dbo].[Raw] ([Text]) " +
                            "VALUES (@Text)", connection))
                    {
                        editBody.Parameters.Clear();
                        editBody.Parameters.AddWithValue("@RawID", workerConfigurationModel.RawModel.Id);
                        editBody.Parameters.AddWithValue("@Text", workerConfigurationModel.RawModel.Text);
                        editBody.ExecuteNonQuery();
                    }
                    await using (SqlCommand deleteFormData = new SqlCommand("DELETE FROM [dbo].[FormData] " +
                            "WHERE WorkerConfigurationID = @WorkerConfigurationID", connection))
                    {
                        deleteFormData.Parameters.Clear();
                        deleteFormData.Parameters.AddWithValue("@WorkerConfigurationID", workerConfigurationModel.ID);
                        deleteFormData.ExecuteNonQuery();
                    }
                    foreach (FormDataModel formData in workerConfigurationModel.FormDataModel)
                    {
                        await using (SqlCommand editFormData = new SqlCommand("INSERT INTO [dbo].[FormData] " +
                            "([WorkerConfigurationID],[Key],[Value],[Description]) " +
                            "VALUES (@WorkerConfigurationID, @Key, @Value, @Description)", connection))
                        {
                            editFormData.Parameters.Clear();
                            editFormData.Parameters.AddWithValue("@WorkerConfigurationID", workerConfigurationModel.ID);
                            editFormData.Parameters.AddWithValue("@FormDataID", formData.Id);
                            editFormData.Parameters.AddWithValue("@Key", formData.Key);
                            editFormData.Parameters.AddWithValue("@Value", formData.Value);
                            editFormData.Parameters.AddWithValue("@Description", formData.Description);
                            editFormData.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task DeleteWorkerConfiguration(int id)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await DeleteAuthentication(id);
                    await DeleteBody(id);
                    connection.Open();
                    await using (SqlCommand deleteWC = new SqlCommand("DELETE wc FROM [dbo].[WorkerConfiguration] as wc " +
                                                        "WHERE wc.WorkerConfigurationID = @WorkerConfigurationID", connection))
                    {
                        deleteWC.Parameters.Clear();
                        deleteWC.Parameters.AddWithValue("@WorkerConfigurationID", id);
                        deleteWC.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task DeleteAuthentication(int id)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    await using (SqlCommand deleteAuth = new SqlCommand("DELETE api FROM [dbo].[WorkerConfiguration] as wc " +
                                                        "JOIN [dbo].[APIKey] as api ON wc.APIKeyID = api.ID " +
                                                        "WHERE wc.WorkerConfigurationID = @WorkerConfigurationID", connection))
                    {
                        deleteAuth.Parameters.Clear();
                        deleteAuth.Parameters.AddWithValue("@WorkerConfigurationID", id);
                        deleteAuth.ExecuteNonQuery();
                    }
                    await using (SqlCommand deleteAuth = new SqlCommand("DELETE basic FROM [dbo].[WorkerConfiguration] as wc " +
                                                        "JOIN [dbo].[BasicAuth] as basic ON wc.BasicAuthID = basic.ID " +
                                                        "WHERE wc.WorkerConfigurationID = @WorkerConfigurationID", connection))
                    {
                        deleteAuth.Parameters.Clear();
                        deleteAuth.Parameters.AddWithValue("@WorkerConfigurationID", id);
                        deleteAuth.ExecuteNonQuery();
                    }
                    await using (SqlCommand deleteAuth = new SqlCommand("DELETE bear FROM [dbo].[WorkerConfiguration] as wc " +
                                                        "JOIN [dbo].[BearerToken] as bear ON wc.BearerTokenID = bear.ID " +
                                                        "WHERE wc.WorkerConfigurationID = @WorkerConfigurationID", connection))
                    {
                        deleteAuth.Parameters.Clear();
                        deleteAuth.Parameters.AddWithValue("@WorkerConfigurationID", id);
                        deleteAuth.ExecuteNonQuery();
                    }
                    await using (SqlCommand deleteAuth = new SqlCommand("DELETE o2 FROM [dbo].[WorkerConfiguration] as wc " +
                                                        "JOIN [dbo].[OAuth2.0] as o2 ON wc.OAuth2ID = o2.ID " +
                                                        "WHERE wc.WorkerConfigurationID = @WorkerConfigurationID", connection))
                    {
                        deleteAuth.Parameters.Clear();
                        deleteAuth.Parameters.AddWithValue("@WorkerConfigurationID", id);
                        deleteAuth.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task DeleteBody(int id)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    await using (SqlCommand deleteBody = new SqlCommand("DELETE raw FROM [dbo].[WorkerConfiguration] as wc " +
                                                        "JOIN [dbo].[Raw] as raw ON wc.RawID = raw.ID " +
                                                        "WHERE wc.WorkerConfigurationID = @WorkerConfigurationID", connection))
                    {
                        deleteBody.Parameters.Clear();
                        deleteBody.Parameters.AddWithValue("@WorkerConfigurationID", id);
                        deleteBody.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}