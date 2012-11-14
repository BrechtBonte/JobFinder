<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="company.aspx.cs" Inherits="company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server"><asp:Literal ID="litTitle" runat="server" /> - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/companies.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <article id="companyCont">
        <section id="companyHead">
            <figure id="logoCont">
                <div class="outer-center">
                    <div class="inner-center">
                        <asp:Image ID="companyLogo" AlternateText="Company Logo" runat="server" />
                    </div>
                </div>
            </figure>
            <h2><asp:Label ID="companyName" runat="server" /></h2>
        </section>
        <section id="tabs" class="clearfix">
            <div id="lineDivPre"></div>
            <asp:LinkButton ID="aboutTab" CssClass="tab open" Text="About" OnClick="aboutTab_Click" runat="server" />
            <asp:LinkButton ID="offerTab" CssClass="tab" Text="Job Offers" OnClick="offerTab_Click" runat="server" />
            <div id="lineDiv"></div>
        </section>
        <asp:MultiView ID="companyTab" ActiveViewIndex="0" runat="server">
            <asp:View runat="server">
                <section id="aboutCont" class="clearfix">
                    <section id="companyDescriptionCont">
                        <asp:Literal ID="companyDescription" runat="server" />
                    </section>
                    <aside id="contactInfo">
                        <p id="contactStreet"><asp:Label ID="lblStreet" runat="server" /></p>
                        <p id="contactCity"><asp:Label ID="lblCity" runat="server" /></p>
                        <p id="contactRegion"><asp:Label ID="lblRegion" runat="server" /></p>
                        <p class="mail"><asp:LinkButton ID="lnkEmail" OnCommand="Redir" runat="server" /></p>
                        <p class="web"><asp:LinkButton ID="lnkWebsite" OnCommand="Redir" runat="server" /></p>
                    </aside>
                </section>
            </asp:View>
            <asp:View runat="server">
                <asp:Repeater ID="offerRepeater" OnItemCommand="offerRepeater_ItemCommand" runat="server">
                    <HeaderTemplate>
                        <section id="jobOffers">
                    </HeaderTemplate>
                    <ItemTemplate>
                            <article class="joboffer clearfix">
                                <h3><%# DataBinder.Eval(Container.DataItem, "Title") %></h3>
                                <p><%# DataBinder.Eval(Container.DataItem, "FirstLine") %></p>
                                <section class="offerTags">
                                    <%# DataBinder.Eval(Container.DataItem, "TagPs") %>
                                </section>
                                <asp:HiddenField ID="HiddenId" Value='<%# DataBinder.Eval(Container.DataItem, "ID") %>' runat="server" />
                                <asp:LinkButton CssClass="ReadMoreBtn" CommandName="more" Text="Read More"  runat="server" />
                            </article>
                    </ItemTemplate>
                    <FooterTemplate>
                        </section>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:View>
        </asp:MultiView>
    </article>
</asp:Content>

