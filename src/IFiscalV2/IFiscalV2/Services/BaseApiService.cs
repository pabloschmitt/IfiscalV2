namespace IFiscalV2.Services
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using IFiscalV2.Common;
    using Plugin.Connectivity;
    using ModernHttpClient;
    using System.Collections.Generic;

    public class BaseApiService : BaseApiService<object> { }

    public class BaseApiService<TResult>
        where TResult: class
    {

        public async Task<Response<TResult>> CheckConnection()
        {
            
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response<TResult>
                {
                    IsSuccess = false,
                    Message = "No hay Conexion de Datos",
                };
            }

            await Task.Delay(1);
            //var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            //if (!isReachable)
            //{
            //    return new Response<TResult>
            //    {
            //        IsSuccess = false,
            //        Message = "No hay Acceso a Internet",
            //    };
            //}

            return new Response<TResult>
            {
                IsSuccess = true,
            };
        }

        public async Task<Response<TObjectResult>> Get<TObjectResult>(string urlBase, string prefix, string methodRoute, object model = null, string accessToken = null)
            where TObjectResult: class
        {
            var connection = await CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", connection.Message, "Acepotar");
                return new Response<TObjectResult>
                {
                     IsSuccess = false,
                     Message = "No hay Conectividad !",
                };
            }

            try
            {
                var client = new HttpClient(
                    new NativeMessageHandler(false, new TLSConfig()
                    {
                        DangerousAcceptAnyServerCertificateValidator = true,
                        Pins = new List<Pin> { },
                    })
                );

                client.Timeout = TimeSpan.FromSeconds(30);

                client.BaseAddress = new Uri(urlBase);
                if (!string.IsNullOrEmpty(accessToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                var url = $"{prefix}{methodRoute}".AttachParameters(model);

                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response<TObjectResult>
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var js = new JsonSerializerSettings
                {
                    Error = HandleDeserializationError,
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                var result = JsonConvert.DeserializeObject<TObjectResult>(answer, js);
                return new Response<TObjectResult>
                {
                    IsSuccess = true,
                    Result = result,
                };
            }
            catch
            {
                return null;
            }
        }

        public static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }

        public async Task<Response<TResult>> Post<TModel>(string urlBase, string prefix, string methodRoute, TModel model, string accessToken = null)
            where TModel: class
        {
            var connection = await CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", connection.Message, "Acepotar");
                return new Response<TResult>
                {
                     IsSuccess = false,
                     Message = "No hay Conectividad !",
                };
            }

            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient(
                    new NativeMessageHandler(false, new TLSConfig()
                    {
                        DangerousAcceptAnyServerCertificateValidator = true,
                        Pins = new List<Pin> { },
                    })
                );

                client.Timeout = TimeSpan.FromSeconds(30);

                client.BaseAddress = new Uri(urlBase);
                if ( !string.IsNullOrEmpty(accessToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var url = $"{prefix}{methodRoute}";
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response<TResult>
                    {
                        IsSuccess = false,
                        StatusCode = response.StatusCode,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<TResult>(answer);
                return new Response<TResult>
                {
                    IsSuccess = true,
                    StatusCode = response.StatusCode,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response<TResult>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
