namespace IFiscalV2.Models.Auth
{
    using Newtonsoft.Json;

    public class Common_ES
    {
        [JsonProperty("siteId")]
        public string SiteId { get; set; }

        [JsonProperty("siteName")]
        public string SiteName { get; set; }

        [JsonProperty("eleccionId")]
        public string EleccionId { get; set; }

        [JsonProperty("eleccionName")]
        public string EleccionName { get; set; }

        [JsonProperty("isGlobal")]
        public bool IsGlobal { get; set; }

        [JsonProperty("inGlobal")]
        public bool InGlobal { get; set; }

        [JsonProperty("inEleccion")]
        public bool InEleccion { get; set; }
    }
}
