using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GameScorecardsModels
{
    public class RestRequest
    {
        public string RequestId { get; set; }
    }

    public class RestRequest<T> : RestRequest where T : class
    {
        public T Request { get; set; }
    }
}
