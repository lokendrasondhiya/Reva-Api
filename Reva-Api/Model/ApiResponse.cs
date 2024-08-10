using System.Net;

namespace Reva_Api.Model
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public Boolean IsSuccess { get; set; }
        public List<string>? Message { get; set; }
        public object? Result { get; set; }

    }
}
