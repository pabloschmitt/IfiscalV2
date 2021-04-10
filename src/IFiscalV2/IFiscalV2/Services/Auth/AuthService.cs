namespace IFiscalV2.Services.Auth
{
    using IFiscalV2.Common;
    using IFiscalV2.Extensions;
    using IFiscalV2.Models.Auth;
    using IFiscalV2.Services.Api;
    using IFiscalV2.Services.Routing;
    using IFiscalV2.ViewModels;
    using Newtonsoft.Json;
    using Splat;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class AuthService : IAuthService
    {
        private readonly IRoutingService _routingService;

        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set { isLoggedIn = value; }
        }

        private JwtAuthToken token;
        public JwtAuthToken Token
        {
            get { return token; }
            set { token = value; }
        }

        public Common_ES CES => token.CommonES;

        private AuthApi authApi;


        #region ROLES GET/SET HAS IF
        private bool hasRole_GlobalSiteAdmin;

        public bool HasRole_GlobalSiteAdmin
        {
            get { return hasRole_GlobalSiteAdmin; }
            set { hasRole_GlobalSiteAdmin = value; }
        }

        private bool hasRole_SiteAdmin;

        public bool HasRole_SiteAdmin
        {
            get { return hasRole_SiteAdmin; }
            set { hasRole_SiteAdmin = value; }
        }

        private bool hasRole_ConfigAdmin;

        public bool HasRole_ConfigAdmin
        {
            get { return hasRole_ConfigAdmin; }
            set { hasRole_ConfigAdmin = value; }
        }

        private bool hasRole_UserAdmin;

        public bool HasRole_UserAdmin
        {
            get { return hasRole_UserAdmin; }
            set { hasRole_UserAdmin = value; }
        }

        private bool hasRole_ResponsableDeEscuela;

        public bool HasRole_ResponsableDeEscuela
        {
            get { return hasRole_ResponsableDeEscuela; }
            set { hasRole_ResponsableDeEscuela = value; }
        }

        private bool hasRole_Verificador;

        public bool HasRole_Verificador
        {
            get { return hasRole_Verificador; }
            set { hasRole_Verificador = value; }
        }

        private bool hasRole_Fiscal;

        public bool HasRole_Fiscal
        {
            get { return hasRole_Fiscal; }
            set { hasRole_Fiscal = value; }
        }

        private bool hasRole_Colador;

        public bool HasRole_Colador
        {
            get { return hasRole_Colador; }
            set { hasRole_Colador = value; }
        }

        private bool hasRole_Resultados;

        public bool HasRole_Resultados
        {
            get { return hasRole_Resultados; }
            set { hasRole_Resultados = value; }
        }

        private bool hasRole_SoloVer;

        public bool HasRole_SoloVer
        {
            get { return hasRole_SoloVer; }
            set { hasRole_SoloVer = value; }
        }

        private bool hasRole_Intendente;

        public bool HasRole_Intendente
        {
            get { return hasRole_Intendente; }
            set { hasRole_Intendente = value; }
        }
        #endregion

        public AuthService(IRoutingService routingService = null)
        {
            _instance = this;

            _routingService = routingService ?? Locator.Current.GetService<IRoutingService>();

            IsLoggedIn = false;
            authApi = new AuthApi();
        }

        #region Singlenton
        private static AuthService _instance;
        public static AuthService Instance => _instance ?? new AuthService();
        #endregion

        public async Task<LoginResult> CheckPreviusLoginAsync()
        {
            if (ApplicationSettings.LastLoginResult != LoginSate.LoginOk)
                return new LoginResult
                {
                    IsSuccess = false,
                };

            var userLogin = ApplicationSettings.LastUserLogin;
            var loginResult = await LoginAsync(userLogin);

            return loginResult;
        }


        private bool LoadTokenInfo(Response<Object> loginApiResponse)
        {
            JwtAuthToken authResult = new JwtAuthToken();
            var stringToken = loginApiResponse.Result.ToString();
            var parts = stringToken.Split('.');
            var decoded = parts[1].Base64UrlDecode();
            var sdecoded = Encoding.UTF8.GetString(decoded);
            var jsonToken = JsonConvert.DeserializeObject<Dictionary<string, object>>(sdecoded);

            #region Token-Parse
            foreach (var kvp in jsonToken)
            {
                switch (kvp.Key)
                {
                    case "sub":
                        authResult.UID = kvp.Value.ToString(); break;
                    case "unique_name":
                        authResult.UserName = kvp.Value.ToString(); break;
                    case "jti":
                        authResult.Jti = kvp.Value.ToString(); break;
                    case "site":
                        authResult.SiteId = kvp.Value.ToString(); break;
                    case "ma":
                        authResult.Ma = kvp.Value.ToString(); break;
                    case "roles":
                        {
                            authResult.Roles = new List<string>();
                            if ((kvp.Value as IList) != null)
                            {
                                foreach (var rl in (IList)kvp.Value)
                                {
                                    authResult.Roles.Add(rl.ToString());
                                }
                            }
                            else
                            {
                                authResult.Roles.Add(kvp.Value.ToString());
                            }
                        }
                        break;
                    case "nbf":
                        authResult.Nbf = kvp.Value.ToString(); break;
                    case "exp":
                        authResult.Exp = kvp.Value.ToString(); break;
                    case "iss":
                        authResult.Iss = kvp.Value.ToString(); break;
                    case "aud":
                        authResult.Aud = kvp.Value.ToString(); break;
                    default:
                        break;

                } // switch (kvp.Key...

            } // foreach (var kvp...
            #endregion
            authResult.AccessToken = stringToken;
            Token = authResult;

            return true;
        }

        public async Task<LoginResult> LoginAsync(UserLogin userLogin)
        {
            StopRefreshToken();

            IsLoggedIn = false;

            ApplicationSettings.Clear();

            var loginApiResponse = await authApi.Post(ApiEndPoints.AUTH_SERVICE_URL, ApiPrefixes.AUTH_PREFIX, "/Token", userLogin);

            if (loginApiResponse.IsSuccess)
            {
                if (LoadTokenInfo(loginApiResponse) == false)
                    return new LoginResult { IsSuccess = false };

                var cesResponse = await authApi.Get<Common_ES>(
                    ApiEndPoints.API_SERVICE_URL, ApiPrefixes.API_PREFIX, "/Common/se",
                    new
                    {
                        SiteId = string.Empty,
                        EleccionIdeleccionId = string.Empty,
                    },
                    Token.AccessToken);

                if (!cesResponse.IsSuccess)
                {
                    ApplicationSettings.Clear();
                    return new LoginResult { IsSuccess = false };
                }

                Token.CommonES = cesResponse.Result;

                // Instanciar el los settings los datos del login
                ApplicationSettings.LastUserLogin = userLogin;
                ApplicationSettings.LastAccessToken = Token.AccessToken;

                // Si Esta en login OK y es renew se actualiza el token
                ApplicationSettings.InGlobal = Token.CommonES.InGlobal;
                ApplicationSettings.InEleccion = Token.CommonES.InEleccion;
                ApplicationSettings.SelectedSiteId = Token.CommonES.SiteId;
                ApplicationSettings.SelectedSiteName = Token.CommonES.SiteName;
                ApplicationSettings.SelectedEleccionId = Token.CommonES.EleccionId;
                ApplicationSettings.SelectedEleccionName = Token.CommonES.EleccionName;
                ApplicationSettings.LastLoginResult = LoginSate.LoginOk;


                // Build Roles List
                hasRole_GlobalSiteAdmin = Token.Roles.Any(w => w.Equals(AvariableRoles.GlobalSiteAdmin));
                hasRole_SiteAdmin = Token.Roles.Any(w => w.Equals(AvariableRoles.SiteAdmin));
                hasRole_UserAdmin = Token.Roles.Any(w => w.Equals(AvariableRoles.UserAdmin));
                hasRole_ResponsableDeEscuela = Token.Roles.Any(w => w.Equals(AvariableRoles.ResponsableDeEscuela));
                hasRole_Verificador = Token.Roles.Any(w => w.Equals(AvariableRoles.Verificador));
                hasRole_Fiscal = Token.Roles.Any(w => w.Equals(AvariableRoles.Fiscal));
                hasRole_Colador = Token.Roles.Any(w => w.Equals(AvariableRoles.Colador));
                hasRole_Resultados = Token.Roles.Any(w => w.Equals(AvariableRoles.Resultados));
                hasRole_SoloVer = Token.Roles.Any(w => w.Equals(AvariableRoles.SoloVer));
                hasRole_Intendente = Token.Roles.Any(w => w.Equals(AvariableRoles.Intendente));

                StartRefreshToken();

                IsLoggedIn = true;
                return new LoginResult { IsSuccess = true };

            } // loginApiResponse.IsSuccess

            ApplicationSettings.Clear();

            return new LoginResult { IsSuccess = false };

            //    if (ApplicationSettings.LastLoginStep == LoginStep.LoginOk && isRenew)
            //    {
            //        inGlobal = ApplicationSettings.InGlobal;
            //        inEleccion = ApplicationSettings.InEleccion;

            //        selectedSiteId = ApplicationSettings.SelectedSiteId;
            //        selectedSiteName = ApplicationSettings.SelectedSiteName;
            //        selectedEleccionId = ApplicationSettings.SelectedEleccionId;
            //        selectedEleccionName = ApplicationSettings.SelectedEleccionName;
            //    }

            //    if (inGlobal)
            //    {
            //        ApplicationSettings.LastLoginStep = LoginStep.SelectSite;
            //        loginResult.LoginStepTo = LoginStep.SelectSite;
            //    }
            //    else if (!inEleccion)
            //    {
            //        ApplicationSettings.LastLoginStep = LoginStep.SelectEleccion;
            //        loginResult.LoginStepTo = LoginStep.SelectEleccion;
            //    }
            //    else
            //    {
            //        // Okey, esta dentro de una eleccion
            //        ApplicationSettings.LastLoginStep = LoginStep.LoginOk;
            //        ApplicationSettings.InEleccion = true;
            //        ApplicationSettings.InGlobal = false;

            //        loginResult.LoginStepTo = LoginStep.LoginOk;
            //    }

            //    ApplicationSettings.SelectedSiteId = selectedSiteId;
            //    ApplicationSettings.SelectedSiteName = selectedSiteName;
            //    ApplicationSettings.SelectedEleccionId = selectedEleccionId;
            //    ApplicationSettings.SelectedEleccionName = selectedEleccionName;

            //    IsLoggedIn = true;

            //    StartRefreshToken();

            //    return loginResult;
            //}



        } //LoginAsync

        public Task LogoutAsync()
        {
            StopRefreshToken();

            ApplicationSettings.Clear();
            Token = new JwtAuthToken();
            IsLoggedIn = false;

            return Task.CompletedTask;
        }


        #region RefreshTokens

        private TimerTask refreshTokenTask;

        public void StartRefreshToken()
        {
            if (refreshTokenTask != null)
            {
                refreshTokenTask.Stop();
            }

            TimeSpan ts = (Token.GetExpiration() - DateTime.Now);
            int seconds = ((int)ts.TotalSeconds) - 300;

            refreshTokenTask = new TimerTask(new TimeSpan(0, 0, seconds), async () => await PerformRefreshToken(), TimerTaskExecution.Single);
            refreshTokenTask.Start();
        }
        public void StopRefreshToken()
        {
            if (refreshTokenTask != null)
                refreshTokenTask.Stop();
        }

        private async Task PerformRefreshToken()
        {
            StopRefreshToken();

            var loginApiResponse = await authApi.Post(ApiEndPoints.AUTH_SERVICE_URL, ApiPrefixes.AUTH_PREFIX, "/refreshtoken",
                new
                {
                    RefreshToken = "b0000_0000_0000_0001",
                }, Token.AccessToken);

            if (!loginApiResponse.IsSuccess || LoadTokenInfo(loginApiResponse) == false)
            {
                await LogoutAsync();

                IsLoggedIn = false;

                await Application.Current.MainPage.DisplayAlert(
                    "ATENCION !",
                    "Sus Credenciales han caducado, por favor inicie sesión nuevamente",
                    "Aceptar");

                // Ahora fuerza la salida de la aplicación al login
                await _routingService.NavigateToAsync("//main/login");

            }
            else
            {
                ApplicationSettings.LastAccessToken = Token.AccessToken;
            }

            // Start new Token refresh
            StartRefreshToken();

        } //PerformRefreshToken

        #endregion



    }
}
