using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GameScorecardsClient.Services
{
    public class RestResponse<TSuccess, TError>
        where TSuccess : class, new()
        where TError : class, new()
    {
        public HttpStatusCode StatusCode { get; set; }
        public TSuccess Response { get; set; }
        public TError Error { get; set; }
    }
}
