using System;
using OpenSrsLib.Helpers;

namespace OpenSrsLib.Commands.Lookup
{
    public class BelongsToRspRequest : RequestBase
    {
        public string Domain { get; set; }

        public void BuildOpsEnvelope(string version, string registrantIp)
        {
            ObjectCollection = OpsObjectHelper.BuildOpsEnvelope(version, "belongs_to_rsp", "DOMAIN", registrantIp);
            ObjectCollection.AttributesArray.Items.Add(new item("domain", Domain));
        }
    }

    public class BelongsToRspResponse : ResponseBase
    {
        public bool BelongsToRsp { get; set; }
        public DateTime? DomainExpiryDate { get; set; }

        public BelongsToRspResponse(string responseXml) : base(responseXml)
        {
            if (IsSuccess)
            {
                BelongsToRsp = OpsObjectHelper.SrsBoolToNetBool(OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "belongs_to_rsp").Text);
                var domainExpiryNode = OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "domain_expdate");
                if (domainExpiryNode != null)
                    DomainExpiryDate = OpsObjectHelper.ConvertToNullableDateTime(domainExpiryNode.Text);
            }
        }
    }
}
