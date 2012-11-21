using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class register : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void Page_Init(object sender, EventArgs e) {

        if (!IsPostBack) {

            if (Request.QueryString["type"] == "user")
                views.ActiveViewIndex = 1;

            else if (Request.QueryString["type"] == "company")
                views.ActiveViewIndex = 2;
        }
    }


    #region - Interaction -

    protected void lnkUser_Command(object sender, CommandEventArgs e) {
        switch (e.CommandArgument.ToString()) {
            case "User": views.ActiveViewIndex = 1; break;
            case "Company": views.ActiveViewIndex = 2; break;
        }
    }

    protected void exEmail_ServerValidate(object source, ServerValidateEventArgs args) {
        if (Logger.MailExists(args.Value.Trim())) args.IsValid = false;
    }

    protected void userNext_Command(object sender, CommandEventArgs e) {
        if (e.CommandArgument.ToString() == "1") {
            this.Page.Validate();
            if (!this.IsValid) return;
        }

        registerUser.ActiveViewIndex = Convert.ToInt32(e.CommandArgument);
    }

    protected void lnkSubmit_Click(object sender, EventArgs e) {
        if (IsValid) {
            string fn = System.IO.Path.GetFileName(imageUpload.PostedFile.FileName);
            string temp = fn;
            int i = 0;
            while(global::User.ImageExists(temp)) {
                i++;
                temp = i + temp;
            }
            string loc = Server.MapPath("~/files/users/imgs/" + temp);

            try {
                imageUpload.PostedFile.SaveAs(loc);
            } catch {
                throw; //TODO: errorhandling
            }

            
            string fn2 = System.IO.Path.GetFileName(fileCV.PostedFile.FileName);
            string temp2 = fn;
            int i2 = 0;
            while(global::User.CVExists(temp2)) {
                i++;
                temp2 = i2 + temp2;
            }
            string loc2 = Server.MapPath("~/files/users/cvs/" + temp2);

            try {
                imageUpload.PostedFile.SaveAs(loc2);
            } catch {
                throw; //TODO: errorhandling
            }


            Logger log = Logger.CreateUser(
                txtEmail.Text.Trim(),
                txtPassword.Text.Trim(),
                temp,
                txtFirstname.Text.Trim(),
                txtLastname.Text.Trim(),
                txtAltMail.Text.Trim(),
                txtPhone.Text.Trim(),
                temp2,
                txtDescr.Text.Trim(),
                chAltMail.Checked);

            Session["LoggerID"] = log.ID;
            lnkSuccess.CommandArgument = log.UserId.ToString();

            registerUser.ActiveViewIndex = 2;
        }
    }

    protected void lnkCompNext_Command(object sender, CommandEventArgs e) {

        int page = Convert.ToInt32(e.CommandArgument);

        if (page == 1) {
            this.Page.Validate();
            if (!this.IsValid) return;

            lstRegion.Items.Clear();
            lstRegion.Items.Add(new ListItem("-- Select Region --", "-1"));
            foreach (Region reg in Region.GetRegions()) {
                lstRegion.Items.Add(new ListItem(reg.Name, reg.ID.ToString()));
            }
        }

        companyView.ActiveViewIndex = page;
    }

    protected void exName_ServerValidate(object source, ServerValidateEventArgs args) {
        if (IsValid) {
            if (Company.Exists(args.Value.Trim())) args.IsValid = false;
        }
    }

    protected void exFullName_ServerValidate(object source, ServerValidateEventArgs args) {
        if(IsValid) {
            if (global::User.NameExists(txtFirstname.Text.Trim() + " " + txtLastname.Text.Trim())) args.IsValid = false;
        }
    }

    protected void imgImage_ServerValidate(object source, ServerValidateEventArgs args) {
        if (imageUpload.HasFile) {
            if (imageUpload.PostedFile.ContentType.ToLower() != "image/jpg" && imageUpload.PostedFile.ContentType.ToLower() != "image/jpeg" && imageUpload.PostedFile.ContentType.ToLower() != "image/png")
                args.IsValid = false;
        }
    }

    protected void imgLogo_ServerValidate(object source, ServerValidateEventArgs args) {
        if (fileLogo.HasFile) {
            if (fileLogo.PostedFile.ContentType.ToLower() != "image/jpg" && fileLogo.PostedFile.ContentType.ToLower() != "image/jpeg" && fileLogo.PostedFile.ContentType.ToLower() != "image/png")
                args.IsValid = false;
        }
    }

    protected void lnkCompSubmit_Click(object sender, EventArgs e) {
        if (IsValid) {
            string fn = System.IO.Path.GetFileName(fileLogo.PostedFile.FileName);
            string temp = fn;
            int i = 0;
            while (global::User.ImageExists(temp)) {
                i++;
                temp = i + temp;
            }
            string loc = Server.MapPath("~/files/companies/imgs/" + temp);

            try {
                fileLogo.PostedFile.SaveAs(loc);
            } catch {
                throw; //TODO: errorhandling
            }

            Logger log = Logger.CreateCompany(
                txtCompEmail.Text.Trim(),
                txtCompPassword.Text.Trim(),
                txtName.Text.Trim(),
                txtCompDescr.Text.Trim(),
                temp,
                txtWebsite.Text.Trim(),
                txtStreet.Text.Trim(),
                txtCity.Text.Trim(),
                Convert.ToInt32(lstRegion.SelectedValue)
            );

            Session["LoggerID"] = log.ID;
            lnkCompSuccess.CommandArgument = log.Company.ID.ToString();

            companyView.ActiveViewIndex = 2;
        }
    }

    protected void lnkSuccess_Command(object sender, CommandEventArgs e) {
        Response.Redirect("user.aspx?id=" + e.CommandArgument);
    }

    protected void lnkCompSuccess_Command(object sender, CommandEventArgs e) {
        Response.Redirect("company.aspx?id=" + e.CommandArgument);
    }

    #endregion
}