using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenlyLocal.Core.Models
{
    public class Council : ILocation
    {
        public override string ToString()
        {
            return Name;
        }

        public double Lng { get; set; }
        public double Lat { get; set; }

        public GeoJson GeoJson { get; set; }
        public string vat_number { get; set; }
        public string region { get; set; }
        public string Name { get; set; }
        public bool defunkt { get; set; }
        public string address { get; set; }
        public bool signed_up_for_1010 { get; set; }
        public string created_at { get; set; }
        public string annual_audit_letter { get; set; }
        public int? wdtk_id { get; set; }
        public int? pension_fund_id { get; set; }
        public int? output_area_classification_id { get; set; }
        public string os_id { get; set; }
        public string data_source_url { get; set; }
        public string country { get; set; }
        public string authority_type { get; set; }
        public string updated_at { get; set; }
        public string parent_authority_id { get; set; }
        public string notes { get; set; }
        public string feed_url { get; set; }
        public int? egr_id { get; set; }
        public string url { get; set; }
        public int? portal_system_id { get; set; }
        public int? population { get; set; }
        public int? ldg_id { get; set; }
        public string data_source_name { get; set; }
        public int? police_force_id { get; set; }
        public string ness_id { get; set; }
        public int id { get; set; }
        public string gss_code { get; set; }
        public string base_url { get; set; }
        public string police_force_url { get; set; }
        public string planning_email { get; set; }
        public string open_data_url { get; set; }
        public string ons_url { get; set; }
        public string wdtk_name { get; set; }
        public string telephone { get; set; }
        public string open_data_licence { get; set; }
        public string wikipedia_url { get; set; }
        public string snac_id { get; set; }
        public string normalised_title { get; set; }
        public string cipfa_code { get; set; }

        public Ward[] wards { get; set; }
    }

    public class Party
    {
        public string name { get; set; }
        public string dbpedia_uri { get; set; }
        public string colour { get; set; }
    }

    public class Member
    {
        public string facebook_account_name { get; set; }
        public string address { get; set; }
        public string created_at { get; set; }
        public string date_left { get; set; }
        public string updated_at { get; set; }
        public string url { get; set; }
        public string uid { get; set; }
        public int id { get; set; }
        public string qualifications { get; set; }
        public string name_title { get; set; }
        public string linked_in_account_name { get; set; }
        public string last_name { get; set; }
        public string date_elected { get; set; }
        public int council_id { get; set; }
        public string blog_url { get; set; }
        public string telephone { get; set; }
        public Party party { get; set; }
        public string register_of_interests { get; set; }
        public int ward_id { get; set; }
        public string first_name { get; set; }
        public string email { get; set; }
    }

    public class Ward : INamedLocation
    {
        public GeoJson GeoJson { get; set; }
        public Council council { get; set; }
        public string Name { get; set; }
        public bool defunkt { get; set; }
        public string created_at { get; set; }
        public string output_area_classification_id { get; set; }
        public string os_id { get; set; }
        public string updated_at { get; set; }
        public string url { get; set; }
        public string crime_area_id { get; set; }
        public string uid { get; set; }
        public string ness_id { get; set; }
        public int id { get; set; }
        public string gss_code { get; set; }
        public List<Member> members { get; set; }
        public int? council_id { get; set; }
        public int? police_team_id { get; set; }
        public string snac_id { get; set; }
        public string police_neighbourhood_url { get; set; }

        public double Lng { get; set; }
        public double Lat { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }

    public class Metres
    {
        public List<double> coordinates { get; set; }
        public string type { get; set; }
    }

    public class Postcode
    {
        public string country { get; set; }
        public string code { get; set; }
        public Ward ward { get; set; }
        public string crime_area_id { get; set; }
        public double lng { get; set; }
        public int id { get; set; }
        public string nhs_region { get; set; }
        public Metres metres { get; set; }
        public string county_id { get; set; }
        public int council_id { get; set; }
        public string nhs_health_authority { get; set; }
        public int quality { get; set; }
        public double lat { get; set; }
        public int ward_id { get; set; }
    }
    
    public class PostcodeRootObject
    {
        public Postcode postcode { get; set; }
    }
    public class WardRootObject
    {
        public Ward ward { get; set; }
    }
    public class CouncilRootObject
    {
        public Council council { get; set; }
    }
    public class CouncilsRootObject
    {
        public Council[] councils { get; set; }
    }
    
    
}
