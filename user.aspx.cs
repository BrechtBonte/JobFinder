using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void Page_Init(object sender, EventArgs e) {

        int id;

        if (Request.QueryString["id"] == null || !int.TryParse(Request.QueryString["id"], out id) || !global::User.Exists(id)) {

            Response.Redirect("users.aspx");

        } else if (!IsPostBack) {

            User usr = global::User.GetUser(id);

            litTitle.Text = usr.Firstname + " " + usr.Lastname;

            profilePic.ImageUrl = "files/users/imgs/" + usr.ImageName;
            firstname.Text = usr.Firstname;
            lastname.Text = usr.Lastname;

            if (usr.Description != null) {
                descrTextPanel.Text = "<p>" + string.Join("</p>\n<p>", usr.Description.Split('\n')) + "</p>\n";
            }

            if (Session["LoggerID"] != null) {

                contactViews.ActiveViewIndex = 1;
                buttonPanel.Visible = true;

                if (usr.ShowMail) {
                    mailPanel.Visible = true;
                    emailaddress.Text = usr.GetEmail();
                    emailLink.CommandArgument = "mailto:" + usr.GetEmail();
                }

                phonenr.Text = usr.Telephone;

                Logger log;
                if((log = Logger.GetLogger(Session["LoggerID"])) != null && log.IsUser) {
                    pmPanel.Visible = true;
                    PrivateMessageUser.PostBackUrl = "inbox.aspx?mode=new&to=" + usr.ID;
                }
                CVUser.CommandArgument = usr.Cv;
            }

            if(usr.GetTags().Count > 0) tagPanel.Text = string.Format("<p class=\"tag\">{0}</p>\n", string.Join("</p>\n<p class=\"tag\">", usr.GetTags()));
        }
    }


    protected void CVUser_Click(object sender, CommandEventArgs e) {
        Response.Redirect("/files/users/cvs/" + e.CommandArgument);
    }

    protected void emailLink_Command(object sender, CommandEventArgs e) {
        Response.Redirect(e.CommandArgument.ToString());
    }
}