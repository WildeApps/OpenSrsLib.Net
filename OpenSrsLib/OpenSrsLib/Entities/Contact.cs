using OpenSrsLib.Helpers;

namespace OpenSrsLib.Entities
{
    public class Contact
    {
        public string title { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string org_name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string lang { get; set; }
        public string vat { get; set; }

        public Contact()
        {
            
        }

        public Contact(item contactItem)
        {
            if (contactItem != null)
            {
                var detailsArray = contactItem.Item as dt_assoc;

                first_name = OpsObjectHelper.GetItemFromArray(detailsArray, "first_name").Text;
                last_name = OpsObjectHelper.GetItemFromArray(detailsArray, "last_name").Text;
                phone = OpsObjectHelper.GetItemFromArray(detailsArray, "phone").Text;
                email = OpsObjectHelper.GetItemFromArray(detailsArray, "email").Text;
                address1 = OpsObjectHelper.GetItemFromArray(detailsArray, "address1").Text;
                city = OpsObjectHelper.GetItemFromArray(detailsArray, "city").Text;
                country = OpsObjectHelper.GetItemFromArray(detailsArray, "country").Text;
                postal_code = OpsObjectHelper.GetItemFromArray(detailsArray, "postal_code").Text;

                //optional values wrapped in try/catch
                try
                {
                    title = OpsObjectHelper.GetItemFromArray(detailsArray, "title").Text;
                }
                catch { }
                try
                {
                    address2 = OpsObjectHelper.GetItemFromArray(detailsArray, "address2").Text;
                }
                catch { }

                try
                {
                    state = OpsObjectHelper.GetItemFromArray(detailsArray, "state").Text;
                }
                catch { }
                try
                {
                    fax = OpsObjectHelper.GetItemFromArray(detailsArray, "fax").Text;
                }
                catch { }
                try
                {
                    lang = OpsObjectHelper.GetItemFromArray(detailsArray, "lang").Text;
                }
                catch { }
                try
                {
                    vat = OpsObjectHelper.GetItemFromArray(detailsArray, "vat").Text;
                }
                catch { }
            }
        }

        public item BuildContactObject(string contactKey)
        {
            var details = new dt_assoc();

            details.Items.Add(new item("first_name", first_name));
            details.Items.Add(new item("last_name", last_name));
            details.Items.Add(new item("phone", phone));
            details.Items.Add(new item("email", email));
            details.Items.Add(new item("org_name", org_name));
            details.Items.Add(new item("address1", address1));
            details.Items.Add(new item("address2", address2));
            details.Items.Add(new item("address3", address3));
            details.Items.Add(new item("city", city));
            details.Items.Add(new item("state", state));
            details.Items.Add(new item("country", country));
            details.Items.Add(new item("postal_code", postal_code));
            details.Items.Add(new item("fax", fax));
            details.Items.Add(new item("lang", lang));
            details.Items.Add(new item("vat", vat));

            return new item(contactKey)
            {
                Item = details
            };
        }
    }
}
