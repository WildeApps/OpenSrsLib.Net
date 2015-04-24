using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace OpenSrsLib.Commands
{
    public class RequestBase
    {
        public OpsObjectCollection ObjectCollection { get; set; }

        public string RequestXml()
        {
            string requestXml = string.Empty;
            var builder = new StringBuilder();
            var xmlSerializer = new XmlSerializer(ObjectCollection.OpsRequest.GetType());
            using (XmlWriter writer = XmlWriter.Create(builder, new XmlWriterSettings(){Encoding = null}))
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                writer.WriteProcessingInstruction("xml", @"version=""1.0"" encoding=""UTF-8"" standalone=""no""");
                writer.WriteDocType("OPS_envelope", null, "ops.dtd", null);
                xmlSerializer.Serialize(writer, ObjectCollection.OpsRequest, ns);
            }
            requestXml = builder.ToString().Replace("utf-16", "utf-8");
            return requestXml;
        }
    }
}
