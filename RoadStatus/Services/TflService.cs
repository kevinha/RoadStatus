using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Movies.Client;
using RoadStatus.DTO;

namespace RoadStatus.Services
{
    public class TflService : ITflService
    {
        private readonly HttpClient _client;
        private readonly IApiConfig _apiConfig;

        public TflService(HttpClient client, IApiConfig apiConfig)
        {
            _client = client;
            client.BaseAddress = apiConfig.AppUrl;
            client.Timeout = new TimeSpan(0, 0, 30);
            client.DefaultRequestHeaders.Clear();
            _apiConfig = apiConfig;
        }
        
        public async Task<Result<TflApiPresentationEntitiesRoadCorridor>> GetRoad(string roadName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                    $"Road/{roadName}?app_id={_apiConfig.AppId}&app_key={_apiConfig.AppKey}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        return Fail(Errors.InvalidId);
                    
                    return Fail(Errors.GeneralApiError);
                }
                var stream = await response.Content.ReadAsStreamAsync();
                var roads = stream.ReadAndDeserializeFromJson<TflApiPresentationEntitiesRoadCorridor[]>();
                // Assuming that only one record should be returned, could also just return first one if 
                // more than one
                if (roads.Length != 1) return Fail(Errors.MultipleResults);
                return roads[0];
            }

            Result<TflApiPresentationEntitiesRoadCorridor> Fail(Errors error)
            {
                return Result<TflApiPresentationEntitiesRoadCorridor>.Failure(error);
            }
        }
    }
}