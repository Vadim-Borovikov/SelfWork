using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace SelfWork
{
    [SuppressMessage("ReSharper", "MemberCanBeInternal")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class RestHelper
    {
        public static TResult CallGetMethod<TResult>(string apiProvider, string method,
            Dictionary<string, object> parameters)
        {
            IRestResponse response = CallGetMethod(apiProvider, method, parameters);

            return JsonConvert.DeserializeObject<TResult>(response.Content);
        }

        public static async Task<TResult> CallPostMethodAsync<TResult>(string apiProvider, string method, object dto,
            JsonSerializerSettings settings, string token = null)
        {
            string json = JsonConvert.SerializeObject(dto, settings);

            IRestResponse response = await CallPostMethodAsync(apiProvider, method, json, token);

            return JsonConvert.DeserializeObject<TResult>(response.Content, settings);
        }

        private static IRestResponse CallGetMethod(string apiProvider, string method,
            Dictionary<string, object> parameters)
        {
            var client = new RestClient($"{apiProvider}");
            var request = new RestRequest(method, Method.GET);
            foreach (string key in parameters.Keys)
            {
                request.AddParameter(key, parameters[key]);
            }

            return client.Execute(request);
        }

        private static Task<IRestResponse> CallPostMethodAsync(string apiProvider, string method, string json, string token)
        {
            var client = new RestClient($"{apiProvider}{method}");
            var request = new RestRequest { Method = Method.POST };

            if (!string.IsNullOrEmpty(token))
            {
                request.AddHeader("Authorization", $"Bearer {token}");
            }
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            return client.ExecuteAsync(request);
        }
    }
}
