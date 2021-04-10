namespace IFiscalV2.Models
{
    using System.Collections.Generic;

    public class ServiceFindAsyncResponse<TResult>
        where TResult : class
    {
        public int Count { get; set; }
        public List<TResult> Data { get; set; }
    }
}
