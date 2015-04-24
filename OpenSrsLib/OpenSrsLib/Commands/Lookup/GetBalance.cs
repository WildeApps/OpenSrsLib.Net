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
        public double Balance { get; set; }
        public double HoldBalance { get; set; }

        public GetBalanceResponse(string xml) : base(xml)
        {
            if (IsSuccess)
            {
                Balance = Convert.ToDouble(OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "balance").Text);
                HoldBalance = Convert.ToDouble(OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "hold_balance").Text);
            }
        }
    }
}
