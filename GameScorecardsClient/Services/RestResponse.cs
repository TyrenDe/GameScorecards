using System.Net;

namespace GameScorecardsClient.Services
{
    public class RestResponse<TSuccess, TError>
    {
        public HttpStatusCode StatusCode { get; set; }
        public TSuccess Response { get; set; }
        public TError Error { get; set; }
    }
}
