﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class users : System.Web.UI.Page {

    public const int RESULTS = 8;

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void Page_Init(object sender, EventArgs e) {

        if (Request.QueryString["mode"] == "browse") {

            if (Request.QueryString["q"] != null) Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) + "?mode=browse&name=" + HttpUtility.UrlEncode(Request.QueryString["q"]));

            if (Request.QueryString["name"] == null) Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path));
            int temp;
            int page = Request.QueryString["page"] == null || !int.TryParse(Request.QueryString["page"], out temp) ? 1 : temp;

            if (!this.IsPostBack) {

                txtName.Text = Request.QueryString["name"];

                resultRepeater.DataSource = global::User.FindUsers(Request.QueryString["name"], (page - 1) * RESULTS, RESULTS);
                resultRepeater.DataBind();

            }

            int count = global::User.FindUsers(Request.QueryString["name"]).Count;

            if (count > RESULTS) {

                if (page > 1) {
                    LinkButton first = new LinkButton();
                    first.CommandName = "first";
                    first.Command += new CommandEventHandler(page_command);
                    first.Text = "&lt;&lt;";
                    first.ID = "firstPage";

                    LinkButton prev = new LinkButton();
                    prev.CommandName = "prev";
                    prev.Command += new CommandEventHandler(page_command);
                    prev.Text = "&lt;";
                    prev.ID = "prevPage";

                    Pages.Controls.Add(first);
                    Pages.Controls.Add(prev);
                }

                for (int i = 1; i - 1 <= Math.Ceiling((double)(count - 1) / (double)RESULTS); i++) {

                    LinkButton pg = new LinkButton();
                    pg.CommandName = "page";
                    pg.CommandArgument = i.ToString();
                    pg.Command += new CommandEventHandler(page_command);
                    pg.Text = i.ToString();
                    pg.CssClass = "page";
                    if (i == page) pg.CssClass += " active";
                    pg.ID = "page" + i.ToString();

                    Pages.Controls.Add(pg);
                }

                if (page - 1 == Math.Ceiling((double)(count - 1) / (double)RESULTS)) {
                    LinkButton next = new LinkButton();
                    next.CommandName = "next";
                    next.Command += new CommandEventHandler(page_command);
                    next.Text = "&gt;";
                    next.ID = "nextPage";

                    LinkButton last = new LinkButton();
                    last.CommandName = "last";
                    last.Command += new CommandEventHandler(page_command);
                    last.Text = "&gt;&gt;";
                    last.ID = "lastPage";

                    Pages.Controls.Add(next);
                    Pages.Controls.Add(last);
                }
            }
        }
    }


    #region - Interaction -

    protected void lnkRegister_Click(object sender, EventArgs e) {
        Response.Redirect("register.aspx?type=user");
    }

    protected void lnkFilter_Click(object sender, EventArgs e) {
        if (txtName.Text.Trim() != "") {

            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) + "?mode=browse&name=" + HttpUtility.UrlEncode(txtName.Text));
        }
    }

    protected void page_command(object sender, CommandEventArgs e) {

        int page = 1;

        switch (e.CommandName) {

            case "first": page = 1; break;
            case "prev": page = Convert.ToInt32(Request.QueryString["page"]) - 1; break;
            case "page": page = Convert.ToInt32(e.CommandArgument); break;
            case "next": page = Convert.ToInt32(Request.QueryString["page"]) + 1; break;
            case "last":
                int count = global::User.FindUsers(Request.QueryString["name"]).Count;
                page = Convert.ToInt32(Math.Ceiling((double)(count - 1) / (double)RESULTS));
                break;
        }

        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) + "?mode=browse&name=" + HttpUtility.UrlEncode(Request.QueryString["name"]) + "&page=" + page);
    }

    #endregion
}