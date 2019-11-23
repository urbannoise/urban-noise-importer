using RestSharp;
using System.Threading.Tasks;

namespace UrbanNoise.Importer.Components.Infrastructure.Clients
{
    public class BaseClient : RestClient
    {
        private bool IsTimeoutResponse(IRestRequest request, IRestResponse response)
        {
            if (response.StatusCode == 0)
            {
                return true;
            }
            return false;
        }

        public override async Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            var response = await base.ExecuteTaskAsync<T>(request);
            if (IsTimeoutResponse(request, response))
            {
                return null;
            }
            return response;
        }

        public async Task<T> GetAsync<T>(IRestRequest request) where T : new()
        {
            var response = await ExecuteTaskAsync<T>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                return default;
            }
        }
    }
}
