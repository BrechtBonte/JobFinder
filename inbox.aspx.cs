using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class inbox : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void Page_Init(object sender, EventArgs e) {

        int id;
        User usr;

        if (Session["LoggerID"] == null || !int.TryParse(Session["LoggerID"].ToString(), out id) || (usr = global::User.GetUser(id)) == null) {
            Response.Redirect("Default.aspx");
        } else if(!IsPostBack) {

            if (Request.QueryString["mode"] == "new") ShowSendView(usr);

            int messageId;
            Message message;

            if (Request.QueryString["mode"] == "show" && int.TryParse(Request.QueryString["id"].ToString(), out messageId) && (message = Message.GetMessage(messageId)) != null) {
                ShowMessage(usr, message);
                return;
            }

            ShowInbox(usr);
        }
    }


    #region - Functions -

    private void SendMessage(User usr) {

        ShowMessage(usr, usr.SendMessage(global::User.GetUser(txtTo.Text), txtSubject.Text, txtMessage.Text));

        txtTo.Text = "";
        txtSubject.Text = "";
        txtMessage.Text = "";
    }

    #endregion


    #region - Loaders -

    private void ShowInbox(User usr) {

        typeSelecter.CssClass = "inbox";

        inboxMessages.DataSource = usr.GetInbox();
        inboxMessages.DataBind();

        lnkInbox.Text = "inbox";
        if (usr.CountNewMessages() > 0) lnkInbox.Text += " (" + usr.CountNewMessages() + ")";
        lnkSend.CommandName = "mode";
        lnkSend.Text = "New";
        lnkSend.CssClass = "";
        lnkSend.CausesValidation = false;
        lnkInbox.CssClass = "opened";
        lnkSent.CssClass = "";
        inboxViews.ActiveViewIndex = 0;
    }

    private void ShowSent(User usr) {

        typeSelecter.CssClass = "sent";

        inboxMessages.DataSource = usr.GetSent();
        inboxMessages.DataBind();

        lnkInbox.Text = "inbox";
        if (usr.CountNewMessages() > 0) lnkInbox.Text += " (" + usr.CountNewMessages() + ")";
        lnkSend.CommandName = "mode";
        lnkSend.Text = "New";
        lnkSend.CssClass = "";
        lnkSend.CausesValidation = false;
        lnkInbox.CssClass = "";
        lnkSent.CssClass = "opened";
        inboxViews.ActiveViewIndex = 0;
    }

    private void ShowMessage(User usr, Message message) {

        message.SetRead(true);


        Subject.Text = message.Subject;

        ViewFrom.CommandArgument = message.FromId.ToString();
        ViewFrom.Text = message.GetFrom().Firstname + " " + message.GetFrom().Lastname;
        
        ViewTo.CommandArgument = message.ToId.ToString();
        ViewTo.Text = message.GetTo().Firstname + " " + message.GetTo().Lastname;

        lnkReply.CommandArgument = message.FromId.ToString();
        litMessage.Text = "<p>" + string.Join("</p>\n<p>", message.Message1.Split('\n')) + "</p>\n";


        lnkInbox.Text = "inbox";
        if (usr.CountNewMessages() > 0) lnkInbox.Text += " (" + usr.CountNewMessages() + ")";
        lnkSend.CommandName = "mode";
        lnkSend.Text = "New";
        lnkSend.CssClass = "";
        lnkSend.CausesValidation = false;
        lnkInbox.CssClass = "";
        lnkSent.CssClass = "";
        inboxViews.ActiveViewIndex = 1;
    }

    private void ShowSendView(User usr) {

        lnkSend.CommandName = "Send";
        lnkSend.Text = "Send";
        lnkSend.CssClass = "SendBtn";
        lnkSend.CausesValidation = true;

        lnkInbox.Text = "inbox";
        if (usr.CountNewMessages() > 0) lnkInbox.Text += " (" + usr.CountNewMessages() + ")";
        lnkInbox.CssClass = "";
        lnkSent.CssClass = "";
        inboxViews.ActiveViewIndex = 2;
    }

    private void ShowSendView(User usr, User to) {

        ShowSendView(usr);

        txtTo.Text = to.Firstname + " " + to.Lastname;
    }

    #endregion


    #region - User Interaction -

    protected void lnkInbox_Command(object sender, CommandEventArgs e) {

        int id;
        User usr;

        if (Session["LoggerID"] == null || !int.TryParse(Session["LoggerID"].ToString(), out id) || (usr = global::User.GetUser(id)) == null) {
            Response.Redirect("Default.aspx");
        } else {

            if (e.CommandName == "mode") {
                switch (e.CommandArgument.ToString()) {

                    case "inbox": ShowInbox(usr); break;
                    case "sent": ShowSent(usr); break;
                    case "new": ShowSendView(usr); break;
                }
            } else if (e.CommandName == "Send") {

                SendMessage(usr);
            }
        }
    }

    protected void inboxMessages_ItemCommand(object source, RepeaterCommandEventArgs e) {

        int id;
        User usr;

        if (Session["LoggerID"] == null || !int.TryParse(Session["LoggerID"].ToString(), out id) || (usr = global::User.GetUser(id)) == null) {
            Response.Redirect("Default.aspx");
        } else {

            int messageId = Convert.ToInt32(((HiddenField)e.Item.FindControl("HiddenId")).Value);

            ShowMessage(usr, Message.GetMessage(messageId));

        }
    }

    protected void lnkReply_Command(object sender, CommandEventArgs e) {
        
        int id;
        User usr;

        if (Session["LoggerID"] == null || !int.TryParse(Session["LoggerID"].ToString(), out id) || (usr = global::User.GetUser(id)) == null) {
            Response.Redirect("Default.aspx");
        } else {

            User to = global::User.GetUser(Convert.ToInt32(e.CommandArgument));

            ShowSendView(usr, to);
        }
    }

    protected void ViewFrom_Command(object sender, CommandEventArgs e) {
        Response.Redirect("user.aspx?id=" + e.CommandArgument);
    }

    protected void custTo_ServerValidate(object source, ServerValidateEventArgs args) {

        if (!global::User.Exists(txtTo.Text)) args.IsValid = false;
    }

    #endregion
}