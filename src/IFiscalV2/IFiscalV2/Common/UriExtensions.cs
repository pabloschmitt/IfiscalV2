namespace IFiscalV2.Common
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;

    public static class UriExtensions
    {
        public static string AttachParameters(this string uri, object parameters = null)
        {
            if ( parameters == null)
                return uri;

            var aa = JsonConvert.SerializeObject(parameters);

            var pmap = JsonConvert.DeserializeObject<Dictionary<string, string>> ( aa );

            var hh = new FormUrlEncodedContent(pmap);
            
            var stringBuilder = new StringBuilder();
            string str = "?";

            foreach ( var p in pmap)
            {
                stringBuilder.Append(str + p.Key + "=" + Uri.EscapeDataString(p.Value));
                str = "&";
            }

            var result = uri + stringBuilder.ToString();
            return result;

        }
    }
}
