using System;
using System.Net;
using System.Runtime.Serialization;

namespace Chess_GEMA.Controllers
{
    /*
     * Klasse fuer eigene Exceptionmethode
     */
    [Serializable]
    internal class HttpResponseException : Exception
    {
        public HttpResponseException()
        {
        }

        public HttpResponseException(HttpStatusCode message)
        {
        }

        public HttpResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}