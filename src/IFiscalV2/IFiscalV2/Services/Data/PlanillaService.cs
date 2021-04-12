namespace IFiscalV2.Services.Data
{
    using IFiscalV2.Common;
    using IFiscalV2.Models.Dto;
    using IFiscalV2.Services.Api;
    using IFiscalV2.Services.Auth;
    using System.Threading.Tasks;


    public class PlanillaService
    {
        private PlanillaApi planillasApi;
        private AuthService authService;
        
        #region Constructors
        public PlanillaService()
        {
            instance = this;

            authService = AuthService.Instance;
            planillasApi = new PlanillaApi();
        } 
        #endregion

        #region Singlenton
        private static PlanillaService instance;

        public static PlanillaService GetInstance()
        {
            if (instance == null)
            {
                return new PlanillaService();
            }

            return instance;
        }
        #endregion

        public async Task<Response<UIPlanillaTemplate>> GetUIPlanillaAsync(int mesaNro)
        {
            var eid = ApplicationSettings.SelectedEleccionId;
            if (string.IsNullOrEmpty(eid))
                return new Response<UIPlanillaTemplate>
                {
                    IsSuccess = false,
                    Message = "No se especifico el eid",
                    Result = null,
                };

            if (mesaNro <= 0 )
                return new Response<UIPlanillaTemplate>
                {
                    IsSuccess = false,
                    Message = $"el numero de mesa {mesaNro} no es Valido",
                    Result = null,
                };


            var apiResult = await planillasApi.Get<UIPlanillaTemplate>(
                ApiEndPoints.API_SERVICE_URL, ApiPrefixes.API_PREFIX, "/planilla/planilla_ui", 
                new
                {
                    EleccionId = ApplicationSettings.SelectedEleccionId,
                    MesaNro = mesaNro,
                }, authService.Token.AccessToken);

            return apiResult;

        } // GetUIPlanillaAsync(int mesaNro)

        public async Task<Response<object>> PostPlanillaAsync(CertPostModel dataToPost)
        {
            var apiResult = await planillasApi.Post(ApiEndPoints.API_SERVICE_URL, ApiPrefixes.API_PREFIX, "/planilla/post",
                dataToPost,
                authService.Token.AccessToken);

            if (!apiResult.IsSuccess)
                return new Response<object> { IsSuccess = false, Message = "Error de conectividad !" };

            return apiResult;

        } // PostPlanillaAsync


    } // class PlanillasService

}
