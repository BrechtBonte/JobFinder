<%@ Page Title="" Language="C#" MasterPageFile="~/Browse.master" AutoEventWireup="true" CodeFile="users.aspx.cs" Inherits="users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BrowseTitle" Runat="Server">Users - JobFinder</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="HeadingCont" Runat="Server">Users</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="RegisterText" Runat="Server">
    <aside id="registerBox">
        <p id="registerButton"><asp:LinkButton ID="lnkRegister" Text="Create a new User!" OnClick="lnkRegister_Click" runat="server" /></p>
    </aside>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Filter" Runat="Server">
    <asp:TextBox ID="txtName" CssClass="name" runat="server" />
    <p id="filterBtn"><asp:LinkButton ID="lnkFilter" Text="Filter" OnClick="lnkFilter_Click" runat="server" /></p>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ResultsHolder" Runat="Server">
    <asp:Repeater ID="resultRepeater" runat="server">
        <ItemTemplate>
            <article class="result user">
                <a href='user.aspx?id=<%# DataBinder.Eval(Container.DataItem, "ID") %>' class="resultLink clearfix">
                    <figure>
                        <div class="outer-center">
                            <div class="inner-center">
                                <img src='files/users/imgs/<%# DataBinder.Eval(Container.DataItem, "imageName") %>' alt="profilePic" />
                            </div>
                        </div>
                    </figure>
                    <section class="userDescr clearfix">
                        <p class="userFirstname"><%# DataBinder.Eval(Container.DataItem, "Firstname") %></p>
                        <p class="userLastname"><%# DataBinder.Eval(Container.DataItem, "Lastname") %></p>
                        <section class="tags clearfix"><%# DataBinder.Eval(Container.DataItem, "TagPs") %></section>
                    </section>
                </a>
            </article>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageinationHolder" Runat="Server">
    <asp:Panel ID="Pages" runat="server" />
</asp:Content>

