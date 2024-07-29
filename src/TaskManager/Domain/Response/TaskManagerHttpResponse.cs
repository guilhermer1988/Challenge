using System.Net;

namespace TaskManager.Domain.Response
{
    public class TaskManagerHttpResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
