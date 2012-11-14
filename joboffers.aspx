<%@ Page Title="" Language="C#" MasterPageFile="~/Browse.master" AutoEventWireup="true" CodeFile="joboffers.aspx.cs" Inherits="joboffers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BrowseTitle" Runat="Server">Job Offers - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadingCont" Runat="Server">Job Offers</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RegisterText" Runat="Server">
    <asp:MultiView ID="registerViews" ActiveViewIndex="0" runat="server">
        <asp:View runat="server"></asp:View>
        <asp:View runat="server">
            <aside id="registerBox">
                <p id="registerButton"><asp:LinkButton ID="lnkSaved" Text="View saved job offers" OnClick="ShowSaved" runat="server" /></p>
            </aside>
        </asp:View>
    </asp:MultiView>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Sugested" Runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Filter" Runat="Server">
    <asp:Panel ID="filterPanel" Visible="true" runat="server">
        <asp:TextBox ID="txtTitle" CssClass="name" runat="server" />
        <p id="filterBtn"><asp:LinkButton ID="lnkFilter" Text="Filter" OnClick="lnkFilter_Click" runat="server" /></p>
        <section id="regions">
            <asp:Panel ID="regionPanel" runat="server" />
        </section>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ResultsHolder" Runat="Server">
    <asp:Repeater ID="resultRepeater" runat="server">
        <ItemTemplate>
            <article class="result offer clearfix">
                <a href='joboffer.aspx?id=<%# DataBinder.Eval(Container.DataItem, "ID") %>' class="resultLink clearfix">
                    <figure>
                        <div class="outer-center">
                            <div class="inner-center">
                                <img src='files/companies/imgs/<%# DataBinder.Eval(Container.DataItem, "CompanyLogo") %>' alt="Company logo" />
                            </div>
                        </div>
                    </figure>
                    <section class="offerDescr clearfix">
                        <p class="title"><%# DataBinder.Eval(Container.DataItem, "Title") %></p>
                        <p class="company"><%# DataBinder.Eval(Container.DataItem, "CompanyName") %></p>
                        <p class="descr"><%# DataBinder.Eval(Container.DataItem, "FirstLine") %></p>
                        <section class="tags clearfix"><%# DataBinder.Eval(Container.DataItem, "TagPs") %></section>
                    </section>
                </a>
            </article>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="PageinationHolder" Runat="Server">
    <asp:Panel ID="Pages" runat="server" />
</asp:Content>

