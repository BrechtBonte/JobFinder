<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="apply.aspx.cs" Inherits="apply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">Apply to
    <asp:Literal ID="litTitle" runat="server" />
    - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" runat="Server">
    <link rel="stylesheet" href="css/forms.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" runat="Server">
    <section class="formcont">
        <asp:MultiView ID="applicationViews" ActiveViewIndex="0" runat="server">
            <asp:View runat="server">

                <h2 class="hasSub">Applying to:</h2>
                <h3><asp:LinkButton ID="lnkJob" OnCommand="lnkJob_Command" runat="server" /> at <asp:LinkButton ID="lnkComp" OnCommand="lnkComp_Command" runat="server" /></h3>
                <article class="form">
                    <section class="inputBlock">
                        <asp:Label ID="lblMotivation" AssociatedControlID="txtMotivation" Text="Motivation:" runat="server" />
                        <section class="formInput">
                            <asp:TextBox ID="txtMotivation" TextMode="MultiLine" runat="server" /><br />
                            <asp:RequiredFieldValidator ID="reqMotivation" CssClass="error" Display="Dynamic" ControlToValidate="txtMotivation" ErrorMessage="Please provide some motivation" runat="server" />
                        </section>
                    </section>
                    <section class="inputBlock clearfix">
                        <section class="formInput">
                            <p class="cancelBtn"> <asp:LinkButton ID="lnkCancel" CausesValidation="false" OnCommand="lnkCancel_Command" Text="Cancel" runat="server" /></p>
                            <p class="submitBtn"> <asp:LinkButton ID="lnkSubmit" OnCommand="lnkSubmit_Command" Text="Apply" runat="server" /></p>
                        </section>
                    </section>
                </article>
            </asp:View>
            <asp:View runat="server">

                <p class="success">You have successfully applied to <asp:LinkButton ID="lnkOffer" OnCommand="lnkJob_Command" runat="server" /> at <asp:LinkButton ID="lnkCompany" OnCommand="lnkComp_Command" runat="server" /></p>
                <p class="successLink">Return to the <a href="Default.aspx">Homepage</a> or check out other <a href="joboffers.aspx">job offers</a>.</p>
            </asp:View>
        </asp:MultiView>
    </section>
</asp:Content>

