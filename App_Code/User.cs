using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.SqlClient;

/// <summary>
/// Summary description for User
/// </summary>
public partial class User {

    private const int TAGS = 4;


    #region - General -

    public static User GetUser(string str) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        Logger temp = dbo.Loggers.SingleOrDefault(l => l.Email == str);

        return temp != null ? temp.User : dbo.Users.SingleOrDefault(u => str.Equals(u.Firstname + " " + u.Lastname));
    }

    public static User GetUser(int id) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Users.SingleOrDefault(u => u.ID == id);
    }

    public static bool Exists(string str) {

        return GetUser(str) != null;
    }

    public static bool Exists(int id) {

        return GetUser(id) != null;
    }

    public static bool NameExists(string name) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Users.SingleOrDefault(u => u.Firstname + " " + u.Lastname == name) != null;
    }


    public static List<User> FindUsers(string name, int start, int amount) {

        DataClassesDataContext dbo = new DataClassesDataContext();
        
        return dbo.Users.Where(u => SqlMethods.Like(u.Firstname + " " + u.Lastname, "%" + name + "%")).OrderBy(u => u.Lastname).ThenBy(u => u.Firstname).Skip(start).Take(amount).ToList();
    }


    public static List<User> FindUsers(string name) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Users.Where(u => string.Compare(u.Firstname + " " + u.Lastname, name, true) == 0).OrderBy(u => u.Lastname).ThenBy(u => u.Firstname).ToList();
    }

    public static bool ImageExists(string name) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Users.SingleOrDefault(u => u.ImageName == name) != null;
    }

    public static bool CVExists(string name) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Users.SingleOrDefault(u => u.Cv == name) != null;
    }

    public static List<User> GetAll() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Users.ToList();
    }

    #endregion


    #region - Tags -

    public string TagPs {
        get {
            if (this.GetTags().Count == 0) return "";

            return "<p class=\"tag\">" + string.Join("</p>\n<p class=\"tag\">", this.GetTags().Take(TAGS)) + "</p>";
        }
    }

    public List<Tag> GetTags() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.UserInterestedIns.Where(i => i.UserId == this.ID).Select(i => i.Tag).ToList();
    }

    #endregion


    #region - Inbox -

    public int CountNewMessages() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Messages.Where(m => m.ToId == this.ID && m.Read == false).Count();
    }

    public string GetEmail() {

        if (this.Email != null || this.Email == "") return this.Email;

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Loggers.Single(l => l.UserId == this.ID).Email;
    }

    public List<Message> GetInbox() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Messages.Where(m => m.ToId == this.ID && m.ToVisible).OrderByDescending(m => m.Sent).ToList();
    }

    public List<Message> GetSent() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Messages.Where(m => m.FromId == this.ID && m.FromVisible).OrderByDescending(m => m.Sent).ToList();
    }


    public Message SendMessage(User to, string subject, string message) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        Message mss = new Message();
        mss.FromId = this.ID;
        mss.ToId = to.ID;
        mss.Subject = subject;
        mss.Message1 = message;
        mss.FromVisible = true;
        mss.ToVisible = true;
        mss.Read = false;
        mss.Sent = DateTime.Now;

        dbo.Messages.InsertOnSubmit(mss);
        dbo.SubmitChanges();

        return mss;
    }

    #endregion


    #region - Saves -

    public void SaveJobOffer(JobOffer offer) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        UserSavesOffer save = new UserSavesOffer();

        save.UserId = this.ID;
        save.OfferId = offer.ID;

        dbo.UserSavesOffers.InsertOnSubmit(save);
        dbo.SubmitChanges();
    }

    public void Unsave(JobOffer offer) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        UserSavesOffer save = dbo.UserSavesOffers.Single(s => s.UserId == this.ID && s.OfferId == offer.ID);

        dbo.UserSavesOffers.DeleteOnSubmit(save);
        dbo.SubmitChanges();
    }

    public List<JobOffer> GetSavedJobs() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.UserSavesOffers.Where(s => s.UserId == this.ID).Select(s => s.JobOffer).ToList();
    }

    public bool HasSaved(JobOffer offer) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.UserSavesOffers.SingleOrDefault(s => s.OfferId == offer.ID && s.UserId == this.ID) != null;
    }

    #endregion


    #region - Applications -

    public void ApplyTo(JobOffer offer, string motivation) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        Application app = new Application();
        app.UserId = this.ID;
        app.OfferId = offer.ID;
        app.Motivation = motivation;
        app.Applied = DateTime.Now;

        dbo.Applications.InsertOnSubmit(app);

        dbo.SubmitChanges();
    }

    #endregion


    #region - Update -

    public void UpdateInfo(string image, string email, string phone, string cv, string descr) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        User user = dbo.Users.Single(u => u.ID == this.ID);

        if (image != null && image != "") user.ImageName = image;
        user.Email = email;
        user.Telephone = phone;
        if (cv != null && cv != "") user.Cv = cv;
        if (descr != null && descr != "") user.Description = descr;

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

        UserInterestedIn inter = new UserInterestedIn();
        inter.TagId = tag.ID;
        inter.UserId = this.ID;

        dbo.UserInterestedIns.InsertOnSubmit(inter);

        dbo.SubmitChanges();
    }

    public void RemoveTag(int id) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        UserInterestedIn inter = dbo.UserInterestedIns.SingleOrDefault(i => i.UserId == this.ID && i.TagId == id);

        if (inter != null) {

            dbo.UserInterestedIns.DeleteOnSubmit(inter);

            dbo.SubmitChanges();
        }
    }

    public void RemoveTag(string id) {

        RemoveTag(Convert.ToInt32(id));
    }

    #endregion

}