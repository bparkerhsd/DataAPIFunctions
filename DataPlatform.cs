using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DMT.DataPlatform.DataBridge.EntityFrameworkDataClass;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace DataAPIFunctions
{
    public static class DataPlatform
    {
        [FunctionName("DataPlatformGet")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                log.Info("C# HTTP trigger function processed a request.");
                var dataPlatforms = new DataPlatforms();

                var dataPlatformList = dataPlatforms.GetAll();

                return dataPlatformList == null
                    ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                    : req.CreateResponse(HttpStatusCode.OK, dataPlatformList, new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch (Exception ex)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
