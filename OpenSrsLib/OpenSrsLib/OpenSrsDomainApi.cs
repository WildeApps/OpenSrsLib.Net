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
            request.BuildOpsEnvelope(_version, _registrantIp);

            return new BelongsToRspResponse(SendRequest(request.RequestXml()));
        }

        public GetBalanceResponse GetBalance(GetBalanceRequest request)
        {
            request.BuildOpsEnvelope(_version, _registrantIp);

            return new GetBalanceResponse(SendRequest(request.RequestXml()));
        }

        public GetDeletedDomainsResponse GetDeletedDomains(GetDeletedDomainsRequest request)
        {
            request.BuildOpsEnvelope(_version, _registrantIp);

            return new GetDeletedDomainsResponse(SendRequest(request.RequestXml()));
        }

        public string SendRequest(string requestXml)
        {
            string responseXml = string.Empty;

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

            return responseXml;
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
