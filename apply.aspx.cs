using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class apply : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void Page_Init(object sender, EventArgs e) {

        int id;
        JobOffer offer;
        Logger log;

        if(Request.QueryString["offer"] == null || !int.TryParse(Request.QueryString["offer"].ToString(), out id) || (offer = JobOffer.GetOffer(id)) == null) {

            Response.Redirect("joboffers.aspx");
        } else if(Session["LoggerID"] == null || (log = Logger.GetLogger(Session["LoggerID"])) == null || !log.IsUser) {

            Response.Redirect(string.Format("joboffer.aspx?id={0}", id));
        } else if(!IsPostBack) {

            litTitle.Text = lnkJob.Text = lnkOffer.Text = offer.Title;
            lnkComp.Text = lnkCompany.Text = offer.CompanyName;

            lnkJob.CommandArgument = lnkOffer.CommandArgument = offer.ID.ToString();
            lnkComp.CommandArgument = lnkCompany.CommandArgument = offer.CompanyId.ToString();

            lnkCancel.CommandArgument = lnkSubmit.CommandArgument = id.ToString();
        }
    }


    #region - Interaction -

    protected void lnkCancel_Command(object sender, CommandEventArgs e) {
        Response.Redirect(string.Format("joboffer.aspx?id={0}", e.CommandArgument));
    }

    protected void lnkSubmit_Command(object sender, CommandEventArgs e) {

        int id;
        JobOffer offer;
        Logger log;

        if (IsValid && Session["LoggerID"] != null && (log = Logger.GetLogger(Session["LoggerID"])) != null && log.IsUser && int.TryParse(e.CommandArgument.ToString(), out id) && (offer = JobOffer.GetOffer(id)) != null) {

            log.User.ApplyTo(offer, txtMotivation.Text);
            applicationViews.ActiveViewIndex = 1;
        }
    }

    protected void lnkJob_Command(object sender, CommandEventArgs e) {
        Response.Redirect(string.Format("joboffer.aspx?id={0}", e.CommandArgument));
    }

    protected void lnkComp_Command(object sender, CommandEventArgs e) {
        Response.Redirect(string.Format("company.aspx?id={0}", e.CommandArgument));
    }

    #endregion
}