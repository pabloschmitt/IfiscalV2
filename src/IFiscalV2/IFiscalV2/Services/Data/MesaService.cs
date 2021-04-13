namespace IFiscalV2.Services.Data
{
    using IFiscalV2.Common;
    using IFiscalV2.Models;
    using IFiscalV2.Models.Dto;
    using IFiscalV2.Services.Api;
    using IFiscalV2.Services.Auth;
    using System.Threading.Tasks;

    public class MesaService
    {
        private MesaApi mesasApi;
        private AuthService authService;

        public MesaService()
        {
            _instance = this;

            authService = AuthService.Instance;
            mesasApi = new MesaApi();
        }

        #region Singlenton
        private static MesaService _instance;
        public static MesaService Instance => _instance ?? new MesaService();
        #endregion

        public async Task<Response<MesaResponseModel>> FindMesaByNroAsync(int mesaNro)
        {
            var apiResult = await FindMesaByNroAsync(ApplicationSettings.SelectedSiteId, ApplicationSettings.SelectedEleccionId, mesaNro);
            return apiResult;
        }

        public async Task<Response<MesaResponseModel>> FindMesaByNroAsync(string siteId, string eleccionId, int mesaNro)
        {
            var apiResult = await mesasApi.Get<MesaResponseModel>(
                ApiEndPoints.API_SERVICE_URL, ApiPrefixes.API_PREFIX, "/Mesa/by_nro",
                new
                {
                    SiteId = siteId,
                    EleccionId = eleccionId,
                    MesaNro = mesaNro,
                }, authService.Token.AccessToken);

            return apiResult;
        }

        public async Task<Response<ServiceFindAsyncResponse<MesaResponseModel>>> FindMesasAsync()
        {
            var result = await FindMesasAsync(ApplicationSettings.SelectedSiteId, ApplicationSettings.SelectedEleccionId);
            return result;
        }
        
        public async Task<Response<ServiceFindAsyncResponse<MesaResponseModel>>> FindMesasAsync(
            string siteId, string eleccionId, string escuelaId = "", int pageIndex = 0, int pageSize = 0, string filterTarget = "",
            string findValue = "", string ordeByTag = "nro_mesa", string sortDirection = "asc")
        {
            var apiResult = await mesasApi.Get<ServiceFindAsyncResponse<MesaResponseModel>>( 
                ApiEndPoints.API_SERVICE_URL, ApiPrefixes.API_PREFIX, "/Mesa/Find",
                new
                {
                    SiteId = siteId,
                    EleccionId = eleccionId,
                    EscuelaId = escuelaId,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    FilterTarget = filterTarget,
                    FindValue = findValue,
                    OrdeByTag = ordeByTag,
                    SortDirection = sortDirection,
                }, authService.Token.AccessToken);

            return apiResult;

        }
    }
}
