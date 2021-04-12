namespace IFiscalV2.Services.Data
{
    using IFiscalV2.Common;
    using IFiscalV2.Models;
    using IFiscalV2.Models.Dto;
    using IFiscalV2.Services.Api;
    using IFiscalV2.Services.Auth;
    using System.Threading.Tasks;


    public class SiteService
    {
        private AuthService authService;
        private SiteApi siteApi;
        public SiteService()
        {
            _instance = this;

            authService = AuthService.Instance;
            siteApi = new SiteApi();
        }


        #region Singleton
        private static SiteService _instance;
        public static SiteService Instance => _instance is null ? new SiteService() : _instance;
        #endregion


        public async Task<Response<ServiceFindAsyncResponse<SiteDto>>> FindSitesAsync(
            int pageIndex = 0, int pageSize = 0, 
            string findValue = "", string ordeByTag = "", string sortDirection = "asc")
        {
            var apiResult = await siteApi.Get<ServiceFindAsyncResponse<SiteDto>>(
                ApiEndPoints.API_SERVICE_URL, ApiPrefixes.API_PREFIX, "/site/find",
                new
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    FindValue = findValue,
                    OrdeByTag = ordeByTag,
                    SortDirection = sortDirection,
                }, authService.Token.AccessToken);

            return apiResult;
        }


    }
}
