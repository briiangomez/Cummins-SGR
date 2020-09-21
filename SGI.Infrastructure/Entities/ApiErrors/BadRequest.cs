using System.Net;

namespace SGI.WebApp.ApiModels.ApiErrors
{
    public class BadRequest : ApiError
    {
        public BadRequest() : base(400, HttpStatusCode.InternalServerError.ToString())
        {
        }

        public BadRequest(string message)
            : base(400, HttpStatusCode.InternalServerError.ToString(), message)
        {
        }
    }
}
