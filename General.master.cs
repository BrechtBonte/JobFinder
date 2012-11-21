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
                    MessagesLabel.Text = string.Format(" ({0})", messages);

                userLinkName.Text = log.Name;

                profileLink.PostBackUrl = string.Format("user.aspx?id={0}", log.UserId);

            } else {

                accountViews.ActiveViewIndex = 2;

                companyLinkLabel.Text = log.Name;

                companyProfileLink.PostBackUrl = string.Format("company.aspx?id={0}", log.CompanyId);

                companyApplications.PostBackUrl = string.Format("applications.aspx?company={0}", log.CompanyId);
            }
        }
    }

    protected void logoutLink_Click(object sender, EventArgs e) {
        Session["LoggerID"] = null;
        Response.Redirect("~/default.aspx");
    }

    protected void redir(object sender, CommandEventArgs e) {
        Response.Redirect(e.CommandArgument.ToString());
    }
}
