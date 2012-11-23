<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="joboffer.aspx.cs" Inherits="joboffer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server"><asp:Literal ID="litTitle" runat="server" /> - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/joboffers.css" />
    <link rel="stylesheet" href="css/forms.css" />
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
        <asp:MultiView ID="infoEdit" ActiveViewIndex="0" runat="server">
            <asp:View runat="server">
                <section class="clearfix">
                    <section id="jobInfo">
                        <h3><asp:Label ID="jobTitle" runat="server" /></h3>
                        <p id="Region"><asp:Label ID="lblRegion" runat="server" /></p>
                        <asp:Panel ID="editInfo" CssClass="editBtn" Visible="false" runat="server">
                            <asp:LinkButton CommandName="info" OnCommand="Show_Command" runat="server" />
                        </asp:Panel>
                    </section>
                </section>
            </asp:View>
            <asp:View runat="server">
                <section class="inputBlock">
                    <asp:Label ID="lblTitle" AssociatedControlID="txtTitle" Text="Title:" runat="server" />
                    <section class="formInput">
                        <asp:TextBox ID="txtTitle" runat="server" />
                        <asp:RequiredFieldValidator ID="reqTitle" ValidationGroup="info" CssClass="error" Display="Dynamic" ControlToValidate="txtTitle" ErrorMessage="Please provide a title" runat="server" />
                        <asp:CustomValidator ID="custTitle" ValidationGroup="info" CssClass="error" Display="Dynamic" ControlToValidate="txtTitle" OnServerValidate="custTitle_ServerValidate" ErrorMessage="Your company already has a joboffer with this title" runat="server" />
                    </section>
                </section>
                <section class="inputBlock">
                    <asp:Label ID="lblEdRegion" AssociatedControlID="lstRegion" Text="Region:" runat="server" />
                    <section class="formInput">
                        <asp:DropDownList ID="lstRegion" runat="server" />
                    </section>
                </section>
                <section class="inputBlock">
                    <asp:Label ID="lblContact" AssociatedControlID="lstContact" Text="Region:" runat="server" />
                    <section class="formInput">
                        <asp:DropDownList ID="lstContact" runat="server" />
                    </section>
                </section>
                <section class="inputBlock">
                    <section class="formInput editBtns clearfix">
                        <p class="cancelBtn"><asp:LinkButton ID="cancelInfo" CausesValidation="false" CommandName="info" OnCommand="Cancel_Command" Text="Cancel" runat="server" /></p>
                        <p class="submitBtn"><asp:LinkButton ID="submitInfo" ValidationGroup="info" CommandName="info" OnCommand="Edit_Command" Text="Submit" runat="server" /></p>
                    </section>
                </section>
            </asp:View>
        </asp:MultiView>
        
        <section id="descriptionCont">
            <section id="description">
                <asp:MultiView ID="descrEdit" ActiveViewIndex="0" runat="server">
                    <asp:View runat="server">
                        <asp:Literal ID="litDescription" runat="server" />
                        <asp:Panel ID="editDescr" CssClass="editBtn" Visible="false" runat="server">
                            <asp:LinkButton CommandName="description" OnCommand="Show_Command" runat="server" />
                        </asp:Panel>
                    </asp:View>
                    <asp:View runat="server">
                        <section class="inputBlock">
                            <asp:Label ID="lblDescr" AssociatedControlID="txtDescr" Text="Description:" runat="server" />
                            <section class="formInput">
                                <asp:TextBox ID="txtDescr" TextMode="MultiLine" runat="server" />
                                <asp:RequiredFieldValidator ID="reqDescr" ValidationGroup="description" CssClass="error" Display="Dynamic" ControlToValidate="txtDescr" ErrorMessage="Please provide a description" runat="server" />
                            </section>
                        </section>
                        <section class="inputBlock">
                            <section class="formInput editBtns clearfix">
                                <p class="cancelBtn"><asp:LinkButton ID="cancelDescr" CausesValidation="false" CommandName="description" OnCommand="Cancel_Command" Text="Cancel" runat="server" /></p>
                                <p class="submitBtn"><asp:LinkButton ID="submitDescr" CommandName="description" OnCommand="Edit_Command" Text="Submit" runat="server" /></p>
                            </section>
                        </section>
                    </asp:View>
                </asp:MultiView>

            </section>
            <section id="tags" class="clearfix">
                <asp:MultiView ID="tagsEdit" ActiveViewIndex="0" runat="server">
                    <asp:View runat="server">
                        <asp:Repeater ID="tagsRepeater" runat="server">
                            <ItemTemplate>
                                <p class="tag"><%# DataBinder.Eval(Container.DataItem, "Name") %></p>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="editTags" CssClass="editBtn" Visible="false" runat="server">
                            <asp:LinkButton CommandName="tags" OnCommand="Show_Command" runat="server" />
                        </asp:Panel>
                    </asp:View>
                    <asp:View runat="server">
                        <section class="clearfix">
                            <asp:Repeater ID="tagsEditRepeater" OnItemCommand="tagsEditRepeater_ItemCommand" runat="server">
                                <ItemTemplate>
                                    <p class="tag"><%# DataBinder.Eval(Container.DataItem, "Name") %><asp:LinkButton ID="LinkButton1" CssClass="remTag" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' runat="server" /></p>
                                </ItemTemplate>
                            </asp:Repeater>
                        </section>
                        <section id="tagText" class="inputBlock">
                            <asp:TextBox ID="txtTag" runat="server" />
                            <asp:RequiredFieldValidator ID="reqTag" ValidationGroup="tags" CssClass="error" Display="Dynamic" ControlToValidate="txtTag" ErrorMessage="Please enter a tag" runat="server" />
                            <asp:CustomValidator ID="custTag" ValidationGroup="tags" CssClass="error" Display="Dynamic" ControlToValidate="txtTag" ErrorMessage="Please limit the length of the tag to 20 chars" OnServerValidate="custTag_ServerValidate" runat="server" />
                        </section>
                        <section class="inputBlock clearfix">
                            <p class="cancelBtn"><asp:LinkButton ID="cancelTags" CausesValidation="false" CommandName="tags" OnCommand="Cancel_Command" Text="Back" runat="server" /></p>
                            <p class="submitBtn"><asp:LinkButton ID="submitTag" ValidationGroup="tags" CommandName="tags" OnCommand="Edit_Command" Text="Add" runat="server" /></p>
                        </section>
                    </asp:View>
                </asp:MultiView>
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

