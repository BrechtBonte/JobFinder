using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addjoboffer : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void Page_Init(object sender, EventArgs e) {

        Logger log;

        if (Session["LoggerID"] == null || (log = Logger.GetLogger(Session["LoggerID"])) == null || !log.IsCompany) {

            Response.Redirect("default.aspx");
        } else {


            if (!IsPostBack) {


                lstRegion.Items.Add(new ListItem("-- Select a region --", "-1"));
                foreach (Region reg in Region.GetRegions()) {
                    lstRegion.Items.Add(new ListItem(reg.Name, reg.ID.ToString()));
                }

                lstRegion.SelectedValue = "-1";


                lstContact.Items.Add(new ListItem("-- Select a contact --", "-1"));
                foreach (User usr in global::User.GetAll().OrderBy(u => u.Lastname).ThenBy(u => u.Firstname)) {
                    lstContact.Items.Add(new ListItem(string.Format("{0}, {1}", usr.Lastname, usr.Firstname), usr.ID.ToString()));
                }

                lstContact.SelectedValue = "-1";
            }
        }
    }


    #region - Interaction -

    protected void custTitle_ServerValidate(object source, ServerValidateEventArgs args) {

        Logger log;

        if (this.IsValid && Session["LoggerID"] != null && (log = Logger.GetLogger(Session["LoggerID"])) != null && log.IsCompany) {

            args.IsValid = !log.Company.HasOffer(txtTitle.Text.Trim());
        }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e) {

        Logger log;

        if (this.IsValid && Session["LoggerID"] != null && (log = Logger.GetLogger(Session["LoggerID"])) != null && log.IsCompany) {

            int? region = null;
            int? contact = null;

            if (lstRegion.SelectedValue != "-1") region = Convert.ToInt32(lstRegion.SelectedValue);
            if (lstContact.SelectedValue != "-1") contact = Convert.ToInt32(lstContact.SelectedValue);

            JobOffer offer = log.Company.AddOffer(txtTitle.Text, txtDescription.Text, region, contact);


            Response.Redirect(string.Format("joboffer.aspx?id={0}", offer.ID));
        }
    }

    #endregion
}