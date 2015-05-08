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
        public string AdminEmail { get; set; }
        public string BillingEmail { get; set; }
        public DateTime? DeleteFrom { get; set; }
        public DateTime? DeleteTo { get; set; }
        public string Domain { get; set; }
        public DateTime? ExpiresFrom { get; set; }
        public DateTime? ExpiresTo { get; set; }
        public int? Limit { get; set; }
        public string OwnerEmail { get; set; }
        public int? Page { get; set; }
        public string TechEmail { get; set; }

        public void BuildOpsEnvelope(string version, string registrantIp)
        {
            ObjectCollection = OpsObjectHelper.BuildOpsEnvelope(version, "get_deleted_domains", "DOMAIN", registrantIp);
            if(!String.IsNullOrEmpty(AdminEmail))
                ObjectCollection.AttributesArray.Items.Add(new item("admin_email", AdminEmail));
            
            if(!String.IsNullOrEmpty(BillingEmail))
                ObjectCollection.AttributesArray.Items.Add(new item("billing_email", BillingEmail));
            
            if(DeleteFrom.HasValue)
                ObjectCollection.AttributesArray.Items.Add(new item("del_from", DeleteFrom.Value.ToString("yyyy-MM-dd")));
            
            if (DeleteTo.HasValue)
                ObjectCollection.AttributesArray.Items.Add(new item("del_to", DeleteTo.Value.ToString("yyyy-MM-dd")));
            
            if(!String.IsNullOrEmpty(Domain))
                ObjectCollection.AttributesArray.Items.Add(new item("domain", Domain));
            
            if (ExpiresFrom.HasValue)
                ObjectCollection.AttributesArray.Items.Add(new item("exp_from", ExpiresFrom.Value.ToString("yyyy-MM-dd")));
            
            if (ExpiresTo.HasValue)
                ObjectCollection.AttributesArray.Items.Add(new item("exp_to", ExpiresTo.Value.ToString("yyyy-MM-dd")));

            if(Limit.HasValue)
                ObjectCollection.AttributesArray.Items.Add(new item("limit", Limit.Value.ToString()));

            if(!String.IsNullOrEmpty(OwnerEmail))
                ObjectCollection.AttributesArray.Items.Add(new item("owner_email", OwnerEmail));

            if(Page.HasValue)
                ObjectCollection.AttributesArray.Items.Add(new item("page", Page.ToString()));

            if (!String.IsNullOrEmpty(TechEmail))
                ObjectCollection.AttributesArray.Items.Add(new item("tech_email", TechEmail));
        }
    }

    public class GetDeletedDomainsResponse : ResponseBase
    {
        public List<DeletedDomain> DeletedDomains { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }


        public GetDeletedDomainsResponse(string xml) : base(xml)
        {
            if (IsSuccess)
            {
                DeletedDomains = new List<DeletedDomain>();
                var delDomainsArray = (dt_array)OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "del_domains").Item;

                foreach (item i in delDomainsArray.Items)
                {
                    var array = (dt_assoc)i.Item;
                    var domain = new DeletedDomain
                    {
                        DeleteDate = OpsObjectHelper.ConvertToDateTime(OpsObjectHelper.GetItemFromArray(array, "delete_date").Text),
                        ExpireDate = OpsObjectHelper.ConvertToDateTime(OpsObjectHelper.GetItemFromArray(array, "expire_date").Text),
                        Name = OpsObjectHelper.GetItemFromArray(array, "name").Text,
                        Reason = OpsObjectHelper.GetItemFromArray(array, "reason").Text
                    };
                    DeletedDomains.Add(domain);
                }

                Page = Convert.ToInt32(OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "page").Text);
                PageSize = Convert.ToInt32(OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "page_size").Text);
                Total = Convert.ToInt32(OpsObjectHelper.GetResponseAttributeItem(ResponseEnvelope, "total").Text);
            }
        }
    }

    public class DeletedDomain
    {
        public DateTime DeleteDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }
    }
}
