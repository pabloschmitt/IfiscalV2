namespace IFiscalV2.Services.Data
{
    using IFiscalV2.Common;
    using IFiscalV2.Models;
    using IFiscalV2.Models.Dto;
    using IFiscalV2.Services.Api;
    using IFiscalV2.Services.Auth;
    using System.Threading.Tasks;

    public class EleccionService
    {
        private AuthService authService;
        private EleccionApi eleccionApi;
        public EleccionService()
        {
            instance = this;

            authService = AuthService.GetInstance();
            eleccionApi = new EleccionApi();
        }


        #region Singlenton
        private static EleccionService instance;

        public static EleccionService GetInstance()
        {
            if (instance == null)
            {
                return new EleccionService();
            }

            return instance;
        }
        #endregion

        public async Task<Response<ServiceFindAsyncResponse<EleccionDto>>> FindAsync( 
            string SiteId, int pageIndex = 0, int pageSize = 0, string findValue = "", string ordeByTag = "", string sortDirection = "asc")
        {
            var apiResult = await eleccionApi.Get<ServiceFindAsyncResponse<EleccionDto>>(
                ApiEndPoints.API_SERVICE_URL, ApiPrefixes.API_PREFIX, "/Eleccion/Find",
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
        }

        public async Task<Response<EleccionDto>> FindByIdAsync( string id )
        {
            var apiResult = await eleccionApi.Get<EleccionDto>(
                ApiEndPoints.API_SERVICE_URL, ApiPrefixes.API_PREFIX, "/Eleccion/get",
                new
                {
                    Id = id,
                }, authService.Token.AccessToken);

            return apiResult;
        }

    }
}
