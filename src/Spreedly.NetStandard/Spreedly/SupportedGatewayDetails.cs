using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Spreedly.NetStandard.Spreedly
{
    [XmlRoot("gateways")]
    public class SupportedGatewaysResult
    {
        [XmlElement("gateway")]
        public List<SupportedGatewayDetails> Gateways { get; set; }

        public static SupportedGatewaysResult FromXml(string xml)
        {
            var serializer = new XmlSerializer(typeof(SupportedGatewaysResult));

            using (var reader = new StringReader(xml))
            {
                var deserialized = serializer.Deserialize(reader);
                var result = (SupportedGatewaysResult)deserialized;

                return result;
            }
        }
    }

    public class SupportedGatewayDetails
    {
        [XmlElement("homepage")]
        public string Homepage { get; set; }

        [XmlElement("company_name")]
        public string CompanyNAme { get; set; }

        [XmlElement("gateway_type")]
        public string Type { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("auth_mode")]
        //[XmlArrayItem("skater", Type = typeof(Skater))]
        //[XmlArrayItem("goalie", Type = typeof(Goalie))]
        public List<AuthenticationModes> AuthenticationModes { get; set; }
    }

    public class AuthenticationModes
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("auth_mode_type")]
        public string Type { get; set; }
    }
}
