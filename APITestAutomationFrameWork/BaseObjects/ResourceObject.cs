using APITestAutomationFrameWork.Model;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using System.Diagnostics;
using System.Linq;

namespace APITestAutomationFrameWork.BaseObjects
{
    public abstract class ResourceObject
    {
        protected IRestClient RestClient;
        private readonly Logger _logger;

        protected ResourceObject()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.ServicePointManager.SecurityProtocol | System.Net.SecurityProtocolType.Tls12;

            _logger = LogManager.GetCurrentClassLogger();
        }

        protected virtual IRestResponse Execute(IRestRequest request)
        {
            IRestResponse response = null;
            var timer = Stopwatch.StartNew();

            try
            {
                timer.Start();
                response = RestClient.Execute(request);
                timer.Stop();
                return response;
            }
            finally
            {
                LogRequest(request, response, timer.ElapsedMilliseconds);
            }
        }

        protected virtual IRestResponse<T> Execute<T>(IRestRequest request) where T : new()
        {
            IRestResponse<T> response = null;
            var timer = new Stopwatch();

            try
            {
                timer.Start();
                response = RestClient.Execute<T>(request);
                timer.Stop();

                return response;
            }
            catch (JsonSerializationException e)
            {
                _logger.Error($"Failed to deserialize object {typeof(T).FullName}: {response?.Content}", e);
            }
            finally
            {
                LogRequest(request, response, timer.ElapsedMilliseconds);
            }

            return default(IRestResponse<T>);
        }

        private void LogRequest(IRestRequest request, IRestResponse response, long durationInMilliseconds)
        {
            _logger.Debug(() => $"Request completed in {durationInMilliseconds} ms, " +
                               $"Request: {ConvertRestRequestToString(request)}, " +
                               $"Response: {ConvertRestResponseToJsonObject(response)}");
        }

        private string ConvertRestRequestToString(IRestRequest request)
        {
            var convertedRequest = new
            {
                uri = RestClient.BuildUri(request),
                resource = request.Resource,
                method = request.Method.ToString(),
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                })
            };

            return JsonConvert.SerializeObject(convertedRequest);
        }

        protected Catalogue ConvertRestResponseToJsonObject(IRestResponse response)
        {
            var convertedResponse = new
            {
                uri = response.ResponseUri,
                statusCode = response.StatusCode,
                headers = response.Headers,
                content = response.Content,
                errorMessage = response.ErrorMessage
            };

            return JsonConvert.DeserializeObject<Catalogue>(response.Content);

        }

        protected T ConvertRestResponseToJsonObject<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

    }
}
