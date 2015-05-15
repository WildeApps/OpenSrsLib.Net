using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSrsLib.Helpers;

namespace OpenSrsLib.Commands.Lookup
{
    public class GetBalanceRequest : RequestBase
    {
        public void BuildOpsEnvelope(string version, string registrantIp)
        {
            ObjectCollection = OpsObjectHelper.BuildOpsEnvelope(version, "get_balance", "balance", registrantIp);
        }
    }

    public class GetBalanceResponse : ResponseBase
    {
        public double balance { get; set; }
        public double hold_balance { get; set; }

        public GetBalanceResponse(string xml) : base(xml)
        {
            if (IsSuccess)
            {
                balance = Convert.ToDouble(OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "balance").Text);
                hold_balance = Convert.ToDouble(OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "hold_balance").Text);
            }
        }
    }
}
