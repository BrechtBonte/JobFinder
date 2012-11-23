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

    public static bool Exists(string name) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Companies.SingleOrDefault(c => c.Name == name) != null;
    }

    public static bool LogoExists(string name) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Companies.SingleOrDefault(c => c.Logo == name) != null;
    }

    public static List<Company> FindCompanies(string name, int start, int amount) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Companies.Where(c => SqlMethods.Like(c.Name, "%" + name + "%")).Skip(start).Take(amount).ToList();
    }

    public static List<Company> FindCompanies(string name) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Companies.Where(c => SqlMethods.Like(c.Name, "%" + name + "%")).ToList();
    }

    public static int Count() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Companies.Count();
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

    public List<Application> GetApplications() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Applications.Where(a => a.JobOffer.CompanyId == this.ID).OrderByDescending(a => a.Applied).ToList();
    }

    public List<Application> GetApplications(int start, int amount) {

        return GetApplications().Skip(start).Take(amount).ToList();
    }

    public bool HasOffer(string title) {

        return JobOffer.GetOffer(this, title) != null;
    }

    public JobOffer AddOffer(string title, string description, int? regionId, int? contactId) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        JobOffer offer = new JobOffer();
        offer.Title = title;
        offer.Description = description;
        offer.AlternateRegionId = regionId;
        offer.ContactId = contactId;
        offer.CompanyId = this.ID;
        offer.Added = DateTime.Now;

        dbo.JobOffers.InsertOnSubmit(offer);

        dbo.SubmitChanges();

        return offer;
    }

    #endregion


    #region - Updates -

    public void UpdateLogo(string filename) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        Company comp = dbo.Companies.Single(c => c.ID == this.ID);

        comp.Logo = filename;

        dbo.SubmitChanges();
    }

    public void UpdateDescription(string description) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        Company comp = dbo.Companies.Single(c => c.ID == this.ID);

        comp.Description = description;

        dbo.SubmitChanges();
    }

    public void UpdateInfo(string street, string city, int region, string email, string website) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        Company comp = dbo.Companies.Single(c => c.ID == this.ID);

        comp.Street = street;
        comp.City = city;
        comp.RegionId = region;
        comp.Email = email;
        comp.Website = website;

        dbo.SubmitChanges();
    }

    #endregion
}