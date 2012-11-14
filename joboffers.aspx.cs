using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class joboffers : System.Web.UI.Page {

    private const int RESULTS = 8;

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void Page_Init(object sender, EventArgs e) {

        if (Request.QueryString["mode"] == "browse") {

            // Build Filter
            regionPanel.Controls.Clear();

            List<Region> regions = new List<Region>();
            if (Request.QueryString[null] == null || !Request.QueryString[null].Split(',').Contains("filterRegions")) regions = Region.GetRegions();
            else {
                foreach (string key in Request.QueryString[null].Split(',')) {
                    if (key.Substring(0, 5) == "check") {
                        int id = Convert.ToInt32(key.Substring(5));
                        regions.Add(Region.GetRegion(id));
                    }
                }
            }

            foreach (Region reg in Region.GetRegions()) {
                CheckBox ch = new CheckBox();
                ch.Checked = regions.Any(r => r.Name == reg.Name);
                ch.Text = reg.Name;
                ch.ID = "check" + reg.ID;

                regionPanel.Controls.Add(ch);
                regionPanel.Controls.Add(new LiteralControl("<br />"));
            }

            if (!this.IsPostBack) {

                if (Request.QueryString["q"] != null) {
                    Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) + "?mode=browse&title=" + HttpUtility.UrlEncode(Request.QueryString["q"]));
                }

                // Page
                int temp;
                int page = Request.QueryString["page"] == null || !int.TryParse(Request.QueryString["page"], out temp) ? 1 : temp;


                // Persistence
                string title = Request.QueryString["title"] == null ? "" : Request.QueryString["title"];
                txtTitle.Text = title;


                // Results
                resultRepeater.DataSource = JobOffer.FindOffers(title, regions, (page - 1) * RESULTS, RESULTS);
                resultRepeater.DataBind();

                int count = JobOffer.FindOffers(title, regions).Count;

                if (count > RESULTS) {

                    if (page > 1) {
                        LinkButton first = new LinkButton();
                        first.CommandName = "first";
                        first.Command += page_command;
                        first.Text = "&lt;&lt;";

                        LinkButton prev = new LinkButton();
                        prev.CommandName = "prev";
                        prev.Command += page_command;
                        prev.Text = "&lt;";

                        Page.Controls.Add(first);
                        Page.Controls.Add(prev);
                    }

                    for (int i = 1; page - 1 <= Math.Ceiling((double)(count - 1) / (double)RESULTS); i++) {

                        LinkButton pg = new LinkButton();
                        pg.CommandName = "page";
                        pg.CommandArgument = i.ToString();
                        pg.Command += page_command;
                        pg.Text = i.ToString();
                        pg.CssClass = "page";

                        Page.Controls.Add(pg);
                    }

                    if (page - 1 == Math.Ceiling((double)(count - 1) / (double)RESULTS)) {
                        LinkButton next = new LinkButton();
                        next.CommandName = "next";
                        next.Command += page_command;
                        next.Text = "&gt;";

                        LinkButton last = new LinkButton();
                        last.CommandName = "last";
                        last.Command += page_command;
                        last.Text = "&gt;&gt;";

                        Page.Controls.Add(next);
                        Page.Controls.Add(last);
                    }
                }
            }
        }

        if (!IsPostBack && Session["LoggerID"] != null && Logger.GetLogger(Session["LoggerID"]).IsUser) {

            registerViews.ActiveViewIndex = 1;

            if (Request.QueryString["mode"] == "saved") {
                filterPanel.Visible = false;

                resultRepeater.DataSource = Logger.GetLogger(Session["LoggerID"]).User.GetSavedJobs();
                resultRepeater.DataBind();
            }
        }
    }


    #region - Interaction -

    protected void lnkRegister_Click(object sender, EventArgs e) {

    }

    protected void ShowSaved(object sender, EventArgs e) {
        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) + "?mode=saved");
    }

    protected void lnkFilter_Click(object sender, EventArgs e) {
        StringBuilder sb = new StringBuilder(Request.Url.GetLeftPart(UriPartial.Path));

        sb.AppendFormat("?mode=browse&title={0}", HttpUtility.UrlEncode(txtTitle.Text));

        bool added = false;

        foreach (Control cont in regionPanel.Controls) {
            if (cont.GetType() == typeof(CheckBox)) {
                CheckBox ch = cont as CheckBox;
                if (ch.Checked) {
                    sb.AppendFormat("&{0}", ch.ID);
                    if (!added) {
                        sb.Append("&filterRegions");
                        added = true;
                    }
                }
            }
        }

        Response.Redirect(sb.ToString());
    }

    protected void page_command(object sender, CommandEventArgs e) {

        int page = 0;

        switch (e.CommandName) {

            case "first": page = 0; break;
            case "prev": page = Convert.ToInt32(Request.QueryString["page"]) - 1; break;
            case "page": page = Convert.ToInt32(e.CommandArgument); break;
            case "next": page = Convert.ToInt32(Request.QueryString["page"]) + 1; break;
            case "last":
                int count = global::User.FindUsers(Request.QueryString["name"]).Count;
                page = Convert.ToInt32(Math.Ceiling((double)(count - 1) / (double)RESULTS));
                break;
        }

        UriBuilder u = new UriBuilder(Request.Url);
        NameValueCollection nv = new NameValueCollection(Request.QueryString);

        nv["page"] = page.ToString();

        StringBuilder sb = new StringBuilder();
        foreach (string k in nv.Keys)
            sb.AppendFormat("&{0}={1}", k, nv[k]);

        u.Query = sb.ToString();

        Response.Redirect(u.Uri.ToString());
    }

    #endregion
}