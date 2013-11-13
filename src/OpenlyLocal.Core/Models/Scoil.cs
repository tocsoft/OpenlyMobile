using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenlyLocal.Core.Models
{
    public class CouncilSimple : INamedLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
        public string Country { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Council : CouncilSimple
    {
        public List<Member> Members { get; set; }
        public List<WardSimple> Wards { get; set; }
        public Dictionary<string, float> MemberParties { get; set; }
        public Dictionary<string, float> ReligionStats { get; set; }
        public Dictionary<string, float> DemographicsStats { get; set; }
        public Dictionary<string, float> SpendingStats { get; set; }
        public Dictionary<string, float> EconomicStats { get; set; }
    }

    public class Member
    {
        public string Name { get; set; }
        public string Party { get; set; }
        public int Id { get; set; }
    }

    public class WardSimple
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }

    public class Ward : WardSimple
    {
        public List<Member> Members { get; set; }
        public GeoPolygon BoundaryLine { get; set; }

        public Dictionary<string, float> MemberParties { get; set; }
        public Dictionary<string, float> ReligionStats { get; set; }
        public Dictionary<string, float> DemographicsStats { get; set; }
        public Dictionary<string, float> SpendingStats { get; set; }
        public Dictionary<string, float> EconomicStats { get; set; }
    }
}
