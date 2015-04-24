using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSrsLib.Helpers;

namespace OpenSrsLib.Commands
{
    public class ResponseBase
    {
        public string Protocol { get; set; }
        public string Action { get; set; }
        public string Object { get; set; }
        public bool IsSuccess { get; set; }
        public long ResponseCode { get; set; }
        public string ResponseText { get; set; }
        public string Xml { get; set; }

        public OPS_envelope ResponseEnvelope;

        public ResponseBase(string xml)
        {
            Xml = xml;
            ResponseEnvelope = SerializationHelper.Deserialize<OPS_envelope>(xml);
            Protocol = OpsObjectHelper.GetResponseDataBlockItem(ResponseEnvelope, "protocol").Text;
            Action = OpsObjectHelper.GetResponseDataBlockItem(ResponseEnvelope, "action").Text;
            Object = OpsObjectHelper.GetResponseDataBlockItem(ResponseEnvelope, "object").Text;
            IsSuccess = OpsObjectHelper.SrsBoolToNetBool(OpsObjectHelper.GetResponseDataBlockItem(ResponseEnvelope, "is_success").Text);
            ResponseCode = Convert.ToInt64(OpsObjectHelper.GetResponseDataBlockItem(ResponseEnvelope, "response_code").Text);
            ResponseText = OpsObjectHelper.GetResponseDataBlockItem(ResponseEnvelope, "response_text").Text;
        }
    }
}
