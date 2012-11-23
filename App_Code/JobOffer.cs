using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.SqlClient;

/// <summary>
/// Summary description for JobOffer
/// </summary>
public partial class JobOffer {

    private const int DESCR_LENGTH = 250;
    private const int TAGS = 6;

    #region - General -

    public static JobOffer GetOffer(int id) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.JobOffers.SingleOrDefault(j => j.ID == id);
    }

    public static JobOffer GetOffer(Company comp, string title) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.JobOffers.SingleOrDefault(o => o.CompanyId == comp.ID && o.Title == title);
    }

    public static bool Exists(int id) {

        return GetOffer(id) != null;
    }


    public static List<JobOffer> FindOffers(string title, IEnumerable<Region> regions, DateTime? date) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        var offers = dbo.JobOffers.Where(o => SqlMethods.Like(o.Title, "%" + title + "%"));

        if (regions != null && regions.Count() > 0) offers = offers.Where(o => regions.Select(r => r.Name).Contains(o.Region.Name) || (o.AlternateRegionId == null && regions.Select(r => r.Name).Contains(o.Company.Region.Name)));

        if (date != null) offers = offers.Where(o => o.Added >= date);

        return offers.OrderByDescending(o => o.Added).ToList<JobOffer>();
    }

    public static List<JobOffer> FindOffers(string title, IEnumerable<Region> regions) {

        return FindOffers(title, regions, null);
    }

    public static List<JobOffer> FindOffers(string title, IEnumerable<Region> regions, DateTime? date, int start, int amount) {

        return FindOffers(title, regions, date).Skip(start).Take(amount).ToList();
    }

    public static List<JobOffer> FindOffers(string title, IEnumerable<Region> regions, int start, int amount) {

        return FindOffers(title, regions, null, start, amount);
    }

    public static int Count() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.JobOffers.Count();
    }

    public static List<JobOffer> GetLatest(int count) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.JobOffers.OrderByDescending(o => o.Added).Take(count).ToList();
    }

    #endregion


    #region - Properties -

    public string FirstLine {
        get {
            return this.Description.Length <= DESCR_LENGTH ? this.Description : this.Description.Substring(0, DESCR_LENGTH - 3) + "&hellip;";
        }
    }

    public string TagPs {
        get {
            if (this.GetTags().Count == 0) return "";

            return "<p class=\"tag\">" + string.Join("</p>\n<p class=\"tag\">", this.GetTags().Take(TAGS)) + "</p>";
        }
    }

    public string CompanyName { get { return this.Company.Name; } }

    public string CompanyLogo { get { return this.Company.Logo; } }

    #endregion


    #region - Instance -

    public List<Tag> GetTags() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.OfferHasTags.Where(o => o.OfferId == this.ID).Select(o => o.Tag).ToList();
    }

    public string GetEMail() {

        if (this.ContactId == null) return this.Company.GetEmail();

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Users.Single(u => u.ID == ContactId).GetEmail();
    }

    public Region GetRegion() {

        if (this.AlternateRegionId != null) return this.Region;

        else return this.Company.Region;
    }

    #endregion


    #region - Update -

    public void UpdateInfo(string title, int? region, int? contact) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        JobOffer job = dbo.JobOffers.Single(j => j.ID == this.ID);

        job.Title = title;
        job.AlternateRegionId = region;
        job.ContactId = contact;

        dbo.SubmitChanges();
    }

    public void UpdateDescr(string description) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        JobOffer job = dbo.JobOffers.Single(j => j.ID == this.ID);

        job.Description = description;

        dbo.SubmitChanges();
    }

    public void AddTag(string name) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        Tag tag = dbo.Tags.SingleOrDefault(t => System.Data.Linq.SqlClient.SqlMethods.Like(t.Name, name));

        if (tag == null) {

            tag = new Tag();
            tag.Name = name;

            dbo.Tags.InsertOnSubmit(tag);
            dbo.SubmitChanges();
        }

        OfferHasTag inter = new OfferHasTag();
        inter.TagId = tag.ID;
        inter.OfferId = this.ID;

        dbo.OfferHasTags.InsertOnSubmit(inter);

        dbo.SubmitChanges();
    }

    public void RemoveTag(int id) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        OfferHasTag inter = dbo.OfferHasTags.SingleOrDefault(i => i.OfferId == this.ID && i.TagId == id);

        if (inter != null) {

            dbo.OfferHasTags.DeleteOnSubmit(inter);

            dbo.SubmitChanges();
        }
    }

    public void RemoveTag(string id) {

        RemoveTag(Convert.ToInt32(id));
    }

    #endregion
}