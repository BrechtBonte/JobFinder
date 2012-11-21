<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="user.aspx.cs" Inherits="user" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server"><asp:Literal ID="litTitle" runat="server" /> JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/users.css" />
    <script type="text/javascript">
        function pageLoad () {
            var figure = document.getElementById("profPicCont");
            figure.style.top = -(figure.offsetHeight / 4) + "px";

            document.getElementById("descriptionText").style.marginTop = (figure.offsetHeight * 3 / 4 - 30) + "px";
        };
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section id="userCont">
        <article id="userCard">
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
            <asp:Literal ID="tagPanel" runat="server" />
        </section>
    </aside>
</asp:Content>

