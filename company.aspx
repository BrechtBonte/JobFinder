<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="company.aspx.cs" Inherits="company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server"><asp:Literal ID="litTitle" runat="server" /> - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/companies.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <article id="companyCont">
        <section id="companyHead">
            <asp:MultiView ID="logoEdit" ActiveViewIndex="0" runat="server">
                <asp:View runat="server">
                    <figure id="logoCont">
                        <div class="outer-center">
                            <div class="inner-center">
                                <asp:Image ID="companyLogo" AlternateText="Company Logo" runat="server" />
                            </div>
                        </div>
                    </figure>
                    <asp:Panel ID="editLogo" CssClass="editBtn" Visible="false" runat="server">
                        <asp:LinkButton CommandArgument="logo" OnCommand="Unnamed_Command" runat="server" />
                    </asp:Panel>
                </asp:View>
                <asp:View runat="server">
                    <asp:FileUpload ID="fileLogo" runat="server" />
                    <asp:RequiredFieldValidator ID="reqLogo" CssClass="error" Display="Dynamic" ControlToValidate="fileLogo" ErrorMessage="Please select a logo" runat="server" />
                    <asp:CustomValidator ID="custLogo" CssClass="error" Display="Dynamic" ControlToValidate="fileLogo" ErrorMessage="Please upload a .jpg or .png image" runat="server" />
                    <section class="editBtns">
                        <p class="cancelBtn"><asp:LinkButton CausesValidation="false" CommandArgument="logo" OnCommand="Cancel_Command" Text="Cancel" runat="server" /></p>
                        <p class="submitBtn"><asp:LinkButton CommandArgument="logo" OnCommand="Edit_Command" Text="Submit" runat="server" /></p>
                    </section>
                </asp:View>
            </asp:MultiView>
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
                        <asp:MultiView ID="descriptionEdit" ActiveViewIndex="0" runat="server">
                            <asp:View runat="server">
                                <asp:Literal ID="companyDescription" runat="server" />
                                <asp:Panel ID="editDescription" CssClass="editBtn" Visible="false" runat="server">
                                    <asp:LinkButton CommandArgument="description" OnCommand="Unnamed_Command" runat="server" />
                                </asp:Panel>
                            </asp:View>
                            <asp:View runat="server">
                                <section class="inputBlock">
                                    <asp:Label ID="lblDescription" AssociatedControlID="txtDescription" Text="Description:" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" />
                                        <asp:RequiredFieldValidator ID="reqDescription" CssClass="error" Display="Dynamic" ControlToValidate="txtDescription" ErrorMessage="Please provide a description" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <p class="cancelBtn"><asp:LinkButton CausesValidation="false" CommandArgument="description" OnCommand="Cancel_Command" runat="server" /></p>
                                    <p class="submitBtn"><asp:LinkButton CommandArgument="description" OnCommand="Edit_Command" runat="server" /></p>
                                </section>
                            </asp:View>
                        </asp:MultiView>
                    </section>
                    <aside id="contactInfo">
                        <asp:MultiView ID="infoEdit" ActiveViewIndex="0" runat="server">
                            <asp:View runat="server">
                                <p id="contactStreet"><asp:Label ID="lblStreet" runat="server" /></p>
                                <p id="contactCity"><asp:Label ID="lblCity" runat="server" /></p>
                                <p id="contactRegion"><asp:Label ID="lblRegion" runat="server" /></p>
                                <p class="mail"><asp:LinkButton ID="lnkEmail" OnCommand="Redir" runat="server" /></p>
                                <p class="web"><asp:LinkButton ID="lnkWebsite" OnCommand="Redir" runat="server" /></p>
                                <asp:Panel ID="editInfo" CssClass="editBtn" Visible="false" runat="server">
                                    <asp:LinkButton CommandArgument="logo" OnCommand="Unnamed_Command" runat="server" />
                                </asp:Panel>
                            </asp:View>
                            <asp:View runat="server">
                                <section class="inputBlock">
                                    <asp:Label ID="lblEdStreet" AssociatedControlID="txtStreet" Text="Street:" runat="server" /><br />
                                    <asp:TextBox ID="txtStreet" runat="server" /><br />
                                    <asp:RequiredFieldValidator ID="reqStreet" CssClass="error" Display="Dynamic" ControlToValidate="txtStreet" ErrorMessage="Please provide a streetname" runat="server" />
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblEdCity" AssociatedControlID="txtCity" Text="City:" runat="server" /><br />
                                    <asp:TextBox ID="txtCity" runat="server" /><br />
                                    <asp:RequiredFieldValidator ID="reqCity" CssClass="error" Display="Dynamic" ControlToValidate="txtCity" ErrorMessage="Please provide a city" runat="server" />
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblEdRegion" AssociatedControlID="lstRegion" Text="Region:" runat="server" /><br />
                                    <asp:DropDownList ID="lstRegion" runat="server" />
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblEdEmail" AssociatedControlID="txtEmail" Text="Email:" runat="server" /><br />
                                    <asp:TextBox ID="txtEmail" runat="server" />
                                    <asp:RequiredFieldValidator ID="reqEmail" CssClass="error" Display="Dynamic" ControlToValidate="txtEmail" ErrorMessage="Please provide an email address" runat="server" />
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblEdWebsite" AssociatedControlID="txtWebsite" Text="Website:" runat="server" /><br />
                                    <asp:TextBox ID="txtWebsite" runat="server" />
                                    <asp:RequiredFieldValidator ID="reqWebsite" CssClass="error" Display="Dynamic" ControlToValidate="txtWebsite" ErrorMessage="Please provide a website" runat="server" />
                                </section>
                                <section class="inputBlock">
                                    <p class="cancelBtn"><asp:LinkButton CausesValidation="false" CommandArgument="info" OnCommand="Cancel_Command" Text="Cancel" runat="server" /></p>
                                    <p class="submitBtn"><asp:LinkButton CommandArgument="info" OnCommand="Edit_Command" Text="Submit" runat="server" /></p>
                                </section>
                            </asp:View>
                        </asp:MultiView>
                    </aside>
                </section>
            </asp:View>
            <asp:View runat="server">
                <asp:Repeater ID="offerRepeater" OnItemCommand="offerRepeater_ItemCommand" runat="server">
                    <HeaderTemplate>
                        <section id="jobOffers">
                        <asp:Panel ID="jobofferPanel" Visible="false" runat="server">
                            <p id="jobofferLink"><a href="addjoboffer.aspx">Add a new job offer</a></p>
                        </asp:Panel>
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

