using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSrsLib.Helpers;

namespace OpenSrsLib.Commands.Lookup
{
    public class GetDeletedDomainsRequest : RequestBase
    {
        public string admin_email { get; set; }
        public string billing_email { get; set; }
        public DateTime? del_from { get; set; }
        public DateTime? del_to { get; set; }
        public string domain { get; set; }
        public DateTime? exp_from { get; set; }
        public DateTime? exp_to { get; set; }
        public int? limit { get; set; }
        public string owner_email { get; set; }
        public int? page { get; set; }
        public string tech_email { get; set; }

        public void BuildOpsEnvelope(string version, string registrantIp)
        {
            ObjectCollection = OpsObjectHelper.BuildOpsEnvelope(version, "get_deleted_domains", "DOMAIN", registrantIp);
            if(!String.IsNullOrEmpty(admin_email))
                ObjectCollection.AttributesArray.Items.Add(new item("admin_email", admin_email));
            
            if(!String.IsNullOrEmpty(billing_email))
                ObjectCollection.AttributesArray.Items.Add(new item("billing_email", billing_email));
            
            if(del_from.HasValue)
                ObjectCollection.AttributesArray.Items.Add(new item("del_from", del_from.Value.ToString("yyyy-MM-dd")));
            
            if (del_to.HasValue)
                ObjectCollection.AttributesArray.Items.Add(new item("del_to", del_to.Value.ToString("yyyy-MM-dd")));
            
            if(!String.IsNullOrEmpty(domain))
                ObjectCollection.AttributesArray.Items.Add(new item("domain", domain));
            
            if (exp_from.HasValue)
                ObjectCollection.AttributesArray.Items.Add(new item("exp_from", exp_from.Value.ToString("yyyy-MM-dd")));
            
            if (exp_to.HasValue)
                ObjectCollection.AttributesArray.Items.Add(new item("exp_to", exp_to.Value.ToString("yyyy-MM-dd")));

            if(limit.HasValue)
                ObjectCollection.AttributesArray.Items.Add(new item("limit", limit.Value.ToString()));

            if(!String.IsNullOrEmpty(owner_email))
                ObjectCollection.AttributesArray.Items.Add(new item("owner_email", owner_email));

            if(page.HasValue)
                ObjectCollection.AttributesArray.Items.Add(new item("page", page.ToString()));

            if (!String.IsNullOrEmpty(tech_email))
                ObjectCollection.AttributesArray.Items.Add(new item("tech_email", tech_email));
        }
    }

    public class GetDeletedDomainsResponse : ResponseBase
    {
        public List<DeletedDomain> del_domains { get; set; }
        public int page { get; set; }
        public int page_size { get; set; }
        public int total { get; set; }


        public GetDeletedDomainsResponse(string xml) : base(xml)
        {
            if (IsSuccess)
            {
                del_domains = new List<DeletedDomain>();
                var delDomainsArray = (dt_array)OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "del_domains").Item;

                foreach (item i in delDomainsArray.Items)
                {
                    var array = (dt_assoc)i.Item;
                    var domain = new DeletedDomain
                    {
                        delete_date = OpsObjectHelper.ConvertToDateTime(OpsObjectHelper.GetItemFromArray(array, "delete_date").Text),
                        expire_date = OpsObjectHelper.ConvertToDateTime(OpsObjectHelper.GetItemFromArray(array, "expire_date").Text),
                        name = OpsObjectHelper.GetItemFromArray(array, "name").Text,
                        reason = OpsObjectHelper.GetItemFromArray(array, "reason").Text
                    };
                    del_domains.Add(domain);
                }

                page = Convert.ToInt32(OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "page").Text);
                page_size = Convert.ToInt32(OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "page_size").Text);
                total = Convert.ToInt32(OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "total").Text);
            }
        }
    }

    public class DeletedDomain
    {
        public DateTime delete_date { get; set; }
        public DateTime expire_date { get; set; }
        public string name { get; set; }
        public string reason { get; set; }
    }
}
