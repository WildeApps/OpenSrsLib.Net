using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSrsLib.Helpers;

namespace OpenSrsLib.Entities
{
    public class TldData
    {
        public AuRegistrantInfo au_registrant_info { get; set; }
        public string br_register_number { get; set; }
        public CedInfo ced_info { get; set; }
        public IprData ipr_data { get; set; }
        public ItRegistrantInfo it_registrant_info { get; set; }
        public Nexus nexus { get; set; }
        public ProfessionalData professional_data { get; set; }
        public RegistrantExtraInfo registrant_extra_info { get; set; }

        public TldData()
        {
            
        }

        public TldData(OPS_envelope response)
        {
            var tldDataItem = OpsObjectHelper.GetResponseAttributeItem(response, "tld_data");
            if (tldDataItem != null)
            {
                var tldData = (dt_assoc)tldDataItem.Item;
                if (tldData != null)
                {
                    var auRegistrantInfoItem = OpsObjectHelper.GetItemFromArray(tldData, "au_registrant_info");
                    if (auRegistrantInfoItem != null)
                        au_registrant_info = new AuRegistrantInfo(auRegistrantInfoItem);

                    var brRegisterNumberItem = OpsObjectHelper.GetItemFromArray(tldData, "br_register_number");
                    if (brRegisterNumberItem != null)
                        br_register_number = brRegisterNumberItem.Text;

                    var cedInfoItem = OpsObjectHelper.GetItemFromArray(tldData, "ced_info");
                    if(cedInfoItem != null)
                        ced_info = new CedInfo(cedInfoItem);

                    var iprDataItem = OpsObjectHelper.GetItemFromArray(tldData, "ipr_data");
                    if(iprDataItem != null)
                        ipr_data = new IprData(iprDataItem);

                    var itRegistrantInfoItem = OpsObjectHelper.GetItemFromArray(tldData, "it_registrant_info");
                    if(itRegistrantInfoItem != null)
                        it_registrant_info = new ItRegistrantInfo(itRegistrantInfoItem);

                    var nexusItem = OpsObjectHelper.GetItemFromArray(tldData, "nexus");
                    if(nexusItem != null)
                        nexus = new Nexus(nexusItem);

                    var professionalDataItem = OpsObjectHelper.GetItemFromArray(tldData, "professional_data");
                    if(professionalDataItem != null)
                        professional_data = new ProfessionalData(professionalDataItem);

                    var registrantExtraInfoItem = OpsObjectHelper.GetItemFromArray(tldData, "registrant_extra_info");
                    if(registrantExtraInfoItem != null)
                        registrant_extra_info = new RegistrantExtraInfo(registrantExtraInfoItem);
                }
            }
        }

        public item BuildItem()
        {
            var itemArray = new dt_assoc();
            if (au_registrant_info != null)
                itemArray.Items.Add(au_registrant_info.BuildItem());

            if(!string.IsNullOrEmpty(br_register_number))
                itemArray.Items.Add(new item("br_register_number", br_register_number));

            if(ced_info != null)
                itemArray.Items.Add(ced_info.BuildItem());

            if(ipr_data != null)
                itemArray.Items.Add(ipr_data.BuildItem());

            if(it_registrant_info != null)
                itemArray.Items.Add(it_registrant_info.BuildItem());

            if(nexus != null)
                itemArray.Items.Add(nexus.BuildItem());

            if(professional_data != null)
                itemArray.Items.Add(professional_data.BuildItem());

            if(registrant_extra_info != null)
                itemArray.Items.Add(registrant_extra_info.BuildItem());

            return new item("tld_data", itemArray);
        }
    }

    public class AuRegistrantInfo
    {
        public string eligibility_id { get; set; }
        public string eligibility_id_type { get; set; }
        public string eligibility_name { get; set; }
        public string eligibility_type { get; set; }
        public int? policy_reason { get; set; }
        public string registrant_id { get; set; }
        public string registrant_id_type { get; set; }
        public string registrant_name { get; set; }

        public AuRegistrantInfo()
        {
            
        }

        public AuRegistrantInfo(item item)
        {
            var detailsArray = item.Item as dt_assoc;
            try
            {
                eligibility_id = OpsObjectHelper.GetItemFromArray(detailsArray, "eligibility_id").Text;
            }
            catch { }
            try
            {
                eligibility_id_type = OpsObjectHelper.GetItemFromArray(detailsArray, "eligibility_id_type").Text;
            }
            catch { }
            try
            {
                eligibility_name = OpsObjectHelper.GetItemFromArray(detailsArray, "eligibility_name").Text;
            }
            catch { }
            try
            {
                eligibility_type = OpsObjectHelper.GetItemFromArray(detailsArray, "eligibility_type").Text;
            }
            catch { }
            try
            {
                policy_reason = OpsObjectHelper.ToNullableInt32(OpsObjectHelper.GetItemFromArray(detailsArray, "policy_reason").Text);
            }
            catch { }
            try
            {
                registrant_id = OpsObjectHelper.GetItemFromArray(detailsArray, "registrant_id").Text;
            }
            catch { }
            try
            {
                registrant_id_type = OpsObjectHelper.GetItemFromArray(detailsArray, "registrant_id_type").Text;
            }
            catch { }
            try
            {
                registrant_name = OpsObjectHelper.GetItemFromArray(detailsArray, "registrant_name").Text;
            }
            catch { }
        }

        public item BuildItem()
        {
            var itemArray = new dt_assoc();

            if (!string.IsNullOrEmpty(eligibility_id))
                itemArray.Items.Add(new item("eligibility_id", eligibility_id));
            if (!string.IsNullOrEmpty(eligibility_id_type))
                itemArray.Items.Add(new item("eligibility_id_type", eligibility_id_type));
            if (!string.IsNullOrEmpty(eligibility_name))
                itemArray.Items.Add(new item("eligibility_name", eligibility_name));
            if (!string.IsNullOrEmpty(eligibility_type))
                itemArray.Items.Add(new item("eligibility_type", eligibility_type));
            if (policy_reason.HasValue)
                itemArray.Items.Add(new item("policy_reason", policy_reason.Value));
            if (!string.IsNullOrEmpty(registrant_id))
                itemArray.Items.Add(new item("registrant_id", registrant_id));
            if (!string.IsNullOrEmpty(registrant_id_type))
                itemArray.Items.Add(new item("registrant_id_type", registrant_id_type));
            if (!string.IsNullOrEmpty(registrant_name))
                itemArray.Items.Add(new item("registrant_name", registrant_name));

            return new item("au_registrant_info", itemArray);
        }
    }

    public class CedInfo
    {
        public string contact_type { get; set; }
        public string id_number { get; set; }
        public string id_type { get; set; }
        public string id_type_info { get; set; }
        public string legal_entity_type { get; set; }
        public string legal_entity_type_info { get; set; }
        public string locality_city { get; set; }
        public string locality_country { get; set; }
        public string locality_state_prov { get; set; }

        public CedInfo()
        {
            
        }

        public CedInfo(item item)
        {
            var detailsArray = item.Item as dt_assoc;
            try
            {
                contact_type = OpsObjectHelper.GetItemFromArray(detailsArray, "contact_type").Text;
            }
            catch { }
            try
            {
                id_number = OpsObjectHelper.GetItemFromArray(detailsArray, "id_number").Text;
            }
            catch { }
            try
            {
                id_type = OpsObjectHelper.GetItemFromArray(detailsArray, "id_type").Text;
            }
            catch { }
            try
            {
                id_type_info = OpsObjectHelper.GetItemFromArray(detailsArray, "id_type_info").Text;
            }
            catch { }
            try
            {
                legal_entity_type = OpsObjectHelper.GetItemFromArray(detailsArray, "legal_entity_type").Text;
            }
            catch { }
            try
            {
                legal_entity_type_info = OpsObjectHelper.GetItemFromArray(detailsArray, "legal_entity_type_info").Text;
            }
            catch { }
            try
            {
                locality_city = OpsObjectHelper.GetItemFromArray(detailsArray, "locality_city").Text;
            }
            catch { }
            try
            {
                locality_country = OpsObjectHelper.GetItemFromArray(detailsArray, "locality_country").Text;
            }
            catch { }
            try
            {
                locality_state_prov = OpsObjectHelper.GetItemFromArray(detailsArray, "locality_state_prov").Text;
            }
            catch { }
        }

        public item BuildItem()
        {
            var itemArray = new dt_assoc();

            if (!string.IsNullOrEmpty(contact_type))
                itemArray.Items.Add(new item("contact_type", contact_type));
            if (!string.IsNullOrEmpty(id_number))
                itemArray.Items.Add(new item("id_number", id_number));
            if (!string.IsNullOrEmpty(id_type))
                itemArray.Items.Add(new item("id_type", id_type));
            if (!string.IsNullOrEmpty(id_type_info))
                itemArray.Items.Add(new item("id_type_info", id_type_info));
            if (!string.IsNullOrEmpty(legal_entity_type))
                itemArray.Items.Add(new item("legal_entity_type", legal_entity_type));
            if (!string.IsNullOrEmpty(legal_entity_type_info))
                itemArray.Items.Add(new item("legal_entity_type_info", legal_entity_type_info));
            if (!string.IsNullOrEmpty(locality_city))
                itemArray.Items.Add(new item("locality_city", locality_city));
            if (!string.IsNullOrEmpty(locality_country))
                itemArray.Items.Add(new item("locality_country", locality_country));
            if (!string.IsNullOrEmpty(locality_state_prov))
                itemArray.Items.Add(new item("locality_state_prov", locality_state_prov));

            return new item("ced_info", itemArray);
        }
    }

    public class IprData
    {
        public string icm_membership_id { get; set; }
        public string ipr_email { get; set; }
        public string ipr_name { get; set; }
        public bool ipr_non_resolver { get; set; }

        public IprData()
        {
            
        }

        public IprData(item item)
        {
            var detailsArray = item.Item as dt_assoc;
            try
            {
                icm_membership_id = OpsObjectHelper.GetItemFromArray(detailsArray, "icm_membership_id").Text;
            }
            catch { }
            try
            {
                ipr_email = OpsObjectHelper.GetItemFromArray(detailsArray, "ipr_email").Text;
            }
            catch { }
            try
            {
                ipr_name = OpsObjectHelper.GetItemFromArray(detailsArray, "ipr_name").Text;
            }
            catch { }
            try
            {
                ipr_non_resolver = OpsObjectHelper.SrsBoolToNetBool(OpsObjectHelper.GetItemFromArray(detailsArray, "ipr_non_resolver").Text);
            }
            catch { }
        }

        public item BuildItem()
        {
            var itemArray = new dt_assoc();

            if (!string.IsNullOrEmpty(icm_membership_id))
                itemArray.Items.Add(new item("icm_membership_id", icm_membership_id));
            if (!string.IsNullOrEmpty(ipr_email))
                itemArray.Items.Add(new item("ipr_email", ipr_email));
            if (!string.IsNullOrEmpty(ipr_name))
                itemArray.Items.Add(new item("ipr_name", ipr_name));
            itemArray.Items.Add(new item("ipr_non_resolver", OpsObjectHelper.NetBoolToSrsBool(ipr_non_resolver)));


            return new item("ipr_data", itemArray);
        }
    }

    public class ItRegistrantInfo
    {
        public string entity_type { get; set; }
        public string nationality_code { get; set; }
        public string reg_code { get; set; }

        public ItRegistrantInfo()
        {
            
        }

        public ItRegistrantInfo(item item)
        {
            var detailsArray = item.Item as dt_assoc;
            try
            {
                entity_type = OpsObjectHelper.GetItemFromArray(detailsArray, "entity_type").Text;
            }
            catch { }
            try
            {
                nationality_code = OpsObjectHelper.GetItemFromArray(detailsArray, "nationality_code").Text;
            }
            catch { }
            try
            {
                reg_code = OpsObjectHelper.GetItemFromArray(detailsArray, "reg_code").Text;
            }
            catch { }
        }

        public item BuildItem()
        {
            var itemArray = new dt_assoc();

            if (!string.IsNullOrEmpty(entity_type))
                itemArray.Items.Add(new item("entity_type", entity_type));
            if (!string.IsNullOrEmpty(nationality_code))
                itemArray.Items.Add(new item("nationality_code", nationality_code));
            if (!string.IsNullOrEmpty(reg_code))
                itemArray.Items.Add(new item("reg_code", reg_code));

            return new item("it_registrant_info", itemArray);
        }
    }

    public class Nexus
    {
        public string app_purpose { get; set; }
        public string category { get; set; }
        public string validator { get; set; }

        public Nexus()
        {
            
        }

        public Nexus(item item)
        {
            var detailsArray = item.Item as dt_assoc;
            try
            {
                app_purpose = OpsObjectHelper.GetItemFromArray(detailsArray, "app_purpose").Text;
            }
            catch { }
            try
            {
                category = OpsObjectHelper.GetItemFromArray(detailsArray, "category").Text;
            }
            catch { }
            try
            {
                validator = OpsObjectHelper.GetItemFromArray(detailsArray, "validator").Text;
            }
            catch { }
        }

        public item BuildItem()
        {
            var itemArray = new dt_assoc();

            if (!string.IsNullOrEmpty(app_purpose))
                itemArray.Items.Add(new item("app_purpose", app_purpose));
            if (!string.IsNullOrEmpty(category))
                itemArray.Items.Add(new item("category", category));
            if (!string.IsNullOrEmpty(validator))
                itemArray.Items.Add(new item("validator", validator));

            return new item("nexus", itemArray);
        }
    }

    public class ProfessionalData
    {
        public string authority { get; set; }
        public string authority_website { get; set; }
        public string license_number { get; set; }
        public string profession { get; set; }

        public ProfessionalData()
        {
            
        }

        public ProfessionalData(item item)
        {
            var detailsArray = item.Item as dt_assoc;
            try
            {
                authority = OpsObjectHelper.GetItemFromArray(detailsArray, "authority").Text;
            }
            catch { }
            try
            {
                authority_website = OpsObjectHelper.GetItemFromArray(detailsArray, "authority_website").Text;
            }
            catch { }
            try
            {
                license_number = OpsObjectHelper.GetItemFromArray(detailsArray, "license_number").Text;
            }
            catch { }
            try
            {
                profession = OpsObjectHelper.GetItemFromArray(detailsArray, "profession").Text;
            }
            catch { }
        }

        public item BuildItem()
        {
            var itemArray = new dt_assoc();

            if (!string.IsNullOrEmpty(authority))
                itemArray.Items.Add(new item("authority", authority));
            if (!string.IsNullOrEmpty(authority_website))
                itemArray.Items.Add(new item("authority_website", authority_website));
            if (!string.IsNullOrEmpty(license_number))
                itemArray.Items.Add(new item("license_number", license_number));
            if (!string.IsNullOrEmpty(profession))
                itemArray.Items.Add(new item("profession", profession));

            return new item("professional_data", itemArray);
        }
    }

    public class RegistrantExtraInfo
    {
        public string aero_ens_id { get; set; }
        public string aero_ens_password { get; set; }
        public string coop_verification_code { get; set; }
        public string country_of_birth { get; set; }
        public DateTime? date_of_birth { get; set; }
        public string id_card_authority { get; set; }
        public DateTime? id_card_issue_date { get; set; }
        public string id_card_number { get; set; }
        public string jobs_admin_type { get; set; }
        public string jobs_association_member { get; set; }
        public string jobs_industry_type { get; set; }
        public string jobs_title { get; set; }
        public string jobs_website { get; set; }
        public string place_of_birth { get; set; }
        public string postal_code_of_birth { get; set; }
        public string province_of_birth { get; set; }
        public string registration_number { get; set; }
        public string registrant_type { get; set; }
        public string registrant_vat_id { get; set; }
        public string siren_siret { get; set; }
        public string tax_number { get; set; }
        public string trademark_number { get; set; }
        public string travel_uin { get; set; }

        public RegistrantExtraInfo()
        {
            
        }

        public RegistrantExtraInfo(item item)
        {
            var detailsArray = item.Item as dt_assoc;
            try
            {
                aero_ens_id = OpsObjectHelper.GetItemFromArray(detailsArray, "aero_ens_id").Text;
            }
            catch { }
            try
            {
                aero_ens_password = OpsObjectHelper.GetItemFromArray(detailsArray, "aero_ens_password").Text;
            }
            catch { }
            try
            {
                coop_verification_code = OpsObjectHelper.GetItemFromArray(detailsArray, "coop_verification_code").Text;
            }
            catch { }
            try
            {
                country_of_birth = OpsObjectHelper.GetItemFromArray(detailsArray, "country_of_birth").Text;
            }
            catch { }
            try
            {
                date_of_birth = OpsObjectHelper.ConvertToNullableDateTime(OpsObjectHelper.GetItemFromArray(detailsArray, "date_of_birth").Text);
            }
            catch { }
            try
            {
                id_card_authority = OpsObjectHelper.GetItemFromArray(detailsArray, "id_card_authority").Text;
            }
            catch { }
            try
            {
                id_card_issue_date = OpsObjectHelper.ConvertToNullableDateTime(OpsObjectHelper.GetItemFromArray(detailsArray, "id_card_issue_date").Text);
            }
            catch { }
            try
            {
                id_card_number = OpsObjectHelper.GetItemFromArray(detailsArray, "id_card_number").Text;
            }
            catch { }
            try
            {
                jobs_admin_type = OpsObjectHelper.GetItemFromArray(detailsArray, "jobs_admin_type").Text;
            }
            catch { }
            try
            {
                jobs_association_member = OpsObjectHelper.GetItemFromArray(detailsArray, "jobs_association_member").Text;
            }
            catch { }
            try
            {
                jobs_industry_type = OpsObjectHelper.GetItemFromArray(detailsArray, "jobs_industry_type").Text;
            }
            catch { }
            try
            {
                jobs_title = OpsObjectHelper.GetItemFromArray(detailsArray, "jobs_title").Text;
            }
            catch { }
            try
            {
                jobs_website = OpsObjectHelper.GetItemFromArray(detailsArray, "jobs_website").Text;
            }
            catch { }
            try
            {
                place_of_birth = OpsObjectHelper.GetItemFromArray(detailsArray, "place_of_birth").Text;
            }
            catch { }
            try
            {
                postal_code_of_birth = OpsObjectHelper.GetItemFromArray(detailsArray, "postal_code_of_birth").Text;
            }
            catch { }
            try
            {
                province_of_birth = OpsObjectHelper.GetItemFromArray(detailsArray, "province_of_birth").Text;
            }
            catch { }
            try
            {
                registration_number = OpsObjectHelper.GetItemFromArray(detailsArray, "registration_number").Text;
            }
            catch { }
            try
            {
                registrant_type = OpsObjectHelper.GetItemFromArray(detailsArray, "registrant_type").Text;
            }
            catch { }
            try
            {
                registrant_vat_id = OpsObjectHelper.GetItemFromArray(detailsArray, "registrant_vat_id").Text;
            }
            catch { }
            try
            {
                siren_siret = OpsObjectHelper.GetItemFromArray(detailsArray, "siren_siret").Text;
            }
            catch { }
            try
            {
                tax_number = OpsObjectHelper.GetItemFromArray(detailsArray, "tax_number").Text;
            }
            catch { }
            try
            {
                trademark_number = OpsObjectHelper.GetItemFromArray(detailsArray, "trademark_number").Text;
            }
            catch { }
            try
            {
                travel_uin = OpsObjectHelper.GetItemFromArray(detailsArray, "travel_uin").Text;
            }
            catch { }
        }

        public item BuildItem()
        {
            var itemArray = new dt_assoc();

            if(!string.IsNullOrEmpty(aero_ens_id))
                itemArray.Items.Add(new item("aero_ens_id", aero_ens_id));
            if(!string.IsNullOrEmpty(aero_ens_password))
                itemArray.Items.Add(new item("aero_ens_password", aero_ens_password));
            if (!string.IsNullOrEmpty(coop_verification_code))
                itemArray.Items.Add(new item("coop_verification_code", coop_verification_code));
            if (!string.IsNullOrEmpty(country_of_birth))
                itemArray.Items.Add(new item("country_of_birth", country_of_birth));
            if (date_of_birth.HasValue)
                itemArray.Items.Add(new item("date_of_birth", date_of_birth.Value.ToString("yyyy-MM-dd")));
            if (!string.IsNullOrEmpty(id_card_authority))
                itemArray.Items.Add(new item("id_card_authority", id_card_authority));
            if (id_card_issue_date.HasValue)
                itemArray.Items.Add(new item("id_card_issue_date", id_card_issue_date.Value.ToString("yyyy-MM-dd")));
            if (!string.IsNullOrEmpty(id_card_number))
                itemArray.Items.Add(new item("id_card_number", id_card_number));
            if (!string.IsNullOrEmpty(jobs_admin_type))
                itemArray.Items.Add(new item("jobs_admin_type", jobs_admin_type));
            if (!string.IsNullOrEmpty(jobs_association_member))
                itemArray.Items.Add(new item("jobs_association_member", jobs_association_member));
            if (!string.IsNullOrEmpty(jobs_industry_type))
                itemArray.Items.Add(new item("jobs_industry_type", jobs_industry_type));
            if (!string.IsNullOrEmpty(jobs_title))
                itemArray.Items.Add(new item("jobs_title", jobs_title));
            if (!string.IsNullOrEmpty(jobs_website))
                itemArray.Items.Add(new item("jobs_website", jobs_website));
            if (!string.IsNullOrEmpty(place_of_birth))
                itemArray.Items.Add(new item("place_of_birth", place_of_birth));
            if (!string.IsNullOrEmpty(postal_code_of_birth))
                itemArray.Items.Add(new item("postal_code_of_birth", postal_code_of_birth));
            if (!string.IsNullOrEmpty(province_of_birth))
                itemArray.Items.Add(new item("province_of_birth", province_of_birth));
            if (!string.IsNullOrEmpty(registration_number))
                itemArray.Items.Add(new item("registration_number", registration_number));
            if (!string.IsNullOrEmpty(registrant_type))
                itemArray.Items.Add(new item("registrant_type", registrant_type));
            if (!string.IsNullOrEmpty(registrant_vat_id))
                itemArray.Items.Add(new item("registrant_vat_id", registrant_vat_id));
            if (!string.IsNullOrEmpty(siren_siret))
                itemArray.Items.Add(new item("siren_siret", siren_siret));
            if (!string.IsNullOrEmpty(tax_number))
                itemArray.Items.Add(new item("tax_number", tax_number));
            if (!string.IsNullOrEmpty(trademark_number))
                itemArray.Items.Add(new item("trademark_number", trademark_number));
            if (!string.IsNullOrEmpty(travel_uin))
                itemArray.Items.Add(new item("travel_uin", travel_uin));

            return new item("registrant_extra_info", itemArray);
        }
    }
}
