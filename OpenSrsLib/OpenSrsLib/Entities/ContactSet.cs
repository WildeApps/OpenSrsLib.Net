using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSrsLib.Helpers;

namespace OpenSrsLib.Entities
{
    public class ContactSet
    {
        public Contact owner { get; set; }
        public Contact admin { get; set; }
        public Contact billing { get; set; }
        public Contact tech { get; set; }

        public ContactSet()
        {
            
        }

        public ContactSet(OPS_envelope response)
        {
            var contacts = (dt_assoc)OpsObjectHelper.GetResponseAttributeItem(response, "contact_set").Item;
            if (contacts != null)
            {
                var adminContact = OpsObjectHelper.GetItemFromArray(contacts, "admin");
                var billingContact = OpsObjectHelper.GetItemFromArray(contacts, "billing");
                var ownerContact = OpsObjectHelper.GetItemFromArray(contacts, "owner");
                var techContact = OpsObjectHelper.GetItemFromArray(contacts, "tech");

                if(adminContact != null)
                    admin = new Contact(adminContact);
                if(billingContact != null)
                    billing = new Contact(billingContact);
                if(ownerContact != null)
                    owner = new Contact(ownerContact);
                if(techContact != null)
                    tech = new Contact(techContact);
            }
        }

        public item BuildContactSet()
        {
            var contactSet = new item("contact_set");

            var contactDetails = new dt_assoc();

            if (owner != null)
                contactDetails.Items.Add(owner.BuildContactObject("owner"));

            if (admin != null)
                contactDetails.Items.Add(admin.BuildContactObject("admin"));

            if (billing != null)
                contactDetails.Items.Add(billing.BuildContactObject("billing"));

            if (tech != null)
                contactDetails.Items.Add(tech.BuildContactObject("tech"));

            contactSet.Item = contactDetails;

            return contactSet;
        }
    }
}
