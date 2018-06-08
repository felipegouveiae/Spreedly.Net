using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

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

        ///// <summary>
        ///// JsonConvert creates a key for every property contained in the json object.
        ///// </summary>
        //[JsonExtensionData]
        //private Dictionary<string, JToken> ExtendedData { get; set; }

        //public Dictionary<string, string> Fields => ExtendedData.ToDictionary(x => x.Key, x => x.Value.ToString());
    }

    public class SupportedGatewaysResult
    {
        [JsonProperty("gateways")]
        public List<SupportedGatewayDetails> Gateways { get; set; }

        public static SupportedGatewaysResult FromJson(string json)
        {
            var converted = JsonConvert.DeserializeObject<SupportedGatewaysResult>(json);

            return converted;
        }
    }

}
