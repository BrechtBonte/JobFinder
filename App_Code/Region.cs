using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Region
/// </summary>
public partial class Region {

    public static List<Region> GetRegions() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Regions.OrderBy(r => r.Name).ToList();
    }

    public static Region GetRegion(int id) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Regions.SingleOrDefault(r => r.ID == id);
    }

    public override string ToString() {
        return this.Name;
    }


    public int CountOffers {
        get {
            DataClassesDataContext dbo = new DataClassesDataContext();
            return dbo.JobOffers.Count(o => o.AlternateRegionId == this.ID || (o.AlternateRegionId == null && o.Company.RegionId == this.ID));
        }
    }
}