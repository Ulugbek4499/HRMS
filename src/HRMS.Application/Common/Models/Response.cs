using System.Net;

namespace HRMS.Application.Common.Models
{
    public class Response<T>
    {
        public HttpStatusCode HttpStatus { get; set; }
        public T Result { get; set; }
        public object Errors { get; set; }

        public Response()
        {

        }

        public Response(T result)
        {
            Result = result;
        }

        public Response(T result, object errors)
        {
            Result = result;
            Errors = errors;
        }

        public Response(T result, object errors, HttpStatusCode statusCode)
        {
            Result = result;
            Errors = errors;
            HttpStatus = statusCode;
        }
    }
}
