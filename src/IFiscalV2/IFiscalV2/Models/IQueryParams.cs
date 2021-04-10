namespace IFiscalV2.Models
{
    public interface IQueryParams
    {
        int PageIndex { get; }
        int PageSize { get; }
        string FindValue { get; }
        string OrdeByTag { get; }
        string SortDirection { get; }
    }

    public class QueryParams : IQueryParams
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string FindValue { get; set; }
        public string OrdeByTag { get; set; }
        public string SortDirection { get; set; }
    }
}
