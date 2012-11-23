<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">Error - JobFinder</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtraHead" Runat="Server">
    <style>
        section#errorCont {
            padding: 140px 0;
            background-color: #fff;
            border: 1px solid #ccc;
        }

        #errorCont p {
            text-align: center;
        }

        p#errorMessage {
            margin-bottom: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyHolder" Runat="Server">
    <section id="errorCont">
        <p id="errorMessage">It seems there has been an error.</p>
        <p id="errorBis">We appologize for any inconvenience this may cause.</p>
    </section>
</asp:Content>

