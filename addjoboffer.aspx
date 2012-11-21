<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="addjoboffer.aspx.cs" Inherits="addjoboffer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Add new job offer - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/forms.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section class="formcont">
        <h2>Add new job offer</h2>
        <article class="form">
            <section class="inputBlock">
                <asp:Label ID="lblTitle" AssociatedControlID="txtTitle" Text="Title:*" runat="server" />
                <section class="formInput">
                    <asp:TextBox ID="txtTitle" runat="server" />
                    <asp:RequiredFieldValidator ID="reqTitle" CssClass="error" Display="Dynamic" ControlToValidate="txtTitle" ErrorMessage="Please fill out a title" runat="server" />
                    <asp:CustomValidator ID="custTitle" CssClass="error" Display="Dynamic" ControlToValidate="txtTitle" ErrorMessage="You already have an offer with this tite" OnServerValidate="custTitle_ServerValidate" runat="server" />
                </section>
            </section>
            <section class="inputBlock">
                <asp:Label ID="lblRegion" AssociatedControlID="lstRegion" Text="Alternate region:" runat="server" />
                <section class="formInput">
                    <asp:DropDownList ID="lstRegion" runat="server" />
                </section>
            </section>
            <section class="inputBlock">
                <asp:Label ID="lblContact" AssociatedControlID="lstContact" Text="Contact:" runat="server" />
                <section class="formInput">
                    <asp:DropDownList ID="lstContact" runat="server" />
                </section>
            </section>
            <section class="inputBlock">
                <asp:Label ID="lblDescription" AssociatedControlID="txtDescription" Text="Description:*" runat="server" />
                <section class="formInput">
                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" />
                    <asp:RequiredFieldValidator ID="reqDescription" CssClass="error" Display="Dynamic" ControlToValidate="txtDescription" ErrorMessage="Please enter a description" runat="server" />
                </section>
            </section>
            <section class="inputBlock clearfix">
                <section class="formInput">
                    <p class="cancelBtn"><a href="Default.aspx">Cancel</a></p>
                    <p class="submitBtn"><asp:LinkButton ID="lnkSubmit" Text="Submit" OnClick="lnkSubmit_Click" runat="server" /></p>
                </section>
            </section>
        </article>
    </section>
</asp:Content>

