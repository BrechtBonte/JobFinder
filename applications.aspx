<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="applications.aspx.cs" Inherits="applications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Applications for <asp:Literal ID="litComp" runat="server" /> - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <link rel="stylesheet" href="css/applications.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <article id="applicationsCont">
        <section id="results">
            <asp:Repeater ID="applicationRepeater" runat="server">
                <ItemTemplate>
                <article class="application">
                    <a href='application.aspx?id=<%# DataBinder.Eval(Container.DataItem, "ID") %>' class="resultLink clearfix">
                        <h3><%# DataBinder.Eval(Container.DataItem, "JobTitle") %></h3>
                        <section class="applUser clearfix">
                            <figure>
                                <img src='files/users/imgs/<%# DataBinder.Eval(Container.DataItem, "UserImage") %>' alt="profile pic" />
                            </figure>
                            <p class="fullName"><%# DataBinder.Eval(Container.DataItem, "UserName") %></p>
                        </section>
                    </a>
                </article>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Panel ID="Pages" runat="server" />
        </section>
    </article>
</asp:Content>

