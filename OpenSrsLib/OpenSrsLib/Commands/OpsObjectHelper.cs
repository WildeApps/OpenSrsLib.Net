using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;
using System.Xml.Serialization;
using OpenSrsLib.Helpers;

namespace OpenSrsLib.Commands
{
    public class OpsObjectHelper
    {
        private string _username;
        private string _privateKey;
        private string _openSrsUrl;

        public OpsObjectHelper(string username, string privateKey, string openSrsUrl)
        {
            _username = username;
            _privateKey = privateKey;
            _openSrsUrl = openSrsUrl;
        }

        /// <summary>
        /// Sets up an initial OPS_envelope object with the common objects that are needed and returns an object that provides easy access to
        /// the important composite objects such as the main body items array and the attributes array
        /// </summary>
        public OpsObjectCollection BuildOpsEnvelope(string version, string action, string obj = null, string registrantIp = null)
        {
            var objects = new OpsObjectCollection();

            // main body array
            objects.MainBodyArray = new dt_assoc();

            // protocol/action/object
            objects.MainBodyArray.Items.Add(new item("protocol", "XCP"));
            objects.MainBodyArray.Items.Add(new item("action", action));

            if (obj != null)
            {
                objects.MainBodyArray.Items.Add(new item("object", obj));
            }

            if (registrantIp != null)
            {
                objects.MainBodyArray.Items.Add(new item("registrant_ip", registrantIp));
            }

            // attributes
            var attributes = new item("attributes");
            objects.AttributesArray = new dt_assoc();

            attributes.Item = objects.AttributesArray;

            objects.MainBodyArray.Items.Add(attributes);

            // create ops reuest object
            objects.OpsRequest = new OPS_envelope
            {
                header = BuildHeader(version),
                body = new body
                {
                    data_block = new data_block
                    {
                        Item = objects.MainBodyArray
                    }
                }
            };

            return objects;
        }

        /// <summary>
        /// Retrieves an item object from a dt_assoc array with the specified key
        /// </summary>
        public item GetItemFromArray(dt_assoc array, string key)
        {
            return array.Items.Cast<item>().FirstOrDefault(i => i.key == key);
        }

        /// <summary>
        /// Returns an item from a response stored in the attributes dt_assoc array with the specified key
        /// </summary>
        public item GetResponseAttributeItem(OPS_envelope response, string key)
        {
            var responseBodyArray = (dt_assoc)response.body.data_block.Item;
            var attributes = GetItemFromArray(responseBodyArray, "attributes");

            if (attributes == null)
                return null;

            var responseAttributesArray = (dt_assoc)attributes.Item;

            return GetItemFromArray(responseAttributesArray, key);
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
        /// Builds the standard message header
        /// </summary>
        private header BuildHeader(string version)
        {
            var header = new header();
            header.version = new version { Text = version };

            return header;
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
