using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) {
    }

    protected void Page_Init(object sender, EventArgs e) {
    }

    protected void exEmail_ServerValidate(object source, ServerValidateEventArgs args) {
        if (this.IsValid && !Logger.Exists(txtEmail.Text)) {
            args.IsValid = false;
        }
    }

    protected void verEmail_ServerValidate(object source, ServerValidateEventArgs args) {
        if (this.IsValid && !Logger.GetLogger(txtEmail.Text).Activated) {
            args.IsValid = false;
        }
    }

    protected void valPass_ServerValidate(object source, ServerValidateEventArgs args) {
        if (this.IsValid && !Logger.GetLogger(txtEmail.Text).CheckPassword(txtPass.Text)) {
            args.IsValid = false;
        }
    }
    protected void loginButton_Click(object sender, EventArgs e) {

        if (Page.IsValid) {

            Session["LoggerID"] = Logger.GetLogger(txtEmail.Text).ID;
            Response.Redirect("default.aspx");
        }
    }
}