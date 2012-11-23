<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Home - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/home.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <article id="contentCont" class="clearfix">
        <section id="statistics">
            <p id="stats">Curently hosting <asp:Label ID="lblCompanies" runat="server" /> companies with <asp:Label ID="lblOffers" runat="server" /> job offers</p>
            <h3>Find the job that's right for you!</h3>
            <article id="queryBox" class="clearfix">
                <asp:Panel ID="Panel1" DefaultButton="lnkSearch" runat="server">
                    <asp:TextBox id="txtQuery" runat="server" />
                    <p id="querySearch"><asp:LinkButton ID="lnkSearch" Text="Search" OnClick="lnkSearch_Click" runat="server" /></p>
                </asp:Panel>
            </article>
        </section>
        <section id="loggedSect">
            <asp:MultiView ID="loggedViews" ActiveViewIndex="0" runat="server">
                <asp:View runat="server">
                    <p class="registerMessage">Want to get the most out of this site?</p>
                    <p id="registerBtn"><a href="register.aspx">Register!</a></p>
                </asp:View>
                <asp:View runat="server">
                    <p id="userP" class="registerMessage"><asp:Label ID="lblNewOffers" runat="server" /> new job offers since your last login</p>
                </asp:View>
                <asp:View runat="server">
                    <p id="companyP" class="registerMessage"><asp:Label ID="lblNewApplications" runat="server" /> new applications since your last login</p>
                </asp:View>
            </asp:MultiView>
        </section>
        <div id="midSect"></div>
        <section id="recentJobs">
            <h3>Recent job offers</h3>
            <asp:Repeater ID="jobRepeater" runat="server">
                <ItemTemplate>
                    <section class="joboffer clearfix">
                        <p class="title"><a href='joboffer.aspx?id=<%# DataBinder.Eval(Container.DataItem, "ID") %>'><%# DataBinder.Eval(Container.DataItem, "Title") %></a></p>
                        <p class="companyName"><a href='company.aspx?id=<%# DataBinder.Eval(Container.DataItem, "CompanyId") %>'><%# DataBinder.Eval(Container.DataItem, "CompanyName") %></a></p>
                        <p class="at">at</p>
                    </section>
                </ItemTemplate>
            </asp:Repeater>
            <p id="alljobsLink"><a href="joboffers.aspx">View all joboffers!</a></p>
        </section>
        <section id="regions">
            <h3>View by region</h3>
            <ul>
                <asp:Repeater ID="regionRepeater" runat="server">
                    <ItemTemplate>
                        <li><a href='joboffers.aspx?mode=browse&title=&filterRegions&check<%# DataBinder.Eval(Container.DataItem, "ID") %>'><%# DataBinder.Eval(Container.DataItem, "Name") %></a> (<%# DataBinder.Eval(Container.DataItem, "CountOffers") %>)</li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </section>
    </article>
</asp:Content>

