using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace KofCWebSite.Models
{
    public class UriExt : Uri
    {
        protected UriExt(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }

        public UriExt(string uriString)
            : base(uriString)
        {
        }

        public UriExt(string uriString, UriKind uriKind)
            : base(uriString, uriKind)
        {
        }

        public UriExt(Uri baseUri, string relativeUri)
            : base(baseUri, relativeUri)
        {
        }

        public UriExt(Uri baseUri, Uri relativeUri) : base(baseUri, relativeUri)
        {
        }

        public static string Combine(params string[] urls)
        {
            if (urls == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            for (int k = 0; k < urls.Length; k++)
            {
                if (string.IsNullOrWhiteSpace(urls[k]))
                    continue;

                if (urls[k].StartsWith("/"))
                    sb.Append(urls[k].Substring(1));
                else
                    sb.Append(urls[k]);

                if (urls[k].EndsWith("/") == false)
                {
                    sb.Append("/");
                }
            }

            if (sb.Length >= 1 && sb[sb.Length - 1] == '/')
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string JsonObjectToUrlParams(object obj, bool removeNullValue = true, bool serializeEnumToString = true)
        {
            JsonSerializerSettings options = new JsonSerializerSettings
            {
                NullValueHandling = removeNullValue ? NullValueHandling.Ignore : NullValueHandling.Include
            };

            if (serializeEnumToString)
            {
                options.Converters.Add(new StringEnumConverter());
            }

            string json = JsonConvert.SerializeObject(obj, Formatting.None, options);
            var jObjs = (JObject)JsonConvert.DeserializeObject(json);

            StringBuilder builder = new StringBuilder();

            foreach (var i in jObjs)
            {
                builder.Append($"{i.Key}={System.Web.HttpUtility.UrlEncode(i.Value.ToString())}&");
            }

            if (builder.Length >= 1 && builder[builder.Length - 1] == '&')
                builder.Remove(builder.Length - 1, 1);

            return $"?{builder.ToString()}";
        }
    }
}
