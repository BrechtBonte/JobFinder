<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="joboffer.aspx.cs" Inherits="joboffer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server"><asp:Literal ID="litTitle" runat="server" /> - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/joboffers.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section id="companyHead">
        <figure id="logoCont">
            <div class="outer-center">
                <div class="inner-center">
                    <asp:LinkButton ID="imageLink" OnCommand="imageLink_Command" runat="server"><asp:Image ID="companyLogo" AlternateText="Company Logo" runat="server" /></asp:LinkButton>
                </div>
            </div>
        </figure>
        <h2><asp:LinkButton ID="companyName" OnCommand="imageLink_Command" runat="server" /></h2>
    </section>
    <section id="offerCont" class="clearfix">
        <h3><asp:Label ID="jobTitle" runat="server" /></h3>
        <p id="Region"><asp:Label ID="lblRegion" runat="server" /></p>
        <section id="descriptionCont">
            <section id="description">
                <asp:Literal ID="litDescription" runat="server" />
            </section>
            <section id="tags" class="clearfix">
                <asp:Literal ID="litTags" runat="server" />
            </section>
        </section>
        <asp:Panel ID="InteractionCont" Visible="false" runat="server">
            <aside id="interaction">
                <p id="SaveApplicationP"><asp:LinkButton ID="SaveApplication" OnCommand="SaveApplication_Command" runat="server" /></p>
                <p id="ApplyP"><asp:LinkButton ID="Apply" Text="Apply" OnCommand="Apply_Command" runat="server" /></p>
                <p class="mail"><asp:LinkButton ID="mailLink" OnCommand="mailLink_Command" runat="server" /></p>
            </aside>
        </asp:Panel>
    </section>
</asp:Content>

