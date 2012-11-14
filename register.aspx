<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Register - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/register.css" />
    <link rel="stylesheet" href="css/forms.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <asp:MultiView ID="views" ActiveViewIndex="0" runat="server">
        <asp:View runat="server">
            <article id="registerChoice">
                <p id="userLink">
                    <asp:LinkButton ID="lnkUser" CommandArgument="user" OnCommand="lnkUser_Command" Text="Create a User!" runat="server" />
                </p>
                <p id="companyLink">
                    <asp:LinkButton ID="lnkCompany" CommandArgument="company" OnCommand="lnkUser_Command" Text="Create a Company!" runat="server" />
                </p>
            </article>
        </asp:View>
        <asp:View runat="server">
            <article class="form">
                <asp:MultiView ID="registerUser" ActiveViewIndex="0" runat="server">
                    <asp:View runat="server">
                        <section class="smallForm">
                            <section class="inputBlock">
                                <asp:Label ID="lblEmail" AssociatedControlID="txtEmail" Text="Email (Account)" runat="server" />
                            </section>
                            <section class="formInput">
                                <asp:TextBox ID="txtEmail"  runat="server" />

                            </section>
                        </section>
                    </asp:View>
                    <asp:View runat="server">

                    </asp:View>
                </asp:MultiView>
                <figure>
                    <asp:Image ID="profileImage" AlternateText="Profile pic" runat="server" />
                </figure>
            </article>
            <asp:FileUpload ID="imageUpload" runat="server" />
        </asp:View>
    </asp:MultiView>
</asp:Content>

