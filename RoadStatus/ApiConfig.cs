namespace RoadStatus
{
    public class ApiConfig : IApiConfig
    {
        public string AppId { get; }
        public string AppKey { get; }

        public ApiConfig(string appId, string appKey)
        {
            AppId = appId;
            AppKey = appKey;
        }
    }

    public interface IApiConfig
    {
        string AppId { get; }
        string AppKey { get; }
    }
}