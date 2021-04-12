namespace IFiscalV2.Services.Data
{
    using IFiscalV2.Common;
    using IFiscalV2.Models;
    using IFiscalV2.Models.Dto;
    using IFiscalV2.Services.Api;
    using IFiscalV2.Services.Auth;
    using System.Threading.Tasks;

    public class CircuitoService
    {
        private AuthService authService;
        private CircuitoApi circuitoApi;
        public CircuitoService()
        {
            instance = this;

            authService = AuthService.Instance;
            circuitoApi = new CircuitoApi();
        }


        #region Singlenton
        private static CircuitoService instance;

        public static CircuitoService GetInstance()
        {
            if (instance == null)
            {
                return new CircuitoService();
            }

            return instance;
        }
        #endregion

        public async Task<Response<ServiceFindAsyncResponse<CircuitoDto>>> FindAsync(
            string SiteId, int pageIndex = 0, int pageSize = 0, string findValue = "", string ordeByTag = "", string sortDirection = "asc")
        {
            var apiResult = await circuitoApi.Get<ServiceFindAsyncResponse<CircuitoDto>>(
                ApiEndPoints.API_SERVICE_URL, ApiPrefixes.API_PREFIX, "/Circuito/Find",
                new
                {
                    SiteId = SiteId,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    FindValue = findValue,
                    OrdeByTag = ordeByTag,
                    SortDirection = sortDirection,
                }, authService.Token.AccessToken);

            return apiResult;
        } //FindAsync

    }
}
