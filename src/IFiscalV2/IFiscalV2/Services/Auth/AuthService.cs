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

        public AuthService(IRoutingService routingService = null)
        {
            instance = this;

            _routingService = routingService ?? Locator.Current.GetService<IRoutingService>();

            IsLoggedIn = false;
            authApi = new AuthApi();
        }

        #region Singlenton
        private static AuthService instance;

        public static AuthService GetInstance()
        {
            if (instance == null)
            {
                return new AuthService();
            }

            return instance;
        }
        #endregion

        public async Task<LoginResult> CheckPreviusLoginAsync()
        {
            if (ApplicationSettings.LastLoginStep != LoginStep.LoginOk)
                return new LoginResult
                {
                    IsSuccess = false,
                };

            var userLogin = ApplicationSettings.LastUserLogin;
            var loginResult = await LoginAsync(userLogin, true);

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

        public async Task<LoginResult> LoginAsync(UserLogin userLogin, bool isRenew = false)
        {
            await Task.Delay(100);
            return new LoginResult { IsSuccess = true };

            //if (!isRenew)
            //    ApplicationSettings.Clear();

            //var loginApiResponse = await authApi.Post(ApiEndPoints.AUTH_SERVICE_URL, ApiPrefixes.AUTH_PREFIX, "/Token", userLogin);
            //if (loginApiResponse.IsSuccess)
            //{

            //    if (LoadTokenInfo(loginApiResponse) == false)
            //    {
            //        return new LoginResult { IsSuccess = false };
            //    }

            //    var cesResponse = await authApi.Get<Common_ES>(
            //    ApiEndPoints.API_SERVICE_URL, ApiPrefixes.API_PREFIX, "/Common/se",
            //    new
            //    {
            //        SiteId = string.Empty,
            //        EleccionIdeleccionId = string.Empty,
            //    },
            //    Token.AccessToken);

            //    if (!cesResponse.IsSuccess)
            //    {
            //        ApplicationSettings.Clear();
            //        return new LoginResult { IsSuccess = false };
            //    }

            //    Token.CommonES = cesResponse.Result;

            //    // Arma login Result
            //    var loginResult = new LoginResult();

            //    loginResult.IsSuccess = true;

            //    // Instanciar el los settings los datos del login
            //    ApplicationSettings.LastUserLogin = userLogin;
            //    ApplicationSettings.LastAccessToken = Token.AccessToken;

            //    var inGlobal = Token.CommonES.InGlobal;
            //    var inEleccion = Token.CommonES.InEleccion;

            //    var selectedSiteId = Token.CommonES.SiteId;
            //    var selectedSiteName = Token.CommonES.SiteName;
            //    var selectedEleccionId = Token.CommonES.EleccionId;
            //    var selectedEleccionName = Token.CommonES.EleccionName;


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

            //ApplicationSettings.Clear();
            //IsLoggedIn = false;
            //return new LoginResult
            //{
            //    IsSuccess = false,
            //};

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
                await Application.Current.MainPage.DisplayAlert(
                    "ATENCION !",
                    "Sus Credenciales han caducado, por favor inicie sesión nuevamente",
                    "Aceptar");

                // Ahora fuerza la salida de la aplicación al login
                await _routingService.NavigateToAsync("//main/login");
                //MainViewModel.GetInstance().SetMainPageFrom(new LoginResult { IsSuccess = false });
            }

            // Start new Token refresh
            StartRefreshToken();

        } //PerformRefreshToken

        #endregion



    }
}
