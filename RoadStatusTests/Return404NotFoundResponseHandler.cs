using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RoadStatus.Tests
{
    public class Return404NotFoundResponseHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            return Task.FromResult(response);
        }
    }
}