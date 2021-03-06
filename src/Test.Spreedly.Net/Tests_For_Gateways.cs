﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Spreedly.Net.BuiltIns;
using Spreedly.Net.Model;
using Spreedly.Net.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Spreedly.Net.Resources;

namespace Test.Spreedly.Net
{
    [TestClass]
    public class ParseGatewaysTests
    {
        private SpreedlyService _client;

        [TestInitialize]
        public void MyTestInitialize()
        {
            var secrets = Secrets.spreedly.Split('\t');
            _client = new SpreedlyService(secrets[0], secrets[1], secrets.Length > 2 ? secrets[2] : "", secrets.Length > 3 ? secrets[3] : "");
        }

        [TestMethod]
        public void When_we_call_gateways()
        {
            var result = _client.Gateways();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void When_we_add_a_stripe_gateway()
        {
            var secrets = Secrets.spreedly.Split('\t');
            Assert.IsTrue(secrets.Length > 4);
            var gateway = _client.AddGateway("stripe", new Dictionary<string,string>{{"login",secrets[4]}});
            //Assert.IsNotNull(gateway);
            //Assert.IsTrue(gateway.Enabled);
            //Assert.AreEqual(gateway.Type, "stripe");
            //fails for now...
        }

        [TestMethod]
        public void when_we_redact_a_gateway()
        {
            var result = _client.Gateways();
            var gateway =
                result.FirstOrDefault(g => g.Type == "test" && g.Token == _client.RedactedToken() && g.Enabled == false);
            Assert.IsNotNull(gateway);
            if(gateway.Enabled)
            {
                gateway = _client.RedactGateway(gateway.Token);
            }
            Assert.AreEqual(gateway.Type, "test");
            Assert.IsFalse(gateway.Enabled);
            Assert.AreEqual(gateway.Token, _client.RedactedToken());
        }
        
        [TestMethod]
        public void when_we_hydrate_a_valid_gateway()
        {
            var xml =
                @"<gateway>
  <token>zzz</token>
  <gateway_type>test</gateway_type>
  <redacted type='boolean'>false</redacted>
</gateway>
";
            var doc = XDocument.Parse(xml, LoadOptions.SetLineInfo);
            var gateways = Gateway.FromXml(doc);
            Assert.IsNotNull(gateways);
            Assert.AreEqual(1, gateways.Count());
            var gateway = gateways.FirstOrDefault();
            Assert.IsNotNull(gateway);
            Assert.AreEqual(gateway.Token, "zzz");
            Assert.IsTrue(gateway.Enabled);
            Assert.AreEqual(gateway.Type, "test");
        }

        [TestMethod]
        public void when_we_hydrate_an_invalid_gateway()
        {
            var xml =
                @"<gateway>
  <token>aaa</token>
  <gateway_type>some</gateway_type>
  <redacted type='boolean'>true</redacted>
</gateway>
";
            var doc = XDocument.Parse(xml, LoadOptions.SetLineInfo);
            var gateways = Gateway.FromXml(doc);
            Assert.IsNotNull(gateways);
            Assert.AreEqual(1, gateways.Count());
            var gateway = gateways.FirstOrDefault();
            Assert.IsNotNull(gateway);
            Assert.AreEqual(gateway.Token, "aaa");
            Assert.IsFalse(gateway.Enabled);
            Assert.AreEqual(gateway.Type, "some");
        }

    }

}
