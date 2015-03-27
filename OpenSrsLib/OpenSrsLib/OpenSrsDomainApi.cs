using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OpenSrsLib.Commands;
using OpenSrsLib.Commands.Lookup;
using OpenSrsLib.Helpers;

namespace OpenSrsLib
{
    public class OpenSrsDomainApi
    {
        private string _version;
        private readonly string _registrantIp;
        private string _username;
        private string _privateKey;
        private string _openSrsUrl;

        public OpenSrsDomainApi(string version, string registrantIp, string username, string privateKey, string openSrsUrl)
        {
            _version = version;
            _registrantIp = registrantIp;
            _username = username;
            _privateKey = privateKey;
            _openSrsUrl = openSrsUrl;
        }

        public BelongsToRspResponse BelongsToRsp(BelongsToRspRequest request)
        {
            OpsObjectCollection opsObjects = OpsObjectHelper.BuildOpsEnvelope(_version, "SW_REGISTER", "DOMAIN", _registrantIp);

            opsObjects.AttributesArray.Items.Add(new item("domain", request.Domain));

            return new BelongsToRspResponse(SendRequest(opsObjects.OpsRequest));
        }

        public OPS_envelope SendRequest(OPS_envelope request)
        {
            string requestXml = string.Empty, responseXml = string.Empty;
            using (var ms = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(OPS_envelope));

                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                serializer.Serialize(ms, request, ns);

                requestXml = Encoding.ASCII.GetString(ms.GetBuffer());
                requestXml = requestXml.Insert(requestXml.IndexOf(Environment.NewLine) + 1, "<!DOCTYPE OPS_envelope SYSTEM \"ops.dtd\">");
            }

            var webRequest = new WebClient();
            webRequest.Encoding = Encoding.UTF8;

            webRequest.Headers.Add("Content-Type", "text/xml");
            webRequest.Headers.Add("X-Username", _username);

            var md5String = ToMd5String(ToMd5String(requestXml + _privateKey) + _privateKey);

            webRequest.Headers.Add("X-Signature", md5String);

            bool complete = false;
            int attempts = 0;
            while (!complete && attempts < 3)
            {
                try
                {
                    responseXml = webRequest.UploadString(_openSrsUrl, requestXml);
                    complete = true;
                }
                catch (Exception e)
                {
                    Thread.Sleep(100);
                }
                attempts++;
            }

            var opsResult = SerializationHelper.Deserialize<OPS_envelope>(responseXml);

            return opsResult;
        }

        /// <summary>
        /// Converts a string to its MD5 equivalent - this is used for verification in the header of messages to openSrs
        /// </summary>
        private string ToMd5String(string value)
        {
            var hash = CryptographyHelper.CalculateMD5Hash(value);

            return !String.IsNullOrEmpty(hash) ? hash.ToLower() : string.Empty;
        }
    }
}
