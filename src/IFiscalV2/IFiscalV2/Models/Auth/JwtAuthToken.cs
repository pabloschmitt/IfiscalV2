namespace IFiscalV2.Models.Auth
{
    
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using IFiscalV2.Extensions;
    using Newtonsoft.Json;
   

    public class JwtAuthToken
    {
        //(Subject) Claim
        [JsonProperty("sub")]
        public string UID { get; set; }

        //Unique Name or User Name Claim
        [JsonProperty("unique_name")]

        public string UserName { get; set; }
        //(JWT ID) Claim
        [JsonProperty("jti")]
        public string Jti { get; set; }

        //(Issued At) Claim
        [JsonProperty("iat")]
        public string Iat { get; set; }

        //Site of the user
        [JsonProperty("site")]
        public string SiteId { get; set; }

        [JsonProperty("ma")]
        public string Ma { get; set; }

        //Roles
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }

        //(Not Before) Claim
        [JsonProperty("nbf")]
        public string Nbf { get; set; }

        //(Expiration Time) Claim
        [JsonProperty("exp")]
        public string Exp { get; set; }

        //"iss" (Issuer) Claim
        [JsonProperty("iss")]
        public string Iss { get; set; }

        //(Audience) Claim
        [JsonProperty("aud")]
        public string Aud { get; set; }

        public DateTime GetExpiration()
        {
            return EpochTimeExtensions.FromEpochToDateTime(Convert.ToInt32(Exp));
        }
        public DateTime GetNotBefore()
        {
            return EpochTimeExtensions.FromEpochToDateTime(Convert.ToInt32(Nbf));
        }
        public DateTime GetIssuedAt()
        {
            return EpochTimeExtensions.FromEpochToDateTime(Convert.ToInt32(Iat));
        }

        //almacena el Token Completo
        private string jwt_Access_Token;

        public string AccessToken
        {
            get {
                return jwt_Access_Token;
            }
            set { jwt_Access_Token = value.TrimStart('"').TrimEnd('"'); }
        }

        public Common_ES CommonES { get; set; }

        public AuthenticationHeaderValue GetBearerHeader()
        {
            return new AuthenticationHeaderValue("Bearer", AccessToken);
        }

    }


}
