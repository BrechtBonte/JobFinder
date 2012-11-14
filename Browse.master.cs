using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Browse : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void Page_Init(object sender, EventArgs e) {

        if (Request.QueryString["mode"] == "browse" || Request.QueryString["mode"] == "saved") {
            browseViews.ActiveViewIndex = 1;
        } else {
            browseViews.ActiveViewIndex = 0;
        }
    }

    protected void lnkSearch_Click(object sender, EventArgs e) {
        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) + "?mode=browse&q=" + txtQuery.Text);
    }
}
