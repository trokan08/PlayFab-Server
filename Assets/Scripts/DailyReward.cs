using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.DataModels;
using System.Collections.Generic;
using PlayFab.ServerModels;


namespace PlayFab.Functions
{
    public class TitleAuthenticationContext
    {
    public string Id { get; set; }
    public string EntityToken { get; set; }
    }
 // Models via ExecuteFunction API
public class FunctionExecutionContext<T>
{
    public PlayFab.ProfilesModels.EntityProfileBody CallerEntityProfile { get; set; }
    public TitleAuthenticationContext TitleAuthenticationContext { get; set; }
    public bool? GeneratePlayStreamEvent { get; set; }
    public T FunctionArgument { get; set; }
}

public class FunctionExecutionContext : FunctionExecutionContext<object>
{
}

    public static class DailyReward
    {
        [FunctionName("DailyReward")]
        public static async Task<dynamic> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            FunctionExecutionContext<dynamic> context = JsonConvert.DeserializeObject<FunctionExecutionContext<dynamic>>(await req.ReadAsStringAsync());
            dynamic args = context.FunctionArgument;
            // args.id senden object

            var apiSettings = new PlayFab.PlayFabApiSettings(){
                
                TitleId = context.TitleAuthenticationContext.Id,
                DeveloperSecretKey = "XSRKR5IPASHGP7MXNOJX4P73A6N6P4R3OC6UBPY7R3YIT5RSFG"
            };

            PlayFab.PlayFabAuthenticationContext titleContext = new PlayFab.PlayFabAuthenticationContext();

            titleContext.EntityToken = context.TitleAuthenticationContext.EntityToken;
            var serverAPI = new PlayFab.PlayFabServerInstanceAPI(apiSettings,titleContext);

            GetTitleDataRequest titleDataRequest = new GetTitleDataRequest{Keys = new List<string>(){"DailyRewards"}};
            var titleDataResult = await serverAPI.GetTitleDataAsync(titleDataRequest);

            if(titleDataResult.Result.Data.ContainsKey("DailyRewards")){
                return new OkObjectResult(titleDataResult.Result.Data["DailyRewards"]);
            }else{
                return null;
            }
            

          
        }
    }
}
