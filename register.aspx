<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Register - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/register.css" />
    <link rel="stylesheet" href="css/forms.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <asp:MultiView ID="views" ActiveViewIndex="0" runat="server">
        <asp:View runat="server">
            <article id="registerChoice" class="clearfix">
                <p id="userLink">
                    <asp:LinkButton ID="lnkUser" CommandArgument="User" OnCommand="lnkUser_Command" Text="Create a User!" runat="server" />
                </p>
                <p id="companyLink">
                    <asp:LinkButton ID="lnkCompany" CommandArgument="Company" OnCommand="lnkUser_Command" Text="Create a Company!" runat="server" />
                </p>
            </article>
        </asp:View>

        <asp:View runat="server">
            <article class="form">
                <asp:MultiView ID="registerUser" ActiveViewIndex="0" runat="server">
                    <asp:View runat="server">
                        <section class="smallForm clearfix">
                            <asp:Panel DefaultButton="userNext" runat="server">
                                <section class="inputBlock">
                                    <asp:Label ID="lblEmail" AssociatedControlID="txtEmail" Text="Email(Account):" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtEmail" CssClass="firstInput"  runat="server" />
                                        <asp:RequiredFieldValidator ID="reqEmail" CssClass="error" Display="Dynamic" ControlToValidate="txtEmail" ErrorMessage="Please enter an email address" runat="server" />
                                        <asp:RegularExpressionValidator ID="regEmail" CssClass="error" Display="Dynamic" ControlToValidate="txtEmail" ValidationExpression="^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}$" ErrorMessage="Please enter a correctly formed email address" runat="server" />
                                        <asp:CustomValidator ID="exEmail" CssClass="error" Display="Dynamic" ControlToValidate="txtEmail" OnServerValidate="exEmail_ServerValidate" ErrorMessage="This account already exists" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblPassword" AssociatedControlID="txtPassword" Text="Password:" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" />
                                        <asp:RequiredFieldValidator ID="reqPassword" CssClass="error" Display="Dynamic" ControlToValidate="txtPassword" ErrorMessage="Please provede a password" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblRePass" AssociatedControlID="txtRePass" Text="Re&#45;enter Password:" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtRePass" TextMode="Password" runat="server" />
                                        <asp:CompareValidator ID="compPass" CssClass="error" Display="Dynamic" ControlToValidate="txtRePass" ControlToCompare="txtPassword" ErrorMessage="The password fields are not the same" runat="server" />
                                    </section>
                                </section>
                                <p class="nextBtn"><asp:LinkButton ID="userNext" CommandArgument="1" CausesValidation="true" OnCommand="userNext_Command" Text="Next step" runat="server" /></p>
                            </asp:Panel>
                        </section>
                    </asp:View>

                    <asp:View runat="server">
                        <section class="smallForm clearfix">
                            <asp:Panel DefaultButton="lnkSubmit" runat="server">
                                <section class="inputBlock">
                                    <asp:Label ID="lblImage" AssociatedControlID="imageUpload" Text="Profile Image:*" runat="server" />
                                    <section class="formInput">
                                        <asp:FileUpload ID="imageUpload" runat="server" /><!-- TODO: check for file -->
                                        <asp:RequiredFieldValidator ID="reqImage" CssClass="error" Display="Dynamic" ControlToValidate="imageUpload" ErrorMessage="Please upload an image" runat="server" />
                                        <asp:CustomValidator ID="imgImage" CssClass="error" Display="Dynamic" ControlToValidate="imageUpload" OnServerValidate="imgImage_ServerValidate" ErrorMessage="Please select a .jpg or .png image" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblFirstname" AssociatedControlID="txtFirstname" CssClass="firstInput" Text="Firstname:*" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtFirstname"  runat="server" />
                                        <asp:RequiredFieldValidator ID="reqFirstname" CssClass="error" Display="Dynamic" ControlToValidate="txtFirstname" ErrorMessage="Please enter your Firstname" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblLastname" AssociatedControlID="txtLastname" Text="Lastname:*" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtLastname"  runat="server" />
                                        <asp:RequiredFieldValidator ID="reqLastname" CssClass="error" Display="Dynamic" ControlToValidate="txtLastname" ErrorMessage="Please enter your Lastname" runat="server" />
                                        <asp:CustomValidator ID="exFullName" CssClass="error" Display="Dynamic" ControlToValidate="txtLastname" ErrorMessage="A user with this name already exists" OnServerValidate="exFullName_ServerValidate" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblAltMail" AssociatedControlID="txtAltMail" Text="Alternate Email:" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtAltMail" runat="server" /><br />
                                        <asp:CheckBox ID="chAltMail" Text="ShowMail" Checked="true" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblPhone" AssociatedControlID="txtPhone" Text="Telephone nr.:" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtPhone" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblDescr" AssociatedControlID="txtDescr" Text="Description:*" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtDescr" TextMode="MultiLine" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblCV" AssociatedControlID="fileCV" Text="CV:*" runat="server" />
                                    <section class="formInput">
                                        <asp:FileUpload ID="fileCV" runat="server" />
                                        <asp:RequiredFieldValidator ID="reqCV" CssClass="error" Display="Dynamic" ControlToValidate="fileCV" ErrorMessage="Please upload your cv" runat="server" />
                                    </section>
                                </section>
                                <p class="prevBtn"><asp:LinkButton ID="lnkPrev" CausesValidation="false" CommandArgument="0" OnCommand="userNext_Command" Text="Prev. step" runat="server" /></p>
                                <p class="submitBtn"><asp:LinkButton ID="lnkSubmit" OnClick="lnkSubmit_Click" Text="Register" runat="server" /></p>
                            </asp:Panel>
                        </section>
                    </asp:View>

                    <asp:View runat="server">
                        <p class="success">You have been registered!</p>
                        <p class="successLink">Go to your <asp:LinkButton ID="lnkSuccess" Text="Profile" OnCommand="lnkSuccess_Command" runat="server" /></p>
                    </asp:View>
                </asp:MultiView>
            </article>
        </asp:View>

        <asp:View runat="server">
            <article class="form">
                <asp:MultiView ID="companyView" ActiveViewIndex="0" runat="server">
                    <asp:View runat="server">
                        <section class="smallForm clearfix">
                            <asp:Panel DefaultButton="lnkCompNext" runat="server">
                                <section class="inputBlock">
                                    <asp:Label ID="lblCompEmail" AssociatedControlID="txtCompEmail" Text="Email(Account):" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtCompEmail" CssClass="firstInput"  runat="server" />
                                        <asp:RequiredFieldValidator ID="reqCompEmail" CssClass="error" Display="Dynamic" ControlToValidate="txtCompEmail" ErrorMessage="Please enter an email address" runat="server" />
                                        <asp:RegularExpressionValidator ID="regCompEmail" CssClass="error" Display="Dynamic" ControlToValidate="txtCompEmail" ValidationExpression="^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}$" ErrorMessage="Please enter a correctly formed email address" runat="server" />
                                        <asp:CustomValidator ID="exCompEmail" CssClass="error" Display="Dynamic" ControlToValidate="txtCompEmail" OnServerValidate="exEmail_ServerValidate" ErrorMessage="This account already exists" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblCompPassword" AssociatedControlID="txtCompPassword" Text="Password:" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtCompPassword" TextMode="Password" runat="server" />
                                        <asp:RequiredFieldValidator ID="reqCompPassword" CssClass="error" Display="Dynamic" ControlToValidate="txtCompPassword" ErrorMessage="Please provede a password" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblCompRePass" AssociatedControlID="txtCompRePass" Text="Re&#45;enter Password:" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtCompRePass" TextMode="Password" runat="server" />
                                        <asp:CompareValidator ID="compCompRePass" CssClass="error" Display="Dynamic" ControlToValidate="txtCompRePass" ControlToCompare="txtCompPassword" ErrorMessage="The password fields are not the same" runat="server" />
                                    </section>
                                </section>
                                <p class="nextBtn"><asp:LinkButton ID="lnkCompNext" CommandArgument="1" OnCommand="lnkCompNext_Command" Text="Next step" runat="server" /></p>
                            </asp:Panel>
                        </section>
                    </asp:View>

                    <asp:View runat="server">
                        <section class="smallForm">
                            <asp:Panel DefaultButton="lnkCompSubmit" runat="server">
                                <section class="inputBlock clearfix">
                                    <asp:Label ID="lblName" AssociatedControlID="txtName" CssClass="firstInput" Text="Name:*" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtName" runat="server" />
                                        <asp:RequiredFieldValidator ID="reqName" CssClass="error" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="Please fill in a name" runat="server" />
                                        <asp:CustomValidator ID="exName" CssClass="error" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="A company with this name already exists" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblLogo" AssociatedControlID="fileLogo" Text="Logo:*" runat="server" />
                                    <section class="formInput">
                                        <asp:FileUpload ID="fileLogo" runat="server" />
                                        <asp:RequiredFieldValidator ID="reqLogo" CssClass="error" Display="Dynamic" ControlToValidate="fileLogo" ErrorMessage="Please select a logo" runat="server" />
                                        <asp:CustomValidator ID="imgLogo" CssClass="error" Display="Dynamic" ControlToValidate="fileLogo" OnServerValidate="imgLogo_ServerValidate" ErrorMessage="Please select a .jpg or .png image" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblWebsite" AssociatedControlID="txtWebsite" Text="Website:*" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtWebsite" runat="server" />
                                        <asp:RequiredFieldValidator ID="reqWebsite" CssClass="error" Display="Dynamic" ControlToValidate="txtWebsite" ErrorMessage="Please provide a website" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblCompAltMail" AssociatedControlID="txtCompAltMail" Text="Shown Email:" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtCompAltMail" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblStreet" AssociatedControlID="txtStreet" Text="Street:*" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtStreet" runat="server" />
                                        <asp:RequiredFieldValidator ID="reqStreet" CssClass="error" Display="Dynamic" ControlToValidate="txtStreet" ErrorMessage="Please fill in a streetname and number" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblCity" AssociatedControlID="txtCity" Text="City:*" runat="server" />
                                    <section class="formInput">
                                        <asp:TextBox ID="txtCity" runat="server" />
                                        <asp:RequiredFieldValidator ID="reqCity" CssClass="error" Display="Dynamic" ControlToValidate="txtCity" ErrorMessage="Please fill in a city" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblRegion" AssociatedControlID="lstRegion" Text="Region:*" runat="server" />
                                    <section class="inputBlock">
                                        <asp:DropDownList ID="lstRegion" runat="server" />
                                        <asp:CompareValidator ID="reqRegion" CssClass="error" Display="Dynamic" ControlToValidate="lstRegion" Type="Integer" Operator="GreaterThan" ValueToCompare="0" ErrorMessage="Please select a region" runat="server" />
                                    </section>
                                </section>
                                <section class="inputBlock">
                                    <asp:Label ID="lblCompDescr" AssociatedControlID="txtCompDescr" Text="Description:*" runat="server" />
                                    <section class="inputBlock">
                                        <asp:TextBox ID="txtCompDescr" TextMode="MultiLine" runat="server" />
                                        <asp:RequiredFieldValidator ID="reqCompDescr" CssClass="error" Display="Dynamic" ControlToValidate="txtCompDescr" ErrorMessage="Please fill in a description" runat="server" />
                                    </section>
                                </section>
                                <p class="prevBtn"><asp:LinkButton ID="lnkCompPrev" CausesValidation="false" CommandArgument="0" OnCommand="lnkCompNext_Command" Text="Prev. step" runat="server" /></p>
                                <p class="submitBtn"><asp:LinkButton ID="lnkCompSubmit" OnClick="lnkCompSubmit_Click" Text="Register" runat="server" /></p>
                            </asp:Panel>
                        </section>
                    </asp:View>

                    <asp:View runat="server">
                        <p class="success">You have been registered!</p>
                        <p class="successLink">Go to your <asp:LinkButton ID="lnkCompSuccess" Text="Profile" OnCommand="lnkCompSuccess_Command" runat="server" /></p>
                    </asp:View>
                </asp:MultiView>
            </article>
        </asp:View>
    </asp:MultiView>
</asp:Content>

