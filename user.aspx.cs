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

            tagRepeater.DataSource = usr.GetTags();
            tagRepeater.DataBind();

            Logger logger;

            if (Session["LoggerID"] != null && (logger = Logger.GetLogger(Session["LoggerID"])) != null && logger.UserId == id) {

                // Show Edit buttons
                infoEdit.Visible = true;
                tagsEdit.Visible = true;

                // Add ID's to submit buttons
                submitInfo.CommandArgument = usr.ID.ToString();
                submitTag.CommandArgument = usr.ID.ToString();
                cancelInfo.CommandArgument = usr.ID.ToString();
                cancelTags.CommandArgument = usr.ID.ToString();

                // Persist
                txtEmail.Text = usr.Email;
                chMail.Checked = usr.ShowMail;
                txtPhone.Text = usr.Telephone;
                txtDescr.Text = usr.Description;
                tagEditRepeater.DataSource = usr.GetTags();
                tagEditRepeater.DataBind();
            }
        }
    }


    protected void CVUser_Click(object sender, CommandEventArgs e) {
        Response.Redirect("/files/users/cvs/" + e.CommandArgument);
    }

    protected void emailLink_Command(object sender, CommandEventArgs e) {
        Response.Redirect(e.CommandArgument.ToString());
    }


    #region - Edit -

    protected void Show_Command(object sender, CommandEventArgs e) {

        switch (e.CommandName) {

            case "info": editInfo.ActiveViewIndex = 1; break;
            case "tags": editTags.ActiveViewIndex = 1; break;
        }
    }

    protected void Cancel_Command(object sender, CommandEventArgs e) {

        int id;
        User usr;

        if (int.TryParse(e.CommandArgument.ToString(), out id) && (usr = global::User.GetUser(id)) != null) {

            switch (e.CommandName) {

                case "info":
                    txtEmail.Text = usr.Email;
                    txtPhone.Text = usr.Telephone;
                    txtDescr.Text = usr.Description;
                    editInfo.ActiveViewIndex = 0;
                    break;

                case "tags":
                    tagRepeater.DataSource = usr.GetTags();
                    tagRepeater.DataBind();
                    editTags.ActiveViewIndex = 0;
                    break;
            }
        }
    }

    protected void Edit_Command(object sender, CommandEventArgs e) {

        int id;
        User usr;

        if (int.TryParse(e.CommandArgument.ToString(), out id) && (usr = global::User.GetUser(id)) != null) {

            switch (e.CommandName) {

                case "info":
                    Page.Validate("info");
                    if (Page.IsValid) {

                        string img = null;
                        string cv = null;

                        if (fileImage.HasFile) {
                            string fn = System.IO.Path.GetFileName(fileImage.PostedFile.FileName);
                            img = fn;
                            int i = 0;
                            while (global::User.ImageExists(img)) {
                                i++;
                                img = i + img;
                            }
                            string loc = Server.MapPath("~/files/users/imgs/" + img);

                            try {
                                fileImage.PostedFile.SaveAs(loc);
                            } catch {
                                throw; //TODO: errorhandling
                            }
                        }

                        if (fileCv.HasFile) {
                            string fn = System.IO.Path.GetFileName(fileImage.PostedFile.FileName);
                            cv = fn;
                            int i = 0;
                            while (global::User.ImageExists(cv)) {
                                i++;
                                cv = i + cv;
                            }
                            string loc = Server.MapPath("~/files/users/cvs/" + cv);

                            try {
                                fileImage.PostedFile.SaveAs(loc);
                            } catch {
                                throw; //TODO: errorhandling
                            }
                        }


                        usr.UpdateInfo(img, txtEmail.Text, txtPhone.Text, cv, txtDescr.Text);
                        profilePic.ImageUrl = "files/users/imgs/" + usr.ImageName;
                        if (usr.Description != null) descrTextPanel.Text = string.Format("<p>{0}</p>\n", string.Join("</p>\n<p>", usr.Description.Split('\n')));
                        emailaddress.Text = usr.GetEmail();
                        emailLink.CommandArgument = "mailto:" + usr.GetEmail();
                        phonenr.Text = usr.Telephone;
                        CVUser.CommandArgument = usr.Cv;

                        editInfo.ActiveViewIndex = 0;
                    }
                    break;


                case "tags":
                    Page.Validate("tags");
                    if (Page.IsValid) {

                        usr.AddTag(txtTag.Text.Trim());
                        txtTag.Text = "";
                        tagEditRepeater.DataSource = usr.GetTags();
                        tagEditRepeater.DataBind();
                    }
                    break;
            }
        }
    }

    protected void custImage_ServerValidate(object source, ServerValidateEventArgs args) {
        if (fileImage.HasFile) {
            if (fileImage.PostedFile.ContentType.ToLower() != "image/jpg" && fileImage.PostedFile.ContentType.ToLower() != "image/jpeg" && fileImage.PostedFile.ContentType.ToLower() != "image/png")
                args.IsValid = false;
        }
    }

    protected void tagEditRepeater_ItemCommand(object source, RepeaterCommandEventArgs e) {

        int id;
        User usr;
        Logger log;

        if (int.TryParse(Request.QueryString["id"].ToString(), out id) && (usr = global::User.GetUser(id)) != null &&
            Session["LoggerID"] != null && (log = Logger.GetLogger(Session["LoggerID"])) != null && log.IsUser && usr.ID == log.UserId) {

            usr.RemoveTag(((LinkButton)e.CommandSource).CommandArgument.ToString());
            tagEditRepeater.DataSource = usr.GetTags();
            tagEditRepeater.DataBind();
        }
    }

    protected void custTag_ServerValidate(object source, ServerValidateEventArgs args) {
        if (IsValid) {
            if (args.Value.Trim().Length > 20) args.IsValid = false;
        }
    }

    #endregion
}