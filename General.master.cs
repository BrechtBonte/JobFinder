using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public enum MainNavItems { JobOffers, Companies, Users }

public partial class General : System.Web.UI.MasterPage {

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void Page_Init(object sender, EventArgs e) {

        Logger log;

        if (!IsPostBack && Session["LoggerID"] != null && (log = Logger.GetLogger(Session["LoggerID"])) != null) {

            if (log.IsUser) {

                accountViews.ActiveViewIndex = 1;

                int messages = log.User.CountNewMessages();

                if (messages != 0)
                    MessagesLabel.Text = " (" + messages + ")";

                userLinkName.Text = log.Name;

                profileLink.PostBackUrl = "user.aspx?id=" + log.User.ID;

            } else {

                accountViews.ActiveViewIndex = 2;

                companyLinkLabel.Text = log.Name;

                companyProfileLink.PostBackUrl = "company.aspx?id=" + log.Company.ID;
            }
        }
    }

    protected void logoutLink_Click(object sender, EventArgs e) {
        Session["LoggerID"] = null;
        Response.Redirect("~/default.aspx");
    }

    public void SetActive(MainNavItems item) {
        offersLink.CssClass = "";
        companiesLink.CssClass = "";
        usersLink.CssClass = "";

        switch (item) {
            case MainNavItems.JobOffers :
                offersLink.CssClass = "active";
                break;
            case MainNavItems.Companies :
                companiesLink.CssClass = "active";
                break;
            case MainNavItems.Users :
                usersLink.CssClass = "active";
                break;
        }
    }
}
