using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoadStatus.Tests
{
    public class Return200WithContentResponseHandler : HttpMessageHandler
    {
        private readonly string _fileName;

        public Return200WithContentResponseHandler(string fileName)
        {
            _fileName = fileName;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            using (Stream stream = typeof(Return200WithContentResponseHandler).Assembly
                .GetManifestResourceStream($"RoadStatus.Tests.{_fileName}"))
            using (StreamReader reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                response.Content = new StringContent(content, Encoding.Default, "application/json");
            }
            
            return Task.FromResult(response);
        }
    }
}