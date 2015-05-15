using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSrsLib.Entities
{
    //TODO - finsih building the TLD Data

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
    }

    public class IprData
    {
        public string icm_membership_id { get; set; }
        public string ipr_email { get; set; }
        public string ipr_name { get; set; }
        public bool ipr_non_resolver { get; set; }
    }

    public class ItRegistrantInfo
    {
        public string entity_type { get; set; }
        public string nationality_code { get; set; }
        public string reg_code { get; set; }
    }

    public class Nexus
    {
        public string app_purpose { get; set; }
        public string category { get; set; }
        public string validator { get; set; }
    }

    public class ProfessionalData
    {
        public string authority { get; set; }
        public string authority_website { get; set; }
        public string license_number { get; set; }
        public string profession { get; set; }
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
    }
}
