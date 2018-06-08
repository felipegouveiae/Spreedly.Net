using Newtonsoft.Json;
using System.Collections.Generic;

namespace Spreedly.NetStandard.Spreedly
{
    public class SupportedGatewayDetails
    {
        public string Homepage { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("gateway_type")]
        public string Type { get; set; }

        public string Name { get; set; }

        [JsonProperty("auth_modes")]
        public List<AuthenticationModes> AuthenticationModes { get; set; }

        public string[] Characteristics { get; set; }

        [JsonProperty("payment_methods")]
        public string[] PaymentMethods { get; set; }

        [JsonProperty("gateway_specific_fields")]
        public string[] SpecifcFields { get; set; }

        [JsonProperty("supported_countries")]
        public string[] SupportedCountries { get; set; }

        [JsonProperty("supported_cardtypes")]
        public string[] supportedCardtypes { get; set; }

        public string[] Regions { get; set; }
    }

    public class AuthenticationModes
    {
        public string Name { get; set; }

        [JsonProperty("auth_mode_type")]
        public string Type { get; set; }

        public List<Credencial> Credentials { get; set; }
    }

    public class Credencial
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public bool Safe { get; set; }
    }

    public class SupportedGatewaysResult
    {
        [JsonProperty("gateways")]
        public List<SupportedGatewayDetails> Gateways { get; set; }

        public static SupportedGatewaysResult FromJson(string json) 
            => JsonConvert.DeserializeObject<SupportedGatewaysResult>(json);
    }

}
