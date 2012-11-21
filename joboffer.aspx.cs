using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class joboffer : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void Page_Init(object sender, EventArgs e) {

        int id;

        if (Request.QueryString["id"] == null || !int.TryParse(Request.QueryString["id"], out id) || !JobOffer.Exists(id)) {

            Response.Redirect("joboffers.aspx");

        } else if (!IsPostBack) {

            JobOffer job = JobOffer.GetOffer(id);

            litTitle.Text = job.Title + " at " + job.Company.Name;

            imageLink.CommandArgument = job.Company.ID.ToString();
            companyLogo.ImageUrl = "files/companies/imgs/" + job.Company.Logo;
            companyName.CommandArgument = job.Company.ID.ToString();
            companyName.Text = job.Company.Name;

            jobTitle.Text = job.Title;
            lblRegion.Text = job.GetRegion().Name;
            litDescription.Text = "<p>" + string.Join("</p>\n<p>", job.Description.Split('\n')) + "</p>\n";
            if(job.GetTags().Count > 0) litTags.Text = "<p class=\"tag\">" + string.Join("</p>\n<p class=\"tag\">", job.GetTags()) + "</p>\n";

            if (Session["LoggerID"] != null && Logger.GetLogger(Session["LoggerID"]).IsUser) {

                User usr = Logger.GetLogger(Session["LoggerID"]).User;

                InteractionCont.Visible = true;

                SaveApplication.CommandArgument = id.ToString();
                if (usr.HasSaved(job)) {
                    SaveApplication.CommandName = SaveApplication.Text = "Unsave";
                } else {
                    SaveApplication.CommandName = SaveApplication.Text = "Save";
                }

                Apply.CommandArgument = id.ToString();

                mailLink.CommandArgument = job.GetEMail();
                mailLink.Text = job.GetEMail();
            }
        }
    }


    protected void imageLink_Command(object sender, CommandEventArgs e) {
        Response.Redirect("company.aspx?id=" + e.CommandArgument);
    }

    protected void SaveApplication_Command(object sender, CommandEventArgs e) {
        if (Session["LoggerID"] != null) {
            if (e.CommandName == "Save") {
                Logger.GetLogger(Session["LoggerID"]).User.SaveJobOffer(
                    JobOffer.GetOffer(Convert.ToInt32(e.CommandArgument))
                );
            } else {
                Logger.GetLogger(Session["LoggerID"]).User.Unsave(
                    JobOffer.GetOffer(Convert.ToInt32(e.CommandArgument))
                );
            }
        }

        Response.Redirect(Request.RawUrl);
    }

    protected void Apply_Command(object sender, CommandEventArgs e) {
        Response.Redirect(string.Format("apply.aspx?offer={0}", e.CommandArgument));
    }

    protected void mailLink_Command(object sender, CommandEventArgs e) {
        Response.Redirect("mailto:" + e.CommandArgument);
    }
}