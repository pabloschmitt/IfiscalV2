namespace IFiscalV2.Services.Data
{
    using IFiscalV2.Common;
    using IFiscalV2.Models;
    using IFiscalV2.Services.Api;
    using IFiscalV2.Services.Auth;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    //TODO CommonService
    public class CommonService
    {

        private AuthService authService;
        private CommonApi commonApi;
        public CommonService()
        {
            instance = this;

            authService = AuthService.GetInstance();
            commonApi = new CommonApi();
        }


        #region Singlenton
        private static CommonService instance;

        public static CommonService GetInstance()
        {
            if (instance == null)
            {
                return new CommonService();
            }

            return instance;
        }
        #endregion

        public async Task<Response<List<AutocompleteModelItem>>> GetAutocompleteFiltersAsync(
            bool siMesas = false, bool siLocalidades = true, bool siCircuitos = true, bool siEscuelas = true,
            bool siPartidos = true, bool siListas = false, bool siCargos = false
            )
        {
            return  await GetAutocompleteFiltersAsync(
                ApplicationSettings.SelectedSiteId, ApplicationSettings.SelectedEleccionId,
                siMesas, siLocalidades, siCircuitos, siEscuelas, siPartidos, siListas, siCargos);
        } //FindAsync

        public async Task<Response<List<AutocompleteModelItem>>> GetAutocompleteFiltersAsync(
            string SiteId, string eleccionId, 
            bool siMesas = false, bool siLocalidades = false, bool siCircuitos = false, bool siEscuelas = false,
            bool siPartidos = false, bool siListas = false, bool siCargos = false )
        {
            var apiResult = await commonApi.Get<List<AutocompleteModelItem>>(
                ApiEndPoints.API_SERVICE_URL, ApiPrefixes.API_PREFIX, "/Common/autocomplete_filters",
                new
                {
                    SiteId = SiteId,
                    EleccionId = eleccionId,
                    SiMesas = siMesas,
                    SiLocalidades = siLocalidades,
                    SiCircuitos = siCircuitos,
                    SiEscuelas = siEscuelas,
                    SiPartidos = siPartidos,
                    SiListas = siListas,
                    SiCargos = siCargos,
                }, authService.Token.AccessToken);

            return apiResult;
        } //FindAsync

    }

}
