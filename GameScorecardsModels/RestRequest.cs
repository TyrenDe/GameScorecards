using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GameScorecardsModels
{
    public class RestRequest
    {
    }

    public class RestRequest<T> : RestRequest where T : class
    {
        public T Request { get; set; }
    }
}
