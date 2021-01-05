using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GameScorecardsAPI.Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public HttpStatusCodeException(HttpStatusCode statusCode) : this(statusCode, null)
        {
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    public class HttpBadRequestException : HttpStatusCodeException
    {
        public HttpBadRequestException() : this(null)
        {
        }

        public HttpBadRequestException(string message) : base(HttpStatusCode.BadRequest, message)
        {
        }
    }

    public class HttpUnauthorizedException : HttpStatusCodeException
    {
        public HttpUnauthorizedException() : this(null)
        {
        }

        public HttpUnauthorizedException(string message) : base(HttpStatusCode.Unauthorized, message)
        {
        }
    }
}
