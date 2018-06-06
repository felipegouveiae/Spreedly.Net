using Spreedly.NetStandard.Model;
using System.IO;
using System.Net.Http;
using System.Xml.Linq;

namespace Spreedly.NetStandard.Extensions
{
    public static class SpreedlyExtensions
    {
        public static bool Failed<T>(this AsyncCallResult<T> result) where T : class
        {
            return result == null || result.FailureReason != AsyncCallFailureReason.None;
        }

        public static string GetStringChild(this XElement node, string name)
        {
            return GetStringChild(node, name, string.Empty);
        }

        public static string GetStringChild(this XElement node, string name, string defaultValue)
        {
            if (node == null) return defaultValue;
            var token = node.Element(name);
            return token == null ? defaultValue : token.Value;
        }
    }
}