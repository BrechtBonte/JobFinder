<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="user.aspx.cs" Inherits="user" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server"><asp:Literal ID="litTitle" runat="server" /> JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/users.css" />
    <link rel="stylesheet" href="css/forms.css" />
    <script type="text/javascript">
        function pageLoad () {
            var figure = document.getElementById("profPicCont");
            if (figure != null) {
                figure.style.top = -(figure.offsetHeight / 4) + "px";

                document.getElementById("descriptionText").style.marginTop = (figure.offsetHeight * 3 / 4 - 30) + "px";
            }
        };
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section id="userCont">
        <article id="userCard">
            <asp:MultiView ID="editInfo" ActiveViewIndex="0" runat="server">
                <asp:View runat="server">
                    <asp:Panel ID="infoEdit" CssClass="editBtn" Visible="false" runat="server">
                        <asp:LinkButton CommandName="info" OnCommand="Show_Command" runat="server" />
                    </asp:Panel>
                    <figure id="profPicCont" class="clearfix">
                        <div class="outer-center">
                            <div class="inner-center">
                                <asp:Image ID="profilePic" AlternateText="Profile Picture" runat="server" />
                            </div>
                        </div>
                    </figure>
                    <p id="fullName"><asp:Label ID="firstname" runat="server" /> <asp:Label ID="lastname" runat="server" /></p>
                    <section id="descriptionText">
                        <asp:Literal ID="descrTextPanel" runat="server" />
                    </section>
                    <section id="contactInfo">
                        <asp:MultiView ID="contactViews" ActiveViewIndex="0" runat="server">
                            <asp:View runat="server">
                                <p class="restricted">You need to <a href="login.aspx">Log In</a> to view users&apos; contact info.</p>
                            </asp:View>
                            <asp:View runat="server">
                                <asp:Panel ID="mailPanel" runat="server">
                                    <p class="mail"><asp:LinkButton ID="emailLink" OnCommand="emailLink_Command" runat="server"><asp:Label ID="emailaddress" runat="server" /></asp:LinkButton></p>
                                </asp:Panel>
                                <p class="phone"><asp:Label ID="phonenr" runat="server" /></p>
                            </asp:View>
                        </asp:MultiView>
                    </section>
                </asp:View>
                <asp:View runat="server">
                    <section class="inputBlock clearfix">
                        <asp:Label ID="lblImage" AssociatedControlID="fileImage" Text="image:" runat="server" />
                        <section class="formInput">
                            <asp:FileUpload ID="fileImage" runat="server" /><br />
                            <asp:CustomValidator ID="custImage" ValidationGroup="info" CssClass="error" Display="Dynamic" ControlToValidate="fileImage" OnServerValidate="custImage_ServerValidate" ErrorMessage="Please provide only .jpg or .png images" runat="server" />
                        </section>
                    </section>
                    <section class="inputBlock clearfix">
                        <asp:Label ID="lblEmail" AssociatedControlID="txtEmail" Text="email (shown):" runat="server" />
                        <section class="formInput">
                            <asp:TextBox ID="txtEmail" runat="server" /><br />
                            <asp:CheckBox ID="chMail" Text="Show Email address" runat="server" />
                        </section>
                    </section>
                    <section class="inputBlock clearfix">
                        <asp:Label ID="lblPhone" AssociatedControlID="txtPhone" Text="Phone Number:" runat="server" />
                        <section class="formInput">
                            <asp:TextBox ID="txtPhone" runat="server" />
                        </section>
                    </section>
                    <section class="inputBlock clearfix">
                        <asp:Label ID="lblCv" AssociatedControlID="fileCv" Text="Cv:" runat="server" />
                        <section class="formInput">
                            <asp:FileUpload ID="fileCv" runat="server" />
                        </section>
                    </section>
                    <section class="inputBlock clearfix">
                        <asp:Label ID="lblDescr" AssociatedControlID="txtDescr" Text="Description:" runat="server" />
                        <section class="formInput">
                            <asp:TextBox ID="txtDescr" TextMode="MultiLine" runat="server" /><br />
                            <asp:RequiredFieldValidator ID="reqDescr" ValidationGroup="info" CssClass="error" Display="Dynamic" ControlToValidate="txtDescr" ErrorMessage="Please enter a description" runat="server" />
                        </section>
                    </section>
                    <section class="editBtns clearfix">
                        <p class="cancelBtn"><asp:LinkButton ID="cancelInfo" CausesValidation="false" CommandName="info" OnCommand="Cancel_Command" Text="Cancel" runat="server" /></p>
                        <p class="submitBtn"><asp:LinkButton ID="submitInfo" ValidationGroup="info" CommandName="info" OnCommand="Edit_Command" Text="Submit" runat="server" /></p>
                    </section>
                </asp:View>
            </asp:MultiView>
        </article>
        <section id="interButtons" class="clearfix">
            <asp:Panel ID="buttonPanel" Visible="false" runat="server">
                <asp:Panel ID="pmPanel" Visible="false" runat="server">
                    <p class="buttonCont"><asp:LinkButton ID="PrivateMessageUser" Text="Send PM" runat="server" /></p>
                </asp:Panel>
                <p class="buttonCont"><asp:LinkButton ID="CVUser" Text="Download CV" OnCommand="CVUser_Click" runat="server" /></p>
            </asp:Panel>
        </section>
    </section>
    <aside id="userTags">
        <h3>Skills</h3>
        <section class="clearfix">
            <asp:MultiView ID="editTags" ActiveViewIndex="0" runat="server">
                <asp:View runat="server">
                    <asp:Repeater ID="tagRepeater" runat="server">
                        <ItemTemplate>
                            <p class="tag"><%# DataBinder.Eval(Container.DataItem, "Name") %></p>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="tagsEdit" CssClass="editBtn" Visible="false" runat="server">
                        <asp:LinkButton CommandName="tags" OnCommand="Show_Command" runat="server" />
                    </asp:Panel>
                </asp:View>
                <asp:View runat="server">
                    <section class="clearfix">
                        <asp:Repeater ID="tagEditRepeater" OnItemCommand="tagEditRepeater_ItemCommand" runat="server">
                            <ItemTemplate>
                                <p class="tag"><%# DataBinder.Eval(Container.DataItem, "Name") %><asp:LinkButton CssClass="remTag" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' runat="server" /></p>
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
    </aside>
</asp:Content>

