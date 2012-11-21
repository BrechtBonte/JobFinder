using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Application
/// </summary>
public partial class Application {

    #region - General -

    public static Application GetApplication(int id) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Applications.SingleOrDefault(a => a.ID == id);
    }

    #endregion


    #region - Instance -

    public string JobTitle { get { return this.JobOffer.Title; } }

    public string UserImage { get { return this.User.ImageName; } }

    public string UserName { get { return string.Format("{0} {1}", this.User.Firstname, this.User.Lastname); } }

    #endregion
}