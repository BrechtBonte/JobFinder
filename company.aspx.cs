using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class company : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }


    protected void Page_Prerender(object sender, EventArgs e) {

        if (Session["regionInd"] != null) {

            lstRegion.SelectedIndex = Convert.ToInt32(Session["regionInd"]);
        }
    }


    protected void Page_Init(object sender, EventArgs e) {

        int id;
        Logger log;

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




            if (Session["LoggerID"] != null && (log = Logger.GetLogger(Session["LoggerID"])) != null && log.IsCompany) {

                editLogo.Visible = true;
                editDescription.Visible = true;
                editInfo.Visible = true;

                txtDescription.Text = comp.Description;
                txtStreet.Text = comp.Street;
                txtCity.Text = comp.City;
                txtEmail.Text = comp.Email;
                txtWebsite.Text = comp.Website;

                foreach (Region reg in Region.GetRegions()) {
                    lstRegion.Items.Add(new ListItem(reg.Name, reg.ID.ToString()));
                }
                Session["regionInd"] = lstRegion.Items.IndexOf(lstRegion.Items.FindByValue(comp.RegionId.ToString()));
            }

        } else if (Session["LoggerID"] != null && (log = Logger.GetLogger(Session["LoggerID"])) != null && log.IsCompany) {

            foreach (Region reg in Region.GetRegions()) {
                lstRegion.Items.Add(new ListItem(reg.Name, reg.ID.ToString()));
            }
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

    protected void Unnamed_Command(object sender, CommandEventArgs e) {
        switch (e.CommandArgument.ToString()) {

            case "logo": logoEdit.ActiveViewIndex = 1; break;
            case "description": descriptionEdit.ActiveViewIndex = 1; break;
            case "info": infoEdit.ActiveViewIndex = 1; break;
        }
    }

    protected void Cancel_Command(object sender, CommandEventArgs e) {
        switch (e.CommandArgument.ToString()) {

            case "logo":
                logoEdit.ActiveViewIndex = 0;
                break;

            case "description":
                txtDescription.Text = companyDescription.Text.Replace("</p>", "").Replace("<p>", "");
                descriptionEdit.ActiveViewIndex = 0;
                break;

            case "info":
                txtStreet.Text = lblStreet.Text;
                txtCity.Text = lblCity.Text;
                txtEmail.Text = lnkEmail.Text;
                txtWebsite.Text = lnkWebsite.Text;
                infoEdit.ActiveViewIndex = 0;
                break;
        }
    }

    protected void Edit_Command(object sender, CommandEventArgs e) {

    }
}