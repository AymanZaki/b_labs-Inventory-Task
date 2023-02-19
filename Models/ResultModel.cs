using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ResultModel<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ResultModel()
        {

        }
        public ResultModel(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public ResultModel(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
        public ResultModel(HttpStatusCode statusCode, T data, string message)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
        }
        public ResultModel(HttpStatusCode statusCode, T data)
        {
            Data = data;
            StatusCode = statusCode;
        }
    }
}
