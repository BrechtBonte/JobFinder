using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class application : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_Init(object sender, EventArgs e) {

        if (!IsPostBack) {

            Logger log;
            int id;
            Application appl;

            if (Request.QueryString["id"] == null || !int.TryParse(Request.QueryString["id"], out id) || (appl = global::Application.GetApplication(id)) == null) {

                Response.Redirect("joboffers.aspx");
            } else if (Session["LoggerID"] == null || (log = Logger.GetLogger(Session["LoggerID"])) == null || !log.IsCompany || appl.JobOffer.CompanyId != log.Company.ID) {

                Response.Redirect(string.Format("joboffer.aspx?id={0}", appl.OfferId));
            } else {

                litTitle.Text = appl.JobOffer.Title;

                lnkUser.CommandArgument = appl.UserId.ToString();
                lnkTitle.Text = appl.JobOffer.Title;
                lnkTitle.CommandArgument = appl.OfferId.ToString();

                lnkImg.ImageUrl = string.Format("files/users/imgs/{0}", appl.User.ImageName);
                lblFirstname.Text = appl.User.Firstname;
                lblLastname.Text = appl.User.Lastname;
                descrTextPanel.Text = string.Format("<p>{0}</p>\n", string.Join("</p>\n<p>", appl.User.Description.Split('\n')));

                if (appl.User.ShowMail) {
                    mailPanel.Visible = true;
                    lblMail.Text = appl.User.Email;
                }
                lblPhone.Text = appl.User.Telephone;

                tagPanel.Text = appl.User.TagPs;


                motivationPanel.Text = string.Format("<p>{0}</p>\n", string.Join("</p>\n<p>", appl.Motivation.Split('\n')));
            }
        }
    }


    #region - Interaction -

    protected void lnkUser_Command(object sender, CommandEventArgs e) {
        Response.Redirect(string.Format("user.aspx?id={0}", e.CommandArgument));
    }

    #endregion
}