<%@ Page Title="" Language="C#" MasterPageFile="~/Browse.master" AutoEventWireup="true" CodeFile="companies.aspx.cs" Inherits="companies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BrowseTitle" Runat="Server">Companies - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadingCont" Runat="Server">Companies</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RegisterText" Runat="Server">
    <aside id="registerBox">
        <p id="registerButton"><asp:LinkButton ID="lnkRegister" Text="Create a new Company!" OnClick="lnkRegister_Click" runat="server" /></p>
    </aside>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Filter" Runat="Server">
    <asp:TextBox ID="txtName" CssClass="name" runat="server" />
    <p id="filterBtn"><asp:LinkButton ID="lnkFilter" Text="Filter" OnClick="lnkFilter_Click" runat="server" /></p>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ResultsHolder" Runat="Server">
    <asp:Repeater ID="resultRepeater" runat="server">
        <ItemTemplate>
            <article class="result company">
                <a href='company.aspx?id=<%# DataBinder.Eval(Container.DataItem, "ID") %>' class="resultLink clearfix">
                    <div class='hiring <%# DataBinder.Eval(Container.DataItem, "Hiring") %>'></div>
                    <figure>
                        <div class="outer-center">
                            <div class="inner-center">
                                <img src='files/companies/imgs/<%# DataBinder.Eval(Container.DataItem, "Logo") %>' alt="Logo" />
                            </div>
                        </div>
                    </figure>
                    <section class="companyDescr">
                        <p class="companyName"><%# DataBinder.Eval(Container.DataItem, "Name") %></p>
                    </section>
                </a>
            </article>
        </ItemTemplate>
        </asp:Repeater>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="PageinationHolder" Runat="Server">
    <asp:PlaceHolder ID="Pages" runat="server" />
</asp:Content>

