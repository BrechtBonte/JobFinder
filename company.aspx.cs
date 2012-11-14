using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class company : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void Page_Init(object sender, EventArgs e) {

        int id;

        if (Request.QueryString["id"] == null || !int.TryParse(Request.QueryString["id"], out id) || !Company.Exists(id)) {

            Response.Redirect("companies.aspx");

        } else if (!IsPostBack) {

            Company comp = Company.GetCompany(id);

            companyLogo.ImageUrl = "files/companies/imgs/" + comp.Logo;

            companyDescription.Text = "<p>" + string.Join("</p>\n<p>", comp.Description.Split('\n')) + "</p>\n";

            litTitle.Text = comp.Name;
            companyName.Text = comp.Name;
            lblStreet.Text = comp.Street;
            lblCity.Text = comp.City;
            lblRegion.Text = comp.GetRegion();
            lnkEmail.Text = comp.GetEmail();
            lnkWebsite.Text = comp.Website;

            lnkEmail.CommandArgument = "mailto:" + comp.Email;
            lnkWebsite.CommandArgument = "http://" + comp.Website;


            offerRepeater.DataSource = comp.GetJobs();
            offerRepeater.DataBind();
        }
    }

    protected void Redir(object sender, CommandEventArgs e) {
        Response.Redirect(e.CommandArgument.ToString());
    }

    protected void offerTab_Click(object sender, EventArgs e) {
        offerTab.CssClass = "tab open";
        aboutTab.CssClass = "tab";

        companyTab.ActiveViewIndex = 1;
    }

    protected void aboutTab_Click(object sender, EventArgs e) {
        offerTab.CssClass = "tab";
        aboutTab.CssClass = "tab open";

        companyTab.ActiveViewIndex = 0;
    }

    protected void offerRepeater_ItemCommand(object source, RepeaterCommandEventArgs e) {
        if (e.CommandName == "more") {
            Response.Redirect("joboffer.aspx?id=" + ((HiddenField)e.Item.FindControl("HiddenId")).Value);
        }
    }
}