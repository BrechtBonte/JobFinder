using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.SqlClient;

/// <summary>
/// Summary description for Company
/// </summary>
public partial class Company {

    #region - General -

    public static Company GetCompany(int id) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Companies.SingleOrDefault(c => c.ID == id);
    }

    public static bool Exists(int id) {

        return GetCompany(id) != null;
    }

    public static List<Company> FindCompanies(string name, int start, int amount) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Companies.Where(c => SqlMethods.Like(c.Name, "%" + name + "%")).Skip(start).Take(amount).ToList();
    }

    public static List<Company> FindCompanies(string name) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Companies.Where(c => SqlMethods.Like(c.Name, "%" + name + "%")).ToList();
    }

    #endregion


    #region - Instance -

    public bool Hiring { get { return this.JobOffers.Count > 0; } }

    public List<JobOffer> GetJobs() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.JobOffers.Where(o => o.CompanyId == this.ID).ToList();
    }

    public string GetRegion() {

        return this.Region.Name;
    }

    public string GetEmail() {

        if (this.Email != null && this.Email != "") return this.Email;

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Loggers.Single(l => l.CompanyId == this.ID).Email;
    }

    #endregion
}