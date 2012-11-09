<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Log In - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/forms.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section class="formcont">
        <h2>Log In</h2>
        <article class="form small clearfix">
            <section class="inputBlock clearfix">
                <asp:Label ID="lblEmail" AssociatedControlID="txtEmail" Text="E&#45;mail address:" runat="server" />
                <section class="formInput">
                    <asp:TextBox ID="txtEmail" CssClass="firstInput" runat="server" />
                    <asp:RequiredFieldValidator ID="reqEmail" CssClass="error" Display="Dynamic" ErrorMessage="Please fill in an e&#45;mail address." ControlToValidate="txtEmail" runat="server" />
                    <asp:CustomValidator ID="exEmail" CssClass="error" Display="Dynamic" ErrorMessage="This account does not exist." ControlToValidate="txtEmail" OnServerValidate="exEmail_ServerValidate" runat="server" />
                    <asp:CustomValidator ID="verEmail" CssClass="error" Display="Dynamic" ErrorMessage="This account has not yet been activated." ControlToValidate="txtEmail" OnServerValidate="verEmail_ServerValidate" runat="server" />
                </section>
            </section>
            <section class="inputBlock clearfix">
                <asp:Label ID="lblPass" AssociatedControlID="txtPass" Text="Password:" runat="server" />
                <section class="formInput">
                    <asp:TextBox ID="txtPass" TextMode="Password" runat="server" />
                    <asp:RequiredFieldValidator ID="reqPass" CssClass="error" Display="Dynamic" ErrorMessage="Please fill in a password." ControlToValidate="txtPass" runat="server" />
                    <asp:CustomValidator ID="valPass" CssClass="error" Display="Dynamic" ErrorMessage="The e&#45;mail &#45; password combination is incorrect." ControlToValidate="txtPass" OnServerValidate="valPass_ServerValidate" runat="server" />
                </section>
            </section>
            <section class="inputBlock clearfix buttons">
                <asp:Button ID="loginButton" Text="Log In" OnClick="loginButton_Click" runat="server" />
                <p>Don't have an account yet? <a href="register.aspx">Register</a>!</p>
            </section>
        </article>
    </section>
</asp:Content>

