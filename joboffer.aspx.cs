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
            litDescription.Text = string.Format("<p>{0}</p>\n", string.Join("</p>\n<p>", job.Description.Split('\n')));
            tagsRepeater.DataSource = job.GetTags();
            tagsRepeater.DataBind();

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


            Logger log;

            if (Session["LoggerID"] != null && (log = Logger.GetLogger(Session["LoggerID"])) != null && log.IsCompany && job.CompanyId == log.CompanyId) {

                // Set visible
                editInfo.Visible = true;
                editDescr.Visible = true;
                editTags.Visible = true;

                // Add ids
                cancelInfo.CommandArgument = job.ID.ToString();
                submitInfo.CommandArgument = job.ID.ToString();
                cancelDescr.CommandArgument = job.ID.ToString();
                submitDescr.CommandArgument = job.ID.ToString();
                cancelTags.CommandArgument = job.ID.ToString();
                submitTag.CommandArgument = job.ID.ToString();

                // Create lists
                lstRegion.Items.Add(new ListItem("-- Select a region --", "-1"));
                foreach (Region reg in Region.GetRegions()) {
                    lstRegion.Items.Add(new ListItem(reg.Name, reg.ID.ToString()));
                }
                lstContact.Items.Add(new ListItem("-- Select a contact --", "-1"));
                foreach (User usr in global::User.GetAll().OrderBy(u => u.Lastname).ThenBy(u => u.Firstname)) {
                    lstContact.Items.Add(new ListItem(string.Format("{0}, {1}", usr.Lastname, usr.Firstname), usr.ID.ToString()));
                }

                // Persist info
                txtTitle.Text = job.Title;
                if (job.AlternateRegionId != null) lstRegion.SelectedIndex = lstRegion.Items.IndexOf(lstRegion.Items.FindByValue(job.AlternateRegionId.ToString()));
                if (job.ContactId != null) lstContact.SelectedIndex = lstContact.Items.IndexOf(lstContact.Items.FindByValue(job.ContactId.ToString()));
                txtDescr.Text = job.Description;
                tagsEditRepeater.DataSource = job.GetTags();
                tagsEditRepeater.DataBind();
            }
        }
    }


    #region - User Interaction -

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

    #endregion


    #region - Edit -

    protected void Show_Command(object sender, CommandEventArgs e) {

        switch (e.CommandName) {

            case "info": infoEdit.ActiveViewIndex = 1; break;
            case "description": descrEdit.ActiveViewIndex = 1; break;
            case "tags": tagsEdit.ActiveViewIndex = 1; break;
        }
    }

    protected void Cancel_Command(object sender, CommandEventArgs e) {

        int id;
        JobOffer job;

        if (int.TryParse(e.CommandArgument.ToString(), out id) && (job = JobOffer.GetOffer(id)) != null) {

            switch (e.CommandName) {

                case "info":
                    txtTitle.Text = job.Title;
                    lstRegion.SelectedIndex = job.AlternateRegionId != null ? lstRegion.Items.IndexOf(lstRegion.Items.FindByValue(job.AlternateRegionId.ToString())) : 0;
                    lstContact.SelectedIndex = job.ContactId != null ? lstContact.Items.IndexOf(lstContact.Items.FindByValue(job.ContactId.ToString())) : 0;
                    infoEdit.ActiveViewIndex = 0;
                    break;

                case "description":
                    txtDescr.Text = job.Description;
                    descrEdit.ActiveViewIndex = 0;
                    break;

                case "tags":
                    tagsRepeater.DataSource = job.GetTags();
                    tagsRepeater.DataBind();
                    tagsEdit.ActiveViewIndex = 0;
                    break;
            }
        }
    }

    protected void Edit_Command(object sender, CommandEventArgs e) {

        int id;
        JobOffer job;

        if (int.TryParse(e.CommandArgument.ToString(), out id) && (job = JobOffer.GetOffer(id)) != null) {

            switch (e.CommandName) {

                case "info":
                    Page.Validate("info");
                    if (Page.IsValid) {
                        int? region = lstRegion.SelectedValue == "-1" ? null : (int?)Convert.ToInt32(lstRegion.SelectedValue);
                        int? contact = lstContact.SelectedValue == "-1" ? null : (int?)Convert.ToInt32(lstContact.SelectedValue);
                        job.UpdateInfo(txtTitle.Text, region, contact);
                        jobTitle.Text = job.Title;
                        lblRegion.Text = job.GetRegion().Name;
                        mailLink.CommandArgument = job.GetEMail();
                        mailLink.Text = job.GetEMail();
                        infoEdit.ActiveViewIndex = 0;
                    }
                    break;

                case "description":
                    Page.Validate("description");
                    if (Page.IsValid) {
                        job.UpdateDescr(txtDescr.Text);
                        litDescription.Text = string.Format("<p>{0}</p>\n", string.Join("</p>\n<p>", job.Description.Split('\n')));
                        descrEdit.ActiveViewIndex = 0;
                    }
                    break;

                case "tags":
                    Page.Validate("tags");
                    if (Page.IsValid) {

                        job.AddTag(txtTag.Text.Trim());
                        txtTag.Text = "";
                        tagsEditRepeater.DataSource = job.GetTags();
                        tagsEditRepeater.DataBind();
                    }
                    break;
            }
        }
    }

    protected void tagsEditRepeater_ItemCommand(object source, RepeaterCommandEventArgs e) {

        int id;
        JobOffer offer;
        Logger log;

        if (int.TryParse(Request.QueryString["id"].ToString(), out id) && (offer = JobOffer.GetOffer(id)) != null &&
            Session["LoggerID"] != null && (log = Logger.GetLogger(Session["LoggerID"])) != null && log.IsCompany && offer.CompanyId == log.CompanyId) {

            offer.RemoveTag(((LinkButton)e.CommandSource).CommandArgument.ToString());
            tagsEditRepeater.DataSource = offer.GetTags();
            tagsEditRepeater.DataBind();
        }
    }

    protected void custTitle_ServerValidate(object source, ServerValidateEventArgs args) {

        int id;
        JobOffer offer;

        if (IsValid && Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"].ToString(), out id) && (offer = JobOffer.GetOffer(id)) != null) {

            if (offer.Company.HasOffer(txtTitle.Text)) args.IsValid = false;
        }
    }

    protected void custTag_ServerValidate(object source, ServerValidateEventArgs args) {
        if (IsValid) {
            if (args.Value.Trim().Length > 20) args.IsValid = false;
        }
    }

    #endregion
}