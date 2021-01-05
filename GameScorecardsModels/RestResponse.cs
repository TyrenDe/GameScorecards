using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GameScorecardsModels
{
    public class RestResponse
    {
        public string RequestId { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode => (int)StatusCode >= 200 && (int)StatusCode < 300;
        public string Message { get; set; }
    }

    public class RestResponse<T> : RestResponse
    {
        public T Result { get; set; }
    }
}
