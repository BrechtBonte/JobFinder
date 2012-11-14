using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Message
/// </summary>
public partial class Message {

    #region - General -

    public static Message GetMessage(int id) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Messages.SingleOrDefault(m => m.ID == id);
    }

    public static bool Exists(int id) {

        return GetMessage(id) != null;
    }

    #endregion


    #region - Instance -

    public string FromName { get { return this.GetFrom().Firstname + " " + this.GetFrom().Lastname; } }
    public string ToName { get { return this.GetTo().Firstname + " " + this.GetTo().Lastname; } }


    public User GetFrom() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Users.SingleOrDefault(u => u.ID == this.FromId);
    }

    public User GetTo() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Users.SingleOrDefault(u => u.ID == this.ToId);
    }

    public bool CheckUser(User usr) {

        return usr.ID == this.ToId || usr.ID == this.FromId;
    }

    public void SetRead(bool state) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        var temp = dbo.Messages.Single(m => m.ID == this.ID);

        temp.Read = true;

        dbo.SubmitChanges();
    }

    #endregion
}