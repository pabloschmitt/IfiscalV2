namespace IFiscalV2.Common
{
    public struct ApiEndPoints
    {
#if DEBUG
        public const string AUTH_SERVICE_URL = @"https://10.10.3.210:4601";
        public const string API_SERVICE_URL = @"https://10.10.3.210:4603";

#else
        public const string AUTH_SERVICE_URL = @"http://xyz.aplicacioneshoy.com:4600";
        public const string API_SERVICE_URL = @"http://xyz.aplicacioneshoy.com:4602";

        // Para Apple Play Store
        //public const string AUTH_SERVICE_URL = @"http://test.aplicacioneshoy.com:4600";
        //public const string API_SERVICE_URL = @"http://test.aplicacioneshoy.com:4602";

#endif
    }

    public struct ApiPrefixes
    {
        public const string AUTH_PREFIX = @"/User";
        public const string API_PREFIX = @"/api";
    }


    public struct AdditionalClaims
    {
        public const string SiteClaim = "site";
        public const string MesasAsingedClaim = "ma";
    }

    public struct AvariableRoles
    {
        public const string GlobalSiteAdmin = "GLO_ADMIN";
        public const string SiteAdmin = "SITE_ADMIN";
        public const string ConfigAdmin = "CFG_ADMIN";
        public const string UserAdmin = "USR_ADMIN";
        public const string ResponsableDeEscuela = "RESP_ESC";
        public const string Verificador = "VERIFICADOR";
        public const string Fiscal = "FISCAL";
        public const string Colador = "COLADOR";
        public const string Resultados = "RESULTADOS";
        public const string SoloVer = "SOLO_VER";
        public const string Intendente = "INTENDENTE";
    }


    public struct ConstantesElecciones
    {
        public const string PARTIDO_SISTEMA_ID = "10000000-0000-0000-0000-000000000001";

        public struct ListasDeSistema
        {
            public const string BLANCOS_ID = "00000000-0000-0000-0000-000000010010";
            public const string BLANCOS_NRO = "90010";

            public const string NULOS_ID = "00000000-0000-0000-0000-000000010020";
            public const string NULOS_NRO = "90020";

            public const string RECURRIDOS_ID = "00000000-0000-0000-0000-000000010030";
            public const string RECURRIDOS_NRO = "90030";

            public const string IMPUGNADOS_ID = "00000000-0000-0000-0000-000000010040";
            public const string IMPUGNADOS_NRO = "90040";

            public const string BOLSIN_ID = "00000000-0000-0000-0000-000000010050";
            public const string BOLSIN_NRO = "90050";

            public const string TOTALES_ID = "00000000-0000-0000-0000-000000010090";
            public const string TOTALES_NRO = "90090";
        }

        public struct ListasDeSistemaDefaultOrder
        {
            public const int BLANCOS = 100;
            public const int NULOS = 120;
            public const int RECURRIDOS = 140;
            public const int IMPUGNADOS = 160;
            public const int BOLSIN = 180;
            public const int TOTALES = 200;
        }

        public struct TipoDeCociente
        {
            public const string GENERAL = "G";
            public const string PARTIDO = "P";
            public const string NO = "N";
        }

        public struct RamaDeMesa
        {
            public const string FEMENINA = "F";
            public const string MASCULINA = "M";
            public const string MIXTA = "X";
        }

        public struct TipoDeCargo
        {
            public const int NACIONAL = 10;
            public const int PROVINCIAL = 20;
            public const int DISTRITAL = 30;
            public const int CONCEJO = 40;
        }

    }


}
