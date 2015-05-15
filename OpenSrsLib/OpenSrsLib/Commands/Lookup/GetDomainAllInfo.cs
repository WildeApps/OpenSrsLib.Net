using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSrsLib.Entities;
using OpenSrsLib.Helpers;

namespace OpenSrsLib.Commands.Lookup
{
    //TODO - finish building
    public class GetDomainAllInfoRequest : RequestBase
    {
        public string domain { get; set; }
        public string type { get { return "all_info"; } }

        public void BuildOpsEnvelope(string version, string registrantIp)
        {
            ObjectCollection = OpsObjectHelper.BuildOpsEnvelope(version, "GET", "DOMAIN", registrantIp);
            ObjectCollection.AttributesArray.Items.Add(new item("domain", domain));
            ObjectCollection.AttributesArray.Items.Add(new item("type", type));
        }
    }

    public class GetDomainAllInfoResponse :ResponseBase
    {
        public string affiliate_id { get; set; }
        public ContactSet contact_set { get; set; }
        public string descr { get; set; }
        public string dns_errors { get; set; }
        public List<Nameserver> nameserver_list { get; set; }
        public DateTime registry_createdate { get; set; }
        public DateTime registry_expiredate { get; set; }
        public DateTime? registry_transferdate { get; set; }
        public DateTime? registry_updatedate { get; set; }
        public bool sponsoring_rsp { get; set; }
        public TldData tld_data { get; set; }

        public GetDomainAllInfoResponse(string xml): base(xml)
        {
        }
    }
}
