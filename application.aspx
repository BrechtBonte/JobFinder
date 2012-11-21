<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="application.aspx.cs" Inherits="application" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Application for <asp:Literal ID="litTitle" runat="server" /> - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/applications.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <article id="applicationCont" class="clearfix">
        <h2><asp:LinkButton ID="lnkTitle" runat="server" /></h2>
        <asp:LinkButton ID="lnkUser" CssClass="clearfix" OnCommand="lnkUser_Command" runat="server">
            <article id="userCont">
                <section id="userHead" class="clearfix">
                    <figure>
                        <asp:Image ID="lnkImg" AlternateText="profile pic" runat="server" />
                    </figure>
                    <p id="userName"><asp:Label ID="lblFirstname" runat="server" /><asp:Literal Text=" " runat="server" /><asp:Label ID="lblLastname" runat="server" /></p>
                    <section id="userContact" class="clearfix">
                        <asp:Panel ID="mailPanel" Visible="false" runat="server">
                            <p class="mail"><asp:Label ID="lblMail" runat="server" /></p>
                        </asp:Panel>
                        <p class="phone"><asp:Label ID="lblPhone" runat="server" /></p>
                    </section>
                </section>
                <section id="descriptionText">
                    <asp:Literal ID="descrTextPanel" runat="server" />
                </section>
                <section id="tags" class="clearfix">
                    <asp:Literal ID="tagPanel" runat="server" />
                </section>
            </article>
        </asp:LinkButton>
        <section id="motivationCont">
            <asp:Literal ID="motivationPanel" runat="server" />
        </section>
    </article>
</asp:Content>

