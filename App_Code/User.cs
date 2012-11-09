using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public partial class User {

    #region - Instance -

    public int CountNewMessages() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Messages.Where(m => m.ToId == this.ID && m.Read == false).Count();
    }

    public List<Tag> GetTags() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.UserInterestedIns.Where(i => i.UserId == this.ID).Select(i => i.Tag).ToList();
    }

    public string GetEmail() {

        if (this.Email != null || this.Email == "") return this.Email;

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Loggers.Single(l => l.UserId == this.ID).Email;
    }

    #endregion

}