namespace IFiscalV2.Common
{
    using System.Net;


    public class Response : Response<object> { }

    public class Response<TResult>
        where TResult : class
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public TResult Result { get; set; }
    }
}
