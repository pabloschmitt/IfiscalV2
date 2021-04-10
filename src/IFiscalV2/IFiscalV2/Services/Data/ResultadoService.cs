namespace IFiscalV2.Services.Data
{
    using IFiscalV2.Common;
    using IFiscalV2.Models.Dto;
    using IFiscalV2.Services.Api;
    using IFiscalV2.Services.Auth;
    using System.Threading.Tasks;

    public class ResultadoService
    {
        private AuthService authService;
        private ResultadoApi resultadoApi;
        public ResultadoService()
        {
            instance = this;

            authService = AuthService.GetInstance();
            resultadoApi = new ResultadoApi();
        }


        #region Singlenton
        private static ResultadoService instance;

        public static ResultadoService GetInstance()
        {
            if (instance == null)
            {
                return new ResultadoService();
            }

            return instance;
        }
        #endregion


        public async Task<Response<ResultadoABCDto>> LoadAsync( string tipoCalculo = CalculoTipoResultado.LISTA, int nroCargo = 0, bool siTomarBlancosComoValidos = false,
            string circuitoId = "", string escuelaId = "", string localidadId = "" )
        {
            return await LoadAsync(
                ApplicationSettings.SelectedEleccionId,
                tipoCalculo,
                nroCargo,
                siTomarBlancosComoValidos,
                circuitoId,
                escuelaId,
                localidadId,
                string.Empty );

        } // LoadAsync

        public async Task<Response<ResultadoABCDto>> LoadAsync(
            string eleccionId, string tipoCalculo = CalculoTipoResultado.LISTA, int nroCargo = 0, bool siTomarBlancosComoValidos = false, 
            string circuitoId = "" , string escuelaId = "", string localidadId = "", string partidoId = "" )
        {
            var apiResult = await resultadoApi.Get<ResultadoABCDto>(
                ApiEndPoints.API_SERVICE_URL, ApiPrefixes.API_PREFIX, "/Calculo/resultados",
                new
                {
                    EleccionId = eleccionId,
                    TipoCalculo = tipoCalculo,
                    NroCargo = nroCargo,
                    SiTomarBlancosComoValidos = siTomarBlancosComoValidos,
                    CircuitoId = circuitoId,
                    EscuelaId = escuelaId,
                    LocalidadId = localidadId,
                    PartidoId = partidoId,

                }, authService.Token.AccessToken);

            return apiResult;

        } // LoadAsync


    }
}
