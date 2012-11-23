using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page {

    private const int OFFERS = 6;

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void Page_Init(object sender, EventArgs e) {

        if (!IsPostBack) {

            lblCompanies.Text = Company.Count().ToString();
            lblOffers.Text = JobOffer.Count().ToString();

            jobRepeater.DataSource = JobOffer.GetLatest(OFFERS);
            jobRepeater.DataBind();

            regionRepeater.DataSource = Region.GetRegions();
            regionRepeater.DataBind();

            Logger log;

            if (Session["LoggerID"] != null && (log = Logger.GetLogger(Session["LoggerID"])) != null) {

                if (log.IsUser) {

                    lblNewOffers.Text = log.CountOffers().ToString();
                    loggedViews.ActiveViewIndex = 1;

                } else if (log.IsCompany) {

                    lblNewApplications.Text = log.CountApplications().ToString();
                    loggedViews.ActiveViewIndex = 2;

                }
            }
        }
    }



    protected void lnkSearch_Click(object sender, EventArgs e) {
        Response.Redirect(string.Format("joboffers.aspx?mode=browse&q={0}", txtQuery.Text));
    }
}