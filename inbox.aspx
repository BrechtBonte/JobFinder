<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="inbox.aspx.cs" Inherits="inbox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Inbox - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/inbox.css" />
    <link rel="stylesheet" href="css/forms.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section id="tabNavs" class="clearfix">
        <p class="tab"><asp:LinkButton ID="lnkInbox" CausesValidation="false" CommandName="mode" CommandArgument="inbox" OnCommand="lnkInbox_Command" runat="server" /></p>
        <p id="sentTab" class="tab"><asp:LinkButton ID="lnkSent" Text="Sent" CausesValidation="false" CommandName="mode" CommandArgument="sent" OnCommand="lnkInbox_Command" runat="server" /></p>
        <div id="lineDiv"></div>
        <p id="rightTab" class="tab"><asp:LinkButton ID="lnkSend" CausesValidation="false" CommandName="mode" CommandArgument="new" OnCommand="lnkInbox_Command" runat="server" /></p>
    </section>
    <asp:MultiView ID="inboxViews" ActiveViewIndex="0" runat="server">
        <asp:View runat="server">
            <asp:Panel ID="typeSelecter" runat="server">
                <asp:Repeater ID="inboxMessages" OnItemCommand="inboxMessages_ItemCommand" runat="server">
                    <HeaderTemplate>
                        <section id="inboxMessageCont">
                            <table>
                                <tr>
                                    <th id="fromH" class="from">From</th>
                                    <th id="toH" class="to">To</th>
                                    <th id="subjH">Subject</th>
                                    <th id="messageH">Message</th>
                                </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="HiddenId" Value='<%# DataBinder.Eval(Container.DataItem, "ID") %>' runat="server" />
                                    <td class="from"><p><asp:LinkButton CssClass='<%# DataBinder.Eval(Container.DataItem, "Read") %>' ID="LinkButton1" runat="server"><%# DataBinder.Eval(Container.DataItem, "FromName") %></asp:LinkButton></p></td>
                                    <td class="to"><p><asp:LinkButton CssClass='<%# DataBinder.Eval(Container.DataItem, "Read") %>' ID="LinkButton4" runat="server"><%# DataBinder.Eval(Container.DataItem, "ToName") %></asp:LinkButton></p></td>
                                    <td><p class="subject"><asp:LinkButton CssClass='<%# DataBinder.Eval(Container.DataItem, "Read") %>' ID="LinkButton2" runat="server"><%# DataBinder.Eval(Container.DataItem, "Subject") %></asp:LinkButton></p></td>
                                    <td><p class="message"><asp:LinkButton CssClass='<%# DataBinder.Eval(Container.DataItem, "Read") %>' ID="LinkButton3" runat="server"><%# DataBinder.Eval(Container.DataItem, "Message1") %></asp:LinkButton></p></td>
                                </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </table>
                        </section>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
        </asp:View>
        <asp:View runat="server">
            <article id="pmCont">
                <h3><asp:Label ID="Subject" runat="server" /></h3>
                <section id="HeadInfo">
                    <p id="ReplyCont"><asp:LinkButton ID="lnkReply" Text="Reply" OnCommand="lnkReply_Command" CausesValidation="false" runat="server" /></p>
                    <p id="FromUser">From:<asp:LinkButton ID="ViewFrom" CausesValidation="false" OnCommand="ViewFrom_Command" runat="server" /></p>
                    <p id="ToUser">To:<asp:LinkButton ID="ViewTo" CausesValidation="false" OnCommand="ViewFrom_Command" runat="server" /></p>
                </section>
                <section id="messageCont">
                    <asp:Literal ID="litMessage" runat="server" />
                </section>
            </article>
        </asp:View>
        <asp:View runat="server">
            <article id="newMessage" class="form">
                <section class="inputBlock clearfix">
                    <asp:Label ID="lblTo" AssociatedControlID="txtTo" Text="To:" runat="server" />
                    <section class="formInput">
                        <asp:TextBox ID="txtTo" runat="server" />
                        <asp:RequiredFieldValidator ID="reqTo" ControlToValidate="txtTo" ErrorMessage="Please fill in a recipient" Display="Dynamic" CssClass="error" runat="server" />
                        <asp:CustomValidator ID="custTo" ControlToValidate="txtTo" OnServerValidate="custTo_ServerValidate" Display="Dynamic" CssClass="error" runat="server" />
                    </section>
                </section>
                <section class="inputBlock clearfix">
                    <asp:Label ID="lblSubject" AssociatedControlID="txtSubject" Text="Subject:" runat="server" />
                    <section class="formInput">
                        <asp:TextBox ID="txtSubject" runat="server" />
                        <asp:RequiredFieldValidator ID="reqSubj" ControlToValidate="txtSubject" ErrorMessage="Please fill in a subject" Display="Dynamic" CssClass="error" runat="server" />
                    </section>
                </section>
                <section class="inputBlock clearfix">
                    <asp:Label ID="lblMessage" AssociatedControlID="txtMessage" Text="Message:" runat="server" />
                    <section class="formInput">
                        <asp:TextBox ID="txtMessage" TextMode="MultiLine" runat="server" />
                        <asp:RequiredFieldValidator ID="reqMessage" ControlToValidate="txtMessage" ErrorMessage="Please fill in a message" Display="Dynamic" CssClass="error" runat="server" />
                    </section>
                </section>
            </article>
        </asp:View>
    </asp:MultiView>
</asp:Content>

