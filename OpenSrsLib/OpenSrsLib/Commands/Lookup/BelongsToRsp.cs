using System;
using OpenSrsLib.Helpers;

namespace OpenSrsLib.Commands.Lookup
{
    public class BelongsToRspRequest : RequestBase
    {
        public string domain { get; set; }

        public void BuildOpsEnvelope(string version, string registrantIp)
        {
            ObjectCollection = OpsObjectHelper.BuildOpsEnvelope(version, "belongs_to_rsp", "DOMAIN", registrantIp);
            ObjectCollection.AttributesArray.Items.Add(new item("domain", domain));
        }
    }

    public class BelongsToRspResponse : ResponseBase
    {
        public bool belongs_to_rsp { get; set; }
        public DateTime? domain_expdate { get; set; }

        public BelongsToRspResponse(string responseXml) : base(responseXml)
        {
            if (IsSuccess)
            {
                belongs_to_rsp = OpsObjectHelper.SrsBoolToNetBool(OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "belongs_to_rsp").Text);
                var domainExpiryNode = OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "domain_expdate");
                if (domainExpiryNode != null)
                    domain_expdate = OpsObjectHelper.ConvertToNullableDateTime(domainExpiryNode.Text);
            }
        }
    }
}
