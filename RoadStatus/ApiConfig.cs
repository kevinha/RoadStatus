using System;

namespace RoadStatus
{
    public class ApiConfig : IApiConfig
    {
        public Uri AppUrl { get; }
        public string AppId { get; }
        public string AppKey { get; }

        public ApiConfig(string appUrl, string appId, string appKey)
        {
            AppUrl = new Uri(appUrl);
            if (string.IsNullOrWhiteSpace(appId)) throw new ArgumentException($"Invalid value {appId} for {nameof(appId)}");
            AppId = appId;
            if (string.IsNullOrWhiteSpace(appKey)) throw new ArgumentException($"Invalid value {appKey} for {nameof(appKey)}");
            AppKey = appKey;
        }
    }

    public interface IApiConfig
    {
        Uri AppUrl { get; }
        string AppId { get; }
        string AppKey { get; }
    }
}