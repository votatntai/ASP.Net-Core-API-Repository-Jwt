using System.Collections.Generic;

namespace JwtAuthentication.Data
{
    public class ResponsePagination<T>
    {
        public ResponsePagination(IEnumerable<T> data)
        {
            Data = data;
            Message = "Successfull";
        }
        public string Message { get; set; }
        public string Type { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
