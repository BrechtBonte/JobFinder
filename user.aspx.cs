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

        if (Request.QueryString["id"] == null || !int.TryParse(Request.QueryString["id"], out id) || !Logger.UserExists(id)) {

            Response.Redirect("users.aspx");

        } else if (!IsPostBack) {

            User usr = Logger.GetUser(id);

            profilePic.ImageUrl = "files/users/imgs/" + usr.ImageName;
            firstname.Text = usr.Firstname;
            lastname.Text = usr.Lastname;

            if (usr.Description != null) {
                foreach (string line in usr.Description.Split('\n')) {
                    descrTextPanel.Controls.Add(new LiteralControl("<p>" + line + "</p>\n"));
                }
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

                PrivateMessageUser.PostBackUrl = "inbox.aspx?mode=new&to=" + usr.ID;
                CVUser.CommandArgument = usr.Cv;
            }

            foreach (Tag tag in usr.GetTags()) {

                tagPanel.Controls.Add(new LiteralControl("<p class=\"tag\">" + tag.Name + "</p>"));
            }
        }
    }


    protected void CVUser_Click(object sender, CommandEventArgs e) {
        Response.Redirect("/files/users/cvs/" + e.CommandArgument);
    }

    protected void emailLink_Command(object sender, CommandEventArgs e) {
        Response.Redirect(e.CommandArgument.ToString());
    }
}